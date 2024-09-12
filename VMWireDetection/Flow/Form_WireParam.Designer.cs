namespace VMWireDetection
{
    partial class Form_WireParam
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
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.cbTopLight = new System.Windows.Forms.CheckBox();
            this.cbBottomLight = new System.Windows.Forms.CheckBox();
            this.numUD_LineNum = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.trackBarHeight = new System.Windows.Forms.TrackBar();
            this.lbKernelHeight = new System.Windows.Forms.Label();
            this.lbKernelWidth = new System.Windows.Forms.Label();
            this.numUDKernelWidth = new System.Windows.Forms.NumericUpDown();
            this.numUDKernelHeight = new System.Windows.Forms.NumericUpDown();
            this.trackBarWidth = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.numUPlineWidthMin = new System.Windows.Forms.NumericUpDown();
            this.numUPlineHeightMin = new System.Windows.Forms.NumericUpDown();
            this.numUPlineWidthMax = new System.Windows.Forms.NumericUpDown();
            this.btn_Test = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_SetRoI = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.numUPlineHeightMax = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_LineNum)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDKernelWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDKernelHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUPlineWidthMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUPlineHeightMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUPlineWidthMax)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUPlineHeightMax)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.cbTopLight, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbBottomLight, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(151, 233);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(579, 35);
            this.tableLayoutPanel4.TabIndex = 7;
            // 
            // cbTopLight
            // 
            this.cbTopLight.AutoSize = true;
            this.cbTopLight.Location = new System.Drawing.Point(293, 4);
            this.cbTopLight.Name = "cbTopLight";
            this.cbTopLight.Size = new System.Drawing.Size(93, 25);
            this.cbTopLight.TabIndex = 1;
            this.cbTopLight.Text = "正面打光";
            this.cbTopLight.UseVisualStyleBackColor = true;
            this.cbTopLight.CheckedChanged += new System.EventHandler(this.cbTopLight_CheckedChanged);
            // 
            // cbBottomLight
            // 
            this.cbBottomLight.AutoSize = true;
            this.cbBottomLight.Location = new System.Drawing.Point(4, 4);
            this.cbBottomLight.Name = "cbBottomLight";
            this.cbBottomLight.Size = new System.Drawing.Size(93, 25);
            this.cbBottomLight.TabIndex = 0;
            this.cbBottomLight.Text = "背面打光";
            this.cbBottomLight.UseVisualStyleBackColor = true;
            this.cbBottomLight.CheckedChanged += new System.EventHandler(this.cbBottomLight_CheckedChanged);
            // 
            // numUD_LineNum
            // 
            this.numUD_LineNum.Location = new System.Drawing.Point(151, 4);
            this.numUD_LineNum.Name = "numUD_LineNum";
            this.numUD_LineNum.Size = new System.Drawing.Size(120, 29);
            this.numUD_LineNum.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(4, 339);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(734, 161);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel5);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(726, 127);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "参数设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel5.Controls.Add(this.trackBarHeight, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.lbKernelHeight, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.lbKernelWidth, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.numUDKernelWidth, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.numUDKernelHeight, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.trackBarWidth, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(720, 121);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // trackBarHeight
            // 
            this.trackBarHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBarHeight.Location = new System.Drawing.Point(292, 64);
            this.trackBarHeight.Name = "trackBarHeight";
            this.trackBarHeight.Size = new System.Drawing.Size(424, 53);
            this.trackBarHeight.TabIndex = 11;
            this.trackBarHeight.Scroll += new System.EventHandler(this.trackBarHeight_Scroll);
            // 
            // lbKernelHeight
            // 
            this.lbKernelHeight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbKernelHeight.AutoSize = true;
            this.lbKernelHeight.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lbKernelHeight.Location = new System.Drawing.Point(50, 82);
            this.lbKernelHeight.Name = "lbKernelHeight";
            this.lbKernelHeight.Size = new System.Drawing.Size(44, 17);
            this.lbKernelHeight.TabIndex = 7;
            this.lbKernelHeight.Text = "核高度";
            // 
            // lbKernelWidth
            // 
            this.lbKernelWidth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbKernelWidth.AutoSize = true;
            this.lbKernelWidth.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lbKernelWidth.Location = new System.Drawing.Point(50, 22);
            this.lbKernelWidth.Name = "lbKernelWidth";
            this.lbKernelWidth.Size = new System.Drawing.Size(44, 17);
            this.lbKernelWidth.TabIndex = 6;
            this.lbKernelWidth.Text = "核宽度";
            // 
            // numUDKernelWidth
            // 
            this.numUDKernelWidth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numUDKernelWidth.Location = new System.Drawing.Point(172, 16);
            this.numUDKernelWidth.Name = "numUDKernelWidth";
            this.numUDKernelWidth.Size = new System.Drawing.Size(89, 29);
            this.numUDKernelWidth.TabIndex = 8;
            this.numUDKernelWidth.ValueChanged += new System.EventHandler(this.numUDKernelWidth_ValueChanged);
            // 
            // numUDKernelHeight
            // 
            this.numUDKernelHeight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numUDKernelHeight.Location = new System.Drawing.Point(172, 76);
            this.numUDKernelHeight.Name = "numUDKernelHeight";
            this.numUDKernelHeight.Size = new System.Drawing.Size(89, 29);
            this.numUDKernelHeight.TabIndex = 9;
            this.numUDKernelHeight.ValueChanged += new System.EventHandler(this.numUDKernelHeight_ValueChanged);
            // 
            // trackBarWidth
            // 
            this.trackBarWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBarWidth.Location = new System.Drawing.Point(292, 4);
            this.trackBarWidth.Name = "trackBarWidth";
            this.trackBarWidth.Size = new System.Drawing.Size(424, 53);
            this.trackBarWidth.TabIndex = 10;
            this.trackBarWidth.Scroll += new System.EventHandler(this.trackBarWidth_Scroll);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(349, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 17);
            this.label11.TabIndex = 11;
            this.label11.Text = "~";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(349, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 17);
            this.label10.TabIndex = 10;
            this.label10.Text = "~";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label9.Location = new System.Drawing.Point(13, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 17);
            this.label9.TabIndex = 5;
            this.label9.Text = "芯线高";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label6.Location = new System.Drawing.Point(13, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "芯线宽";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Location = new System.Drawing.Point(73, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(85, 29);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox2.Location = new System.Drawing.Point(73, 76);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(85, 29);
            this.textBox2.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label8.Location = new System.Drawing.Point(185, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "上下限";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label7.Location = new System.Drawing.Point(185, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "上下限";
            // 
            // btn_Save
            // 
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Save.Location = new System.Drawing.Point(553, 4);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(177, 43);
            this.btn_Save.TabIndex = 1;
            this.btn_Save.Text = "保存设置";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // numUPlineWidthMin
            // 
            this.numUPlineWidthMin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numUPlineWidthMin.Location = new System.Drawing.Point(257, 16);
            this.numUPlineWidthMin.Name = "numUPlineWidthMin";
            this.numUPlineWidthMin.Size = new System.Drawing.Size(85, 29);
            this.numUPlineWidthMin.TabIndex = 6;
            // 
            // numUPlineHeightMin
            // 
            this.numUPlineHeightMin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numUPlineHeightMin.Location = new System.Drawing.Point(257, 76);
            this.numUPlineHeightMin.Name = "numUPlineHeightMin";
            this.numUPlineHeightMin.Size = new System.Drawing.Size(85, 29);
            this.numUPlineHeightMin.TabIndex = 7;
            // 
            // numUPlineWidthMax
            // 
            this.numUPlineWidthMax.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numUPlineWidthMax.Location = new System.Drawing.Point(372, 16);
            this.numUPlineWidthMax.Name = "numUPlineWidthMax";
            this.numUPlineWidthMax.Size = new System.Drawing.Size(89, 29);
            this.numUPlineWidthMax.TabIndex = 8;
            // 
            // btn_Test
            // 
            this.btn_Test.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Test.Location = new System.Drawing.Point(370, 4);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(176, 43);
            this.btn_Test.TabIndex = 0;
            this.btn_Test.Text = "测试";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(37, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "芯线数量";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(37, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 22);
            this.label3.TabIndex = 1;
            this.label3.Text = "检测区域";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(37, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 22);
            this.label4.TabIndex = 2;
            this.label4.Text = "检出条件";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(37, 239);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 22);
            this.label5.TabIndex = 3;
            this.label5.Text = "打光方式";
            // 
            // btn_SetRoI
            // 
            this.btn_SetRoI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_SetRoI.Location = new System.Drawing.Point(151, 45);
            this.btn_SetRoI.Name = "btn_SetRoI";
            this.btn_SetRoI.Size = new System.Drawing.Size(189, 34);
            this.btn_SetRoI.TabIndex = 5;
            this.btn_SetRoI.Text = "设置检测区域";
            this.btn_SetRoI.UseVisualStyleBackColor = true;
            this.btn_SetRoI.Click += new System.EventHandler(this.btn_SetRoI_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.label11, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.label10, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBox2, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label8, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label7, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.numUPlineWidthMin, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.numUPlineHeightMin, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.numUPlineWidthMax, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.numUPlineHeightMax, 5, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(151, 86);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(466, 122);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // numUPlineHeightMax
            // 
            this.numUPlineHeightMax.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numUPlineHeightMax.Location = new System.Drawing.Point(372, 76);
            this.numUPlineHeightMax.Name = "numUPlineHeightMax";
            this.numUPlineHeightMax.Size = new System.Drawing.Size(89, 29);
            this.numUPlineHeightMax.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(323, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "剥皮检测";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.btn_SetRoI, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.numUD_LineNum, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 60);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(734, 272);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(742, 562);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.Controls.Add(this.btn_Test, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.btn_Save, 2, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(4, 507);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(734, 51);
            this.tableLayoutPanel6.TabIndex = 3;
            // 
            // Form_WireParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 562);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form_WireParam";
            this.Text = "Form_WireParam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_WireParam_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.Form_WireParam_VisibleChanged);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_LineNum)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDKernelWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDKernelHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUPlineWidthMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUPlineHeightMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUPlineWidthMax)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUPlineHeightMax)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.CheckBox cbTopLight;
        private System.Windows.Forms.CheckBox cbBottomLight;
        private System.Windows.Forms.NumericUpDown numUD_LineNum;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TrackBar trackBarHeight;
        private System.Windows.Forms.Label lbKernelHeight;
        private System.Windows.Forms.Label lbKernelWidth;
        private System.Windows.Forms.NumericUpDown numUDKernelWidth;
        private System.Windows.Forms.NumericUpDown numUDKernelHeight;
        private System.Windows.Forms.TrackBar trackBarWidth;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.NumericUpDown numUPlineWidthMin;
        private System.Windows.Forms.NumericUpDown numUPlineHeightMin;
        private System.Windows.Forms.NumericUpDown numUPlineWidthMax;
        private System.Windows.Forms.Button btn_Test;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_SetRoI;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.NumericUpDown numUPlineHeightMax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
    }
}