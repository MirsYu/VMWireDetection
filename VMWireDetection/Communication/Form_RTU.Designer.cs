namespace VMWireDetection
{
    partial class Form_RTU
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
            this.gbComCtrl = new System.Windows.Forms.GroupBox();
            this.cbBauderate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbComNo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOpenOrCloseCom = new System.Windows.Forms.Button();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbParityBits = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Adres1 = new System.Windows.Forms.TextBox();
            this.tb_Adres2 = new System.Windows.Forms.TextBox();
            this.tb_Adres3 = new System.Windows.Forms.TextBox();
            this.tb_Adres4 = new System.Windows.Forms.TextBox();
            this.btn_Write = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.gbComCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbComCtrl
            // 
            this.gbComCtrl.Controls.Add(this.cbBauderate);
            this.gbComCtrl.Controls.Add(this.label1);
            this.gbComCtrl.Controls.Add(this.cbComNo);
            this.gbComCtrl.Controls.Add(this.label4);
            this.gbComCtrl.Controls.Add(this.btnOpenOrCloseCom);
            this.gbComCtrl.Controls.Add(this.cbStopBits);
            this.gbComCtrl.Controls.Add(this.cbDataBits);
            this.gbComCtrl.Controls.Add(this.label5);
            this.gbComCtrl.Controls.Add(this.label3);
            this.gbComCtrl.Controls.Add(this.cbParityBits);
            this.gbComCtrl.Controls.Add(this.label2);
            this.gbComCtrl.Font = new System.Drawing.Font("SimSun-ExtB", 9F, System.Drawing.FontStyle.Bold);
            this.gbComCtrl.ForeColor = System.Drawing.Color.ForestGreen;
            this.gbComCtrl.Location = new System.Drawing.Point(12, 12);
            this.gbComCtrl.Name = "gbComCtrl";
            this.gbComCtrl.Size = new System.Drawing.Size(301, 154);
            this.gbComCtrl.TabIndex = 16;
            this.gbComCtrl.TabStop = false;
            this.gbComCtrl.Text = "串口设置";
            // 
            // cbBauderate
            // 
            this.cbBauderate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBauderate.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            this.cbBauderate.FormattingEnabled = true;
            this.cbBauderate.Location = new System.Drawing.Point(64, 45);
            this.cbBauderate.Name = "cbBauderate";
            this.cbBauderate.Size = new System.Drawing.Size(136, 20);
            this.cbBauderate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口号";
            // 
            // cbComNo
            // 
            this.cbComNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComNo.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            this.cbComNo.FormattingEnabled = true;
            this.cbComNo.Location = new System.Drawing.Point(63, 18);
            this.cbComNo.Name = "cbComNo";
            this.cbComNo.Size = new System.Drawing.Size(136, 20);
            this.cbComNo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(16, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "停止位";
            // 
            // btnOpenOrCloseCom
            // 
            this.btnOpenOrCloseCom.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            this.btnOpenOrCloseCom.ForeColor = System.Drawing.Color.Black;
            this.btnOpenOrCloseCom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOpenOrCloseCom.Location = new System.Drawing.Point(216, 18);
            this.btnOpenOrCloseCom.Name = "btnOpenOrCloseCom";
            this.btnOpenOrCloseCom.Size = new System.Drawing.Size(68, 124);
            this.btnOpenOrCloseCom.TabIndex = 2;
            this.btnOpenOrCloseCom.Text = "打开串口";
            this.btnOpenOrCloseCom.UseVisualStyleBackColor = true;
            this.btnOpenOrCloseCom.Click += new System.EventHandler(this.btnOpenOrCloseCom_Click);
            // 
            // cbStopBits
            // 
            this.cbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBits.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(64, 97);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(136, 20);
            this.cbStopBits.TabIndex = 1;
            // 
            // cbDataBits
            // 
            this.cbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBits.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Location = new System.Drawing.Point(63, 70);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(136, 20);
            this.cbDataBits.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(16, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "校验位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(16, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "数据位";
            // 
            // cbParityBits
            // 
            this.cbParityBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParityBits.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            this.cbParityBits.FormattingEnabled = true;
            this.cbParityBits.Location = new System.Drawing.Point(63, 122);
            this.cbParityBits.Name = "cbParityBits";
            this.cbParityBits.Size = new System.Drawing.Size(136, 20);
            this.cbParityBits.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(16, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "波特率";
            // 
            // tb_Adres1
            // 
            this.tb_Adres1.Location = new System.Drawing.Point(75, 206);
            this.tb_Adres1.Name = "tb_Adres1";
            this.tb_Adres1.Size = new System.Drawing.Size(100, 21);
            this.tb_Adres1.TabIndex = 17;
            // 
            // tb_Adres2
            // 
            this.tb_Adres2.Location = new System.Drawing.Point(75, 253);
            this.tb_Adres2.Name = "tb_Adres2";
            this.tb_Adres2.Size = new System.Drawing.Size(100, 21);
            this.tb_Adres2.TabIndex = 18;
            // 
            // tb_Adres3
            // 
            this.tb_Adres3.Location = new System.Drawing.Point(75, 300);
            this.tb_Adres3.Name = "tb_Adres3";
            this.tb_Adres3.Size = new System.Drawing.Size(100, 21);
            this.tb_Adres3.TabIndex = 19;
            // 
            // tb_Adres4
            // 
            this.tb_Adres4.Location = new System.Drawing.Point(75, 347);
            this.tb_Adres4.Name = "tb_Adres4";
            this.tb_Adres4.Size = new System.Drawing.Size(100, 21);
            this.tb_Adres4.TabIndex = 20;
            // 
            // btn_Write
            // 
            this.btn_Write.Location = new System.Drawing.Point(209, 204);
            this.btn_Write.Name = "btn_Write";
            this.btn_Write.Size = new System.Drawing.Size(75, 23);
            this.btn_Write.TabIndex = 21;
            this.btn_Write.Text = "写入测试";
            this.btn_Write.UseVisualStyleBackColor = true;
            this.btn_Write.Click += new System.EventHandler(this.btn_Write_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(208, 407);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 22;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // Form_RTU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 450);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_Write);
            this.Controls.Add(this.tb_Adres4);
            this.Controls.Add(this.tb_Adres3);
            this.Controls.Add(this.tb_Adres2);
            this.Controls.Add(this.tb_Adres1);
            this.Controls.Add(this.gbComCtrl);
            this.Name = "Form_RTU";
            this.Text = "Form_RTU";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_RTU_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.Form_RTU_VisibleChanged);
            this.gbComCtrl.ResumeLayout(false);
            this.gbComCtrl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbComCtrl;
        private System.Windows.Forms.ComboBox cbBauderate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbComNo;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Button btnOpenOrCloseCom;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbParityBits;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_Adres1;
        private System.Windows.Forms.TextBox tb_Adres2;
        private System.Windows.Forms.TextBox tb_Adres3;
        private System.Windows.Forms.TextBox tb_Adres4;
        private System.Windows.Forms.Button btn_Write;
        private System.Windows.Forms.Button btn_Save;
    }
}