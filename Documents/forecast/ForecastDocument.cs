using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Documents
{
    public class ForecastDocument : ForecastDocumentBase
    {
        ForecastDocument() { }
        //Загрузка документа
        #region Глобальные функции
        
        public static  ForecastDocument LoadDocument(int docnum, string headtable, string bodytable, string connection)
        {
            ForecastDocument result = new ForecastDocument();
            result.bodyTable = bodytable;
            result.headTable = headtable;
            result.connectionString = connection;

            SqlConnection sql = new SqlConnection(connection);
            string sqlcmd = @"SELECT DocumentDate, RPID, ParentDocument, DocTypeID
                           FROM " + headtable + @" WHERE DocumentID = " + docnum.ToString();
            SqlCommand cmd = new SqlCommand(sqlcmd, sql);
            SqlDataReader reader = null;
            try
            {
                sql.Open();
                reader = cmd.ExecuteReader();
            }
            catch (Exception exception)
            {
                sql.Close();
                //return null;
                throw new Exception("Ошибка соединения с сервером.", exception);
            }
            if (!reader.Read())
            {
                sql.Close();
                //return null;
                throw new Exception("Не удалось загрузить документ с заданным номером.");
            };
            result.Head.DocumentDate = reader.GetDateTime(0);
            result.Head.RPInfo = reader.GetInt32(1);
            result.Head.DocType = reader.GetInt32(3);
            result.Head.DocumentParent = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0;

            sql.Close();

            sqlcmd = @"SELECT DocumentID, DataHour, DataValue
                FROM " + bodytable + @" WHERE DocumentID = " + docnum.ToString();
            var adapter = new SqlDataAdapter(sqlcmd, sql);
            try
            {
                adapter.Fill(result.DocumentBody);
            }
            catch (Exception exception)
            {
                //return null;
                throw new Exception("Ошибка загрузки документа.", exception);
            }
            result.SetBodySettings();
            return result;
        }
 
        public static ForecastDocument CreateDocument(ForecastDocumentHead head, string headtable, string bodytable, string connection)
        {   
            ForecastDocument result = new ForecastDocument();
            result.Head = head;
            result.bodyTable = bodytable;
            result.headTable = headtable;
            result.connectionString = connection;

            if (head.DocumentParent == 0) // если нет связанного документа
            {
                result.DocumentBody.Columns.Add("DocumentID", Type.GetType("System.Int32"));
                result.DocumentBody.Columns.Add("DataHour", Type.GetType("System.Int32"));
                result.DocumentBody.Columns.Add("DataValue", Type.GetType("System.Double"));
                
                for (int i = 1; i <= 24; ++i)
                {
                    DataRow row = result.DocumentBody.NewRow();
                    row["DocumentID"] = result.Head.DocumentNumber;
                    row["DataHour"] = i;
                    row["DataValue"] = 0;
                    result.DocumentBody.Rows.Add(row);
                }
            }
            else //если есть связанный документ
            {
                //заполнение данными из связанного документа
                SqlConnection sql = new SqlConnection(result.connectionString);
                SqlDataAdapter adapter = new SqlDataAdapter(string.Format("SELECT {0} as DocumentID, DataHour, DataValue FROM {2} WHERE DocumentID = {1}", head.DocumentNumber ,head.DocumentParent, bodytable), sql);
                try
                {
                    adapter.Fill(result.DocumentBody);
                }
                catch(Exception exception)
                {
                    //return null;
                    throw new Exception("Ошибка загрузки связанного документа.",exception);
                }
            }
            result.SetBodySettings();
            if(!result.SaveDocument()) return null;
            return result;
        }
        #endregion

        public override bool UpdateDocument()
        {
            if (ReadOnly) return false;
            var connection = new SqlConnection(connectionString);
            SqlTransaction tx = null;
            try
            {
                connection.Open();
                tx = connection.BeginTransaction();
            }
            catch
            {
                connection.Close();
                return false;
            }

            var sql = "SELECT DocumentID, DataHour, DataValue FROM "
                + bodyTable + " WHERE DocumentID = " + Head.DocumentNumber.ToString();
            var cmd = new SqlCommand(sql, connection, tx);
            var adapter = new SqlDataAdapter(cmd);
            var builder = new SqlCommandBuilder(adapter);

            var data = new DataTable();
            try
            {
                adapter.Fill(data);
            }
            catch (Exception exception)
            {
                error = exception;
                return false;
                //Не удалось загрузить документ для обновления.
            }
            DataColumn col = data.Columns["DataHour"];
            data.PrimaryKey = new DataColumn[] {col };

            data.Merge(DocumentBody);
            try
            {
                adapter.Update(data);
                tx.Commit();
            }
            catch (Exception exception)
            {
                error = new Exception("Не удалось обновить документ." + exception.Message, exception);
                //Не удалось обновить документ.
                tx.Rollback();
                return false;
            }
            connection.Close();
            return true;
        }
        protected override bool SaveDocument()
        {
            SqlConnection sql = new SqlConnection(connectionString);
            string parent = Head.DocumentParent == 0 ? "NULL" : Head.DocumentParent.ToString();
            string sqlcmd = string.Format("INSERT INTO {5}(DocumentID, DocumentDate, RPID, ParentDocument,DocTypeID) VALUES({0}, '{1}', {2}, {3}, {4})", Head.DocumentNumber, Head.DocumentDate.ToString("yyyyMMdd"), Head.RPInfo, parent, Head.DocType, headTable);
            try
            {
                sql.Open();
            }
            catch (Exception exception)
            {
                error = exception;
                return false;
                //throw new Exception("Ошибка соединения с сервером.", exception);
            }
            SqlTransaction transaction = sql.BeginTransaction();
            SqlCommand cmd = new SqlCommand(sqlcmd, sql, transaction);
                
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                error = exception;
                transaction.Rollback();
                return false;
                //throw new Exception("Документ с таким номером уже существует.", exception);
            }
            SqlCommand sqlbody = new SqlCommand("SELECT DocumentID, DataHour, DataValue FROM " + bodyTable + " WHERE DocumentID = " + Head.DocumentNumber.ToString(), sql, transaction);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlbody);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataTable data = new DataTable();
            try
            {
                adapter.Fill(data);
            }
            catch (Exception exception)
            {
                error = exception;
                transaction.Rollback();
                //throw new Exception("Ошибка загрузки документа.", exception);  
            }
            DataColumn col = data.Columns["DataHour"];
            data.PrimaryKey = new DataColumn[] { col };
            foreach (DataRow row in DocumentBody.Rows)
            {
                if (data.Rows.Contains(row["DataHour"]))
                {
                    DataRow update = data.Rows.Find(row["DataHour"]);
                    update.BeginEdit();
                    update["DataValue"] = row["DataValue"];
                    update.EndEdit();
                }
                else
                {
                    DataRow newrow = data.NewRow();
                    newrow["DocumentID"] = Head.DocumentNumber;
                    newrow["DataHour"] = row["DataHour"];
                    newrow["DataValue"] = row["DataValue"];
                    data.Rows.Add(newrow);
                }
                //
            }
            try
            {
                adapter.Update(data);
                transaction.Commit();
            }
            catch (Exception exception)
            {
                error = exception;
                transaction.Rollback();
                return false;
                //throw new Exception("Ошибка обновления документа.", exception);
            }
            sql.Close();
            return true;
        }

        void SetBodySettings()
        {
            DocumentBody.Columns["DocumentID"].ReadOnly = true;
            DocumentBody.Columns["DataHour"].ReadOnly = true;
            DocumentBody.Columns["DataValue"].Caption = "КВатт*час";
            DataColumn col = DocumentBody.Columns["DataHour"];
            DocumentBody.PrimaryKey = new DataColumn[] { col };
        }
    }
}
