namespace VMWireDetection
{
    partial class UserControl_CamShow
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
            this.mvdRenderActivex1 = new VisionDesigner.MVDRenderActivex();
            this.tbPixelInfo = new System.Windows.Forms.TextBox();
            this.btn_Zoom = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // mvdRenderActivex1
            // 
            this.mvdRenderActivex1.EnableRetainShape = false;
            this.mvdRenderActivex1.InteractType = VisionDesigner.MVDRenderInteractType.StandardAndCustom;
            this.mvdRenderActivex1.InterpolationMode = VisionDesigner.MVD_INTERPOLATION_MODE.MvdNearestNeighbor;
            this.mvdRenderActivex1.Location = new System.Drawing.Point(8, 81);
            this.mvdRenderActivex1.MenuLanguage = VisionDesigner.MVDRenderMenuLangType.Default;
            this.mvdRenderActivex1.Name = "mvdRenderActivex1";
            this.mvdRenderActivex1.Size = new System.Drawing.Size(574, 418);
            this.mvdRenderActivex1.TabIndex = 0;
            this.mvdRenderActivex1.MVDMouseEvent += new VisionDesigner.MVDRenderActivex.MVDMouseEventHandler(this.mvdRenderActivex1_MVDMouseEvent);
            // 
            // tbPixelInfo
            // 
            this.tbPixelInfo.Location = new System.Drawing.Point(206, 505);
            this.tbPixelInfo.Name = "tbPixelInfo";
            this.tbPixelInfo.Size = new System.Drawing.Size(376, 21);
            this.tbPixelInfo.TabIndex = 22;
            // 
            // btn_Zoom
            // 
            this.btn_Zoom.Location = new System.Drawing.Point(479, 15);
            this.btn_Zoom.Name = "btn_Zoom";
            this.btn_Zoom.Size = new System.Drawing.Size(103, 41);
            this.btn_Zoom.TabIndex = 23;
            this.btn_Zoom.Text = "图像自适应";
            this.btn_Zoom.UseVisualStyleBackColor = true;
            this.btn_Zoom.Click += new System.EventHandler(this.btn_Zoom_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(8, 55);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(370, 20);
            this.comboBox1.TabIndex = 24;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // UserControl_CamShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_Zoom);
            this.Controls.Add(this.tbPixelInfo);
            this.Controls.Add(this.mvdRenderActivex1);
            this.Name = "UserControl_CamShow";
            this.Size = new System.Drawing.Size(585, 529);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public VisionDesigner.MVDRenderActivex mvdRenderActivex1;
        private System.Windows.Forms.TextBox tbPixelInfo;
        private System.Windows.Forms.Button btn_Zoom;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
