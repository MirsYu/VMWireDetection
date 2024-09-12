using System;
using System.Threading;
using System.Windows.Forms;
using VisionDesigner;

namespace VMWireDetection
{
    public partial class Form_LiveCam : Form
    {
        public string strCameraSN = "DefaultCamera";
        public string strLightName = "DefaultLight";
        private Thread thread = null;
        private bool isRunning = false;
        private CancellationTokenSource cancellationTokenSource;


        public Form_LiveCam()
        {
            InitializeComponent();

            this.cbFLowName.Items.Clear();
            this.cbFLowName.Items.Add(Global._Wire.strFlowName);
            this.cbFLowName.Items.Add(Global._Casing.strFlowName);
            this.panel1.Controls.Add(Global._Cam);
            this.panel2.Controls.Add(Global._Light);
        }

        // 定义一个回调函数，当采集到图像时调用
        private void OnImageCaptured(CMvdImage img)
        {
            if (img != null)
            {
                // 在UI线程中更新图像显示
                this.Invoke(new Action(() =>
                {
                    this.userControl_CamShow1.imgCam = img;
                    this.userControl_CamShow1.SetImage();
                }));
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            // 保存配置文件
            Global._Cam.SaveJson($"{Global.strProjectPath}\\{strCameraSN}.json");
            Global._Light.SaveJson($"{Global.strProjectPath}\\{strLightName}.json");
        }

        private void Form_LiveCam_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (thread != null && cancellationTokenSource != null)
                {
                    // 发出取消信号
                    cancellationTokenSource.Cancel();

                    // 等待线程结束
                    if (thread.IsAlive)
                    {
                        thread.Abort(); // 此时应该可以正常退出，不会无限阻塞
                    }

                    thread = null; // 清空线程引用
                }
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

        private void Form_LiveCam_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                // 加载配置文件
                this.cbFLowName.SelectedIndex = 0;
                Global._Cam.LoadJson($"{Global.strProjectPath}\\{strCameraSN}.json");
                Global._Light.LoadJson($"{Global.strProjectPath}\\{strLightName}.json");
                this.userControl_CamShow1.strCamSN = strCameraSN;
                // 设置回调函数
                Global._Cam.OnImageCaptured += OnImageCaptured;

                if (thread == null || cancellationTokenSource == null || cancellationTokenSource.IsCancellationRequested)
                {
                    cancellationTokenSource = new CancellationTokenSource();
                    CancellationToken token = cancellationTokenSource.Token;

                    thread = new Thread(() =>
                    {
                        while (!token.IsCancellationRequested)
                        {
                            Thread.Sleep(1); // 减少CPU使用

                            // 触发相机执行
                            Global._Cam.TriggerExec();

                            // 可选：如果是长时间操作，定期检查取消标志
                            if (token.IsCancellationRequested)
                            {
                                break;
                            }
                        }
                    });

                    thread.IsBackground = true;
                    thread.Start();
                }
            }

        }

        private void cbFLowName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.strCameraSN = $"{cbFLowName.Text}_Cam";
            this.strLightName = $"{cbFLowName.Text}_Light";

            if (thread != null && cancellationTokenSource != null)
            {
                // 发出取消信号
                cancellationTokenSource.Cancel();

                // 等待线程结束
                if (thread.IsAlive)
                {
                    thread.Abort(); // 此时应该可以正常退出，不会无限阻塞
                }

                thread = null; // 清空线程引用
            }

            // 加载配置文件
            Global._Cam.LoadJson($"{Global.strProjectPath}\\{strCameraSN}.json");
            Global._Light.LoadJson($"{Global.strProjectPath}\\{strLightName}.json");
            this.userControl_CamShow1.strCamSN = strCameraSN;
            // 设置回调函数
            Global._Cam.OnImageCaptured += OnImageCaptured;

            // 初始化相机触发
            if (thread == null || cancellationTokenSource == null || cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource = new CancellationTokenSource();
                CancellationToken token = cancellationTokenSource.Token;

                thread = new Thread(() =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        Thread.Sleep(1); // 减少CPU使用

                        // 触发相机执行
                        Global._Cam.TriggerExec();

                        // 可选：如果是长时间操作，定期检查取消标志
                        if (token.IsCancellationRequested)
                        {
                            break;
                        }
                    }
                });

                thread.IsBackground = true;
                thread.Start();
            }
        }
    }
}
