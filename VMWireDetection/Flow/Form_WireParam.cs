using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;
using VisionDesigner;

namespace VMWireDetection
{
    public partial class Form_WireParam : Form
    {
        public struct WireParam
        {
            // ROI
            public float RoiCenterX;
            public float RoiCenterY;
            public float RoiWidth;
            public float RoiHeight;
            public float RoiAngle;

            // ImageMorphTool1
            public int Morph1Iterations;
            public int Morph1KernelSize_X;
            public int Morph1KernelSize_Y;

            // ImageFilterTool
            public int FilterKernelWidth;
            public int FilterKernelHeight;

            // BinaryTool
            public int BinaryLow;
            public int BinaryHigh;

            // ImageMorphTool2
            public int Morph2KernelSize_X;
            public int Morph2KernelSize_Y;

            // Blob
            public int BlobLow;
            public int BlobHigh;
            public int MinArea;
            public int MaxArea;

            // 
            public int LineNum;
            public int MinLineWidth;
            public int MaxLineWidth;

            public int MinLineHeight;
            public int MaxLineHeight;

            public int LightPosition;

        }

        public WireParam param = new WireParam();

        CMvdRectangleF rect;
        public Form_WireParam()
        {
            InitializeComponent();
            InitWindow();
            InitTool();
            LoadJson($"{Global.strProjectPath}\\{Global._Wire.strFlowName}_Flow.json");
        }

        public void InitWindow()
        {
            numUD_LineNum.Increment = 1;
            numUD_LineNum.Minimum = 0;
            numUD_LineNum.Maximum = 20;

            numUPlineWidthMin.Increment = 1;
            numUPlineWidthMin.Minimum = 0;
            numUPlineWidthMin.Maximum = 500;

            numUPlineWidthMax.Increment = 1;
            numUPlineWidthMax.Minimum = 0;
            numUPlineWidthMax.Maximum = 1000;

            numUPlineHeightMin.Increment = 1;
            numUPlineHeightMin.Minimum = 0;
            numUPlineHeightMin.Maximum = 500;

            numUPlineHeightMax.Increment = 1;
            numUPlineHeightMax.Minimum = 0;
            numUPlineHeightMax.Maximum = 1000;

            numUDKernelHeight.Increment = 1;
            numUDKernelHeight.Minimum = 1;
            numUDKernelHeight.Maximum = 51;

            trackBarHeight.TickFrequency = 1;
            trackBarHeight.Minimum = 1;
            trackBarHeight.Maximum = 51;

            numUDKernelWidth.Increment = 1;
            numUDKernelWidth.Minimum = 1;
            numUDKernelWidth.Maximum = 51;

            trackBarWidth.TickFrequency = 1;
            trackBarWidth.Minimum = 1;
            trackBarWidth.Maximum = 51;
        }

        public void InitTool()
        {
            Global._Wire.imageColorConverTool._tool.SetRunParam("ColorTransformType", "RGB2GRAY");
            Global._Wire.imageColorConverTool._tool.SetRunParam("RGB2GrayType", "GeneralRatio");
            Global._Wire.imageColorConverTool._tool.SetRunParam("Rratio", "0.33");
            Global._Wire.imageColorConverTool._tool.SetRunParam("Gratio", "0.33");
            Global._Wire.imageColorConverTool._tool.SetRunParam("Bratio", "0.33");

            Global._Wire.imageMorphTool1._tool.SetRunParam("Type", "Close");
            Global._Wire.imageMorphTool1._tool.SetRunParam("Shape", "Cross");
            Global._Wire.imageMorphTool1._tool.SetRunParam("Iterations", "9");
            Global._Wire.imageMorphTool1._tool.SetRunParam("KernelSize_X", "9");
            Global._Wire.imageMorphTool1._tool.SetRunParam("KernelSize_Y", "9");

            Global._Wire.imageFilterTool._tool.SetRunParam("FilterType", "Median");
            Global._Wire.imageFilterTool._tool.SetRunParam("KernelWidth", "90");
            Global._Wire.imageFilterTool._tool.SetRunParam("KernelHeight", "3");

            Global._Wire.imageArithmeticTool._tool.SetRunParam("ArithmeticType", "Subtract");
            Global._Wire.imageArithmeticTool._tool.BasicParam.Weight1 = 1;
            Global._Wire.imageArithmeticTool._tool.BasicParam.Weight2 = 0;
            Global._Wire.imageArithmeticTool._tool.BasicParam.Offset = 0;

            Global._Wire.imageBinaryTool._tool.SetRunParam("BinaryType", "HardThreshold");
            Global._Wire.imageBinaryTool._tool.SetRunParam("LowThreshold", "120");
            Global._Wire.imageBinaryTool._tool.SetRunParam("HighThreshold", "255");

            Global._Wire.imageMorphTool2._tool.SetRunParam("Type", "Open");
            Global._Wire.imageMorphTool2._tool.SetRunParam("Shape", "Cross");
            Global._Wire.imageMorphTool2._tool.SetRunParam("KernelSize_X", "9");
            Global._Wire.imageMorphTool2._tool.SetRunParam("KernelSize_Y", "9");

            Global._Wire.blob1._tool.SetRunParam("ThresholdType", "SingleThreshold");
            Global._Wire.blob1._tool.SetRunParam("Polarity", "BrightObject");
            Global._Wire.blob1._tool.SetRunParam("LowThreshold", "100");
            Global._Wire.blob1._tool.SetRunParam("HightThreshold", "255");
            Global._Wire.blob1._tool.SetRunParam("SelectByArea", "1");
            Global._Wire.blob1._tool.SetRunParam("MinArea", "10");
            Global._Wire.blob1._tool.SetRunParam("MaxArea", "999999");
            Global._Wire.blob1._tool.BasicParam.ShowBlobImageStatus = false;
            Global._Wire.blob1._tool.BasicParam.ShowOutlineStatus = false;
            Global._Wire.blob1._tool.BasicParam.ShowBinaryImageStatus = false;
        }

        public void SetParam(WireParam param)
        {
            CMvdRectangleF _ROI = new CMvdRectangleF(param.RoiCenterX, param.RoiCenterY, param.RoiWidth, param.RoiHeight);
            _ROI.Angle = param.RoiAngle;
            try
            {
                if (Global._Wire._RenderControl != null)
                {
                    Global._Wire._RenderControl.mvdRenderActivex1.ClearShapes();
                    _ROI.BorderColor = new MVD_COLOR(0, 0, byte.MaxValue, byte.MaxValue);
                    //_ROI.FillColor = new MVD_COLOR(0, 0, byte.MaxValue, 128);
                    _ROI.Interaction = false;
                    Global._Wire._RenderControl.CusAddShape(_ROI);

                }
            }
            catch (Exception ex)
            {
            }

            Global._Wire.imageMorphTool1._Roi = _ROI;
            Global._Wire.imageMorphTool1._tool.SetRunParam("Iterations", param.Morph1Iterations.ToString());
            Global._Wire.imageMorphTool1._tool.SetRunParam("KernelSize_X", param.Morph1KernelSize_X.ToString());
            Global._Wire.imageMorphTool1._tool.SetRunParam("KernelSize_Y", param.Morph1KernelSize_Y.ToString());

            Global._Wire.imageFilterTool._Roi = _ROI;
            Global._Wire.imageFilterTool._tool.SetRunParam("KernelWidth", param.FilterKernelWidth.ToString());
            Global._Wire.imageFilterTool._tool.SetRunParam("KernelHeight", param.FilterKernelHeight.ToString());

            CMvdRectangleF _Shape = _ROI;
            _Shape.Angle = 0;
            Global._Wire.imageArithmeticTool._Roi = _Shape;

            Global._Wire.imageBinaryTool._Roi = _ROI;
            Global._Wire.imageBinaryTool._tool.SetRunParam("LowThreshold", param.BinaryLow.ToString());
            Global._Wire.imageBinaryTool._tool.SetRunParam("HighThreshold", "255");

            Global._Wire.imageMorphTool2._Roi = _ROI;
            Global._Wire.imageMorphTool2._tool.SetRunParam("KernelSize_X", param.Morph2KernelSize_X.ToString());
            Global._Wire.imageMorphTool2._tool.SetRunParam("KernelSize_Y", param.Morph2KernelSize_Y.ToString());

            Global._Wire.blob1._Roi = _ROI;
            Global._Wire.blob1._tool.SetRunParam("LowThreshold", param.BlobLow.ToString());
            Global._Wire.blob1._tool.SetRunParam("HightThreshold", param.BlobHigh.ToString());
            Global._Wire.blob1._tool.SetRunParam("MinArea", param.MinArea.ToString());
            Global._Wire.blob1._tool.SetRunParam("MaxArea", param.MaxArea.ToString());
        }

        public void SaveJson(string path)
        {
            param.LineNum = (int)numUD_LineNum.Value;
            param.MinLineWidth = (int)numUPlineWidthMin.Value;
            param.MaxLineWidth = (int)numUPlineWidthMax.Value;
            param.MinLineHeight = (int)numUPlineHeightMin.Value;
            param.MaxLineHeight = (int)numUPlineHeightMax.Value;
            param.LightPosition = cbBottomLight.Checked ? 0 : 1;

            param.Morph2KernelSize_X = (int)numUDKernelWidth.Value;
            param.Morph2KernelSize_Y = (int)numUDKernelHeight.Value;
            var json = JsonConvert.SerializeObject(param, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public void LoadJson(string path)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                param = JsonConvert.DeserializeObject<WireParam>(json);

                numUD_LineNum.Value = param.LineNum;
                numUPlineWidthMin.Value = param.MinLineWidth;
                numUPlineWidthMax.Value = param.MaxLineWidth;
                numUPlineHeightMin.Value = param.MinLineHeight;
                numUPlineHeightMax.Value = param.MaxLineHeight;
                cbBottomLight.Checked = param.LightPosition == 0;
                cbTopLight.Checked = param.LightPosition == 1;

                numUDKernelWidth.Value = param.Morph2KernelSize_X;
                numUDKernelHeight.Value = param.Morph2KernelSize_Y;

                SetParam(param);
            }
            else
            {
                param.LineNum = 12;
                param.MinLineWidth = 8;
                param.MaxLineWidth = 19;
                param.MinLineHeight = 8;
                param.MaxLineHeight = 1000;
                param.LightPosition = 1;
                param.Morph1Iterations = 9;
                param.Morph1KernelSize_X = 9;
                param.Morph1KernelSize_Y = 9;

                param.FilterKernelHeight = 90;
                param.FilterKernelWidth = 3;

                param.BinaryLow = 120;
                param.BinaryHigh = 255;

                param.BlobHigh = 255;
                param.BlobLow = 100;

                param.Morph2KernelSize_X = 9;
                param.Morph2KernelSize_Y = 9;

                param.MinArea = 10;
                param.MaxArea = 999999;

                numUD_LineNum.Value = param.LineNum;
                numUPlineWidthMin.Value = param.MinLineWidth;
                numUPlineWidthMax.Value = param.MaxLineWidth;
                numUPlineHeightMin.Value = param.MinLineHeight;
                numUPlineHeightMax.Value = param.MaxLineHeight;
                cbBottomLight.Checked = param.LightPosition == 0;

                numUDKernelWidth.Value = param.Morph2KernelSize_X;
                numUDKernelHeight.Value = param.Morph2KernelSize_Y;

                SaveJson(path);
            }
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            Global._Wire.Run();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveJson($"{Global.strProjectPath}\\{Global._Wire.strFlowName}_Flow.json");
        }

        private void btn_SetRoI_Click(object sender, EventArgs e)
        {
            if (btn_SetRoI.Text == "设置检测区域")
            {
                rect = Global._Wire._RenderControl.DrawRectangle("剥皮ROI");
                btn_SetRoI.Text = "绘制结束";
            }
            else if (btn_SetRoI.Text == "绘制结束")
            {
                CMvdRectangleF rectResult = Global._Wire._RenderControl.GetRectangle(rect);
                if (rectResult != null)
                {
                    param.RoiCenterX = rectResult.CenterX;
                    param.RoiCenterY = rectResult.CenterY;
                    param.RoiWidth = rectResult.Width;
                    param.RoiHeight = rectResult.Height;
                    param.RoiAngle = rectResult.Angle;
                    btn_SetRoI.Text = "设置检测区域";
                }
                else
                {
                    btn_SetRoI.Text = "设置检测区域";
                }
            }
        }

        private void trackBarWidth_Scroll(object sender, EventArgs e)
        {
            this.numUDKernelWidth.Value = trackBarWidth.Value;
        }

        private void trackBarHeight_Scroll(object sender, EventArgs e)
        {
            this.numUDKernelHeight.Value = trackBarHeight.Value;
        }

        private void numUDKernelWidth_ValueChanged(object sender, EventArgs e)
        {
            this.trackBarWidth.Value = (int)numUDKernelWidth.Value;
        }

        private void numUDKernelHeight_ValueChanged(object sender, EventArgs e)
        {
            this.trackBarHeight.Value = (int)numUDKernelHeight.Value;
        }

        private void cbBottomLight_CheckedChanged(object sender, EventArgs e)
        {
            cbTopLight.Checked = !cbBottomLight.Checked;
            Global._Light.LoadJson($"{Global.strProjectPath}\\{Global._Wire.strFlowName}_Light.json");
            Global._Light.lightParam.LightPort = 0;
            Global._Light.UpdateLight();
            Global._Light.SaveJson($"{Global.strProjectPath}\\{Global._Wire.strFlowName}_Light.json");
        }

        private void cbTopLight_CheckedChanged(object sender, EventArgs e)
        {
            cbBottomLight.Checked = !cbTopLight.Checked;
            Global._Light.LoadJson($"{Global.strProjectPath}\\{Global._Wire.strFlowName}_Light.json");
            Global._Light.lightParam.LightPort = 1;
            Global._Light.UpdateLight();
            Global._Light.SaveJson($"{Global.strProjectPath}\\{Global._Wire.strFlowName}_Light.json");
        }

        private void Form_WireParam_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true; // 取消默认的关闭行为
                    this.Hide();     // 隐藏窗体而不销毁
                }
            }
            catch (Exception exs)
            {
                return;
            }
        }

        private void Form_WireParam_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadJson($"{Global.strProjectPath}\\{Global._Wire.strFlowName}_Flow.json");
            }
        }
    }
}
