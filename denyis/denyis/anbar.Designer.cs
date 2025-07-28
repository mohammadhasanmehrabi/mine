namespace denyis
{
    partial class anbar
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
            this.panelAnbar = new System.Windows.Forms.Panel();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdite = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMinStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSupplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPurchaseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStockStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelToothColor = new System.Windows.Forms.Label();
            this.cmbStockStatus = new System.Windows.Forms.ComboBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cmbTeethColor = new System.Windows.Forms.ComboBox();
            this.txtSku = new System.Windows.Forms.TextBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalPrice = new System.Windows.Forms.TextBox();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.txtMinStock = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSupplierContact = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblLowStockItems = new System.Windows.Forms.Label();
            this.lblDentalItems = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblTotalProducts = new System.Windows.Forms.Label();
            this.panelAnbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAnbar
            // 
            this.panelAnbar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelAnbar.Controls.Add(this.btnImport);
            this.panelAnbar.Controls.Add(this.btnExport);
            this.panelAnbar.Controls.Add(this.panel1);
            this.panelAnbar.Controls.Add(this.btnDelete);
            this.panelAnbar.Controls.Add(this.btnEdite);
            this.panelAnbar.Controls.Add(this.btnSave);
            this.panelAnbar.Controls.Add(this.btnClear);
            this.panelAnbar.Controls.Add(this.label2);
            this.panelAnbar.Controls.Add(this.btnSearch);
            this.panelAnbar.Controls.Add(this.label1);
            this.panelAnbar.Controls.Add(this.txtSearch);
            this.panelAnbar.Location = new System.Drawing.Point(0, 1);
            this.panelAnbar.Name = "panelAnbar";
            this.panelAnbar.Size = new System.Drawing.Size(1537, 116);
            this.panelAnbar.TabIndex = 0;
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnImport.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnImport.Location = new System.Drawing.Point(75, 33);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(148, 40);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "📥 ورودی اکسل";
            this.btnImport.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Lime;
            this.btnExport.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnExport.Location = new System.Drawing.Point(229, 33);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(151, 40);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "📊 خروجی اکسل";
            this.btnExport.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 117);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1537, 684);
            this.panel1.TabIndex = 1;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDelete.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnDelete.Location = new System.Drawing.Point(386, 33);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(127, 40);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "🗑حذف";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnEdite
            // 
            this.btnEdite.BackColor = System.Drawing.Color.Yellow;
            this.btnEdite.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnEdite.Location = new System.Drawing.Point(513, 33);
            this.btnEdite.Name = "btnEdite";
            this.btnEdite.Size = new System.Drawing.Size(127, 40);
            this.btnEdite.TabIndex = 5;
            this.btnEdite.Text = "✏️ویرایش";
            this.btnEdite.UseVisualStyleBackColor = false;
            this.btnEdite.Click += new System.EventHandler(this.BtnEdite_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Lime;
            this.btnSave.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSave.Location = new System.Drawing.Point(640, 33);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(127, 40);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "💾ذخیره کرد ";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnClear.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnClear.Location = new System.Drawing.Point(773, 30);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 40);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "🧹";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("B Nazanin", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(1091, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(234, 49);
            this.label2.TabIndex = 1;
            this.label2.Text = "||| جستجو در انبار:";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSearch.Location = new System.Drawing.Point(849, 30);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 40);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "🔍";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("B Nazanin", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(1316, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 49);
            this.label1.TabIndex = 0;
            this.label1.Text = "بخش انبار داری ";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(925, 30);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(160, 37);
            this.txtSearch.TabIndex = 1;
            // 
            // dgvInventory
            // 
            this.dgvInventory.AllowUserToAddRows = false;
            this.dgvInventory.AllowUserToDeleteRows = false;
            this.dgvInventory.AllowUserToResizeRows = false;
            this.dgvInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInventory.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvInventory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvInventory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvInventory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colProductName,
            this.colSku,
            this.colCategory,
            this.colQuantity,
            this.colMinStock,
            this.colUnitPrice,
            this.colSupplier,
            this.colPurchaseDate,
            this.colStockStatus,
            this.colNotes});
            this.dgvInventory.GridColor = System.Drawing.Color.LightGray;
            this.dgvInventory.Location = new System.Drawing.Point(0, 118);
            this.dgvInventory.MultiSelect = false;
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.ReadOnly = true;
            this.dgvInventory.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvInventory.RowHeadersVisible = false;
            this.dgvInventory.RowHeadersWidth = 51;
            this.dgvInventory.RowTemplate.Height = 35;
            this.dgvInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInventory.Size = new System.Drawing.Size(1538, 287);
            this.dgvInventory.TabIndex = 20;
            this.dgvInventory.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvInventory_CellClick);
            // 
            // colId
            // 
            this.colId.HeaderText = "شناسه";
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            // 
            // colProductName
            // 
            this.colProductName.HeaderText = "نام محصول";
            this.colProductName.MinimumWidth = 6;
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            // 
            // colSku
            // 
            this.colSku.HeaderText = "کد محصول";
            this.colSku.MinimumWidth = 6;
            this.colSku.Name = "colSku";
            this.colSku.ReadOnly = true;
            // 
            // colCategory
            // 
            this.colCategory.HeaderText = "دسته‌بندی";
            this.colCategory.MinimumWidth = 6;
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            // 
            // colQuantity
            // 
            this.colQuantity.HeaderText = "تعداد";
            this.colQuantity.MinimumWidth = 6;
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            // 
            // colMinStock
            // 
            this.colMinStock.HeaderText = "حداقل";
            this.colMinStock.MinimumWidth = 6;
            this.colMinStock.Name = "colMinStock";
            this.colMinStock.ReadOnly = true;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.HeaderText = "قیمت واحد";
            this.colUnitPrice.MinimumWidth = 6;
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.ReadOnly = true;
            // 
            // colSupplier
            // 
            this.colSupplier.HeaderText = "تأمین‌کننده";
            this.colSupplier.MinimumWidth = 6;
            this.colSupplier.Name = "colSupplier";
            this.colSupplier.ReadOnly = true;
            // 
            // colPurchaseDate
            // 
            this.colPurchaseDate.HeaderText = "تاریخ خرید";
            this.colPurchaseDate.MinimumWidth = 6;
            this.colPurchaseDate.Name = "colPurchaseDate";
            this.colPurchaseDate.ReadOnly = true;
            // 
            // colStockStatus
            // 
            this.colStockStatus.HeaderText = "وضعیت";
            this.colStockStatus.MinimumWidth = 6;
            this.colStockStatus.Name = "colStockStatus";
            this.colStockStatus.ReadOnly = true;
            // 
            // colNotes
            // 
            this.colNotes.HeaderText = "توضیحات";
            this.colNotes.MinimumWidth = 6;
            this.colNotes.Name = "colNotes";
            this.colNotes.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.labelToothColor);
            this.groupBox1.Controls.Add(this.cmbStockStatus);
            this.groupBox1.Controls.Add(this.cmbCategory);
            this.groupBox1.Controls.Add(this.cmbTeethColor);
            this.groupBox1.Controls.Add(this.txtSku);
            this.groupBox1.Controls.Add(this.txtProductName);
            this.groupBox1.Location = new System.Drawing.Point(0, 401);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 261);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "📦  اطلاعات محصول";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(172, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 29);
            this.label6.TabIndex = 13;
            this.label6.Text = "وضعیت موجودی:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(188, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 29);
            this.label5.TabIndex = 12;
            this.label5.Text = "دسته‌بندی:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(181, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 29);
            this.label4.TabIndex = 11;
            this.label4.Text = "کد محصول :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 29);
            this.label3.TabIndex = 10;
            this.label3.Text = "نام محصول:";
            // 
            // labelToothColor
            // 
            this.labelToothColor.AutoSize = true;
            this.labelToothColor.Location = new System.Drawing.Point(188, 130);
            this.labelToothColor.Name = "labelToothColor";
            this.labelToothColor.Size = new System.Drawing.Size(91, 29);
            this.labelToothColor.TabIndex = 14;
            this.labelToothColor.Text = "رنگ دندان:";
            // 
            // cmbStockStatus
            // 
            this.cmbStockStatus.FormattingEnabled = true;
            this.cmbStockStatus.Location = new System.Drawing.Point(7, 208);
            this.cmbStockStatus.Name = "cmbStockStatus";
            this.cmbStockStatus.Size = new System.Drawing.Size(160, 37);
            this.cmbStockStatus.TabIndex = 9;
            // 
            // cmbCategory
            // 
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(7, 165);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(160, 37);
            this.cmbCategory.TabIndex = 3;
            // 
            // cmbTeethColor
            // 
            this.cmbTeethColor.FormattingEnabled = true;
            this.cmbTeethColor.Location = new System.Drawing.Point(7, 122);
            this.cmbTeethColor.Name = "cmbTeethColor";
            this.cmbTeethColor.Size = new System.Drawing.Size(160, 37);
            this.cmbTeethColor.TabIndex = 14;
            // 
            // txtSku
            // 
            this.txtSku.Location = new System.Drawing.Point(7, 81);
            this.txtSku.Name = "txtSku";
            this.txtSku.Size = new System.Drawing.Size(160, 37);
            this.txtSku.TabIndex = 8;
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(6, 36);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(160, 37);
            this.txtProductName.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtTotalPrice);
            this.groupBox2.Controls.Add(this.txtUnitPrice);
            this.groupBox2.Controls.Add(this.txtMinStock);
            this.groupBox2.Controls.Add(this.txtQuantity);
            this.groupBox2.Location = new System.Drawing.Point(315, 401);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(337, 222);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "💰  اطلاعات مالی";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(206, 168);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 29);
            this.label10.TabIndex = 20;
            this.label10.Text = "قیمت کل:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(206, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 29);
            this.label9.TabIndex = 19;
            this.label9.Text = "قیمت هر واحد:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(206, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 29);
            this.label8.TabIndex = 18;
            this.label8.Text = "حداقل موجودی:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(206, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 29);
            this.label7.TabIndex = 14;
            this.label7.Text = "تعداد موجودی:";
            // 
            // txtTotalPrice
            // 
            this.txtTotalPrice.Location = new System.Drawing.Point(6, 165);
            this.txtTotalPrice.Name = "txtTotalPrice";
            this.txtTotalPrice.Size = new System.Drawing.Size(160, 37);
            this.txtTotalPrice.TabIndex = 17;
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Location = new System.Drawing.Point(6, 125);
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.Size = new System.Drawing.Size(160, 37);
            this.txtUnitPrice.TabIndex = 16;
            // 
            // txtMinStock
            // 
            this.txtMinStock.Location = new System.Drawing.Point(6, 79);
            this.txtMinStock.Name = "txtMinStock";
            this.txtMinStock.Size = new System.Drawing.Size(160, 37);
            this.txtMinStock.TabIndex = 15;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(6, 36);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(160, 37);
            this.txtQuantity.TabIndex = 14;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.dateTimePicker1);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtSupplierContact);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtSupplier);
            this.groupBox3.Location = new System.Drawing.Point(652, 401);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(492, 197);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "🏢  اطلاعات تأمین‌کننده";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(380, 133);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 29);
            this.label13.TabIndex = 25;
            this.label13.Text = "تاریخ خرید:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(8, 127);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(366, 37);
            this.dateTimePicker1.TabIndex = 24;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(279, 84);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(207, 29);
            this.label12.TabIndex = 23;
            this.label12.Text = " اطلاعات تماس تأمین‌کننده:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSupplierContact
            // 
            this.txtSupplierContact.Location = new System.Drawing.Point(102, 76);
            this.txtSupplierContact.Name = "txtSupplierContact";
            this.txtSupplierContact.Size = new System.Drawing.Size(160, 37);
            this.txtSupplierContact.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(336, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 29);
            this.label11.TabIndex = 21;
            this.label11.Text = "نام تأمین‌کننده:";
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(102, 33);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(160, 37);
            this.txtSupplier.TabIndex = 21;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblNotes);
            this.groupBox4.Controls.Add(this.txtNotes);
            this.groupBox4.Location = new System.Drawing.Point(1137, 401);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(401, 379);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "📝 توضیحات";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(147, 22);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(130, 29);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "توضیحات اضافی:";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(0, 110);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(388, 263);
            this.txtNotes.TabIndex = 26;
            // 
            // statusBar
            // 
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Location = new System.Drawing.Point(0, 783);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1538, 22);
            this.statusBar.TabIndex = 6;
            this.statusBar.Text = "statusStrip1";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblLowStockItems);
            this.groupBox5.Controls.Add(this.lblDentalItems);
            this.groupBox5.Controls.Add(this.lblTotalValue);
            this.groupBox5.Controls.Add(this.lblTotalProducts);
            this.groupBox5.Location = new System.Drawing.Point(321, 623);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(810, 151);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "امار انبار ";
            // 
            // lblLowStockItems
            // 
            this.lblLowStockItems.AutoSize = true;
            this.lblLowStockItems.Location = new System.Drawing.Point(508, 119);
            this.lblLowStockItems.Name = "lblLowStockItems";
            this.lblLowStockItems.Size = new System.Drawing.Size(295, 29);
            this.lblLowStockItems.TabIndex = 29;
            this.lblLowStockItems.Text = "چه محصولاتی به حداقل در انبار رسیدن=";
            // 
            // lblDentalItems
            // 
            this.lblDentalItems.AutoSize = true;
            this.lblDentalItems.Location = new System.Drawing.Point(237, 106);
            this.lblDentalItems.Name = "lblDentalItems";
            this.lblDentalItems.Size = new System.Drawing.Size(139, 29);
            this.lblDentalItems.TabIndex = 28;
            this.lblDentalItems.Text = "تعداد دندان ها =0";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Location = new System.Drawing.Point(248, 33);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(128, 29);
            this.lblTotalValue.TabIndex = 27;
            this.lblTotalValue.Text = "ارزش کل انبار=0";
            // 
            // lblTotalProducts
            // 
            this.lblTotalProducts.AutoSize = true;
            this.lblTotalProducts.Location = new System.Drawing.Point(636, 33);
            this.lblTotalProducts.Name = "lblTotalProducts";
            this.lblTotalProducts.Size = new System.Drawing.Size(167, 29);
            this.lblTotalProducts.TabIndex = 26;
            this.lblTotalProducts.Text = "تعداد کل محصولات=0";
            // 
            // anbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.ClientSize = new System.Drawing.Size(1538, 805);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.dgvInventory);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelAnbar);
            this.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "anbar";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "بخش انبار داری ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelAnbar.ResumeLayout(false);
            this.panelAnbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelAnbar;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdite;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvInventory;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSku;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMinStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPurchaseDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNotes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelToothColor;
        private System.Windows.Forms.ComboBox cmbStockStatus;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.ComboBox cmbTeethColor;
        private System.Windows.Forms.TextBox txtSku;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTotalPrice;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.TextBox txtMinStock;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSupplierContact;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblLowStockItems;
        private System.Windows.Forms.Label lblDentalItems;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblTotalProducts;
    }
}