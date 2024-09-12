using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace VMWireDetection
{
    public partial class Form_RTU : Form
    {
        public int[] m_nBauderate = { 9600, 115200 };

        public int[] m_nDatabits = { 8 };

        public enum PARITY_SCHEME
        {
            None,
            Odd,
            Even,
            Mark,
            Space
        };

        public float[] m_fStopbits = { 0f, 1f, 2f, 1.5f };

        public RTUParam param = new RTUParam();

        public string RTUPath = "DefaultRTU";

        public struct AddressInfo
        {
            public string AddressType;
            public string Address;
        }

        public struct RTUParam
        {
            public string PortName;
            public int BaudRate;
            public int DataBits;
            public StopBits StopBits;
            public Parity Parity;
            public List<AddressInfo> ReadAddress;
            public List<AddressInfo> WriteAddress;
        }

        public HSL_ModbusRTU RTU = new HSL_ModbusRTU();

        private bool isRunning = false;

        Thread thread;

        Thread threadRun;

        public Form_RTU()
        {
            InitializeComponent();
            InitSerialWindow();
            LoadJson($"{Global.strProjectPath}\\RTU.json");
            Init();
        }

        private void InitSerialWindow()
        {
            /******ch:串口控制区 | en:Serial port control******/
            cbBauderate.Items.Clear();
            for (int i = 0; i < m_nBauderate.Length; ++i)
            {
                cbBauderate.Items.Add(m_nBauderate[i]);
            }
            cbBauderate.SelectedItem = 115200;

            // DataBits
            cbDataBits.Items.Clear();
            for (int i = 0; i < m_nDatabits.Length; ++i)
            {
                cbDataBits.Items.Add(m_nDatabits[i]);
            }
            cbDataBits.SelectedItem = 8;

            // StopBits
            cbStopBits.Items.Clear();
            foreach (var item in Enum.GetValues(typeof(StopBits)))
            {
                cbStopBits.Items.Add(item);
            }
            cbStopBits.SelectedItem = 1f;

            // ParityBits
            cbParityBits.Items.Clear();
            foreach (var item in Enum.GetValues(typeof(Parity)))
            {
                cbParityBits.Items.Add(item);
            }
            cbParityBits.SelectedItem = Parity.None;

            // ComName
            cbComNo.Items.Clear();
            //VC2000光源 串口从com1 开始；
            cbComNo.Items.Add("Com1");
            cbComNo.Items.Add("Com2");
            cbComNo.Items.Add("Com3");
            cbComNo.Items.Add("Com4");
            cbComNo.Items.Add("Com5");
            cbComNo.Items.Add("Com6");
            cbComNo.Items.Add("Com7");
            cbComNo.Items.Add("Com8");
            cbComNo.Items.Add("Com9");
            cbComNo.Items.Add("Com10");
            cbComNo.Items.Add("Com11");
            cbComNo.Items.Add("Com12");
            cbComNo.SelectedIndex = 0; //默认连接COM1

            param.ReadAddress = new List<AddressInfo>();
            param.WriteAddress = new List<AddressInfo>();
        }

        private bool Init()
        {
            bool bResult = RTU.Open(param.PortName, param.BaudRate, param.DataBits, param.StopBits, param.Parity);
            if (!bResult)
            {
                LogManager.WriteError("Failed to open RTU connection.");
                return bResult;
            }

            isRunning = true;
            if (thread == null)
            {
                thread = new Thread(() =>
                {
                    while (isRunning) // 添加一个循环以定期刷新数据
                    {
                        if (!isRunning) { break; }
                        for (int i = 0; i < param.ReadAddress.Count; i++)
                        {
                            int iResult = -1;
                            bool readSuccess = RTU.Read<Int32>(param.ReadAddress[i].Address, ref iResult);

                            if (readSuccess)
                            {
                                // 使用 Invoke 在 UI 线程上更新 TextBox
                                UpdateTextBox(iResult);
                                if (param.ReadAddress[i].Address == "1886" && iResult == 1)
                                {
                                    if (RTU.Write<Int32>(param.ReadAddress[i].Address, 0))
                                    {
                                        int iLight = -1;
                                        RTU.Read<Int32>("1880", ref iLight);
                                        if (iLight == 1)
                                        {
                                            Global._Main.Invoke((MethodInvoker)delegate
                                            {
                                                Global._Wire.Run();
                                            });
                                        }
                                        else if (iLight == 2)
                                        {
                                            Global._Main.Invoke((MethodInvoker)delegate
                                            {
                                                Global._Casing.Run();
                                            });
                                        }
                                        else
                                        {
                                            LogManager.WriteError("PLC Light Address Value Error");
                                        }
                                    }

                                }
                            }
                            else
                            {
                                // 处理读取失败的情况，例如记录日志或显示错误消息
                            }
                        }

                        // 添加一个适当的延时以避免线程占用过多资源
                        Thread.Sleep(1); // 例如，每秒刷新一次
                    }
                });

                thread.IsBackground = true; // 设置为后台线程，使其在主线程结束时自动终止
                thread.Start();
            }
            return true;
        }

        private void UpdateTextBox(int value)
        {
            // 使用 Invoke 在 UI 线程上更新 TextBox 内容
            if (this.tb_Adres4.InvokeRequired)
            {
                this.tb_Adres4.Invoke(new Action(() => this.tb_Adres4.Text = value.ToString()));
            }
            else
            {
                this.tb_Adres4.Text = value.ToString();
            }
        }

        private void btn_Write_Click(object sender, EventArgs e)
        {
            RTU.Write<Int32>(param.WriteAddress[0].Address, int.Parse(tb_Adres1.Text));
            RTU.Write<Int32>(param.WriteAddress[1].Address, int.Parse(tb_Adres2.Text));
            RTU.Write<Int32>(param.WriteAddress[2].Address, int.Parse(tb_Adres3.Text));
        }

        public void SaveJson(string path)
        {
            param.PortName = cbComNo.Text;
            param.BaudRate = int.Parse(cbBauderate.Text);
            param.DataBits = int.Parse(cbDataBits.Text);
            param.StopBits = (StopBits)cbStopBits.SelectedIndex;
            param.Parity = (Parity)cbParityBits.SelectedIndex;
            param.ReadAddress.Clear();
            param.WriteAddress.Clear();
            AddressInfo info = new AddressInfo();
            info.Address = "1886";
            info.AddressType = "Int32";
            param.ReadAddress.Add(info);
            info.Address = "1880";
            info.AddressType = "Int32";
            param.WriteAddress.Add(info);
            info.Address = "1882";
            info.AddressType = "Int32";
            param.WriteAddress.Add(info);
            info.Address = "1884";
            info.AddressType = "Int32";
            param.WriteAddress.Add(info);

            var json = JsonConvert.SerializeObject(param, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public void LoadJson(string path)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                param = JsonConvert.DeserializeObject<RTUParam>(json);
                cbComNo.Text = param.PortName;
                cbBauderate.Text = param.BaudRate.ToString();
                cbDataBits.Text = param.DataBits.ToString();
                cbStopBits.SelectedIndex = (int)param.StopBits;
                cbParityBits.SelectedIndex = (int)param.Parity;
            }
            else
            {
                param.PortName = "Com3";
                param.BaudRate = 115200;
                param.DataBits = 8;
                param.StopBits = StopBits.None;
                param.Parity = Parity.None;
                cbComNo.Text = param.PortName;
                cbBauderate.Text = param.BaudRate.ToString();
                cbDataBits.Text = param.DataBits.ToString();
                cbStopBits.SelectedIndex = (int)param.StopBits;
                cbParityBits.SelectedIndex = (int)param.Parity;
                SaveJson(path);
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveJson($"{Global.strProjectPath}\\RTU.json");
            Init();
        }

        private void Form_RTU_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //thread.Abort();
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

        private void btnOpenOrCloseCom_Click(object sender, EventArgs e)
        {
            SaveJson($"{Global.strProjectPath}\\RTU.json");
            if (Init() && btnOpenOrCloseCom.Text == "打开串口")
            {
                btnOpenOrCloseCom.Text = "关闭串口";
            }
            else if (btnOpenOrCloseCom.Text == "关闭串口")
            {
                btnOpenOrCloseCom.Text = "打开串口";
            }
        }

        private void Form_RTU_VisibleChanged(object sender, EventArgs e)
        {
            LoadJson($"{Global.strProjectPath}\\RTU.json");
            Init();
        }
    }
}
