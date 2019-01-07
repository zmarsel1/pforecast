using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Documents
{
    public class PlanDocument : InputDocumentBase
    {
        protected PlanDocument() { }

        public static PlanDocument LoadDocument(int docnum, string headtable, string bodytable, string connection)
        {
            var result = new PlanDocument();
            
            result.headTable = headtable;
            result.bodyTable = bodytable;
            result.connectionString = connection;

            var sql = new SqlConnection(connection);
            string sqlhead = @"SELECT DocumentID, FactoryID, DocTypeID, DocumentDate FROM " +
                         headtable + " WHERE  DocTypeID in (1, 3) AND DocumentHead.DocumentID = " + docnum.ToString();
            var cmd = new SqlCommand(sqlhead, sql);

            try
            {
                sql.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    return null; //Не удалось найти документ  с заданным номером, или номер документа не соотвествет типу документа
                }
                result.Head.Factory = reader.GetInt32(1);
                result.Head.DocType = reader.GetInt32(2);
                result.Head.DocumentDate = reader.GetDateTime(3);
                sql.Close();
            }
            catch (Exception exception)
            {
                string msg = @"Ошибка загрузки документа. Не удалось найти документ
                            с заданным номером, или номер документа не соотвествет
                            типу документа.\n" + exception.Message;
                throw new Exception(msg, exception);
            }
            
            var sqlbody = @"SELECT DocumentID, DataHour, ProductID, DataValue1, DataValue2 FROM " +
                  bodytable + " WHERE DocumentID = " + docnum.ToString();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlbody, sql);

            try
            {
                adapter.Fill(result.DocumentBody);
            }
            catch (Exception exception)
            {
                throw new Exception("Не удалось загрузить документ.\n"+exception.Message, exception);
                //return null; // Не удалось загрузить документ
            }
            result.SetBodySettings();

            return result;
        }
        public static PlanDocument CreateDocument(InputDocumentHead head, string headtable, string bodytable, string connection, int parent)
        {
            PlanDocument parentDocument;
            PlanDocument result;

            try
            {
                parentDocument = PlanDocument.LoadDocument(parent, headtable, bodytable, connection);
            }
            catch (Exception exception)
            {
                throw new Exception("Ошибка загрузки связанного документа.\n" + exception.Message, exception);
                //return null;
                // Ошибка загрузки связанного документа;
            }

            result = new PlanDocument();
            result.Head = head;
            result.headTable = headtable;
            result.bodyTable = bodytable;
            result.connectionString = connection;

            result.DocumentBody.Columns.Add("DocumentID");
            result.DocumentBody.Columns["DocumentID"].DataType = Type.GetType("System.Int32");
            result.DocumentBody.Columns.Add("DataHour");
            result.DocumentBody.Columns["DataHour"].DataType = Type.GetType("System.Int32");
            result.DocumentBody.Columns.Add("ProductID");
            result.DocumentBody.Columns["ProductID"].DataType = Type.GetType("System.Int32");
            result.DocumentBody.Columns.Add("DataValue1");
            result.DocumentBody.Columns["DataValue1"].DataType = Type.GetType("System.Double");
            result.DocumentBody.Columns.Add("DataValue2");
            result.DocumentBody.Columns["DataValue2"].DataType = Type.GetType("System.Double");
            result.SetBodySettings();

            foreach (DataRow source in parentDocument.DocumentBody.Rows)
            {
                DataRow destenation = result.DocumentBody.NewRow();
                destenation["DocumentID"] = head.DocumentNumber;
                destenation["DataHour"] = source["DataHour"];
                destenation["ProductID"] = source["ProductID"];
                destenation["DataValue1"] = source["DataValue1"];
                destenation["DataValue2"] = source["DataValue2"];
                result.DocumentBody.Rows.Add(destenation);
            }
            if (!result.SaveDocument()) return null;
            return result;
        }
        public static PlanDocument CreateDocument(InputDocumentHead head, string headtable, string bodytable, string connection)
        {
            var result = new PlanDocument();
            result.Head = head;
            result.headTable = headtable;
            result.bodyTable = bodytable;
            result.connectionString = connection;

            result.DocumentBody.Columns.Add("DocumentID");
            result.DocumentBody.Columns["DocumentID"].DataType = Type.GetType("System.Int32");
            result.DocumentBody.Columns.Add("DataHour");
            result.DocumentBody.Columns["DataHour"].DataType = Type.GetType("System.Int32");
            result.DocumentBody.Columns.Add("ProductID");
            result.DocumentBody.Columns["ProductID"].DataType = Type.GetType("System.Int32");
            result.DocumentBody.Columns.Add("DataValue1");
            result.DocumentBody.Columns["DataValue1"].DataType = Type.GetType("System.Double");
            result.DocumentBody.Columns.Add("DataValue2");
            result.DocumentBody.Columns["DataValue2"].DataType = Type.GetType("System.Double");
            result.SetBodySettings();
            for (int i = 1; i <= 24; ++i)
            {
                DataRow row = result.DocumentBody.NewRow();
                row["DocumentID"] = result.Head.DocumentNumber;
                row["DataHour"] = i;
                row["ProductID"] = 0;
                row["DataValue1"] = 0;
                row["DataValue2"] = 0;
                result.DocumentBody.Rows.Add(row);
            }

            return result.SaveDocument() ? result : null;
        }
        public override bool UpdateDocument()
        {
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

            var sql = @"SELECT DocumentID, DataHour, ProductID, DataValue1, DataValue2 FROM " +
                        bodyTable + " WHERE DocumentID = " + Head.DocumentNumber.ToString();
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
            DataColumn key1 = data.Columns["DataHour"];
            DataColumn key2 = data.Columns["ProductID"];
            data.PrimaryKey = new DataColumn[] { key1, key2 };

            foreach (DataRow row in data.Rows)
            {
                if (!DocumentBody.Rows.Contains(new object[] { row["DataHour"], row["ProductID"] }))
                {
                    row.Delete();
                }
                else
                {
                    DataRow r = DocumentBody.Rows.Find(new object[] { row["DataHour"], row["ProductID"] });
                    row.BeginEdit();
                    row["DataValue1"] = r["DataValue1"];
                    row["DataValue2"] = r["DataValue2"];
                    row.EndEdit();
                }
            }
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
            var connection = new SqlConnection(connectionString);
            string sql = string.Format("INSERT INTO {4}(DocumentID, DocumentDate, FactoryID, DocTypeID) VALUES({0},'{1}',{2},{3})", Head.DocumentNumber, Head.DocumentDate.ToString("yyyyMMdd"), Head.Factory, Head.DocType, headTable);
            try
            {
                connection.Open();
            }
            catch (Exception exception)
            {
                error = exception;
                return false;
                //throw new Exception("Не удалось установить связь с сервером.", exception);
            }
            var transaction = connection.BeginTransaction();
            var cmd = new SqlCommand(sql, connection, transaction);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception) // Документ с заданным номером уже существует
            {
                error = exception;
                transaction.Rollback();
                return false;

            }
            sql = @"SELECT DocumentID, DataHour, ProductID, DataValue1, DataValue2 FROM " +
                bodyTable + " WHERE DocumentID = " + Head.DocumentNumber.ToString();
            var body = new SqlCommand(sql, connection, transaction);
            var adapter = new SqlDataAdapter(body);
            var builder = new SqlCommandBuilder(adapter);
            var data = new DataTable();
            try
            {
                adapter.Fill(data);
            }
            catch (Exception exception) // Ошибка загрузки документа.
            {
                error = exception;
                transaction.Rollback();
                return false;
            }
            DataColumn key1 = data.Columns["DataHour"];
            DataColumn key2 = data.Columns["ProductID"];
            data.PrimaryKey = new DataColumn[] { key1, key2 };

            foreach (DataRow row in data.Rows)
            {
                if (!DocumentBody.Rows.Contains(new object[] { row["DataHour"], row["ProductID"] }))
                {
                    row.Delete();
                }
                else
                {
                    DataRow r = DocumentBody.Rows.Find(new object[] { row["DataHour"], row["ProductID"] });
                    row.BeginEdit();
                    row["DataValue1"] = r["DataValue1"];
                    row["DataValue2"] = r["DataValue2"];
                    row.EndEdit();
                }
            }
            data.Merge(DocumentBody);
            try
            {
                adapter.Update(data);
                transaction.Commit();
            }
            catch (Exception exception) // Ошибка сохранения документа.
            {
                error = exception;
                transaction.Rollback();
                return false;
            }
            connection.Close();
            return true;
        }
        void SetBodySettings()
        {
            DocumentBody.Columns["DataHour"].Caption = "Час";
            DocumentBody.Columns["ProductID"].Caption = "Продукция";
            DocumentBody.Columns["DataValue1"].Caption = "тыс. м2";
            DocumentBody.Columns["DataValue2"].Caption = "т.";
            DocumentBody.Columns["DataHour"].ReadOnly = true;
            DataColumn key1 = DocumentBody.Columns["DataHour"];
            DataColumn key2 = DocumentBody.Columns["ProductID"];
            DocumentBody.PrimaryKey = new DataColumn[] { key1, key2 };
        }
    }
}
