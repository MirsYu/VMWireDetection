using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows.Forms;
using VisionDesigner;
using VisionDesigner.Caliper;

namespace VMWireDetection
{
    public partial class Form_CasingParam : Form
    {
        public struct CusROI
        {
            public float ROICenterX;
            public float ROICenterY;
            public float ROIWidth;
            public float ROIHeight;
            public float ROIAngle;
        }

        public struct CasingParam
        {
            // 匹配建模ROI
            public CusROI ModelROI;

            // 匹配运行ROI
            public CusROI PathROI;

            // 卡尺ROI参数
            public float CaliperYOffset;
            public float CaliperHeight;

            // 端子ROI参数
            public float TitleYOffset;
            public float TitleHeight;

            // 线序ROI参数
            public float LineYOffset;
            public float LineHeight;

            public int CasingNum;

            public int TitleNum;

            // imgPathTool
            public float PathScore;

            // lineFind
            public int CasingLineType;
            public int CasingLineStrength;
            public int CasingRayNum;

            // lineFind
            public int TitleLineType;
            public int TitleLineStrength;
            public int TitleRayNum;

            // blob
            public int BlobType;
            public int LowThreshold;

            public int CheckType;

            // 颜色基准图 基准值
            public List<Vector3> ChannelValue;

            // 颜色上下限
            public float ColorGap;

            // 端子到位上下限
            public float MinTitle;
            public float MaxTitle;

            // 线间距
            public float MinLineGap;
            public float MaxLineGap;

            public int isTitleCheck;
            public int isLineCheck;
            public int isLineGapCheck;
        }

        public CasingParam param = new CasingParam();
        CMvdRectangleF rectModel;
        CMvdRectangleF rectPath;

        public Form_CasingParam()
        {
            InitializeComponent();
            InitWindow();
            InitTool();
            LoadJson($"{Global.strProjectPath}\\{Global._Casing.strFlowName}_Flow.json");
        }

        public void InitWindow()
        {
            numUDShell.Increment = 1;
            numUDShell.Minimum = 0;
            numUDShell.Maximum = 20;

            numUDTerminal.Increment = 1;
            numUDTerminal.Minimum = 0;
            numUDTerminal.Maximum = 500;

            numerUDMatch.Increment = 0.1m;
            numerUDMatch.DecimalPlaces = 2;
            numerUDMatch.Minimum = 0.1m;
            numerUDMatch.Maximum = 1;

            numUDMin.Increment = 0.1m;
            numUDMin.DecimalPlaces = 2;
            numUDMin.Minimum = 0m;
            numUDMin.Maximum = 50m;

            numUDMax.Increment = 0.1m;
            numUDMax.DecimalPlaces = 2;
            numUDMax.Minimum = 0m;
            numUDMax.Maximum = 100m;

            numUDColorGap.Increment = 0.1m;
            numUDColorGap.DecimalPlaces = 2;
            numUDColorGap.Minimum = 0m;
            numUDColorGap.Maximum = 50m;

            numUDLineDisMin.Increment = 0.1m;
            numUDLineDisMin.DecimalPlaces = 2;
            numUDLineDisMin.Minimum = 0m;
            numUDLineDisMin.Maximum = 50m;

            numUDLineDisMax.Increment = 0.1m;
            numUDLineDisMax.DecimalPlaces = 2;
            numUDLineDisMax.Minimum = 0m;
            numUDLineDisMax.Maximum = 100m;
        }

        public void InitTool()
        {
            Global._Casing.imageColorConverTool1._tool.SetRunParam("ColorTransformType", "RGB2GRAY");
            Global._Casing.imageColorConverTool1._tool.SetRunParam("RGB2GrayType", "GeneralRatio");
            Global._Casing.imageColorConverTool1._tool.SetRunParam("Rratio", "0.33");
            Global._Casing.imageColorConverTool1._tool.SetRunParam("Gratio", "0.33");
            Global._Casing.imageColorConverTool1._tool.SetRunParam("Bratio", "0.33");

            Global._Casing.imgPathTool._tool.SetRunParam("MinScore", "0.5");
            Global._Casing.imgPathTool._tool.SetRunParam("MaxMatchNum", "6");
            Global._Casing.imgPathTool._tool.SetRunParam("SortType", "X");

            Global._Casing.lineFind1._tool.SetRunParam("EdgeStrength", "15");
            Global._Casing.lineFind1._tool.SetRunParam("RayNum", "20");
            Global._Casing.lineFind1._tool.SetRunParam("KernelSize", "2");
            Global._Casing.lineFind1._tool.SetRunParam("LineFindMode", "Best");
            Global._Casing.lineFind1._tool.SetRunParam("FindOrient", "UpToDown");
            Global._Casing.lineFind1._tool.SetRunParam("EdgePolarity", "BlackToWhite");

            Global._Casing.caliperTool1._tool.BasicParam.EdgeMode = MVD_CALIPERTOOL_EDGEMODE.MVD_CALIPERTOOL_EDGEMODE_EDGE_PAIR;
            Global._Casing.caliperTool1._tool.SetRunParam("Edge0Polarity", "WhiteToBlack");
            Global._Casing.caliperTool1._tool.SetRunParam("Edge1Polarity", "BlackToWhite");
            Global._Casing.caliperTool1._tool.SetRunParam("ProjectionType", "ProjectionToWidth");
            Global._Casing.caliperTool1._tool.SetRunParam("ContrastTH", "10");
            Global._Casing.caliperTool1._tool.SetRunParam("EdgePairWidth", "30");
            Global._Casing.caliperTool1._tool.SetRunParam("Maximum", "2");
            Global._Casing.caliperTool1._tool.SetRunParam("SizeDiffNormEnable", "1");
            Global._Casing.caliperTool1._tool.SetRunParam("SizeDiffNormAsymDrop", "Drop");
            Global._Casing.caliperTool1._tool.SetRunParam("SizeDiffNormAsymX0", "0");
            Global._Casing.caliperTool1._tool.SetRunParam("SizeDiffNormAsymX1", "1");
            Global._Casing.caliperTool1._tool.SetRunParam("SizeDiffNormAsymXc", "1");
            Global._Casing.caliperTool1._tool.SetRunParam("SizeDiffNormAsymY0", "0");
            Global._Casing.caliperTool1._tool.SetRunParam("SizeDiffNormAsymY1", "100");
            Global._Casing.caliperTool1._tool.SetRunParam("ContrastEnable", "0");
            Global._Casing.caliperTool1._tool.SetRunParam("SortType", "Forward");

            Global._Casing.caliperTool2._tool.BasicParam.EdgeMode = MVD_CALIPERTOOL_EDGEMODE.MVD_CALIPERTOOL_EDGEMODE_SINGLE_EDGE;
            Global._Casing.caliperTool2._tool.SetRunParam("HalfKernelSize", "1");
            Global._Casing.caliperTool2._tool.SetRunParam("ContrastTH", "5");
            Global._Casing.caliperTool2._tool.SetRunParam("EdgePolarity", "BlackToWhite");
            Global._Casing.caliperTool2._tool.SetRunParam("ProjectionType", "ProjectionToHeight");
            Global._Casing.caliperTool2._tool.SetRunParam("Maximum", "1");
            Global._Casing.caliperTool2._tool.SetRunParam("PositionNegEnable", "1");
            Global._Casing.caliperTool2._tool.SetRunParam("PositionNegDrop", "Drop");
            Global._Casing.caliperTool2._tool.SetRunParam("PositionNegX0", "-100");
            Global._Casing.caliperTool2._tool.SetRunParam("PositionNegX1", "100");
            Global._Casing.caliperTool2._tool.SetRunParam("PositionNegXC", "10000");
            Global._Casing.caliperTool2._tool.SetRunParam("PositionNegY0", "0");
            Global._Casing.caliperTool2._tool.SetRunParam("PositionNegY1", "100");
            Global._Casing.caliperTool2._tool.SetRunParam("ContrastEnable", "0");
            Global._Casing.caliperTool2._tool.SetRunParam("SortType", "Forward");

            Global._Casing.caliperTool3._tool.BasicParam.EdgeMode = MVD_CALIPERTOOL_EDGEMODE.MVD_CALIPERTOOL_EDGEMODE_EDGE_PAIR;
            Global._Casing.caliperTool3._tool.SetRunParam("Edge0Polarity", "WhiteToBlack");
            Global._Casing.caliperTool3._tool.SetRunParam("Edge1Polarity", "BlackToWhite");
            Global._Casing.caliperTool3._tool.SetRunParam("ProjectionType", "ProjectionToWidth");
            Global._Casing.caliperTool3._tool.SetRunParam("ContrastTH", "3");
            Global._Casing.caliperTool3._tool.SetRunParam("EdgePairWidth", "40");
            Global._Casing.caliperTool3._tool.SetRunParam("Maximum", "1");
            Global._Casing.caliperTool3._tool.SetRunParam("SizeDiffNormEnable", "1");
            Global._Casing.caliperTool3._tool.SetRunParam("SizeDiffNormAsymDrop", "Rise");
            Global._Casing.caliperTool3._tool.SetRunParam("SizeDiffNormAsymX0", "-1");
            Global._Casing.caliperTool3._tool.SetRunParam("SizeDiffNormAsymX1", "-1");
            Global._Casing.caliperTool3._tool.SetRunParam("SizeDiffNormAsymXc", "0");
            Global._Casing.caliperTool3._tool.SetRunParam("SizeDiffNormAsymY0", "0");
            Global._Casing.caliperTool3._tool.SetRunParam("SizeDiffNormAsymY1", "100");
            Global._Casing.caliperTool3._tool.SetRunParam("SizeDiffNormAsymDroph", "Drop");
            Global._Casing.caliperTool3._tool.SetRunParam("SizeDiffNormAsymX0h", "0");
            Global._Casing.caliperTool3._tool.SetRunParam("SizeDiffNormAsymX1h", "2");
            Global._Casing.caliperTool3._tool.SetRunParam("SizeDiffNormAsymXch", "2");
            Global._Casing.caliperTool3._tool.SetRunParam("SizeDiffNormAsymY0h", "0");
            Global._Casing.caliperTool3._tool.SetRunParam("SizeDiffNormAsymY1h", "100");
            Global._Casing.caliperTool3._tool.SetRunParam("ContrastEnable", "0");
            Global._Casing.caliperTool3._tool.SetRunParam("SortType", "Forward");

            Global._Casing.caliperTool4._tool.BasicParam.EdgeMode = MVD_CALIPERTOOL_EDGEMODE.MVD_CALIPERTOOL_EDGEMODE_EDGE_PAIR;
            Global._Casing.caliperTool4._tool.SetRunParam("Edge0Polarity", "BlackToWhite");
            Global._Casing.caliperTool4._tool.SetRunParam("Edge1Polarity", "WhiteToBlack");
            Global._Casing.caliperTool4._tool.SetRunParam("ProjectionType", "ProjectionToWidth");
            Global._Casing.caliperTool4._tool.SetRunParam("ContrastTH", "3");
            Global._Casing.caliperTool4._tool.SetRunParam("EdgePairWidth", "30");
            Global._Casing.caliperTool4._tool.SetRunParam("Maximum", "1");
            Global._Casing.caliperTool4._tool.SetRunParam("SizeDiffNormEnable", "1");
            Global._Casing.caliperTool4._tool.SetRunParam("SizeDiffNormAsymDrop", "Rise");
            Global._Casing.caliperTool4._tool.SetRunParam("SizeDiffNormAsymX0", "-1");
            Global._Casing.caliperTool4._tool.SetRunParam("SizeDiffNormAsymX1", "-1");
            Global._Casing.caliperTool4._tool.SetRunParam("SizeDiffNormAsymXc", "0");
            Global._Casing.caliperTool4._tool.SetRunParam("SizeDiffNormAsymY0", "0");
            Global._Casing.caliperTool4._tool.SetRunParam("SizeDiffNormAsymY1", "100");
            Global._Casing.caliperTool4._tool.SetRunParam("SizeDiffNormAsymDroph", "Drop");
            Global._Casing.caliperTool4._tool.SetRunParam("SizeDiffNormAsymX0h", "0");
            Global._Casing.caliperTool4._tool.SetRunParam("SizeDiffNormAsymX1h", "2");
            Global._Casing.caliperTool4._tool.SetRunParam("SizeDiffNormAsymXch", "2");
            Global._Casing.caliperTool4._tool.SetRunParam("SizeDiffNormAsymY0h", "0");
            Global._Casing.caliperTool4._tool.SetRunParam("SizeDiffNormAsymY1h", "100");
            Global._Casing.caliperTool4._tool.SetRunParam("ContrastEnable", "0");
            Global._Casing.caliperTool4._tool.SetRunParam("SortType", "Forward");

            Global._Casing.lineFind2._tool.SetRunParam("EdgeStrength", "15");
            Global._Casing.lineFind2._tool.SetRunParam("RayNum", "20");
            Global._Casing.lineFind2._tool.SetRunParam("LineFindMode", "Best");
            Global._Casing.lineFind2._tool.SetRunParam("EdgePolarity", "BlackToWhite");

            Global._Casing.colorMeasureTool1._tool.SetRunParam("ColorSpace", "RGB");
            Global._Casing.colorMeasureTool2._tool.SetRunParam("ColorSpace", "RGB");

            Global._Casing.blob._tool.SetRunParam("Polarity", "BrightObject");
            Global._Casing.blob._tool.SetRunParam("LowThreshold", "20");
            Global._Casing.blob._tool.SetRunParam("FindNum", "1");
            Global._Casing.blob._tool.BasicParam.ShowBlobImageStatus = false;
            Global._Casing.blob._tool.BasicParam.ShowBinaryImageStatus = false;
            Global._Casing.blob._tool.BasicParam.ShowOutlineStatus = false;
        }

        public void SetParam(CasingParam param)
        {
            CMvdRectangleF _ModeROI = new CMvdRectangleF(param.ModelROI.ROICenterX, param.ModelROI.ROICenterY, param.ModelROI.ROIWidth, param.ModelROI.ROIHeight);
            _ModeROI.Angle = param.ModelROI.ROIAngle;

            CMvdRectangleF _PathROI = new CMvdRectangleF(param.PathROI.ROICenterX, param.PathROI.ROICenterY, param.PathROI.ROIWidth, param.PathROI.ROIHeight);
            _PathROI.Angle = param.PathROI.ROIAngle;

            Global._Casing.imgPathTool._tool.SetRunParam("MinScore", param.PathScore.ToString());
            Global._Casing.imgPathTool._tool.SetRunParam("MaxMatchNum", param.CasingNum.ToString());

            Global._Casing.lineFind1._tool.SetRunParam("EdgeStrength", param.CasingLineStrength.ToString());
            Global._Casing.lineFind1._tool.SetRunParam("RayNum", param.CasingRayNum.ToString());

            Global._Casing.caliperTool1._tool.SetRunParam("Maximum", param.TitleNum.ToString());

            Global._Casing.caliperTool3._tool.SetRunParam("Maximum", param.TitleNum.ToString());

            Global._Casing.caliperTool4._tool.SetRunParam("Maximum", (param.TitleNum - 1).ToString());

            string strCasingType = "BlackToWhite";
            switch (param.CasingLineType)
            {
                case 0:
                    strCasingType = "BlackToWhite";
                    break;
                case 1:
                    strCasingType = "WhiteToBlack";
                    break;
                case 2:
                    strCasingType = "Both";
                    break;
                default:
                    break;
            }
            Global._Casing.lineFind1._tool.SetRunParam("EdgePolarity", strCasingType);

            Global._Casing.lineFind2._tool.SetRunParam("EdgeStrength", param.CasingLineStrength.ToString());
            Global._Casing.lineFind2._tool.SetRunParam("RayNum", param.CasingRayNum.ToString());

            string strTitleType = "BlackToWhite";
            switch (param.CasingLineType)
            {
                case 0:
                    strCasingType = "BlackToWhite";
                    break;
                case 1:
                    strCasingType = "WhiteToBlack";
                    break;
                case 2:
                    strCasingType = "Both";
                    break;
                default:
                    break;
            }
            Global._Casing.lineFind2._tool.SetRunParam("EdgePolarity", strTitleType);

            Global._Casing.blob._tool.SetRunParam("Polarity", param.BlobType == 0 ? "BrightObject" : "DarkObject");
            Global._Casing.blob._tool.SetRunParam("LowThreshold", param.LowThreshold.ToString());
        }

        public void SaveJson(string path)
        {
            param.CasingNum = (int)numUDShell.Value;
            param.TitleNum = (int)numUDTerminal.Value;
            param.PathScore = (float)numerUDMatch.Value;
            param.ColorGap = (float)numUDColorGap.Value;
            param.MinTitle = (float)numUDMin.Value;
            param.MaxTitle = (float)numUDMax.Value;
            param.MinLineGap = (float)numUDLineDisMin.Value;
            param.MaxLineGap = (float)numUDLineDisMax.Value;

            param.isTitleCheck = checkBox1.Checked ? 1 : 0;
            param.isLineCheck = checkBox2.Checked ? 1 : 0;
            param.isLineGapCheck = checkBox3.Checked ? 1 : 0;

            var json = JsonConvert.SerializeObject(param, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public void LoadJson(string path)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                param = JsonConvert.DeserializeObject<CasingParam>(json);

                numUDShell.Value = param.CasingNum;
                numUDTerminal.Value = param.TitleNum;
                numerUDMatch.Value = (decimal)param.PathScore;
                numUDColorGap.Value = (decimal)param.ColorGap;

                numUDMin.Value = (decimal)param.MinTitle;
                numUDMax.Value = (decimal)param.MaxTitle;

                numUDLineDisMin.Value = (decimal)param.MinLineGap;
                numUDLineDisMax.Value = (decimal)param.MaxLineGap;

                checkBox1.Checked = param.isTitleCheck == 1 ? true : false;
                checkBox2.Checked = param.isLineCheck == 1 ? true : false;
                checkBox3.Checked = param.isLineCheck == 1 ? true : false;

                SetParam(param);
            }
            else
            {
                param.TitleHeight = 60;
                param.TitleYOffset = 20;
                param.CaliperYOffset = 30;
                param.CaliperHeight = 40;
                param.LineYOffset = 10;
                param.LineHeight = 40;

                param.CasingNum = 6;
                param.TitleNum = 2;
                param.PathScore = 0.5f;
                param.CasingLineType = 0;
                param.CasingLineStrength = 15;
                param.CasingRayNum = 20;

                param.TitleLineType = 0;
                param.TitleLineStrength = 15;
                param.TitleRayNum = 20;

                param.BlobType = 0;
                param.LowThreshold = 20;

                param.ChannelValue = new List<Vector3>();

                param.ColorGap = 50;

                param.MinTitle = 20;
                param.MaxTitle = 50;

                param.MinLineGap = 20;
                param.MaxLineGap = 40;

                param.isTitleCheck = 1;
                param.isLineCheck = 1;
                param.isLineGapCheck = 1;


                numUDShell.Value = param.CasingNum;
                numUDTerminal.Value = param.TitleNum;
                numerUDMatch.Value = (decimal)param.PathScore;
                numUDColorGap.Value = (decimal)param.ColorGap;

                numUDMin.Value = (decimal)param.MinTitle;
                numUDMax.Value = (decimal)param.MaxTitle;

                numUDLineDisMin.Value = (decimal)param.MinLineGap;
                numUDLineDisMax.Value = (decimal)param.MaxLineGap;

                checkBox1.Checked = param.isTitleCheck == 1 ? true : false;
                checkBox2.Checked = param.isLineCheck == 1 ? true : false;
                checkBox3.Checked = param.isLineCheck == 1 ? true : false;

                SaveJson(path);
            }
        }

        private void btn_Test1_Click(object sender, EventArgs e)
        {
            Global._Casing.Run();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveJson($"{Global.strProjectPath}\\{Global._Casing.strFlowName}_Flow.json");
        }

        private void btn_Match_Click(object sender, EventArgs e)
        {
            if (btn_Match.Text == "建模")
            {
                rectModel = Global._Casing._RenderControl.DrawRectangle("建模ROI");
                btn_Match.Text = "绘制结束";
            }
            else if (btn_Match.Text == "绘制结束")
            {
                CMvdRectangleF rectResult = Global._Casing._RenderControl.GetRectangle(rectModel);
                if (rectResult != null)
                {
                    param.ModelROI.ROICenterX = rectResult.CenterX;
                    param.ModelROI.ROICenterY = rectResult.CenterY;
                    param.ModelROI.ROIWidth = rectResult.Width;
                    param.ModelROI.ROIHeight = rectResult.Height;
                    param.ModelROI.ROIAngle = rectResult.Angle;
                    Global._Casing.CreateMode(rectResult);
                    Global._Casing.imgPathTool._tool.Pattern.ExportPattern($"{Global.strProjectPath}\\{Global._Casing.strFlowName}.contourmxml");
                    btn_Match.Text = "建模";
                }
                else
                {
                    btn_Match.Text = "建模";
                }
            }
        }

        private void btn_ROI_Click(object sender, EventArgs e)
        {
            if (btn_ROI.Text == "运行ROI")
            {
                rectModel = Global._Casing._RenderControl.DrawRectangle("运行ROI");
                btn_ROI.Text = "绘制结束";
            }
            else if (btn_ROI.Text == "绘制结束")
            {
                CMvdRectangleF rectResult = Global._Casing._RenderControl.GetRectangle(rectModel);
                if (rectResult != null)
                {
                    param.PathROI.ROICenterX = rectResult.CenterX;
                    param.PathROI.ROICenterY = rectResult.CenterY;
                    param.PathROI.ROIWidth = rectResult.Width;
                    param.PathROI.ROIHeight = rectResult.Height;
                    param.PathROI.ROIAngle = rectResult.Angle;
                    btn_ROI.Text = "运行ROI";
                }
                else
                {
                    btn_ROI.Text = "运行ROI";
                }
            }
        }

        private void Form_CasingParam_FormClosing(object sender, FormClosingEventArgs e)
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

        private void Form_CasingParam_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadJson($"{Global.strProjectPath}\\{Global._Casing.strFlowName}_Flow.json");
            }
        }

        private void btn_SetColorBase_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.png"; // 只显示图片文件
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                Bitmap bitmap = new Bitmap(filePath);
                Global._Casing.localImage = new CMvdImage();
                Global._Casing.localImage.InitImage(filePath);
                Global._Casing.useLocalImage = true; // 设置为使用本地图片
                Global._Casing.IsGetColorBase = true;
                Global._Casing.Run();
            }
        }
    }
}