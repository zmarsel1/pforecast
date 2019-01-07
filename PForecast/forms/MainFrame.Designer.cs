namespace PForecast
{
    partial class MainFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolCreateTender = new System.Windows.Forms.ToolStripMenuItem();
            this.toolPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolReportCompare = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTenderFactGrReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuReport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolPrintView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolReportCompare2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolReportCompareGraphic = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.contextDocType = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextForecastType = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabViewForecast = new System.Windows.Forms.TabPage();
            this.gridForecastView = new System.Windows.Forms.DataGridView();
            this.btnForecastType2 = new System.Windows.Forms.Button();
            this.btnPrintViewForecast = new System.Windows.Forms.Button();
            this.tabPlan = new System.Windows.Forms.TabPage();
            this.gridPlanDocuments = new System.Windows.Forms.DataGridView();
            this.btnCreatePlan = new System.Windows.Forms.Button();
            this.btnEditPlan = new System.Windows.Forms.Button();
            this.btnDocType = new System.Windows.Forms.Button();
            this.tabForecast = new System.Windows.Forms.TabPage();
            this.cmbFactory = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpForecastDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateForecast = new System.Windows.Forms.Button();
            this.btnTender = new System.Windows.Forms.Button();
            this.gridForecastDocuments = new System.Windows.Forms.DataGridView();
            this.btnEditTender = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnForecastType = new System.Windows.Forms.Button();
            this.btnDeleteTender = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.contextMenuEdit.SuspendLayout();
            this.contextMenuReport.SuspendLayout();
            this.tabViewForecast.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridForecastView)).BeginInit();
            this.tabPlan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPlanDocuments)).BeginInit();
            this.tabForecast.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridForecastDocuments)).BeginInit();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuEdit
            // 
            this.contextMenuEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolEdit,
            this.toolCreateTender,
            this.toolPrint,
            this.toolStripSeparator1,
            this.toolReportCompare,
            this.toolTenderFactGrReport,
            this.toolDelete});
            this.contextMenuEdit.Name = "contextMenuEdit";
            this.contextMenuEdit.Size = new System.Drawing.Size(237, 142);
            // 
            // toolEdit
            // 
            this.toolEdit.Name = "toolEdit";
            this.toolEdit.Size = new System.Drawing.Size(236, 22);
            this.toolEdit.Text = "Редактировать";
            this.toolEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolCreateTender
            // 
            this.toolCreateTender.Name = "toolCreateTender";
            this.toolCreateTender.Size = new System.Drawing.Size(236, 22);
            this.toolCreateTender.Text = "Создать Заявку";
            this.toolCreateTender.Click += new System.EventHandler(this.toolCreateTender_Click);
            // 
            // toolPrint
            // 
            this.toolPrint.Name = "toolPrint";
            this.toolPrint.Size = new System.Drawing.Size(236, 22);
            this.toolPrint.Text = "Печать";
            this.toolPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(233, 6);
            // 
            // toolReportCompare
            // 
            this.toolReportCompare.Name = "toolReportCompare";
            this.toolReportCompare.Size = new System.Drawing.Size(236, 22);
            this.toolReportCompare.Text = "Отчёт: Заявка-Факт";
            this.toolReportCompare.Click += new System.EventHandler(this.toolReportCompare_Click);
            // 
            // toolTenderFactGrReport
            // 
            this.toolTenderFactGrReport.Name = "toolTenderFactGrReport";
            this.toolTenderFactGrReport.Size = new System.Drawing.Size(236, 22);
            this.toolTenderFactGrReport.Text = "Отчёт: Заявка-Факт(График)";
            this.toolTenderFactGrReport.Click += new System.EventHandler(this.toolTenderFactGrReport_Click);
            // 
            // toolDelete
            // 
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(236, 22);
            this.toolDelete.Text = "Удалить";
            this.toolDelete.Click += new System.EventHandler(this.btnDeleteTender_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(826, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "&Поиск";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(479, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "С:";
            // 
            // dtpStart
            // 
            this.dtpStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStart.Location = new System.Drawing.Point(502, 3);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(144, 20);
            this.dtpStart.TabIndex = 9;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEnd.Location = new System.Drawing.Point(682, 4);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(138, 20);
            this.dtpEnd.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(652, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "По:";
            // 
            // contextMenuReport
            // 
            this.contextMenuReport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolPrintView,
            this.toolReportCompare2,
            this.toolReportCompareGraphic});
            this.contextMenuReport.Name = "contextMenuReport";
            this.contextMenuReport.Size = new System.Drawing.Size(237, 70);
            // 
            // toolPrintView
            // 
            this.toolPrintView.Name = "toolPrintView";
            this.toolPrintView.Size = new System.Drawing.Size(236, 22);
            this.toolPrintView.Text = "Печать";
            this.toolPrintView.Click += new System.EventHandler(this.btnPrintViewForecast_Click);
            // 
            // toolReportCompare2
            // 
            this.toolReportCompare2.Name = "toolReportCompare2";
            this.toolReportCompare2.Size = new System.Drawing.Size(236, 22);
            this.toolReportCompare2.Text = "Отчёт: Заявка-Факт";
            this.toolReportCompare2.Click += new System.EventHandler(this.toolReportCompare2_Click);
            // 
            // toolReportCompareGraphic
            // 
            this.toolReportCompareGraphic.Name = "toolReportCompareGraphic";
            this.toolReportCompareGraphic.Size = new System.Drawing.Size(236, 22);
            this.toolReportCompareGraphic.Text = "Отчёт: Заявка-Факт(График)";
            this.toolReportCompareGraphic.Click += new System.EventHandler(this.toolReportCompareGraphic_Click);
            // 
            // cmbRole
            // 
            this.cmbRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(331, 2);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(142, 21);
            this.cmbRole.TabIndex = 14;
            this.cmbRole.SelectedValueChanged += new System.EventHandler(this.cmbRole_SelectedValueChanged);
            // 
            // contextDocType
            // 
            this.contextDocType.Name = "contextDocType";
            this.contextDocType.Size = new System.Drawing.Size(61, 4);
            // 
            // contextForecastType
            // 
            this.contextForecastType.Name = "contextForecastType";
            this.contextForecastType.Size = new System.Drawing.Size(61, 4);
            // 
            // tabViewForecast
            // 
            this.tabViewForecast.Controls.Add(this.btnPrintViewForecast);
            this.tabViewForecast.Controls.Add(this.btnForecastType2);
            this.tabViewForecast.Controls.Add(this.gridForecastView);
            this.tabViewForecast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabViewForecast.Location = new System.Drawing.Point(4, 33);
            this.tabViewForecast.Name = "tabViewForecast";
            this.tabViewForecast.Padding = new System.Windows.Forms.Padding(3);
            this.tabViewForecast.Size = new System.Drawing.Size(894, 647);
            this.tabViewForecast.TabIndex = 2;
            this.tabViewForecast.Text = "Заявки";
            this.tabViewForecast.UseVisualStyleBackColor = true;
            // 
            // gridForecastView
            // 
            this.gridForecastView.AllowUserToAddRows = false;
            this.gridForecastView.AllowUserToDeleteRows = false;
            this.gridForecastView.AllowUserToResizeRows = false;
            this.gridForecastView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridForecastView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridForecastView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridForecastView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridForecastView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridForecastView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridForecastView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridForecastView.ContextMenuStrip = this.contextMenuReport;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridForecastView.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridForecastView.Location = new System.Drawing.Point(3, 34);
            this.gridForecastView.MultiSelect = false;
            this.gridForecastView.Name = "gridForecastView";
            this.gridForecastView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridForecastView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridForecastView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridForecastView.Size = new System.Drawing.Size(888, 613);
            this.gridForecastView.TabIndex = 0;
            this.gridForecastView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridForecastView_CellContentDoubleClick);
            this.gridForecastView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridForecastView_CellFormatting);
            // 
            // btnForecastType2
            // 
            this.btnForecastType2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnForecastType2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForecastType2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnForecastType2.Location = new System.Drawing.Point(863, 6);
            this.btnForecastType2.Name = "btnForecastType2";
            this.btnForecastType2.Size = new System.Drawing.Size(23, 23);
            this.btnForecastType2.TabIndex = 1;
            this.btnForecastType2.Text = ">";
            this.btnForecastType2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnForecastType2.UseCompatibleTextRendering = true;
            this.btnForecastType2.UseVisualStyleBackColor = true;
            this.btnForecastType2.Click += new System.EventHandler(this.btnForecastType2_Click);
            // 
            // btnPrintViewForecast
            // 
            this.btnPrintViewForecast.Location = new System.Drawing.Point(8, 6);
            this.btnPrintViewForecast.Name = "btnPrintViewForecast";
            this.btnPrintViewForecast.Size = new System.Drawing.Size(75, 23);
            this.btnPrintViewForecast.TabIndex = 2;
            this.btnPrintViewForecast.Text = "Печать";
            this.btnPrintViewForecast.UseVisualStyleBackColor = true;
            this.btnPrintViewForecast.Click += new System.EventHandler(this.btnPrintViewForecast_Click);
            // 
            // tabPlan
            // 
            this.tabPlan.Controls.Add(this.btnDocType);
            this.tabPlan.Controls.Add(this.btnEditPlan);
            this.tabPlan.Controls.Add(this.btnCreatePlan);
            this.tabPlan.Controls.Add(this.gridPlanDocuments);
            this.tabPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPlan.Location = new System.Drawing.Point(4, 33);
            this.tabPlan.Name = "tabPlan";
            this.tabPlan.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlan.Size = new System.Drawing.Size(894, 647);
            this.tabPlan.TabIndex = 1;
            this.tabPlan.Text = "Производство";
            this.tabPlan.UseVisualStyleBackColor = true;
            // 
            // gridPlanDocuments
            // 
            this.gridPlanDocuments.AllowUserToAddRows = false;
            this.gridPlanDocuments.AllowUserToDeleteRows = false;
            this.gridPlanDocuments.AllowUserToResizeRows = false;
            this.gridPlanDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPlanDocuments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPlanDocuments.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridPlanDocuments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridPlanDocuments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPlanDocuments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridPlanDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPlanDocuments.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridPlanDocuments.Location = new System.Drawing.Point(3, 34);
            this.gridPlanDocuments.MultiSelect = false;
            this.gridPlanDocuments.Name = "gridPlanDocuments";
            this.gridPlanDocuments.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPlanDocuments.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridPlanDocuments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPlanDocuments.Size = new System.Drawing.Size(888, 613);
            this.gridPlanDocuments.TabIndex = 0;
            this.gridPlanDocuments.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPlanDocuments_CellDoubleClick);
            this.gridPlanDocuments.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridPlanDocuments_CellFormatting);
            // 
            // btnCreatePlan
            // 
            this.btnCreatePlan.Location = new System.Drawing.Point(8, 6);
            this.btnCreatePlan.Name = "btnCreatePlan";
            this.btnCreatePlan.Size = new System.Drawing.Size(87, 23);
            this.btnCreatePlan.TabIndex = 1;
            this.btnCreatePlan.Text = "Создать";
            this.btnCreatePlan.UseVisualStyleBackColor = true;
            this.btnCreatePlan.Click += new System.EventHandler(this.btnCreatePlan_Click);
            // 
            // btnEditPlan
            // 
            this.btnEditPlan.Location = new System.Drawing.Point(101, 6);
            this.btnEditPlan.Name = "btnEditPlan";
            this.btnEditPlan.Size = new System.Drawing.Size(103, 23);
            this.btnEditPlan.TabIndex = 2;
            this.btnEditPlan.Text = "Редактировать";
            this.btnEditPlan.UseVisualStyleBackColor = true;
            this.btnEditPlan.Click += new System.EventHandler(this.btnEditPlan_Click);
            // 
            // btnDocType
            // 
            this.btnDocType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDocType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDocType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDocType.Location = new System.Drawing.Point(863, 6);
            this.btnDocType.Name = "btnDocType";
            this.btnDocType.Size = new System.Drawing.Size(23, 23);
            this.btnDocType.TabIndex = 3;
            this.btnDocType.Text = ">";
            this.btnDocType.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDocType.UseCompatibleTextRendering = true;
            this.btnDocType.UseVisualStyleBackColor = true;
            this.btnDocType.Click += new System.EventHandler(this.btnDocType_Click);
            // 
            // tabForecast
            // 
            this.tabForecast.Controls.Add(this.btnDeleteTender);
            this.tabForecast.Controls.Add(this.btnForecastType);
            this.tabForecast.Controls.Add(this.btnPrint);
            this.tabForecast.Controls.Add(this.btnEditTender);
            this.tabForecast.Controls.Add(this.gridForecastDocuments);
            this.tabForecast.Controls.Add(this.btnTender);
            this.tabForecast.Controls.Add(this.btnCreateForecast);
            this.tabForecast.Controls.Add(this.label1);
            this.tabForecast.Controls.Add(this.dtpForecastDate);
            this.tabForecast.Controls.Add(this.label2);
            this.tabForecast.Controls.Add(this.cmbFactory);
            this.tabForecast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabForecast.Location = new System.Drawing.Point(4, 33);
            this.tabForecast.Name = "tabForecast";
            this.tabForecast.Padding = new System.Windows.Forms.Padding(3);
            this.tabForecast.Size = new System.Drawing.Size(894, 647);
            this.tabForecast.TabIndex = 0;
            this.tabForecast.Text = "Прогнозирование";
            this.tabForecast.UseVisualStyleBackColor = true;
            // 
            // cmbFactory
            // 
            this.cmbFactory.FormattingEnabled = true;
            this.cmbFactory.Location = new System.Drawing.Point(174, 8);
            this.cmbFactory.Name = "cmbFactory";
            this.cmbFactory.Size = new System.Drawing.Size(194, 21);
            this.cmbFactory.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(374, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Дата:";
            // 
            // dtpForecastDate
            // 
            this.dtpForecastDate.Location = new System.Drawing.Point(416, 7);
            this.dtpForecastDate.Name = "dtpForecastDate";
            this.dtpForecastDate.Size = new System.Drawing.Size(143, 20);
            this.dtpForecastDate.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Фабрика:";
            // 
            // btnCreateForecast
            // 
            this.btnCreateForecast.Location = new System.Drawing.Point(8, 6);
            this.btnCreateForecast.Name = "btnCreateForecast";
            this.btnCreateForecast.Size = new System.Drawing.Size(87, 23);
            this.btnCreateForecast.TabIndex = 0;
            this.btnCreateForecast.Text = "&Прогноз";
            this.btnCreateForecast.UseVisualStyleBackColor = true;
            this.btnCreateForecast.Click += new System.EventHandler(this.btnCreateForecast_Click);
            // 
            // btnTender
            // 
            this.btnTender.Location = new System.Drawing.Point(8, 35);
            this.btnTender.Name = "btnTender";
            this.btnTender.Size = new System.Drawing.Size(87, 23);
            this.btnTender.TabIndex = 6;
            this.btnTender.Text = "&Новая Заявка";
            this.btnTender.UseVisualStyleBackColor = true;
            this.btnTender.Click += new System.EventHandler(this.btnTender_Click);
            // 
            // gridForecastDocuments
            // 
            this.gridForecastDocuments.AllowUserToAddRows = false;
            this.gridForecastDocuments.AllowUserToDeleteRows = false;
            this.gridForecastDocuments.AllowUserToResizeRows = false;
            this.gridForecastDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridForecastDocuments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridForecastDocuments.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridForecastDocuments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridForecastDocuments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridForecastDocuments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gridForecastDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridForecastDocuments.ContextMenuStrip = this.contextMenuEdit;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridForecastDocuments.DefaultCellStyle = dataGridViewCellStyle8;
            this.gridForecastDocuments.Location = new System.Drawing.Point(3, 64);
            this.gridForecastDocuments.MultiSelect = false;
            this.gridForecastDocuments.Name = "gridForecastDocuments";
            this.gridForecastDocuments.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridForecastDocuments.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.gridForecastDocuments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridForecastDocuments.Size = new System.Drawing.Size(888, 580);
            this.gridForecastDocuments.TabIndex = 0;
            this.gridForecastDocuments.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridForecastDocuments_CellDoubleClick);
            this.gridForecastDocuments.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridForecastDocuments_CellFormatting);
            // 
            // btnEditTender
            // 
            this.btnEditTender.Location = new System.Drawing.Point(106, 35);
            this.btnEditTender.Name = "btnEditTender";
            this.btnEditTender.Size = new System.Drawing.Size(111, 23);
            this.btnEditTender.TabIndex = 14;
            this.btnEditTender.Text = "&Редактировать";
            this.btnEditTender.UseVisualStyleBackColor = true;
            this.btnEditTender.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(223, 35);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(101, 23);
            this.btnPrint.TabIndex = 15;
            this.btnPrint.Text = "Печать";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnForecastType
            // 
            this.btnForecastType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnForecastType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForecastType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnForecastType.Location = new System.Drawing.Point(863, 6);
            this.btnForecastType.Name = "btnForecastType";
            this.btnForecastType.Size = new System.Drawing.Size(23, 23);
            this.btnForecastType.TabIndex = 16;
            this.btnForecastType.Text = ">";
            this.btnForecastType.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnForecastType.UseCompatibleTextRendering = true;
            this.btnForecastType.UseVisualStyleBackColor = true;
            this.btnForecastType.Click += new System.EventHandler(this.btnForecastType_Click);
            // 
            // btnDeleteTender
            // 
            this.btnDeleteTender.Location = new System.Drawing.Point(816, 35);
            this.btnDeleteTender.Name = "btnDeleteTender";
            this.btnDeleteTender.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteTender.TabIndex = 17;
            this.btnDeleteTender.Text = "Удалить";
            this.btnDeleteTender.UseVisualStyleBackColor = true;
            this.btnDeleteTender.Click += new System.EventHandler(this.btnDeleteTender_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabForecast);
            this.tabControl.Controls.Add(this.tabPlan);
            this.tabControl.Controls.Add(this.tabViewForecast);
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl.Location = new System.Drawing.Point(0, 29);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(902, 684);
            this.tabControl.TabIndex = 0;
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 713);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.btnSearch);
            this.MinimumSize = new System.Drawing.Size(910, 740);
            this.Name = "MainFrame";
            this.Text = "ПППЭ";
            this.Load += new System.EventHandler(this.MainFrame_Load);
            this.contextMenuEdit.ResumeLayout(false);
            this.contextMenuReport.ResumeLayout(false);
            this.tabViewForecast.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridForecastView)).EndInit();
            this.tabPlan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPlanDocuments)).EndInit();
            this.tabForecast.ResumeLayout(false);
            this.tabForecast.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridForecastDocuments)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuEdit;
        private System.Windows.Forms.ToolStripMenuItem toolEdit;
        private System.Windows.Forms.ToolStripMenuItem toolReportCompare;
        private System.Windows.Forms.ToolStripMenuItem toolPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolTenderFactGrReport;
        private System.Windows.Forms.ToolStripMenuItem toolCreateTender;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.ContextMenuStrip contextDocType;
        private System.Windows.Forms.ContextMenuStrip contextForecastType;
        private System.Windows.Forms.ContextMenuStrip contextMenuReport;
        private System.Windows.Forms.ToolStripMenuItem toolPrintView;
        private System.Windows.Forms.ToolStripMenuItem toolReportCompare2;
        private System.Windows.Forms.ToolStripMenuItem toolReportCompareGraphic;
        private System.Windows.Forms.ToolStripMenuItem toolDelete;
        private System.Windows.Forms.TabPage tabViewForecast;
        private System.Windows.Forms.Button btnPrintViewForecast;
        private System.Windows.Forms.Button btnForecastType2;
        private System.Windows.Forms.DataGridView gridForecastView;
        private System.Windows.Forms.TabPage tabPlan;
        private System.Windows.Forms.Button btnDocType;
        private System.Windows.Forms.Button btnEditPlan;
        private System.Windows.Forms.Button btnCreatePlan;
        private System.Windows.Forms.DataGridView gridPlanDocuments;
        private System.Windows.Forms.TabPage tabForecast;
        private System.Windows.Forms.Button btnDeleteTender;
        private System.Windows.Forms.Button btnForecastType;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnEditTender;
        private System.Windows.Forms.DataGridView gridForecastDocuments;
        private System.Windows.Forms.Button btnTender;
        private System.Windows.Forms.Button btnCreateForecast;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpForecastDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFactory;
        private System.Windows.Forms.TabControl tabControl;
    }
}

