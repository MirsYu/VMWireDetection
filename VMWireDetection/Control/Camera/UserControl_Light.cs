using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace VMWireDetection
{
    public partial class UserControl_Light : UserControl
    {
        public struct LightParam
        {
            public int LightPort;
            public int DurationTime;
            public int LightValue;
            public int LightState;
            public int RisingEdge;
            public int PortIndex;
        }

        public LightParam lightParam = new LightParam();

        private VC2000_Light light = new VC2000_Light();
        public UserControl_Light()
        {
            InitializeComponent();

            InitWindow();
        }

        public void InitWindow()
        {
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

            cbLightPort.Items.Clear();
            foreach (var item in Enum.GetValues(typeof(PORT_LIGHT_NUMBER)))
            {
                cbLightPort.Items.Add(item);
            }
            cbLightPort.SelectedItem = PORT_LIGHT_NUMBER.Port1;

            //初始化光源触发输入
            cbLightTriggerSource.Items.Clear();
            foreach (var item in Enum.GetValues(typeof(PORT_LIGHT_TRIGGERSOURCE)))
            {
                cbLightTriggerSource.Items.Add(item);
            }
            cbLightTriggerSource.SelectedItem = PORT_LIGHT_TRIGGERSOURCE.None;
        }

        public bool UpdateLight()
        {
            bool bResult = false;
            bResult = light.SetLightTrigger(lightParam.LightPort, lightParam.PortIndex);
            bResult = light.SetLight(lightParam.LightPort, lightParam.DurationTime, lightParam.LightValue, lightParam.LightState, lightParam.RisingEdge, lightParam.PortIndex);
            return bResult;
        }

        public bool CloseLight()
        {
            bool bResult = false;
            bResult = light.SetLightTrigger(lightParam.LightPort, lightParam.PortIndex);
            bResult = light.SetLight(lightParam.LightPort, lightParam.DurationTime, 0, lightParam.LightState, lightParam.RisingEdge, lightParam.PortIndex);
            return bResult;
        }

        public void SaveJson(string path)
        {
            lightParam.PortIndex = cbComNo.SelectedIndex;
            lightParam.LightPort = cbLightPort.SelectedIndex;
            lightParam.DurationTime = int.Parse(textLightTime.Text);
            lightParam.LightValue = (trackBarLight.Value);
            if (true == rdbLightStateOn.Checked)
            {
                lightParam.LightState = (UInt16)CIOControllerSDK.MV_IO_LIGHTSTATE.MV_IO_LIGHTSTATE_ON;
            }
            else if (true == rdbLightStateOff.Checked)
            {
                lightParam.LightState = (UInt16)CIOControllerSDK.MV_IO_LIGHTSTATE.MV_IO_LIGHTSTATE_OFF;
            }
            else
            {
                lightParam.LightState = (UInt16)CIOControllerSDK.MV_IO_LIGHTSTATE.MV_IO_LIGHTSTATE_TRIGGER;
            }
            lightParam.RisingEdge = (UInt16)(rbRisingEdge.Checked ? CIOControllerSDK.MV_IO_EDGE.MV_IO_EDGE_RISING : CIOControllerSDK.MV_IO_EDGE.MV_IO_EDGE_DOWN);
            var json = JsonConvert.SerializeObject(lightParam, Formatting.Indented);
            File.WriteAllText(path, json);
            UpdateLight();
        }

        public void LoadJson(string path)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                lightParam = JsonConvert.DeserializeObject<LightParam>(json);
                light.Open(lightParam.PortIndex);
                cbComNo.SelectedIndex = lightParam.PortIndex;
                cbLightPort.SelectedIndex = lightParam.LightPort;
                textLightTime.Text = lightParam.DurationTime.ToString();
                trackBarLight.Value = lightParam.LightValue;

                if ((1 == (int)(LIGHT_STATE)lightParam.LightState))
                {
                    rdbLightStateOn.Checked = true;
                }
                else if ((2 == (int)(LIGHT_STATE)lightParam.LightState))
                {
                    rdbLightStateOff.Checked = true;
                }
                else
                {
                    rdbLightStateTrigger.Checked = true;
                }

                if ((0x01 == (int)(EDGE_TYPE)lightParam.RisingEdge))
                {
                    rbRisingEdge.Checked = true;
                }
                else
                {
                    rbDownEdge.Checked = true;
                }
                UpdateLight();
            }
            else
            {
                lightParam.PortIndex = 0;
                lightParam.LightPort = 0;
                lightParam.DurationTime = 0;
                lightParam.LightValue = 0;
                light.Open(lightParam.PortIndex);
                cbComNo.SelectedIndex = lightParam.PortIndex;
                cbLightPort.SelectedIndex = lightParam.LightPort;
                textLightTime.Text = lightParam.DurationTime.ToString();
                trackBarLight.Value = lightParam.LightValue;

                if ((1 == (int)(LIGHT_STATE)lightParam.LightState))
                {
                    rdbLightStateOn.Checked = true;
                }
                else if ((2 == (int)(LIGHT_STATE)lightParam.LightState))
                {
                    rdbLightStateOff.Checked = true;
                }
                else
                {
                    rdbLightStateTrigger.Checked = true;
                }

                if ((0x01 == (int)(EDGE_TYPE)lightParam.RisingEdge))
                {
                    rbRisingEdge.Checked = true;
                }
                else
                {
                    rbDownEdge.Checked = true;
                }
                SaveJson(path);
            }
        }
    }
}
