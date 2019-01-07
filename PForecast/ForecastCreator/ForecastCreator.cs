using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using Documents;

namespace PForecast
{
    class ForecastCreator
    {
        const int PLAN = 1;
        //const int
        Dictionary<int, string> Scripts = new Dictionary<int,string>();
        static ForecastCreator mInstance = null;
        static public  ForecastCreator Instance
        { 
            get
            {
                if (mInstance == null)
                    mInstance = new ForecastCreator();
                return mInstance;
            }
        }
        private ForecastCreator() {
            Scripts[1] = "rp1.ded";
            Scripts[2] = "rp2.ded";
            Scripts[3] = "other.ded";
            Scripts[4] = "kbk.ded";
            ; }

        public string ErrorMessage { get; set; } //ошибка

        private bool KFFactory(DateTime date)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand cmd = new SqlCommand(string.Format("Select dbo.FindPlan({0}, '{1}')", 1, date.ToString("yyyyMMdd")), connection);
            connection.Open();
            bool result = true;
            //проверка наличия плана производства 
            if ((int)cmd.ExecuteScalar() > 0)
            {
                //загруска плана производства в таблицу TempPlan
                SqlCommand plan = new SqlCommand(string.Format("exec dbo.LoadPlan {0},'{1}'", 1, date.ToString("yyyyMMdd")), connection);
                plan.ExecuteNonQuery();
                //запуск скрипта дедуктора
                Process prc = null;
                string output = string.Empty;

                try
                {
                    // Устанавливаем параметры запуска процесса
                    prc = new Process();
                    prc.StartInfo.FileName = ConfigurationManager.AppSettings["DeductorString"];
                    prc.StartInfo.Arguments = Scripts[1] + " /run";

                    // Старт
                    prc.Start();

                    // Ждем пока процесс не завершится
                    prc.WaitForExit();

                    if (prc.ExitCode == -1)
                    {
                        result = false;
                        ErrorMessage = "Ошибка серваера прогнозирования.";
                    }
                }
                finally
                {
                    if (prc != null) prc.Close();
                }
            }
            else
            {
                ErrorMessage = string.Format("отсутствует план производства на {0}", date.ToString("dd.MM.yyyy"));
                result = false;
            }
            connection.Close();
            return result;
        }
        private bool BFFactory(DateTime date)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand cmd = new SqlCommand(string.Format("Select dbo.FindPlan({0}, '{1}')", 2, date.ToString("yyyyMMdd")), connection);
            connection.Open();
            bool result = true;
            //проверка наличия плана производства 
            if ((int)cmd.ExecuteScalar() > 0)
            {
                //загруска плана производства в таблицу TempPlan
                SqlCommand plan = new SqlCommand(string.Format("exec dbo.LoadPlan {0},'{1}'", 2, date.ToString("yyyyMMdd")), connection);
                plan.ExecuteNonQuery();
                //запуск скрипта дедуктора
                Process prc = null;
                string output = string.Empty;

                try
                {
                    // Устанавливаем параметры запуска процесса
                    prc = new Process();
                    prc.StartInfo.FileName = ConfigurationManager.AppSettings["DeductorString"];
                    prc.StartInfo.Arguments = Scripts[2] + " /run";

                    // Старт
                    prc.Start();

                    // Ждем пока процесс не завершится
                    prc.WaitForExit();
                    if (prc.ExitCode == -1)
                    {
                        result = false;
                        ErrorMessage = "Ошибка серваера прогнозирования.";
                    }
                }
                finally
                {
                    if (prc != null) prc.Close();
                }
            }
            else
            {
                ErrorMessage = string.Format("отсутствует план производства на {0}", date.ToString("dd.MM.yyyy"));
                result = false;
            }
            connection.Close();
            return result;
        }
        private bool OtherFactory(DateTime date)
        {
            InputDocumentHead head = new InputDocumentHead();
            head.DocType = PLAN;
            head.DocumentDate = date;
            head.Factory = 3; // остальные потребители  
            PlanDocument doc = null;

            var connection = new SqlConnection(GlobalValues.Instance.ConnectionString);
            
            var rplist = new int[6] {0,4,5,6,7,8 };
            var cmd = new SqlCommand();

            cmd = new SqlCommand(string.Format("Select dbo.FindPlan({0}, '{1}')", 3, date.ToString("yyyyMMdd")), connection);
            connection.Open();
            bool result = true;
            //проверка наличия плана производства
            int num = (int)cmd.ExecuteScalar();
            if (num > 0)
            {
                doc = PlanDocument.LoadDocument(num, GlobalValues.Instance.ActiveSchema + ".DocumentHead", GlobalValues.Instance.ActiveSchema + ".DocumentBody", GlobalValues.Instance.ConnectionString);
                doc.DeleteDocument();
                doc = null;
            }

            doc = PlanDocument.CreateDocument(head, GlobalValues.Instance.ActiveSchema + ".DocumentHead", GlobalValues.Instance.ActiveSchema + ".DocumentBody", GlobalValues.Instance.ConnectionString);
            doc.BeginEdit();
            doc.DocumentBody.Clear();

            var lastdate = date.Subtract(new TimeSpan(4, 0, 0, 0));
            cmd.Connection = connection;
            for (int j = 1; j <= 5; ++j)
            {
                cmd.CommandText = "Select DATEPART(hour, e.DataDate) as h, e.DataValue FROM dbo.Energy e WHERE e.RPID = " + rplist[j].ToString() +
                    " AND ((CONVERT(char(8), e.DataDate, 112) = '" + lastdate.ToString("yyyyMMdd") + "' AND DATEPART(hour, e.DataDate) <> 0) OR " +
                    "(CONVERT(char(8), e.DataDate, 112) = '" + lastdate.AddDays(1).ToString("yyyyMMdd") + "' AND DATEPART(hour, e.DataDate) = 0))";
                var adapter = new SqlDataAdapter(cmd);
                var data = new DataTable();
                adapter.Fill(data);
                data.Columns["h"].DataType = typeof(int);
                DataColumn key = data.Columns["h"];
                data.PrimaryKey = new DataColumn[] { key };

                for (int i = 1; i <= 24; ++i)
                {
                    DataRow row = data.Rows.Find(i % 24);
                    DataRow add = doc.DocumentBody.NewRow();
                    add["DocumentID"] = doc.Head.DocumentNumber;
                    add["ProductID"] = j;
                    add["DataHour"] = i;
                    add["DataValue1"] = row["DataValue"];
                    doc.DocumentBody.Rows.Add(add);
                }
            }
            doc.UpdateDocument();
            doc.EndEdit();

            connection.Close();
            
            cmd = new SqlCommand(string.Format("Select dbo.FindPlan({0}, '{1}')", 3, date.ToString("yyyyMMdd")), connection);
            connection.Open();
            //проверка наличия плана производства 
            if ((int)cmd.ExecuteScalar() > 0)
            {
                //загруска плана производства в таблицу TempPlan
                SqlCommand plan = new SqlCommand(string.Format("exec dbo.LoadPlan {0},'{1}'", 3, date.ToString("yyyyMMdd")), connection);
                plan.ExecuteNonQuery();
                //запуск скрипта дедуктора
                Process prc = null;
                string output = string.Empty;

                try
                {
                    // Устанавливаем параметры запуска процесса
                    prc = new Process();
                    prc.StartInfo.FileName = ConfigurationManager.AppSettings["DeductorString"];
                    prc.StartInfo.Arguments = Scripts[3] + " /run";

                    // Старт
                    prc.Start();

                    // Ждем пока процесс не завершится
                    prc.WaitForExit();
                    if (prc.ExitCode == -1)
                    {
                        result = false;
                        ErrorMessage = "Ошибка серваера прогнозирования.";
                    }
                }
                finally
                {
                    if (prc != null) prc.Close();
                }
            }
            else
            {
                ErrorMessage = string.Format("отсутствует план производства на {0}", date.ToString("dd.MM.yyyy"));
                result = false;
            }
            return result;
        }
        private bool KBK(DateTime date)
        {
            InputDocumentHead head = new InputDocumentHead();
            head.DocType = PLAN;
            head.DocumentDate = date;
            head.Factory = 4; //идентификатор КБК
            PlanDocument doc = null;

            var connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

            //var rplist = new int[6] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            var cmd = new SqlCommand();

            cmd = new SqlCommand(string.Format("Select dbo.FindPlan({0}, '{1}')", 4, date.ToString("yyyyMMdd")), connection);
            connection.Open();
            //проверка наличия плана производства 
            int num = (int)cmd.ExecuteScalar();
            if (num > 0)
            {
                doc = PlanDocument.LoadDocument(num, GlobalValues.Instance.ActiveSchema + ".DocumentHead", GlobalValues.Instance.ActiveSchema + ".DocumentBody", GlobalValues.Instance.ConnectionString);
                doc.DeleteDocument();
                doc = null;
            }
            doc = PlanDocument.CreateDocument(head, GlobalValues.Instance.ActiveSchema + ".DocumentHead", GlobalValues.Instance.ActiveSchema+".DocumentBody", GlobalValues.Instance.ConnectionString);
            doc.BeginEdit();
            doc.DocumentBody.Clear();

            //var lastdate = date.Subtract(new TimeSpan(4, 0, 0, 0));
            cmd.Connection = connection;
            for (int j = 1; j <= 8; ++j)
            {
                cmd.CommandText = "SELECT     fdb.DataHour as h, fdb.DataValue FROM dbo.vRP rp INNER JOIN " + 
                                  " dbo.vForecastDocumentHead fdh ON rp.RPID = fdh.RPID INNER JOIN " +
                                  " dbo.vForecastDocumentBody fdb ON fdh.DocumentID = fdb.DocumentID" +
                                  " WHERE (fdh.DocumentDate = '" + date.ToString("yyyyMMdd") + "') " +
                                  " AND (fdh.DocTypeID = 2) AND " + 
                                  " (rp.RPID = " + j.ToString() + ")";
                var adapter = new SqlDataAdapter(cmd);
                var data = new DataTable();
                adapter.Fill(data);
                data.Columns["h"].DataType = typeof(int);
                DataColumn key = data.Columns["h"];
                data.PrimaryKey = new DataColumn[] { key };

                if (data.Rows.Count != 24)
                {
                    doc.EndEdit();
                    connection.Close();
                    ErrorMessage = string.Format("отсутствует одна из заявок на {0}", date.ToString("dd.MM.yyyy"));
                    doc.DeleteDocument();
                    return false;
                }
                
                for (int i = 1; i <= 24; ++i)
                {
                    DataRow row = data.Rows.Find(i);
                    DataRow add = doc.DocumentBody.NewRow();
                    add["DocumentID"] = doc.Head.DocumentNumber;
                    add["ProductID"] = j;
                    add["DataHour"] = i;
                    add["DataValue1"] = row["DataValue"];
                    doc.DocumentBody.Rows.Add(add);
                }
            }
            doc.UpdateDocument();
            doc.EndEdit();

            connection.Close();

            cmd = new SqlCommand(string.Format("Select dbo.FindPlan({0}, '{1}')", 4, date.ToString("yyyyMMdd")), connection);
            connection.Open();
            bool result = true;
            //проверка наличия плана производства 
            if ((int)cmd.ExecuteScalar() > 0)
            {
                //загруска плана производства в таблицу TempPlan
                SqlCommand plan = new SqlCommand(string.Format("exec dbo.LoadPlan {0},'{1}'", 4, date.ToString("yyyyMMdd")), connection);
                plan.ExecuteNonQuery();
                connection.Close();
                //запуск скрипта дедуктора
                Process prc = null;
                string output = string.Empty;

                try
                {
                    // Устанавливаем параметры запуска процесса
                    prc = new Process();
                    prc.StartInfo.FileName = ConfigurationManager.AppSettings["DeductorString"];
                    prc.StartInfo.Arguments = Scripts[4] + " /run";

                    // Старт
                    prc.Start();

                    // Ждем пока процесс не завершится
                    prc.WaitForExit();
                    if (prc.ExitCode == -1)
                    {
                        result = false;
                        ErrorMessage = "Ошибка серваера прогнозирования.";
                    }
                }
                finally
                {
                    if (prc != null) prc.Close();
                }
            }
            else
            {
                ErrorMessage = string.Format("отсутствует план производства на {0}", date);
                result = false;
            }
            return result;
        }
        public bool DoForecast(int factory, DateTime date)
        {
            switch (factory)
            {
                case 1:
                    return KFFactory(date);
                case 2:
                    return BFFactory(date);
                case 3:
                    return OtherFactory(date);
                case 4:
                    return KBK(date);
                default:
                    break;
            }
            
            return false;
        }
    }
}
