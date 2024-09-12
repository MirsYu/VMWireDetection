namespace VMWireDetection
{
    partial class UserCameraControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lb_TriggerSource = new System.Windows.Forms.Label();
            this.cb_TriggerSource = new System.Windows.Forms.ComboBox();
            this.bnContinuesMode = new System.Windows.Forms.RadioButton();
            this.bnTriggerMode = new System.Windows.Forms.RadioButton();
            this.lb_TriggerModel = new System.Windows.Forms.Label();
            this.tb_CamExposureTime = new System.Windows.Forms.TextBox();
            this.lb_CamExposureTime = new System.Windows.Forms.Label();
            this.lb_CamSN = new System.Windows.Forms.Label();
            this.cbDeviceList = new System.Windows.Forms.ComboBox();
            this.tb_CameraSN = new System.Windows.Forms.TextBox();
            this.lab_CameraName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lb_TriggerSource);
            this.groupBox1.Controls.Add(this.cb_TriggerSource);
            this.groupBox1.Controls.Add(this.bnContinuesMode);
            this.groupBox1.Controls.Add(this.bnTriggerMode);
            this.groupBox1.Controls.Add(this.lb_TriggerModel);
            this.groupBox1.Controls.Add(this.tb_CamExposureTime);
            this.groupBox1.Controls.Add(this.lb_CamExposureTime);
            this.groupBox1.Controls.Add(this.lb_CamSN);
            this.groupBox1.Controls.Add(this.cbDeviceList);
            this.groupBox1.Controls.Add(this.tb_CameraSN);
            this.groupBox1.Controls.Add(this.lab_CameraName);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 278);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lb_TriggerSource
            // 
            this.lb_TriggerSource.AutoSize = true;
            this.lb_TriggerSource.Location = new System.Drawing.Point(24, 241);
            this.lb_TriggerSource.Name = "lb_TriggerSource";
            this.lb_TriggerSource.Size = new System.Drawing.Size(47, 12);
            this.lb_TriggerSource.TabIndex = 10;
            this.lb_TriggerSource.Text = "触发源:";
            // 
            // cb_TriggerSource
            // 
            this.cb_TriggerSource.FormattingEnabled = true;
            this.cb_TriggerSource.Location = new System.Drawing.Point(77, 238);
            this.cb_TriggerSource.Name = "cb_TriggerSource";
            this.cb_TriggerSource.Size = new System.Drawing.Size(122, 20);
            this.cb_TriggerSource.TabIndex = 9;
            this.cb_TriggerSource.SelectedIndexChanged += new System.EventHandler(this.cb_TriggerSource_SelectedIndexChanged);
            // 
            // bnContinuesMode
            // 
            this.bnContinuesMode.AutoSize = true;
            this.bnContinuesMode.Location = new System.Drawing.Point(179, 188);
            this.bnContinuesMode.Name = "bnContinuesMode";
            this.bnContinuesMode.Size = new System.Drawing.Size(71, 16);
            this.bnContinuesMode.TabIndex = 8;
            this.bnContinuesMode.TabStop = true;
            this.bnContinuesMode.Text = "连续触发";
            this.bnContinuesMode.UseVisualStyleBackColor = true;
            this.bnContinuesMode.CheckedChanged += new System.EventHandler(this.bnContinuesMode_CheckedChanged);
            // 
            // bnTriggerMode
            // 
            this.bnTriggerMode.AutoSize = true;
            this.bnTriggerMode.Location = new System.Drawing.Point(89, 188);
            this.bnTriggerMode.Name = "bnTriggerMode";
            this.bnTriggerMode.Size = new System.Drawing.Size(71, 16);
            this.bnTriggerMode.TabIndex = 7;
            this.bnTriggerMode.TabStop = true;
            this.bnTriggerMode.Text = "触发模式";
            this.bnTriggerMode.UseVisualStyleBackColor = true;
            this.bnTriggerMode.CheckedChanged += new System.EventHandler(this.bnTriggerMode_CheckedChanged);
            // 
            // lb_TriggerModel
            // 
            this.lb_TriggerModel.AutoSize = true;
            this.lb_TriggerModel.Location = new System.Drawing.Point(24, 190);
            this.lb_TriggerModel.Name = "lb_TriggerModel";
            this.lb_TriggerModel.Size = new System.Drawing.Size(59, 12);
            this.lb_TriggerModel.TabIndex = 6;
            this.lb_TriggerModel.Text = "触发模式:";
            // 
            // tb_CamExposureTime
            // 
            this.tb_CamExposureTime.Location = new System.Drawing.Point(77, 136);
            this.tb_CamExposureTime.Name = "tb_CamExposureTime";
            this.tb_CamExposureTime.Size = new System.Drawing.Size(122, 21);
            this.tb_CamExposureTime.TabIndex = 5;
            // 
            // lb_CamExposureTime
            // 
            this.lb_CamExposureTime.AutoSize = true;
            this.lb_CamExposureTime.Location = new System.Drawing.Point(24, 139);
            this.lb_CamExposureTime.Name = "lb_CamExposureTime";
            this.lb_CamExposureTime.Size = new System.Drawing.Size(35, 12);
            this.lb_CamExposureTime.TabIndex = 4;
            this.lb_CamExposureTime.Text = "曝光:";
            // 
            // lb_CamSN
            // 
            this.lb_CamSN.AutoSize = true;
            this.lb_CamSN.Location = new System.Drawing.Point(24, 88);
            this.lb_CamSN.Name = "lb_CamSN";
            this.lb_CamSN.Size = new System.Drawing.Size(47, 12);
            this.lb_CamSN.TabIndex = 3;
            this.lb_CamSN.Text = "相机SN:";
            // 
            // cbDeviceList
            // 
            this.cbDeviceList.FormattingEnabled = true;
            this.cbDeviceList.Location = new System.Drawing.Point(77, 34);
            this.cbDeviceList.Name = "cbDeviceList";
            this.cbDeviceList.Size = new System.Drawing.Size(122, 20);
            this.cbDeviceList.TabIndex = 2;
            this.cbDeviceList.SelectedIndexChanged += new System.EventHandler(this.cbDeviceList_SelectedIndexChanged);
            // 
            // tb_CameraSN
            // 
            this.tb_CameraSN.Location = new System.Drawing.Point(77, 85);
            this.tb_CameraSN.Name = "tb_CameraSN";
            this.tb_CameraSN.Size = new System.Drawing.Size(122, 21);
            this.tb_CameraSN.TabIndex = 1;
            // 
            // lab_CameraName
            // 
            this.lab_CameraName.AutoSize = true;
            this.lab_CameraName.Location = new System.Drawing.Point(24, 37);
            this.lab_CameraName.Name = "lab_CameraName";
            this.lab_CameraName.Size = new System.Drawing.Size(47, 12);
            this.lab_CameraName.TabIndex = 0;
            this.lab_CameraName.Text = "相机名:";
            // 
            // UserCameraControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "UserCameraControl";
            this.Size = new System.Drawing.Size(261, 288);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lb_TriggerSource;
        private System.Windows.Forms.ComboBox cb_TriggerSource;
        private System.Windows.Forms.RadioButton bnContinuesMode;
        private System.Windows.Forms.RadioButton bnTriggerMode;
        private System.Windows.Forms.Label lb_TriggerModel;
        private System.Windows.Forms.TextBox tb_CamExposureTime;
        private System.Windows.Forms.Label lb_CamExposureTime;
        private System.Windows.Forms.Label lb_CamSN;
        private System.Windows.Forms.ComboBox cbDeviceList;
        private System.Windows.Forms.TextBox tb_CameraSN;
        private System.Windows.Forms.Label lab_CameraName;
    }
}
