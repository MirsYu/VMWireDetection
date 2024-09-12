using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VisionDesigner;

namespace VMWireDetection
{
    public partial class Form_Main : Form
    {
        public string strProjectPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Project\\";

        public bool IsRunMode = false;

        Thread thread;

        public Form_Main()
        {
            if (!Directory.Exists(strProjectPath))
            {
                Directory.CreateDirectory(strProjectPath);
            }
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            Global._Main = this;

            Global.strProjectPath = strProjectPath;

            Global._RTU.LoadJson($"{Global.strProjectPath}\\RTU.json");

            Global._Wire._RenderControl = userControl_CamShow1;

            Global._Wire.Init();

            Global._Casing._RenderControl = userControl_CamShow2;

            Global._Casing.Init();

            this.userControl_CamShow1.strCamSN = Global._Wire.strFlowName;
            this.userControl_CamShow2.strCamSN = Global._Casing.strFlowName;

            LogInit.Init(textBoxErrorMessage);

            Global.LoadJson($"{Global.strProjectPath}\\Global.json");

            // 更新CheckBox
            this.cb_SaveOrgInOK.Checked = Global._GlobalData.SaveOrgInOK == 1 ? true : false;
            this.cb_SaveOrgInNG.Checked = Global._GlobalData.SaveOrgInNG == 1 ? true : false;
            this.cb_SaveRenderInNG.Checked = Global._GlobalData.SaveRenderInNG == 1 ? true : false;
            this.cb_SaveRenderInOK.Checked = Global._GlobalData.SaveRenderInOK == 1 ? true : false;

            thread = new Thread(() =>
            {
                while (true)
                {
                    // 切换到主线程更新控件
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            // 更新Label
                            lblPeelrunCost.Text = Global._GlobalData.LineTime.ToString();
                            lblPeelOK.Text = Global._GlobalData.lineOKNum.ToString();
                            lblPeelNG.Text = Global._GlobalData.lineNGNum.ToString();
                            lblPeelTotal.Text = Global._GlobalData.lineTotalNum.ToString();

                            lblShellrunCost.Text = Global._GlobalData.ShellTime.ToString();
                            lblShellOK.Text = Global._GlobalData.ShellOKNum.ToString();
                            lblShellNG.Text = Global._GlobalData.ShellNGNum.ToString();
                            lblShellTotal.Text = Global._GlobalData.ShellTotalNum.ToString();

                            // 处理良率显示
                            lblPeelYield.Text = Global._GlobalData.lineTotalNum == 0 ? "0" : ((Global._GlobalData.lineOKNum / (double)Global._GlobalData.lineTotalNum) * 100).ToString("F2");
                            lblShellYield.Text = Global._GlobalData.ShellTotalNum == 0 ? "0" : ((Global._GlobalData.ShellOKNum / (double)Global._GlobalData.ShellTotalNum) * 100).ToString("F2");
                        });
                    }

                    // 每次迭代之间的暂停
                    Thread.Sleep(10);
                }
            });
            thread.Start();
        }

        private void btn_WireParam_Click(object sender, EventArgs e)
        {
            Global._WireParam.Show();
        }

        private void btn_WireCamFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.png"; // 只显示图片文件
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                Bitmap bitmap = new Bitmap(filePath);
                Global._Wire.localImage = new CMvdImage();
                Global._Wire.localImage.InitImage(filePath);
                Global._Wire.useLocalImage = true; // 设置为使用本地图片
                Global._Wire.Run();
            }
        }

        private void btn_Casing_Click(object sender, EventArgs e)
        {
            Global._CasingParam.Show();
        }

        private void btn_CasingCamFile_Click(object sender, EventArgs e)
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
                Global._Casing.Run();
            }
        }

        private void btn_Light_Click(object sender, EventArgs e)
        {
            Global._LiveCam.Show();
        }

        private void btn_PLC_Click(object sender, EventArgs e)
        {
            Global._RTU.Show();
        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global._Cam.CloseDevices();
            Global._RTU.Close();
            // 将控件的值保存到 Global._GlobalData
            Global._GlobalData.SaveOrgInOK = this.cb_SaveOrgInOK.Checked ? 1 : 0;
            Global._GlobalData.SaveOrgInNG = this.cb_SaveOrgInNG.Checked ? 1 : 0;
            Global._GlobalData.SaveRenderInNG = this.cb_SaveRenderInNG.Checked ? 1 : 0;
            Global._GlobalData.SaveRenderInOK = this.cb_SaveRenderInOK.Checked ? 1 : 0;

            // 将 UI 文本框中的值转换为数值并保存到 Global._GlobalData
            if (double.TryParse(lblPeelrunCost.Text, out double peelTime))
            {
                Global._GlobalData.LineTime = (float)peelTime;
            }

            if (int.TryParse(lblPeelOK.Text, out int peelOK))
            {
                Global._GlobalData.lineOKNum = peelOK;
            }

            if (int.TryParse(lblPeelNG.Text, out int peelNG))
            {
                Global._GlobalData.lineNGNum = peelNG;
            }

            if (int.TryParse(lblPeelTotal.Text, out int peelTotal))
            {
                Global._GlobalData.lineTotalNum = peelTotal;
            }

            if (double.TryParse(lblShellrunCost.Text, out double shellTime))
            {
                Global._GlobalData.ShellTime = (float)shellTime;
            }

            if (int.TryParse(lblShellOK.Text, out int shellOK))
            {
                Global._GlobalData.ShellOKNum = shellOK;
            }

            if (int.TryParse(lblShellNG.Text, out int shellNG))
            {
                Global._GlobalData.ShellNGNum = shellNG;
            }

            if (int.TryParse(lblShellTotal.Text, out int shellTotal))
            {
                Global._GlobalData.ShellTotalNum = shellTotal;
            }

            // 如果需要反向写入良率值（根据 OK 数量和总数量计算的百分比）
            if (double.TryParse(lblPeelYield.Text, out double peelYield) && Global._GlobalData.lineTotalNum > 0)
            {
                Global._GlobalData.lineOKNum = (int)((peelYield / 100) * Global._GlobalData.lineTotalNum);
            }

            if (double.TryParse(lblShellYield.Text, out double shellYield) && Global._GlobalData.ShellTotalNum > 0)
            {
                Global._GlobalData.ShellOKNum = (int)((shellYield / 100) * Global._GlobalData.ShellTotalNum);
            }

            Global.SaveJson($"{Global.strProjectPath}\\Global.json");

            thread.Abort();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            lblPeelrunCost.Text = "0";
            lblPeelOK.Text = "0";
            lblPeelNG.Text = "0";
            lblPeelTotal.Text = "0";

            lblShellrunCost.Text = "0";
            lblShellOK.Text = "0";
            lblShellNG.Text = "0";
            lblShellTotal.Text = "0";

            lblPeelYield.Text = "0";
            lblShellYield.Text = "0";


            // 将 UI 文本框中的值转换为数值并保存到 Global._GlobalData
            if (double.TryParse(lblPeelrunCost.Text, out double peelTime))
            {
                Global._GlobalData.LineTime = (float)peelTime;
            }

            if (int.TryParse(lblPeelOK.Text, out int peelOK))
            {
                Global._GlobalData.lineOKNum = peelOK;
            }

            if (int.TryParse(lblPeelNG.Text, out int peelNG))
            {
                Global._GlobalData.lineNGNum = peelNG;
            }

            if (int.TryParse(lblPeelTotal.Text, out int peelTotal))
            {
                Global._GlobalData.lineTotalNum = peelTotal;
            }

            if (double.TryParse(lblShellrunCost.Text, out double shellTime))
            {
                Global._GlobalData.ShellTime = (float)shellTime;
            }

            if (int.TryParse(lblShellOK.Text, out int shellOK))
            {
                Global._GlobalData.ShellOKNum = shellOK;
            }

            if (int.TryParse(lblShellNG.Text, out int shellNG))
            {
                Global._GlobalData.ShellNGNum = shellNG;
            }

            if (int.TryParse(lblShellTotal.Text, out int shellTotal))
            {
                Global._GlobalData.ShellTotalNum = shellTotal;
            }

            // 如果需要反向写入良率值（根据 OK 数量和总数量计算的百分比）
            if (double.TryParse(lblPeelYield.Text, out double peelYield) && Global._GlobalData.lineTotalNum > 0)
            {
                Global._GlobalData.lineOKNum = (int)((peelYield / 100) * Global._GlobalData.lineTotalNum);
            }

            if (double.TryParse(lblShellYield.Text, out double shellYield) && Global._GlobalData.ShellTotalNum > 0)
            {
                Global._GlobalData.ShellOKNum = (int)((shellYield / 100) * Global._GlobalData.ShellTotalNum);
            }

            Global.SaveJson($"{Global.strProjectPath}\\Global.json");
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            if (btn_run.Text == "运行")
            {
                this.IsRunMode = true;
                btn_run.ImageKey = "ic_停止运行_click.png";
                btn_run.Text = "停止";
            }
            else if (btn_run.Text == "停止")
            {
                this.IsRunMode = false;
                btn_run.ImageKey = "ic_play_单流程_click.png";
                btn_run.Text = "运行";
            }
        }

        private void cb_SaveRenderInOK_CheckedChanged(object sender, EventArgs e)
        {
            Global._GlobalData.SaveRenderInOK = this.cb_SaveRenderInOK.Checked ? 1 : 0;
        }

        private void cb_SaveRenderInNG_CheckedChanged(object sender, EventArgs e)
        {
            Global._GlobalData.SaveRenderInNG = this.cb_SaveRenderInNG.Checked ? 1 : 0;
        }

        private void cb_SaveOrgInOK_CheckedChanged(object sender, EventArgs e)
        {
            Global._GlobalData.SaveOrgInOK = this.cb_SaveOrgInOK.Checked ? 1 : 0;
        }

        private void cb_SaveOrgInNG_CheckedChanged(object sender, EventArgs e)
        {
            Global._GlobalData.SaveOrgInNG = this.cb_SaveOrgInNG.Checked ? 1 : 0;
        }
    }
}
