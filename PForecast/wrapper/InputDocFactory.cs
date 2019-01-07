using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Documents;

namespace PForecast
{
    public class InputDocFactory
    {
        const int PRODUCTS = 1;
        const int DEVICES = 2;

        public static IInputDocument CreateDocument(InputDocumentHead head, IGlobalValues data)
        {
            switch (head.DocType)
            {
                case 1:
                case 3:
                    return PlanDocument.CreateDocument(head, data.ActiveSchema + ".DocumentHead", data.ActiveSchema + ".DocumentBody", data.ConnectionString);
                case 2:
                case 4:
                    return DeviceDocument.CreateDocument(head, data.ActiveSchema + ".DocumentHead", data.ActiveSchema + ".DocumentBody", data.ConnectionString, new List<int>(data.ProductGroups[DEVICES].Keys));
                default:
                    throw new Exception("Неверный тип документа.");
            }
        }
        public static IInputDocument LoadDocument(int docnum, IGlobalValues data)
        {
            var sql = new SqlConnection(data.ConnectionString);
            string sqlcmd = @"SELECT DocTypeID FROM " + data.ActiveSchema +
                ".DocumentHead WHERE  DocumentHead.DocumentID = " + docnum.ToString();
            SqlCommand cmd = new SqlCommand(sqlcmd, sql);
            int type;
            try
            {
                sql.Open();
                type = (int)cmd.ExecuteScalar();
                sql.Close();
            }
            catch (Exception exception)
            {
                //return null;
                throw new Exception("Ошибка загрузки свойств документа.", exception);
            }
            switch (type)
            {
                case 1:
                case 3:
                    return PlanDocument.LoadDocument(docnum, data.ActiveSchema + ".DocumentHead", data.ActiveSchema + ".DocumentBody", data.ConnectionString);

                case 2:
                case 4:
                    return DeviceDocument.LoadDocument(docnum, data.ActiveSchema + ".DocumentHead", data.ActiveSchema + ".DocumentBody", data.ConnectionString, new List<int>(data.ProductGroups[DEVICES].Keys));
                default:
                    throw new Exception("Неверный тип документа.");
            }
        }
        public static IInputDocument CreateDocument(InputDocumentHead head, IGlobalValues data, int parent)
        {
            switch (head.DocType)
            {
                case 1:
                case 3:
                    return PlanDocument.CreateDocument(head, data.ActiveSchema + ".DocumentHead", data.ActiveSchema + ".DocumentBody", data.ConnectionString, parent);
                case 2:
                case 4:
                    return DeviceDocument.CreateDocument(head, data.ActiveSchema + ".DocumentHead", data.ActiveSchema + ".DocumentBody", data.ConnectionString, new List<int>(data.ProductGroups[DEVICES].Keys), parent);
                default:
                    throw new Exception("Неверный тип документа.");
            }
        }
    }
}
