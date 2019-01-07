using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Documents;

namespace PForecast
{
    public partial class CreateTender : Form
    {
        const int TENDER = 2;
        ForecastDocumentHead parentDoc = null;
        ForecastDocumentHead docHead = new ForecastDocumentHead();
        ForecastDocument Doc = null;
        IGlobalValues Data = null;

        public ForecastDocument Document { get { return Doc;} }
        protected CreateTender() { }

        public CreateTender(IGlobalValues data)
        {
            Data = data;
            InitializeComponent();
        }
        public CreateTender(IGlobalValues data, int parent)
        {
            InitializeComponent();
            Data = data;
            ForecastDocument doc = ForecastDocument.LoadDocument(parent, Data.ActiveSchema + ".ForecastDocumentHead", Data.ActiveSchema + ".ForecastDocumentBody", Data.ConnectionString);
            parentDoc = doc.Head;
        }
        private void CreateTender_Load(object sender, EventArgs e)
        {
            cmbRP.DataSource = new BindingSource(Data.RPList, null);
            cmbRP.DisplayMember = "Value";
            cmbRP.ValueMember = "Key";
            cmbRP.SelectedIndex = 0;
            cmbRP.Enabled = (cmbRP.Items.Count != 1);

            if (parentDoc != null)
            {
                cmbRP.SelectedValue = parentDoc.RPInfo;
                txtParentDocNum.Text = parentDoc.DocumentNumber.ToString();
                chkParentDocument.Checked = true;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            docHead.DocType = TENDER; //Заявка
            docHead.DocumentDate = dtpDateDoc.Value;
            if (chkParentDocument.Checked)
                docHead.DocumentParent = int.Parse(txtParentDocNum.Text);
            docHead.RPInfo = (int)cmbRP.SelectedValue;
            if (docHead.DocumentNumber == 0)
            {
                MessageBox.Show("Возникла ошибка при создании документа.\nНеверный номер документа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                IForecastDocument document = ForecastDocumentFactory.CreateDocument(docHead, Data);
                if (document == null) throw new Exception();
                Doc = document as ForecastDocument;
                string message = "Документ № " + document.Head.DocumentNumber.ToString() + " успешно создан.\nПерейти к редактированию?";
                if (MessageBox.Show(message, "Информация", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Yes;
                }
            }
            catch(Exception exception)
            {
                ErrorMsgBox.Show("Ошибка", "Возникла ошибка при создании документа.\n" + exception.Message, exception.ToString());
            }
            this.Dispose();
        }

        private void chkParentDocument_CheckedChanged(object sender, EventArgs e)
        {
            label1.Enabled = chkParentDocument.Checked;
            txtParentDocNum.Enabled = chkParentDocument.Checked;
            btnSearch.Enabled = chkParentDocument.Checked;
            
        }

        private void cmbRP_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkParentDocument.Checked = false;
            txtParentDocNum.Text = string.Empty;
            docHead.DocumentParent = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ChooseParent parent = new ChooseParent((int)cmbRP.SelectedValue, Data);
            if (parent.ShowDialog() == DialogResult.OK)
            {
                txtParentDocNum.Text = parent.DocumentID.ToString();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
