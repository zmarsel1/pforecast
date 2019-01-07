using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Documents
{   
    public interface IInputDocument
    {
        InputDocumentHead Head { get; set; }
        bool ReadOnly { get; }
        DataTable DocumentBody { get; }
        Exception Error { get; }
        
        bool DeleteDocument();
        bool UpdateDocument();
        void BeginEdit();
        void EndEdit();
    }

    public class InputDocumentHead
    {
        public InputDocumentHead()
        {
            Factory = 0;
            DocType = 0;
            DocumentDate = DateTime.Now;
        }
        public int DocumentNumber
        {
            get
            {
                if (DocumentDate != null && DocType > 0 && Factory > 0)
                {
                    return int.Parse(DocType.ToString() + Factory.ToString() + DocumentDate.ToString("yyMMdd"));
                }
                else
                {
                    return 0;
                }
            }
        }
        public DateTime DocumentDate { get; set; }
        public int Factory { get; set; }
        public int DocType { get; set; }
    }
    public abstract class InputDocumentBase : IInputDocument
    {
        protected InputDocumentBase()
        {
            Head = new InputDocumentHead();
            ReadOnly = true;
        }

        protected Exception error = null;
        protected string connectionString = string.Empty;
        protected string headTable = string.Empty;
        protected string bodyTable = string.Empty;
        
        DataTable documentBody = new DataTable();
        DBDocumentLocker documentLocker  = new DBDocumentLocker();

        public bool ReadOnly { get; protected set; }
        public InputDocumentHead Head {get; set;}
        public DataTable DocumentBody { get { return documentBody; } }

        public Exception Error { get { return error; } }
        public virtual void BeginEdit()
        {
            if (ReadOnly)
                ReadOnly = documentLocker.Lock("i" + Head.DocumentNumber.ToString(), connectionString) ? !documentLocker.Locked : true;
        }
        public virtual void EndEdit()
        {
            if (!ReadOnly)
            {
                documentLocker.Unlock();
                ReadOnly = true;
            }
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
                var cmd = new SqlCommand(sql2, connection, tn);
                cmd.ExecuteNonQuery();
                cmd.CommandText = sql1;
                cmd.ExecuteNonQuery();
                tn.Commit();
            }
            catch (Exception exc)
            {
                error = exc;
                tn.Rollback();
                return false;
            }
            Head = new InputDocumentHead();
            DocumentBody.Clear();
            return true;
        }
        public static DataTable SelectAllDocuments(IGlobalValues data, string headtable, string factorytable, DateTime start, DateTime end, string types)
        {
            var sql = new SqlConnection(data.ConnectionString);
            var table = new DataTable();
            string query = "SELECT     dh.DocumentID, dh.DocumentDate, f.FactoryName, dt.DocTypeName, dh.DocTypeID, dh.FactoryID " + 
                " FROM " + headtable +  " dh INNER JOIN dbo.DocumentType dt ON dh.DocTypeID = dt.DocTypeID INNER JOIN " +
                factorytable + " f ON dh.FactoryID = f.FactoryID " +
                string.Format(" WHERE (dh.DocumentDate>='{0}') AND ((dh.DocumentDate<='{1}'))", start.ToString("yyyyMMdd"), end.ToString("yyyyMMdd")) +
                ( types != string.Empty ? " AND dh.DocTypeID in (" + types + ")" : "") + " ORDER BY dh.DocumentDate, dh.DocTypeID";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sql);
            adapter.Fill(table);
            return table;
        }
        public abstract bool UpdateDocument();
        protected abstract bool SaveDocument();
    }
}
