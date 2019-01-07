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
    public partial class ChooseParentInputDoc : Form
    {
        public int DocType { get; set; }
        public int Factory { get; set; }
        public int DocumentID { get; set; }
        IGlobalValues Data = null;
        protected ChooseParentInputDoc() { }

        public ChooseParentInputDoc(IGlobalValues data)
        {
            InitializeComponent();
            Data = data;
        }

        private void ChooseParentPlan_Load(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));
            dtpEnd.Value = DateTime.Now.AddDays(3);
            
            SqlConnection connection = new SqlConnection(Data.ConnectionString);
            string sql = @"SELECT     TOP 50 dh.DocumentID as DocumentID, dh.DocumentDate as DocumentDate,
                       f.FactoryName as FactoryName, dt.DocTypeName as DocTypeName FROM " +
                      Data.ActiveSchema +@".DocumentHead dh INNER JOIN dbo.DocumentType dt ON dh.DocTypeID = dt.DocTypeID INNER JOIN " +
                      Data.ActiveSchema + @".Factory f ON dh.FactoryID = f.FactoryID WHERE dh.FactoryID = " + Factory.ToString()
                      + " AND dh.DocTypeID = " + DocType.ToString();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            gridData.DataSource = data;
            gridData.Columns["DocumentID"].HeaderText = "№ Документа";
            gridData.Columns["DocumentDate"].HeaderText = "Дата документа";
            gridData.Columns["FactoryName"].HeaderText = "Фабрика";
            gridData.Columns["DocTypeName"].HeaderText = "Тип документа";

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Data.ConnectionString);
            string sql = @"SELECT     dh.DocumentID as DocumentID, dh.DocumentDate as DocumentDate, f.FactoryName as FactoryName, dt.DocTypeName as DocTypeName
                      FROM " + Data.ActiveSchema + @".DocumentHead dh INNER JOIN DocumentType dt ON dh.DocTypeID = dt.DocTypeID INNER JOIN " +
                      Data.ActiveSchema + ".Factory f ON dh.FactoryID = f.FactoryID WHERE DocumentDate >= '" +
                      dtpStart.Value.ToString("yyyyMMdd") + "' AND DocumentDate <= '" + dtpEnd.Value.ToString("yyyyMMdd") + "' " +
                      " AND dh.FactoryID = " + Factory.ToString() + " AND dh.DocTypeID = " + DocType.ToString();

            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            gridData.DataSource = data;
            gridData.Columns["DocumentID"].HeaderText = "№ Документа";
            gridData.Columns["DocumentDate"].HeaderText = "Дата документа";
            gridData.Columns["FactoryName"].HeaderText = "Фабрика";
            gridData.Columns["DocTypeName"].HeaderText = "Тип документа";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (gridData.SelectedRows.Count > 0)
            {
                DocumentID = (int)gridData.SelectedRows[0].Cells["DocumentID"].Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Документ не выбран.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridData.SelectedRows.Count > 0)
            {
                DocumentID = (int)gridData.SelectedRows[0].Cells["DocumentID"].Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
