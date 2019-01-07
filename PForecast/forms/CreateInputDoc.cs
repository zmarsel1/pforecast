using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Documents;

namespace PForecast
{
    public partial class CreateInputDoc : Form
    {
        const int devices = 2;
        const int products = 1;

        protected CreateInputDoc() { }

        public IInputDocument Document { get; set; }
        IGlobalValues Data { get; set; }

        public CreateInputDoc(IGlobalValues data)
        {
            InitializeComponent();
            
            Data = data;
            
            cmbDocType.DataSource = new BindingSource(data.PlanDocType, null);
            cmbDocType.ValueMember = "Key";
            cmbDocType.DisplayMember = "Value";

            cmbFactory.DataSource = new BindingSource(data.FactoryList, null);
            cmbFactory.DisplayMember = "Value";
            cmbFactory.ValueMember = "Key";

            cmbFactory.Enabled = (cmbFactory.Items.Count != 1);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            InputDocumentHead head = new InputDocumentHead();
            head.DocType = (int)cmbDocType.SelectedValue;
            head.DocumentDate = dtpDate.Value;
            head.Factory = (int)cmbFactory.SelectedValue;

            try
            {
                if (chkParent.Checked)
                {
                    switch (head.DocType)
                    { 
                        case 1:
                        case 3:
                            Document = InputDocFactory.CreateDocument(head, Data, int.Parse(txtParent.Text));
                            break;
                        case 2:
                        case 4:
                            Document = InputDocFactory.CreateDocument(head, Data, int.Parse(txtParent.Text));
                           break;
                    }
                }
                else
                {
                    switch (head.DocType)
                    {
                        case 1:
                        case 3:
                            Document = InputDocFactory.CreateDocument(head, Data);
                            break;
                        case 2:
                        case 4:
                            Document = InputDocFactory.CreateDocument(head, Data); 
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorMsgBox.Show("Ошибка", "Ошибка создания документа.\n" + exception.Message, exception.ToString());
                return;
            }
            if (Document != null)
            {  
                string message = "Документ № " + head.DocumentNumber.ToString() + " успешно создан.\nПерейти к редактированию?";
                if (MessageBox.Show(message, "Информация", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Yes;
                }
            }
            else
            {
                ErrorMsgBox.Show("Ошибка", "Ошибка создания документа.", "Документ с таким номером уже существует.");
            }
            this.Close();
        }

        private void CreatePlan_Load(object sender, EventArgs e)
        {
            btnSearchParent.Enabled = chkParent.Checked;
            label4.Enabled = chkParent.Checked;
        }

        private void btnSearchParent_Click(object sender, EventArgs e)
        {
            ChooseParentInputDoc choose = new ChooseParentInputDoc(Data);
            choose.DocType = (int)cmbDocType.SelectedValue;
            choose.Factory = (int)cmbFactory.SelectedValue;

            if (choose.ShowDialog() == DialogResult.OK)
            {
                txtParent.Text = choose.DocumentID.ToString();
            }
        }

        private void chkParent_CheckedChanged(object sender, EventArgs e)
        {
            btnSearchParent.Enabled = chkParent.Checked;
            label4.Enabled = chkParent.Checked;
        }

        private void cmbDocType_SelectedValueChanged(object sender, EventArgs e)
        {
            chkParent.Checked = false;
            txtParent.Text = string.Empty;
        }
    }
}
