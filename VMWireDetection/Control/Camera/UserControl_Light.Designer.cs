namespace VMWireDetection
{
    partial class UserControl_Light
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label38 = new System.Windows.Forms.Label();
            this.cbLightTriggerSource = new System.Windows.Forms.ComboBox();
            this.gbLightCtrl = new System.Windows.Forms.GroupBox();
            this.cbLightPort = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.textLightTime = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.gbLighjtValue = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbLightValue = new System.Windows.Forms.TextBox();
            this.trackBarLight = new System.Windows.Forms.TrackBar();
            this.label33 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbDownEdge = new System.Windows.Forms.RadioButton();
            this.rbRisingEdge = new System.Windows.Forms.RadioButton();
            this.gbLightState = new System.Windows.Forms.GroupBox();
            this.rdbLightStateTrigger = new System.Windows.Forms.RadioButton();
            this.rdbLightStateOff = new System.Windows.Forms.RadioButton();
            this.rdbLightStateOn = new System.Windows.Forms.RadioButton();
            this.cbComNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5.SuspendLayout();
            this.gbLightCtrl.SuspendLayout();
            this.gbLighjtValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLight)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.gbLightState.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbComNo);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.gbLightCtrl);
            this.groupBox5.Font = new System.Drawing.Font("SimSun-ExtB", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox5.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(452, 219);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "光源设置";
            // 
            // label38
            // 
            this.label38.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            this.label38.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label38.Location = new System.Drawing.Point(13, 75);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(53, 18);
            this.label38.TabIndex = 17;
            this.label38.Text = "触发输入";
            // 
            // cbLightTriggerSource
            // 
            this.cbLightTriggerSource.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            this.cbLightTriggerSource.FormattingEnabled = true;
            this.cbLightTriggerSource.Location = new System.Drawing.Point(73, 75);
            this.cbLightTriggerSource.Name = "cbLightTriggerSource";
            this.cbLightTriggerSource.Size = new System.Drawing.Size(73, 20);
            this.cbLightTriggerSource.TabIndex = 11;
            // 
            // gbLightCtrl
            // 
            this.gbLightCtrl.Controls.Add(this.label38);
            this.gbLightCtrl.Controls.Add(this.cbLightPort);
            this.gbLightCtrl.Controls.Add(this.cbLightTriggerSource);
            this.gbLightCtrl.Controls.Add(this.label31);
            this.gbLightCtrl.Controls.Add(this.textLightTime);
            this.gbLightCtrl.Controls.Add(this.label34);
            this.gbLightCtrl.Controls.Add(this.gbLighjtValue);
            this.gbLightCtrl.Controls.Add(this.label33);
            this.gbLightCtrl.Controls.Add(this.groupBox3);
            this.gbLightCtrl.Controls.Add(this.gbLightState);
            this.gbLightCtrl.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            this.gbLightCtrl.Location = new System.Drawing.Point(0, 37);
            this.gbLightCtrl.Name = "gbLightCtrl";
            this.gbLightCtrl.Size = new System.Drawing.Size(438, 176);
            this.gbLightCtrl.TabIndex = 10;
            this.gbLightCtrl.TabStop = false;
            // 
            // cbLightPort
            // 
            this.cbLightPort.FormattingEnabled = true;
            this.cbLightPort.Location = new System.Drawing.Point(72, 9);
            this.cbLightPort.Name = "cbLightPort";
            this.cbLightPort.Size = new System.Drawing.Size(74, 20);
            this.cbLightPort.TabIndex = 15;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label31.Location = new System.Drawing.Point(13, 13);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(29, 12);
            this.label31.TabIndex = 14;
            this.label31.Text = "通道";
            // 
            // textLightTime
            // 
            this.textLightTime.Location = new System.Drawing.Point(72, 39);
            this.textLightTime.Name = "textLightTime";
            this.textLightTime.Size = new System.Drawing.Size(74, 19);
            this.textLightTime.TabIndex = 13;
            this.textLightTime.Text = "0";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ForeColor = System.Drawing.Color.Black;
            this.label34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label34.Location = new System.Drawing.Point(147, 42);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(17, 12);
            this.label34.TabIndex = 12;
            this.label34.Text = "ms";
            // 
            // gbLighjtValue
            // 
            this.gbLighjtValue.Controls.Add(this.label12);
            this.gbLighjtValue.Controls.Add(this.tbLightValue);
            this.gbLighjtValue.Controls.Add(this.trackBarLight);
            this.gbLighjtValue.Location = new System.Drawing.Point(13, 101);
            this.gbLighjtValue.Name = "gbLighjtValue";
            this.gbLighjtValue.Size = new System.Drawing.Size(182, 66);
            this.gbLighjtValue.TabIndex = 7;
            this.gbLighjtValue.TabStop = false;
            this.gbLighjtValue.Text = "光源亮度";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(143, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 12);
            this.label12.TabIndex = 7;
            this.label12.Text = "100";
            // 
            // tbLightValue
            // 
            this.tbLightValue.Location = new System.Drawing.Point(21, 32);
            this.tbLightValue.Name = "tbLightValue";
            this.tbLightValue.Size = new System.Drawing.Size(126, 19);
            this.tbLightValue.TabIndex = 3;
            this.tbLightValue.Text = "0";
            // 
            // trackBarLight
            // 
            this.trackBarLight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.trackBarLight.Location = new System.Drawing.Point(12, 14);
            this.trackBarLight.Maximum = 100;
            this.trackBarLight.Name = "trackBarLight";
            this.trackBarLight.Size = new System.Drawing.Size(135, 45);
            this.trackBarLight.TabIndex = 6;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ForeColor = System.Drawing.Color.Black;
            this.label33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label33.Location = new System.Drawing.Point(11, 45);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(53, 12);
            this.label33.TabIndex = 10;
            this.label33.Text = "持续时间";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbDownEdge);
            this.groupBox3.Controls.Add(this.rbRisingEdge);
            this.groupBox3.Location = new System.Drawing.Point(237, 94);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(158, 66);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "触发沿";
            // 
            // rbDownEdge
            // 
            this.rbDownEdge.ForeColor = System.Drawing.Color.Black;
            this.rbDownEdge.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbDownEdge.Location = new System.Drawing.Point(87, 22);
            this.rbDownEdge.Name = "rbDownEdge";
            this.rbDownEdge.Size = new System.Drawing.Size(65, 31);
            this.rbDownEdge.TabIndex = 1;
            this.rbDownEdge.TabStop = true;
            this.rbDownEdge.Text = "下降沿";
            this.rbDownEdge.UseVisualStyleBackColor = true;
            // 
            // rbRisingEdge
            // 
            this.rbRisingEdge.ForeColor = System.Drawing.Color.Black;
            this.rbRisingEdge.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbRisingEdge.Location = new System.Drawing.Point(14, 22);
            this.rbRisingEdge.Name = "rbRisingEdge";
            this.rbRisingEdge.Size = new System.Drawing.Size(59, 29);
            this.rbRisingEdge.TabIndex = 0;
            this.rbRisingEdge.TabStop = true;
            this.rbRisingEdge.Text = "上升沿";
            this.rbRisingEdge.UseVisualStyleBackColor = true;
            // 
            // gbLightState
            // 
            this.gbLightState.Controls.Add(this.rdbLightStateTrigger);
            this.gbLightState.Controls.Add(this.rdbLightStateOff);
            this.gbLightState.Controls.Add(this.rdbLightStateOn);
            this.gbLightState.Location = new System.Drawing.Point(238, 13);
            this.gbLightState.Name = "gbLightState";
            this.gbLightState.Size = new System.Drawing.Size(176, 66);
            this.gbLightState.TabIndex = 1;
            this.gbLightState.TabStop = false;
            this.gbLightState.Text = "状态";
            // 
            // rdbLightStateTrigger
            // 
            this.rdbLightStateTrigger.AutoSize = true;
            this.rdbLightStateTrigger.ForeColor = System.Drawing.Color.Black;
            this.rdbLightStateTrigger.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdbLightStateTrigger.Location = new System.Drawing.Point(110, 27);
            this.rdbLightStateTrigger.Name = "rdbLightStateTrigger";
            this.rdbLightStateTrigger.Size = new System.Drawing.Size(47, 16);
            this.rdbLightStateTrigger.TabIndex = 1;
            this.rdbLightStateTrigger.Text = "触发";
            this.rdbLightStateTrigger.UseVisualStyleBackColor = true;
            // 
            // rdbLightStateOff
            // 
            this.rdbLightStateOff.AutoSize = true;
            this.rdbLightStateOff.Checked = true;
            this.rdbLightStateOff.ForeColor = System.Drawing.Color.Black;
            this.rdbLightStateOff.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdbLightStateOff.Location = new System.Drawing.Point(60, 27);
            this.rdbLightStateOff.Name = "rdbLightStateOff";
            this.rdbLightStateOff.Size = new System.Drawing.Size(47, 16);
            this.rdbLightStateOff.TabIndex = 0;
            this.rdbLightStateOff.TabStop = true;
            this.rdbLightStateOff.Text = "常灭";
            this.rdbLightStateOff.UseVisualStyleBackColor = true;
            // 
            // rdbLightStateOn
            // 
            this.rdbLightStateOn.AutoSize = true;
            this.rdbLightStateOn.ForeColor = System.Drawing.Color.Black;
            this.rdbLightStateOn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdbLightStateOn.Location = new System.Drawing.Point(6, 27);
            this.rdbLightStateOn.Name = "rdbLightStateOn";
            this.rdbLightStateOn.Size = new System.Drawing.Size(47, 16);
            this.rdbLightStateOn.TabIndex = 0;
            this.rdbLightStateOn.Text = "常亮";
            this.rdbLightStateOn.UseVisualStyleBackColor = true;
            // 
            // cbComNo
            // 
            this.cbComNo.FormattingEnabled = true;
            this.cbComNo.Location = new System.Drawing.Point(73, 18);
            this.cbComNo.Name = "cbComNo";
            this.cbComNo.Size = new System.Drawing.Size(74, 20);
            this.cbComNo.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "串口号：";
            // 
            // UserControl_Light
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox5);
            this.Name = "UserControl_Light";
            this.Size = new System.Drawing.Size(464, 225);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.gbLightCtrl.ResumeLayout(false);
            this.gbLightCtrl.PerformLayout();
            this.gbLighjtValue.ResumeLayout(false);
            this.gbLighjtValue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLight)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.gbLightState.ResumeLayout(false);
            this.gbLightState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.ComboBox cbLightTriggerSource;
        private System.Windows.Forms.GroupBox gbLightCtrl;
        private System.Windows.Forms.ComboBox cbLightPort;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox textLightTime;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.GroupBox gbLighjtValue;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbLightValue;
        private System.Windows.Forms.TrackBar trackBarLight;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbDownEdge;
        private System.Windows.Forms.RadioButton rbRisingEdge;
        private System.Windows.Forms.GroupBox gbLightState;
        private System.Windows.Forms.RadioButton rdbLightStateTrigger;
        private System.Windows.Forms.RadioButton rdbLightStateOff;
        private System.Windows.Forms.RadioButton rdbLightStateOn;
        private System.Windows.Forms.ComboBox cbComNo;
        private System.Windows.Forms.Label label1;
    }
}
