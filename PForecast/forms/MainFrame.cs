using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using Documents;
using Microsoft.Reporting.WinForms;

namespace PForecast
{
    public partial class MainFrame : Form
    {
        Color[] PlanColors = new Color[] {Color.White, Color.LightBlue, Color.LavenderBlush, Color.SteelBlue, Color.Peru};
        Color[] ForecastColors = new Color[] {Color.White, Color.LightGoldenrodYellow, Color.LightSteelBlue };
        Color[] ViewColors = new Color[] { Color.White, Color.LightBlue };
        public MainFrame()
        {
            InitializeComponent();
        }

        private void btnCreateForecast_Click(object sender, EventArgs e)
        {
            if (ForecastCreator.Instance.DoForecast((int)cmbFactory.SelectedValue, dtpForecastDate.Value))
            {
                MessageBox.Show("Прогноз успешно создан.");
                btnSearch_Click(null, null);
            }
            else
            {
                MessageBox.Show("Ошибка создания прогноза:\n" + ForecastCreator.Instance.ErrorMessage);
            }
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            
            if (!GlobalValues.Instance.InitUser(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                ErrorMsgBox.Show("Ошибка", "Ошибка инициализации пользователя.\n" + GlobalValues.Instance.exception.Message, GlobalValues.Instance.exception.ToString());
                this.Close();
            }
            if (!GlobalValues.Instance.InitData())
            {
                ErrorMsgBox.Show("Ошибка", "Ошибка инициализации настроек.\n" + GlobalValues.Instance.exception.Message, GlobalValues.Instance.exception.ToString());
                this.Close();
            }
            //GlobalData.Instance.InitData();
            if (GlobalValues.Instance.RoleTable.Rows.Count == 1) cmbRole.Visible = false;
            else
            {

                cmbRole.DataSource = GlobalValues.Instance.RoleTable;
                cmbRole.DisplayMember = "RoleName";
                cmbRole.ValueMember = "RoleID";
                cmbRole.SelectedValue = GlobalValues.Instance.ActiveRole;
            }
            if (GlobalValues.Instance.ActiveRole.Contains("energy"))
            {
                cmbFactory.DataSource = null;
                cmbFactory.DataSource = new BindingSource(GlobalValues.Instance.FactoryList, null);
                cmbFactory.DisplayMember = "Value";
                cmbFactory.ValueMember = "Key";
                cmbFactory.SelectedIndex = 0;
                cmbFactory.Enabled = cmbFactory.Items.Count != 1;
            }
            dtpEnd.Value = DateTime.Now.AddDays(3);
            dtpStart.Value = DateTime.Now.Subtract(new TimeSpan(3, 0, 0, 0));
            ChangeRole();

            contextDocType.Closing+= new ToolStripDropDownClosingEventHandler(contextDocType_Closing);
            contextForecastType.Closing += new ToolStripDropDownClosingEventHandler(contextDocType_Closing);
            btnSearch_Click(null, null);
        }
        private void contextDocType_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked ^= true;
        }
        private void contextDocType_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
                e.Cancel = true;
        }
        private void btnTender_Click(object sender, EventArgs e)
        {
            CreateTender f = new CreateTender(GlobalValues.Instance);
            f.ShowDialog();
            if (f.DialogResult == DialogResult.Yes)
            {
                FormTender edit = new FormTender(f.Document, GlobalValues.Instance);
                edit.Show();
                edit.Activate();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string role = GlobalValues.Instance.ActiveRole;
            string doctypes = string.Empty;
            if (tabControl.TabPages.Contains(tabForecast)) // отображение прогнозов и заявок
            {

                doctypes = string.Empty;
                foreach (ToolStripMenuItem item in contextForecastType.Items)
                {
                    if (item.Checked)
                    {
                        doctypes += (doctypes == string.Empty ? "" : ",") + item.Tag.ToString();
                    }
                }
                try
                {
                    var data = DocumentSelector.SelectForecastDocuments(GlobalValues.Instance, dtpStart.Value, dtpEnd.Value, doctypes);
                    gridForecastDocuments.DataSource = data;
                    gridForecastDocuments.Columns["DocumentID"].HeaderText = "№ Документа";
                    gridForecastDocuments.Columns["DocumentDate"].HeaderText = "Дата";
                    gridForecastDocuments.Columns["RPName"].HeaderText = "РП";
                    gridForecastDocuments.Columns["FactoryName"].HeaderText = "Фабрика";
                    gridForecastDocuments.Columns["ParentDocument"].HeaderText = "Связанный документ";
                    gridForecastDocuments.Columns["DocTypeName"].HeaderText = "Тип документа";
                    gridForecastDocuments.Columns["DocTypeID"].Visible = false;
                    gridForecastDocuments.Columns["RPID"].Visible = false;
                    gridForecastDocuments.Columns["FactoryID"].Visible = false;
                }
                catch (Exception exc)
                {
                    ErrorMsgBox.Show("Ошибка", "Ошибка запроса заявок(прогнозов)\r\n" + exc.Message, exc.ToString());
                }
            }
            if (tabControl.TabPages.Contains(tabPlan)) // отображение плнов произвостваи графиков работы оборудования
            {
                doctypes = string.Empty;
                foreach (ToolStripMenuItem item in contextDocType.Items)
                {
                    if (item.Checked)
                    {
                        doctypes += (doctypes == string.Empty ? "" : ",") + item.Tag.ToString();
                    }
                }
                try
                {
                    var data = DocumentSelector.SelectInputDocuments(GlobalValues.Instance, dtpStart.Value, dtpEnd.Value, doctypes);
                    gridPlanDocuments.DataSource = data;
                    gridPlanDocuments.Columns["DocumentID"].HeaderText = "№ Документа";
                    gridPlanDocuments.Columns["DocumentDate"].HeaderText = "Дата";
                    gridPlanDocuments.Columns["FactoryName"].HeaderText = "Фабрика";
                    gridPlanDocuments.Columns["DocTypeName"].HeaderText = "Тип документа";
                    gridPlanDocuments.Columns["DocTypeID"].Visible = false;
                    gridPlanDocuments.Columns["FactoryID"].Visible = false;
                }
                catch (Exception exc)
                {
                    ErrorMsgBox.Show("Ошибка", "Ошибка запроса планов производства(графиков работы оборудования)\r\n" + exc.Message, exc.ToString());
                }
            }
            if (tabControl.TabPages.Contains(tabViewForecast)) // просмотр заявок(прогнозов)
            {
                doctypes = string.Empty;
                foreach (ToolStripMenuItem item in contextForecastType.Items)
                {
                    if (item.Checked)
                    {
                        doctypes += (doctypes == string.Empty ? "" : ",") + item.Tag.ToString();
                    }
                }
                try
                {
                    var data = DocumentSelector.SelectAllForecastDocuments(GlobalValues.Instance, dtpStart.Value, dtpEnd.Value, doctypes);
                    gridForecastView.DataSource = data;
                    gridForecastView.Columns["DocumentID"].HeaderText = "№ Документа";
                    gridForecastView.Columns["DocumentDate"].HeaderText = "Дата";
                    gridForecastView.Columns["RPName"].HeaderText = "РП";
                    gridForecastView.Columns["FactoryName"].HeaderText = "Фабрика";
                    gridForecastView.Columns["ParentDocument"].HeaderText = "Связанный документ";
                    gridForecastView.Columns["DocTypeName"].HeaderText = "Тип документа";
                    gridForecastView.Columns["DocTypeID"].Visible = false;
                    gridForecastView.Columns["RPID"].Visible = false;
                    gridForecastView.Columns["FactoryID"].Visible = false;
                }
                catch (Exception exc)
                {
                    ErrorMsgBox.Show("Ошибка", "Ошибка запроса заявок(прогнозов)\r\n" + exc.Message, exc.ToString());
                }
            }
            //if (tabControl.TabPages.Contains(tabLog)) // просмотр журнала событий
            //{
            //    try
            //    {
            //        var data = new DataTable();
            //        var query = "SELECT * FROM dbo.DocsHistory WHERE adate >= '" + dtpStart.Value.ToString("yyyyMMdd") + "' AND adate <= '" + dtpEnd.Value.ToString("yyyyMMdd") + "'";
            //        var sql = new SqlConnection(GlobalValues.Instance.ConnectionString);
            //        var adapter = new SqlDataAdapter(query, sql);
            //        adapter.Fill(data);
            //        gridLog.DataSource = data;
            //        gridLog.Columns["cmd"].HeaderText = "Команда";
            //        gridLog.Columns["actiontable"].HeaderText = "Тип документа";
            //        gridLog.Columns["adate"].HeaderText = "Дата";
            //        gridLog.Columns["docid"].HeaderText = "№ Документа";
            //        gridLog.Columns["username"].HeaderText = "Пользователь";
            //    }
            //    catch (Exception exc)
            //    {
            //        ErrorMsgBox.Show("Ошибка", "Ошибка запроса \"Журнала событий\"\r\n" + exc.Message, exc.ToString());
            //    }
            //}
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (gridForecastDocuments.SelectedRows.Count > 0)
            {
                try
                {
                    int docid = (int)gridForecastDocuments.SelectedRows[0].Cells["DocumentID"].Value;
                    ForecastDocument document = ForecastDocumentFactory.LoadDocument(docid, GlobalValues.Instance);
                    FormTender edit = new FormTender(document, GlobalValues.Instance);
                    edit.Show();
                }
                catch (Exception exc)
                {
                    ErrorMsgBox.Show("Ошибка", "Ошибка редактирования документа\r\n" + exc.Message, exc.ToString());
                    return;
                }
            }
        }

        private void btnCreatePlan_Click(object sender, EventArgs e)
        {
            try
            {
                CreateInputDoc create = new CreateInputDoc(GlobalValues.Instance);
                create.ShowDialog();
                if (create.DialogResult == DialogResult.Yes)
                {
                    FormInputDoc edit = new FormInputDoc(create.Document, GlobalValues.Instance);
                    edit.Show();
                    edit.Activate();
                }
            }
            catch (Exception exc)
            {
                ErrorMsgBox.Show("Ошибка", "Ошибка создания документа\r\n" + exc.Message, exc.ToString());
                return;
            }
        }

        private void btnEditPlan_Click(object sender, EventArgs e)
        {
            if (gridPlanDocuments.SelectedRows.Count > 0)
            {
                try
                {
                    IInputDocument doc = InputDocFactory.LoadDocument((int)gridPlanDocuments.SelectedRows[0].Cells["DocumentID"].Value, GlobalValues.Instance);
                    FormInputDoc edit = new FormInputDoc(doc, GlobalValues.Instance);
                    edit.Show();
                    edit.Activate();
                }
                catch (Exception exc)
                {
                    ErrorMsgBox.Show("Ошибка","Ошибка редактирования документа\r\n" + exc.Message, exc.ToString());
                    return;
                }

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            
            if (gridForecastDocuments.SelectedRows.Count > 0)
            {
                
                DocumentViewer view = new DocumentViewer();
                int docNumber = (int) gridForecastDocuments.SelectedRows[0].Cells["DocumentID"].Value; 
                view.Paremeters.Add(new ReportParameter("DocID", docNumber.ToString(), false)); 
                view.Report = ConfigurationManager.AppSettings["TenderReport"];
                view.Show();
            }
            else
            {
                MessageBox.Show("Документа не выбран.");
            }
        }

        private void gridPlanDocuments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridPlanDocuments.SelectedRows.Count > 0)
            {
                try
                {
                    IInputDocument doc = InputDocFactory.LoadDocument((int)gridPlanDocuments.SelectedRows[0].Cells["DocumentID"].Value, GlobalValues.Instance);

                    FormInputDoc edit = new FormInputDoc(doc, GlobalValues.Instance);
                    edit.Show();
                    edit.Activate();
                }
                catch (Exception exc)
                {
                    ErrorMsgBox.Show("Ошибка", "Ошибка редактирования документа\r\n" + exc.Message, exc.ToString());
                    return;
                }
            }
        }

        private void gridForecastDocuments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridForecastDocuments.SelectedRows.Count > 0)
            {
                try
                {
                    int docid = (int)gridForecastDocuments.SelectedRows[0].Cells["DocumentID"].Value;
                    ForecastDocument document = ForecastDocumentFactory.LoadDocument(docid, GlobalValues.Instance);
                    FormTender edit = new FormTender(document, GlobalValues.Instance);
                    edit.Show();
                    edit.Activate();
                }
                catch (Exception exc)
                {
                    ErrorMsgBox.Show("Ошибка", "Ошибка редактирования документа\r\n" + exc.Message, exc.ToString());
                    return;
                }
            }
        }

        private void toolReportCompare_Click(object sender, EventArgs e)
        {
            if (gridForecastDocuments.SelectedRows.Count > 0)
            {
                DocumentViewer view = new DocumentViewer();
                int docNumber = (int)gridForecastDocuments.SelectedRows[0].Cells["DocumentID"].Value;
                view.Paremeters.Add(new ReportParameter("DocID", docNumber.ToString(), false));
                view.Report = ConfigurationManager.AppSettings["TenderFactReport"];
                view.Show();
            }
            else
            {
                MessageBox.Show("Документа не выбран.");
            }
        }

        private void toolTenderFactGrReport_Click(object sender, EventArgs e)
        {
            if (gridForecastDocuments.SelectedRows.Count > 0)
            {
                DocumentViewer view = new DocumentViewer();
                int docNumber = (int)gridForecastDocuments.SelectedRows[0].Cells["DocumentID"].Value;
                view.Paremeters.Add(new ReportParameter("DocID", docNumber.ToString(), false));
                view.Report = ConfigurationManager.AppSettings["TenderFactGrReport"];
                view.Show();
            }
            else
            {
                MessageBox.Show("Документа не выбран.");
            }
        }

        private void toolCreateTender_Click(object sender, EventArgs e)
        {
            if (gridForecastDocuments.SelectedRows.Count > 0)
            {
                try
                {
                    int documentNumber = (int)gridForecastDocuments.SelectedRows[0].Cells["DocumentID"].Value;
                    CreateTender tender = new CreateTender(GlobalValues.Instance, documentNumber);
                    tender.Show();
                }
                catch (Exception exc)
                {
                    ErrorMsgBox.Show("Ошибка", "Ошибка создания заявки\r\n" + exc.Message, exc.ToString());
                    return;
                }
            }
            else
            {
                MessageBox.Show("Документа не выбран.");
            }
        }

        private void gridForecastDocuments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int index = (int)gridForecastDocuments.Rows[e.RowIndex].Cells["DocTypeID"].Value;
            try
            {
                e.CellStyle.BackColor = ForecastColors[index];
            }
            catch
            { 
            
            }
        }

        private void gridPlanDocuments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int index = (int)gridPlanDocuments.Rows[e.RowIndex].Cells["DocTypeID"].Value;
            try
            {
                e.CellStyle.BackColor = PlanColors[index];
            }
            catch
            {

            }
        }

        private void cmbRole_SelectedValueChanged(object sender, EventArgs e)
        {
            if(GlobalValues.Instance.RoleTable.Rows.Contains(cmbRole.SelectedValue))
            {
                string role = cmbRole.SelectedValue.ToString();
                if (role != GlobalValues.Instance.ActiveRole)
                {
                    GlobalValues.Instance.ActiveRole = role;
                    GlobalValues.Instance.InitData();
                    ChangeRole(); // меняем интерфейс
                    if (role.Contains("energy"))
                    {
                        cmbFactory.DataSource = null;
                        cmbFactory.DataSource = new BindingSource(GlobalValues.Instance.FactoryList, null);
                        cmbFactory.DisplayMember = "Value";
                        cmbFactory.ValueMember = "Key";
                        cmbFactory.SelectedIndex = 0;
                        cmbFactory.Enabled = cmbFactory.Items.Count != 1;  
                    }
                    
                    btnSearch_Click(null, null);
                }
            }
        }

        private void btnDocType_Click(object sender, EventArgs e)
        {
            Point location = new Point(btnDocType.Location.X + btnDocType.Width, btnDocType.Location.Y);
            contextDocType.Show(tabPlan, location);
        }

        private void btnForecastType_Click(object sender, EventArgs e)
        {
            Point location = new Point(btnForecastType.Location.X + btnForecastType.Width, btnForecastType.Location.Y);
            contextForecastType.Show(tabForecast, location);
        }

        private void btnForecastType2_Click(object sender, EventArgs e)
        {
            Point location = new Point(btnForecastType2.Location.X + btnForecastType2.Width, btnForecastType2.Location.Y);
            contextForecastType.Show(tabViewForecast, location);
        }

        private void btnPrintViewForecast_Click(object sender, EventArgs e)
        {
            if (gridForecastView.SelectedRows.Count > 0)
            {
                DocumentViewer view = new DocumentViewer();
                int docNumber = (int)gridForecastView.SelectedRows[0].Cells["DocumentID"].Value;
                view.Paremeters.Add(new ReportParameter("DocID", docNumber.ToString(), false));
                view.Report = ConfigurationManager.AppSettings["TenderReport"];
                view.Show();
            }
            else
            {
                MessageBox.Show("Документа не выбран.");
            }
        }

        private void toolReportCompare2_Click(object sender, EventArgs e)
        {
            if (gridForecastView.SelectedRows.Count > 0)
            {
                DocumentViewer view = new DocumentViewer();
                int docNumber = (int)gridForecastView.SelectedRows[0].Cells["DocumentID"].Value;
                view.Paremeters.Add(new ReportParameter("DocID", docNumber.ToString(), false));
                view.Report = ConfigurationManager.AppSettings["TenderFactReport"];
                view.Show();
            }
            else
            {
                MessageBox.Show("Документа не выбран.");
            }
        }

        private void toolReportCompareGraphic_Click(object sender, EventArgs e)
        {
            if (gridForecastView.SelectedRows.Count > 0)
            {
                DocumentViewer view = new DocumentViewer();
                int docNumber = (int)gridForecastView.SelectedRows[0].Cells["DocumentID"].Value;
                view.Paremeters.Add(new ReportParameter("DocID", docNumber.ToString(), false));
                view.Report = ConfigurationManager.AppSettings["TenderFactGrReport"];
                view.Show();
            }
            else
            {
                MessageBox.Show("Документа не выбран.");
            }
        }
        private void ChangeRole()
        {
            tabControl.TabPages.Clear();
            switch (GlobalValues.Instance.ActiveRole)
            {
                case "kf_plan":
                case "bf_plan":
                    tabControl.TabPages.Insert(0, tabPlan);
                    break;
                case "kf_energy":
                case "bf_energy":
                    tabControl.TabPages.Insert(0, tabForecast);
                    tabControl.TabPages.Insert(1, tabPlan);
                    break;

                case "other_energy":
                    tabControl.TabPages.Insert(0,tabForecast);
                    break;
                case "combine_energy":
                    tabControl.TabPages.Insert(0,tabForecast);
                    tabControl.TabPages.Add(tabViewForecast);
                    //tabControl.TabPages.Add(tabLog);
                    break;
                default:
                    break;
            }
            ToolStripMenuItem item;
            contextDocType.Items.Clear();
            foreach (var key in GlobalValues.Instance.PlanDocType.Keys)
            {
                item = new ToolStripMenuItem(GlobalValues.Instance.PlanDocType[key]);
                item.Checked = true;
                item.Tag = key;
                item.Click += new EventHandler(contextDocType_Click);
                contextDocType.Items.Add(item);
            }
            contextForecastType.Items.Clear();
            foreach (var key in GlobalValues.Instance.ForecastDocType.Keys)
            {
                item = new ToolStripMenuItem(GlobalValues.Instance.ForecastDocType[key]);
                item.Checked = true;
                item.Tag = key;
                item.Click += new EventHandler(contextDocType_Click);
                contextForecastType.Items.Add(item);
            }
        }
        private void gridForecastView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int index = ((DateTime)gridForecastView.Rows[e.RowIndex].Cells["DocumentDate"].Value).DayOfYear % 2;
            try
            {
                e.CellStyle.BackColor = ViewColors[index];
            }
            catch
            {

            }
        }

        private void btnDeleteTender_Click(object sender, EventArgs e)
        {
            if (gridForecastDocuments.SelectedRows.Count > 0)
            {
                try
                {
                    int docid = (int)gridForecastDocuments.SelectedRows[0].Cells["DocumentID"].Value;
                    ForecastDocument document = ForecastDocumentFactory.LoadDocument(docid, GlobalValues.Instance);
                    if (MessageBox.Show("Вы хотите удалить документ № " + docid.ToString(), "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (document.DeleteDocument()) MessageBox.Show("Документ № " + docid.ToString() + " удалён.");
                    }
                }
                catch (Exception exc)
                {
                    ErrorMsgBox.Show("Ошибка", "Ошибка удаления документа\r\n" + exc.Message, exc.ToString());
                    return;
                }

            }
        }

        private void gridForecastView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridForecastView.SelectedRows.Count > 0)
            {
                DocumentViewer view = new DocumentViewer();
                int docNumber = (int)gridForecastView.SelectedRows[0].Cells["DocumentID"].Value;
                view.Paremeters.Add(new ReportParameter("DocID", docNumber.ToString(), false));
                view.Report = ConfigurationManager.AppSettings["TenderReport"];
                view.Show();
            }
            else
            {
                MessageBox.Show("Документа не выбран.");
            }
        }
    }
}
