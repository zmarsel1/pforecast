using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Documents;


namespace PForecast
{
    public partial class ChooseParent : Form
    {
        public int DocumentID { get; set; }
        IGlobalValues Data = null;
        int rpID = 0;

        protected ChooseParent()
        {
            InitializeComponent();
        }
        public ChooseParent(int rp, IGlobalValues data)
        {
            InitializeComponent();
            rpID = rp;
            Data = data;
        }

        private void ChooseParent_Load(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));
            dtpEnd.Value = DateTime.Now.AddDays(3);
            SqlConnection connection = new SqlConnection(Data.ConnectionString);
            string sql = @"SELECT     TOP 50 fdh.DocumentID as DocumentID, fdh.DocumentDate as DocumentDate,
                        RP.RPName as RPName, f.FactoryName as FactoryName, fdt.DocTypeName as DocTypeName
                      FROM " + Data.ActiveSchema + @".ForecastDocumentHead fdh INNER JOIN
                      ForecastDocumentType fdt ON fdh.DocTypeID = fdt.DocTypeID INNER JOIN " +
                      Data.ActiveSchema + @".RP ON fdh.RPID = RP.RPID INNER JOIN " +
                      Data.ActiveSchema + ".Factory f ON RP.FactoryID = f.FactoryID WHERE fdh.RPID = " + rpID.ToString();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            gridForecastDocuments.DataSource = data;
            gridForecastDocuments.Columns["DocumentID"].HeaderText = "№ Документа";
            gridForecastDocuments.Columns["DocumentDate"].HeaderText = "Дата документа";
            gridForecastDocuments.Columns["RPName"].HeaderText = "РП";
            gridForecastDocuments.Columns["FactoryName"].HeaderText = "Фабрика";
            gridForecastDocuments.Columns["DocTypeName"].HeaderText = "Тип документа";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Data.ConnectionString);
            string sql = @"SELECT     TOP 50 fdh.DocumentID as DocumentID, fdh.DocumentDate as DocumentDate,
                        RP.RPName as RPName, f.FactoryName as FactoryName, fdt.DocTypeName as DocTypeName
                      FROM " + Data.ActiveSchema + @".ForecastDocumentHead fdh INNER JOIN 
                      ForecastDocumentType fdt ON fdh.DocTypeID = fdt.DocTypeID INNER JOIN " +
                      Data.ActiveSchema + @".RP ON fdh.RPID = RP.RPID INNER JOIN " +
                      Data.ActiveSchema + @".Factory f ON RP.FactoryID = f.FactoryID WHERE DocumentDate >= '" + 
                      dtpStart.Value.ToString("yyyyMMdd") + "' AND DocumentDate <= '" + 
                      dtpEnd.Value.ToString("yyyyMMdd") + "' "+
                      " AND fdh.RPID = " + rpID.ToString();

            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            gridForecastDocuments.DataSource = data;
            gridForecastDocuments.Columns["DocumentID"].HeaderText = "№ Документа";
            gridForecastDocuments.Columns["DocumentDate"].HeaderText = "Дата документа";
            gridForecastDocuments.Columns["RPName"].HeaderText = "РП";
            gridForecastDocuments.Columns["FactoryName"].HeaderText = "Фабрика";
            gridForecastDocuments.Columns["DocTypeName"].HeaderText = "Тип документа";
 
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (gridForecastDocuments.SelectedRows.Count > 0)
            {
                DocumentID = (int)gridForecastDocuments.SelectedRows[0].Cells["DocumentID"].Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Документ не выбран.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridForecastDocuments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridForecastDocuments.SelectedRows.Count > 0)
            {
                DocumentID = (int)gridForecastDocuments.SelectedRows[0].Cells["DocumentID"].Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
