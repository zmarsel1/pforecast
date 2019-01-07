using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documents;
using System.Data.SqlClient;

namespace PForecast
{
    class ForecastDocumentFactory
    {
        ForecastDocumentFactory() { }

        public static ForecastDocument CreateDocument(ForecastDocumentHead head, IGlobalValues data)
        {
            return ForecastDocument.CreateDocument(head, data.ActiveSchema + ".ForecastDocumentHead", data.ActiveSchema + ".ForecastDocumentBody", data.ConnectionString);
        }
        public static ForecastDocument LoadDocument(int docnum, IGlobalValues data)
        {
            return ForecastDocument.LoadDocument(docnum, data.ActiveSchema + ".ForecastDocumentHead", data.ActiveSchema + ".ForecastDocumentBody", data.ConnectionString);
        }
    }
}
