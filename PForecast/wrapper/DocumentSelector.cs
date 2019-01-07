using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documents;
using System.Data;

namespace PForecast
{
    class DocumentSelector
    {
        DocumentSelector() { }
        public static DataTable SelectInputDocuments(IGlobalValues data, DateTime start, DateTime end, string types)
        {
            return InputDocumentBase.SelectAllDocuments(data, data.ActiveSchema+".DocumentHead", data.ActiveSchema+".Factory", start, end, types);
        }
        public static DataTable SelectForecastDocuments(IGlobalValues data, DateTime start, DateTime end, string types)
        {
            return ForecastDocumentBase.SelectAllDocuments(data, data.ActiveSchema+".ForecastDocumentHead", data.ActiveSchema+".Factory", data.ActiveSchema+".RP", start, end, types);
        }
        public static DataTable SelectAllForecastDocuments(IGlobalValues data, DateTime start, DateTime end, string types)
        {
            return ForecastDocumentBase.SelectAllDocuments(data, "dbo.vForecastDocumentHead", "dbo.vFactory", "dbo.vRP", start, end, types);
        }
    }
}
