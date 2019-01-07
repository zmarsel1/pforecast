using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Documents
{
    public partial class FormInputDoc : Form
    {
        const int PRODUCTS = 1;
        const int DEVICES = 2;

        protected FormInputDoc() { }

        IGlobalValues Data = null;
        IInputDocument Document = null;

        public FormInputDoc(IInputDocument document, IGlobalValues data)
        {
            InitializeComponent();
            Document = document;
            Data = data;

            docInfo.Text = "Документ № " + Document.Head.DocumentNumber.ToString() + 
                "\nДата: " + Document.Head.DocumentDate.ToString("dd.MM.yyyy") + 
                "\nФабрика: " + Data.FactoryList[Document.Head.Factory];
            this.Text = Data.PlanDocType[Document.Head.DocType] + " № " + Document.Head.DocumentNumber.ToString();
        }

        private void FormPlan_Load(object sender, EventArgs e)
        {
            dataGrid.DataSource = Document.DocumentBody;

            if (Document.Head.DocType == 1 || Document.Head.DocType == 3)
            {
                DataGridViewComboBoxColumn column_hour = new DataGridViewComboBoxColumn();
                column_hour.Name = "Hour";
                column_hour.HeaderText = "Час";
                column_hour.DisplayMember = "Value";
                column_hour.ValueMember = "Key";
                column_hour.DataSource = new BindingSource(Data.HourList, null);
                column_hour.DataPropertyName = "DataHour";
                column_hour.ReadOnly = true;
                column_hour.SortMode = DataGridViewColumnSortMode.Automatic;
                column_hour.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                dataGrid.Columns.Insert(1, column_hour);

                DataGridViewComboBoxColumn column_product = new DataGridViewComboBoxColumn();
                column_product.Name = "Product";
                column_product.HeaderText = "Продукция";
                column_product.DataSource = new BindingSource(Data.ProductGroups[PRODUCTS], null);
                column_product.DisplayMember = "Value";
                column_product.ValueMember = "Key";
                column_product.DataPropertyName = "ProductID";

                dataGrid.Columns.Insert(2, column_product);

                dataGrid.Columns["DocumentID"].Visible = false;
                dataGrid.Columns["DataHour"].Visible = false;
                dataGrid.Columns["ProductID"].Visible = false;
                dataGrid.Columns["DataValue1"].HeaderText = Document.DocumentBody.Columns["DataValue1"].Caption;
                dataGrid.Columns["DataValue2"].HeaderText = Document.DocumentBody.Columns["DataValue2"].Caption;
                dataGrid.Columns["DataValue1"].DefaultCellStyle.Format = "0.00";
                dataGrid.Columns["DataValue2"].DefaultCellStyle.Format = "0.00";
             }
            else
            {
                chkCascade.Checked = false;
                DataGridViewComboBoxColumn column_hour = new DataGridViewComboBoxColumn();
                column_hour.Name = "Hour";
                column_hour.HeaderText = "Час";
                column_hour.DisplayMember = "Value";
                column_hour.ValueMember = "Key";
                column_hour.DataSource = new BindingSource(Data.HourList, null);
                column_hour.DataPropertyName = "DataHour";
                column_hour.ReadOnly = true;
                column_hour.SortMode = DataGridViewColumnSortMode.Automatic;
                column_hour.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                column_hour.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGrid.Columns.Insert(0, column_hour);

                int i = 1;
                foreach (var key in Data.ProductGroups[DEVICES].Keys)
                {

                    DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                    column.DataPropertyName = key.ToString();
                    column.HeaderText = Data.ProductGroups[DEVICES][key];
                    dataGrid.Columns.Insert(i, column);
                    dataGrid.Columns[key.ToString()].Visible = false;
                    ++i;
                }


                dataGrid.Columns["DataHour"].Visible = false;
            }
            Document.BeginEdit();

            if (Document.ReadOnly)
            {
                btnSave.Enabled = false;
                dataGrid.ReadOnly = true;
                this.Text += " (Просмотр)";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Document.UpdateDocument())
            {
                string msg  =  "Изменения не внесены.\n" + Document.Error.Message;
                ErrorMsgBox.Show("Ошибка.", msg, Document.Error.ToString());
                this.Close();
            }
            else
            {
                Document.EndEdit();
                MessageBox.Show("Изменения внесены.", "Информация", MessageBoxButtons.OK);
                Document.BeginEdit();
            }
        }

        private void dataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (chkCascade.Checked)
            {
                object data = dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                for (int i = e.RowIndex + 1; i < dataGrid.Rows.Count; ++i)
                {
                    //DataRow row = (DataRow)dataGrid.Rows[i].DataBoundItem;
                    dataGrid.Rows[i].Cells[e.ColumnIndex].Value = data;
                }
            }
        }

        private void dataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void FormPlan_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!Document.ReadOnly) Document.EndEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
