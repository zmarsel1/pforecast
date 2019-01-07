using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Documents
{
    public class DeviceDocument : InputDocumentBase
    {
        protected DeviceDocument() { }
        List<int> deviceList = new List<int>();

        public static DeviceDocument LoadDocument(int docnum, string headtable, string bodytable, string connection, List<int> devicelist)
        {
            var result = new DeviceDocument();
            result.headTable = headtable;
            result.bodyTable = bodytable;
            result.deviceList = devicelist;
            result.connectionString = connection;
            var sql = new SqlConnection(connection);
            string sqlhead = @"SELECT dh.DocumentID, dh.FactoryID, dh.DocTypeID, dh.DocumentDate " +
                          "FROM " + headtable + @" dh WHERE  DocTypeID in (2, 4) AND dh.DocumentID = " + docnum.ToString();
            SqlCommand cmd = new SqlCommand(sqlhead, sql);
            try
            {
                sql.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    throw new Exception("Не удалось найти документ  с заданным номером, или номер документа не соотвествет типу документа.");
                    //return null;
                    //Не удалось найти документ  с заданным номером, или номер документа не соотвествет типу документа.
                };
                result.Head.Factory = reader.GetInt32(1);
                result.Head.DocType = reader.GetInt32(2);
                result.Head.DocumentDate = reader.GetDateTime(3);
                sql.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            var sqlbody = @"SELECT DocumentID, DataHour, ProductID, DataValue1 FROM " +
                bodytable + " WHERE DocumentID = " + docnum.ToString();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlbody, sql);
            var temp = new DataTable();

            try
            {
                adapter.Fill(temp);
            }
            catch (Exception exception)
            {
                //return null;
                throw new Exception("Не удалось считать данные документа.", exception);
            }
            DataColumn key1 = temp.Columns["DataHour"];
            DataColumn key2 = temp.Columns["ProductID"];
            temp.PrimaryKey = new DataColumn[] { key1, key2 };

            result.SetBodySettings();
            for (int i = 1; i <= 24; ++i)
            {
                DataRow row = result.DocumentBody.NewRow();
                row["DataHour"] = i;
                foreach (var device in devicelist)
                {
                    if (temp.Rows.Contains(new object[] { i, device }))
                    {
                        DataRow info = temp.Rows.Find(new object[] { i, device });
                        row[device.ToString()] = (double)info["DataValue1"] != 0.0;
                    }
                    else
                    {
                        row[device.ToString()] = 0;
                    }
                }
                result.DocumentBody.Rows.Add(row);
            }
            return result;
        }
        public static DeviceDocument CreateDocument(InputDocumentHead head, string headtable, string bodytable, string connection, List<int> devicelist, int parent)
        {
            DeviceDocument parentDocument;
            DeviceDocument result = new DeviceDocument();

            result.Head = head;
            result.headTable = headtable;
            result.bodyTable = bodytable;
            result.connectionString = connection;
            result.deviceList = devicelist;
            result.SetBodySettings();
            try
            {
                parentDocument = DeviceDocument.LoadDocument(parent, headtable, bodytable, connection, devicelist);
            }
            catch (Exception exception)
            {
                //return null;
                throw new Exception("Ошибка загрузки связанного документа.", exception);
            }
            result.DocumentBody.Load(parentDocument.DocumentBody.CreateDataReader(), LoadOption.OverwriteChanges);
            if (!result.SaveDocument()) return null;
            return result;
        }
        public static DeviceDocument CreateDocument(InputDocumentHead head, string headtable, string bodytable, string connection, List<int> devicelist)
        {
            var result = new DeviceDocument();

            result.Head = head;
            result.headTable = headtable;
            result.bodyTable = bodytable;
            result.connectionString = connection;
            result.deviceList = devicelist;

            result.SetBodySettings();

            for (int i = 1; i <= 24; ++i)
            {
                DataRow row = result.DocumentBody.NewRow();
                row["DataHour"] = i;
                foreach (var device in devicelist)
                {
                    row[device.ToString()] = true;
                }
                result.DocumentBody.Rows.Add(row);
            }
            result.SaveDocument();
            return result;
        }
        protected override bool  SaveDocument()
        {
            var sql = new SqlConnection(connectionString);
            string sqlhead = string.Format("INSERT INTO {4}(DocumentID, DocumentDate, FactoryID, DocTypeID) VALUES({0},'{1}',{2},{3})", Head.DocumentNumber, Head.DocumentDate.ToString("yyyyMMdd"), Head.Factory, Head.DocType, headTable);
            try
            {
                sql.Open();
            }
            catch (Exception exception)
            {
                error = exception;
                return false;
                //throw new Exception("Не удалось установить связь с Сервером.", exception);
            }
            var tx = sql.BeginTransaction();
            var cmd = new SqlCommand(sqlhead, sql, tx);

            try
            {
                cmd.ExecuteNonQuery();
                //tx.Commit();
            }
            catch (Exception exception)
            {
                error = exception;
                tx.Rollback();
                sql.Close();
                return false;
                //throw new Exception("Документ с заданным номером уже существует.", exception);
            }
            var sqlbody = @"SELECT DocumentID, DataHour, ProductID, DataValue1 FROM " +
                            bodyTable + " WHERE DocumentID = " + Head.DocumentNumber.ToString();
            var cmdbody = new SqlCommand(sqlbody, sql, tx);
            var adapter = new SqlDataAdapter(cmdbody);
            var builder = new SqlCommandBuilder(adapter);
            var data = new DataTable();
            try
            {
                adapter.Fill(data);
            }
            catch (Exception exception)
            {
                error = exception;
                tx.Rollback();
                sql.Close();
                return false;
                //throw new Exception("Ошибка загрузки документа.", exception);
            }
            DataColumn key1 = data.Columns["DataHour"];
            DataColumn key2 = data.Columns["ProductID"];
            data.PrimaryKey = new DataColumn[] { key1, key2 };

            //devices
            foreach (DataRow row in DocumentBody.Rows)
            {
                foreach (var device in deviceList)
                {
                    if (data.Rows.Contains(new object[] { row["DataHour"], device }))
                    {
                        DataRow edit = data.Rows.Find(new object[] { row["DataHour"], device });
                        edit.BeginEdit();
                        if (row[device.ToString()].GetType() == typeof(bool))
                        {
                            edit["DataValue1"] = (bool)row[device.ToString()] ? 1.0 : 0.0;
                        }
                        else
                        {
                            edit["DataValue1"] = row[device.ToString()];
                        }
                        edit.EndEdit();
                    }
                    else
                    {
                        DataRow add = data.NewRow();
                        add["DocumentID"] = Head.DocumentNumber;
                        add["DataHour"] = row["DataHour"];
                        add["ProductID"] = device;
                        if (row[device.ToString()].GetType() == typeof(bool))
                        {
                            add["DataValue1"] = (bool)row[device.ToString()] ? 1.0 : 0.0;
                        }
                        else
                        {
                            add["DataValue1"] = row[device.ToString()];
                        }
                        data.Rows.Add(add);
                    }
                }
            }
            try
            {
                adapter.Update(data);
                tx.Commit();
            }
            catch (Exception exception)
            {
                error = exception;
                tx.Rollback();
                return false;
                //throw new Exception("Ошибка загрузки документа.", exception);
            }
            sql.Close();
            return true;
        }
        public override bool UpdateDocument()
        {
            var sql = new SqlConnection(connectionString);
            string sqlbody = @"SELECT DocumentID, DataHour, ProductID, DataValue1 FROM " +
                bodyTable + " WHERE DocumentID = " + Head.DocumentNumber.ToString();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlbody, sql);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataTable data = new DataTable();
            try
            {
                adapter.Fill(data);
            }
            catch (Exception exception)
            {
                throw new Exception("Ошибка загрузки документа.", exception);
            }
            DataColumn key1 = data.Columns["DataHour"];
            DataColumn key2 = data.Columns["ProductID"];
            data.PrimaryKey = new DataColumn[] { key1, key2 };

            foreach (DataRow row in DocumentBody.Rows)
            {
                foreach (var device in deviceList)
                {
                    if (data.Rows.Contains(new object[] { row["DataHour"], device }))
                    {
                        DataRow edit = data.Rows.Find(new object[] { row["DataHour"], device });
                        edit.BeginEdit();
                        if (row[device.ToString()].GetType() == typeof(bool))
                        {
                            edit["DataValue1"] = (bool)row[device.ToString()] ? 1.0 : 0.0;
                        }
                        else
                        {
                            edit["DataValue1"] = row[device.ToString()];
                        }
                        edit.EndEdit();
                    }
                    else
                    {
                        DataRow add = data.NewRow();
                        add["DocumentID"] = Head.DocumentNumber;
                        add["DataHour"] = row["DataHour"];
                        add["ProductID"] = device;
                        if (row[device.ToString()].GetType() == typeof(bool))
                        {
                            add["DataValue1"] = (bool)row[device.ToString()] ? 1.0 : 0.0;
                        }
                        else
                        {
                            add["DataValue1"] = row[device.ToString()];
                        }
                        data.Rows.Add(add);
                    }
                }
            }
            try
            {
                adapter.Update(data);
            }
            catch (Exception exception)
            {
                error = exception;
                return false;
                //throw new Exception("Ошибка обновления документа.", exception);
            }
            return true;
        }
        void SetBodySettings()
        {
            DataColumn key = DocumentBody.Columns.Add("DataHour", typeof(Int32));
            foreach (var device in deviceList)
                DocumentBody.Columns.Add(device.ToString(), typeof(bool));
            DocumentBody.PrimaryKey = new DataColumn[] { key };
        }
    }
}
