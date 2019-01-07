using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using Microsoft.Reporting.WinForms;

namespace PForecast
{
    public partial class DocumentViewer : Form
    {
        List<ReportParameter> parameters = new List<ReportParameter>() { };
        public List<ReportParameter> Paremeters { get { return parameters; } }
        
        public string Report { get; set; }
        public DocumentViewer()
        {
            InitializeComponent();
        }

        private void DocumentViewer_Load(object sender, EventArgs e)
        {
            Microsoft.Reporting.WinForms.ServerReport server = reportViewer1.ServerReport;

            ReportParameter[] param = new ReportParameter[1];
            server.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);
            server.ReportPath = Report;
            server.ReportServerCredentials.ImpersonationUser = System.Security.Principal.WindowsIdentity.GetCurrent();
            server.SetParameters(parameters);
            this.reportViewer1.RefreshReport();

            //this.Text = "Заявка №" + DocumentNumber.ToString();
        }
    }
}
