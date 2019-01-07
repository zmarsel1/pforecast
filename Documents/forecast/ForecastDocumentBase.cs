using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Documents
{
    public interface IForecastDocument
    {
        ForecastDocumentHead Head { get; set; }
        bool ReadOnly { get; }
        DataTable DocumentBody { get; }
        Exception Error { get; }
        bool UpdateDocument();
        bool DeleteDocument();
        void BeginEdit();
        void EndEdit();
    }
    public class ForecastDocumentHead
    {
        public ForecastDocumentHead() { }
        public ForecastDocumentHead(DateTime docdate, int rp)
        {
            DocumentDate = docdate;
            RPInfo = rp;
        }
        public ForecastDocumentHead(DateTime docdate, int rp, int parent)
        {
            DocumentDate = docdate;
            RPInfo = rp;
            DocumentParent = parent;
        }

        //Свойства и значения заголовка докукмента

        public int DocumentNumber //номер документа
        {
            get
            {
                if (DocumentDate != null && RPInfo > 0 && DocType > 0)
                {
                    return int.Parse(DocType.ToString() + RPInfo.ToString() + DocumentDate.ToString("yyMMdd"));
                }
                else
                {
                    return 0;
                }
            }
        }  
        public int DocumentParent { get; set; } //рожительский документ
        public DateTime DocumentDate { get; set; } //Дата документа
        public int RPInfo { get; set; } //РП
        public int DocType { get; set; } //тип документа 1 - прогноз 2 - заявка
    }

    public abstract class ForecastDocumentBase : IForecastDocument
    {
        DBDocumentLocker documentLocker = new DBDocumentLocker();
        protected string connectionString = string.Empty;
        protected string headTable = string.Empty;
        protected string bodyTable = string.Empty;
        DataTable documentBody = new DataTable();
        protected Exception error = null;

        protected ForecastDocumentBase()
        {
            Head = new ForecastDocumentHead();
            ReadOnly = true;
        }

        public Exception Error { get { return error; } }
        public bool ReadOnly { get; private set; }
        public ForecastDocumentHead Head { get; set; }
        public DataTable DocumentBody { get { return documentBody; } }
        public abstract bool UpdateDocument();
        protected abstract bool SaveDocument();

        public static DataTable SelectAllDocuments(IGlobalValues data, string headtable, string factorytable, string rptable, DateTime start, DateTime end, string types)
        {
            var sql = new SqlConnection(data.ConnectionString);
            var table = new DataTable();
            string query = "SELECT     fdh.DocumentID, fdh.DocumentDate, rp.RPName, f.FactoryName, fdh.ParentDocument, fdt.DocTypeName, fdh.DocTypeID, fdh.RPID, f.FactoryID" +
                " FROM " + headtable + " fdh INNER JOIN dbo.ForecastDocumentType fdt ON fdh.DocTypeID = fdt.DocTypeID INNER JOIN " +
                rptable +" rp ON fdh.RPID = rp.RPID INNER JOIN " + factorytable + " f ON rp.FactoryID = f.FactoryID " +
                string.Format(" WHERE (fdh.DocumentDate>='{0}') AND ((fdh.DocumentDate<='{1}'))", start.ToString("yyyyMMdd"), end.ToString("yyyyMMdd")) +
                (types != string.Empty ? " AND fdh.DocTypeID in (" + types + ") " : "") +
                " ORDER BY fdh.DocumentDate, rp.RPID, fdt.DocTypeID";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sql);
            try
            {
                adapter.Fill(table);
            }
            catch (Exception exc)
            {
                throw new Exception("Ошибка загрузки списка документов.\n" + exc.Message, exc);
                //return null;
            }
            return table;
        }
        public bool DeleteDocument()
        {
            var connection = new SqlConnection(connectionString);

            var sql1 = string.Format("DELETE FROM {0} WHERE DocumentID = {1}", bodyTable, Head.DocumentNumber);
            var sql2 = string.Format("DELETE FROM {0} WHERE DocumentID = {1}", headTable, Head.DocumentNumber);
            SqlTransaction tn = null; ;
            try
            {
                connection.Open();
                tn = connection.BeginTransaction();
                var cmd = new SqlCommand(sql1, connection, tn);
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql2;
                cmd.ExecuteNonQuery();
                tn.Commit();
            }
            catch (Exception e)
            {
                error = e;
                tn.Rollback();
                return false;
            }
            Head = new ForecastDocumentHead();
            DocumentBody.Clear();
            return true;
        }
        public void BeginEdit()
        {
            if (ReadOnly)
                ReadOnly = documentLocker.Lock("f" + Head.DocumentNumber.ToString(), connectionString) ? !documentLocker.Locked : true;
   
        }
        public void EndEdit()
        {
            if (!ReadOnly)
            {
                documentLocker.Unlock();
                ReadOnly = true;
            }
        }
    }
}
