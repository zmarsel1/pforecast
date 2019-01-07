using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Documents
{
    public partial class FormTender : Form
    {
        const int FORECAST = 1;
        const int TENDER = 2;

        protected FormTender() { }
        IGlobalValues Data { get; set; }
        IForecastDocument Document { get; set; }
        public FormTender(IForecastDocument document, IGlobalValues data)
        {
            InitializeComponent();
            Data = data;
            Document = document;
        }

        private void FormTender_Load(object sender, EventArgs e)
        {
            gridDocBody.DataSource = Document.DocumentBody;
            
            DataGridViewComboBoxColumn hour = new DataGridViewComboBoxColumn();
            hour.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            hour.ReadOnly = true;
            hour.HeaderText = "Час";
            hour.DataSource = new BindingSource(Data.HourList, null);
            hour.DisplayMember = "Value";
            hour.ValueMember = "Key";
            hour.DataPropertyName = "DataHour";
            hour.SortMode = DataGridViewColumnSortMode.Automatic;
            gridDocBody.Columns.Insert(1, hour);
            
            gridDocBody.Columns["DataHour"].Visible = false;
            gridDocBody.Columns["DocumentID"].Visible = false;
            gridDocBody.Columns["DataValue"].HeaderText = Document.DocumentBody.Columns["DataValue"].Caption;
            gridDocBody.Columns["DataValue"].DefaultCellStyle.Format = "0.00";

            gridDocBody_CellValueChanged(null, null);

            string tender = Document.Head.DocType == FORECAST ? "Прогноз № " : "Заявка № ";
            lblInfo.Text = tender + Document.Head.DocumentNumber.ToString() + "\nДата: " + Document.Head.DocumentDate.ToString("dd.MM.yyyy") + "\nРП: " + Data.RPList[Document.Head.RPInfo];
            this.Text = tender + Document.Head.DocumentNumber.ToString();
            Document.BeginEdit();
            if (Document.Head.DocType == FORECAST || Document.ReadOnly) //если прогноз или документ только для чтения
            {
                gridDocBody.ReadOnly = true;
                btnSave.Enabled = false;
                this.Text += " (Просмотр)";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Document.UpdateDocument())
                { 
                    string msg = Document.Error.Message;
                    ErrorMsgBox.Show("Ошбика.", msg, Document.Error.ToString());
                };
                Document.EndEdit();
                MessageBox.Show("Изменения сохранены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Document.BeginEdit();
                //this.Close();
            
            }
            catch(Exception exception)
            {
                ErrorMsgBox.Show("Ошбика.", "Изменения не были внесены. "+exception.Message, exception.ToString());
                this.Close();
            }
        }

        private void FormTender_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Document.ReadOnly) Document.EndEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridDocBody_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (chkCascade.Checked)
            {
                object data = gridDocBody.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                for (int i = e.RowIndex + 1; i < gridDocBody.Rows.Count; ++i)
                {
                    //DataRow row = (DataRow)dataGrid.Rows[i].DataBoundItem;
                    gridDocBody.Rows[i].Cells[e.ColumnIndex].Value = data;
                }
            }
        }

        private void gridDocBody_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt = gridDocBody.DataSource as DataTable;
            lblTotal.Text = "Сумма: " + ((double)dt.Compute("SUM(DataValue)", "")).ToString("0.00");
        
        }
    }
}
