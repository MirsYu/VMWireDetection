using MvCamCtrl.NET;
using MvCamCtrl.NET.CameraParams;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VisionDesigner;

namespace VMWireDetection
{
    public partial class UserCameraControl : UserControl
    {
        private CCamera m_MyCamera = new CCamera();
        List<CCameraInfo> m_ltDeviceList = new List<CCameraInfo>();

        public event Action<CMvdImage> OnImageCaptured;

        bool _isGrab = false;

        // ch:Bitmap | en:Bitmap
        Bitmap m_pcBitmap = null;
        PixelFormat m_enBitmapPixelFormat = PixelFormat.DontCare;

        public UserCameraControl()
        {
            InitializeComponent();
            this.cb_TriggerSource.Items.Clear();
            this.cb_TriggerSource.Items.Add("Line0");
            this.cb_TriggerSource.Items.Add("Line2");
            this.cb_TriggerSource.Items.Add("Software");
            this.cbDeviceList.Items.Clear();
            this.cbDeviceList.Items.Add("Close");
            DeviceListAcq();
        }


        /// <summary>
        /// 枚举设备
        /// </summary>
        public void DeviceListAcq()
        {
            // ch:创建设备列表 | en:Create Device List
            System.GC.Collect();
            cbDeviceList.Items.Clear();
            this.cbDeviceList.Items.Add("Close");
            m_ltDeviceList.Clear();

            int nRet = CSystem.EnumDevices(CSystem.MV_GIGE_DEVICE | CSystem.MV_USB_DEVICE, ref m_ltDeviceList);
            if (0 != nRet)
            {
                //LogManager.WriteError("Enumerate devices fail!");
                return;
            }

            // ch:在窗体列表中显示设备名 | en:Display device name in the form list
            for (int i = 0; i < m_ltDeviceList.Count; i++)
            {
                if (m_ltDeviceList[i].nTLayerType == CSystem.MV_GIGE_DEVICE)
                {
                    CGigECameraInfo gigeInfo = (CGigECameraInfo)m_ltDeviceList[i];

                    if (gigeInfo.UserDefinedName != "")
                    {
                        cbDeviceList.Items.Add("GEV: " + gigeInfo.UserDefinedName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                    else
                    {
                        cbDeviceList.Items.Add("GEV: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                }
                else if (m_ltDeviceList[i].nTLayerType == CSystem.MV_USB_DEVICE)
                {
                    CUSBCameraInfo usbInfo = (CUSBCameraInfo)m_ltDeviceList[i];
                    if (usbInfo.UserDefinedName != "")
                    {
                        cbDeviceList.Items.Add("U3V: " + usbInfo.UserDefinedName + " (" + usbInfo.chSerialNumber + ")");
                    }
                    else
                    {
                        cbDeviceList.Items.Add("U3V: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
                    }
                }
            }
            m_ltDeviceList.Insert(0, new CCameraInfo());
            // ch:选择第一项 | en:Select the first item
            if (m_ltDeviceList.Count != 0)
            {
                cbDeviceList.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        public void OpenDevices()
        {
            if (m_ltDeviceList.Count == 0 || cbDeviceList.SelectedIndex == -1)
            {
                ShowErrorMsg("No device, please select", 0);
                return;
            }

            // ch:获取选择的设备信息 | en:Get selected device information
            CCameraInfo device = m_ltDeviceList[cbDeviceList.SelectedIndex];

            // ch:打开设备 | en:Open device
            if (m_MyCamera == null || m_MyCamera.GetCameraHandle() == IntPtr.Zero)
            {
                m_MyCamera = new CCamera();
                if (null == m_MyCamera)
                {
                    return;
                }
            }

            int nRet = m_MyCamera.CreateHandle(ref device);
            if (CErrorDefine.MV_OK != nRet)
            {
                return;
            }

            nRet = m_MyCamera.OpenDevice();
            if (CErrorDefine.MV_OK != nRet)
            {
                m_MyCamera.DestroyHandle();
                ShowErrorMsg("Device open fail!", nRet);
                return;
            }

            // ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)
            if (device.nTLayerType == CSystem.MV_GIGE_DEVICE)
            {
                int nPacketSize = m_MyCamera.GIGE_GetOptimalPacketSize();
                if (nPacketSize > 0)
                {
                    nRet = m_MyCamera.SetIntValue("GevSCPSPacketSize", (uint)nPacketSize);
                    if (nRet != CErrorDefine.MV_OK)
                    {
                        //LogManager.WriteError("Set Packet Size failed!");
                    }
                }
            }
        }

        public void CloseDevices()
        {
            // ch:关闭设备 | en:Close Device
            m_MyCamera.CloseDevice();
            m_MyCamera.DestroyHandle();
        }

        private void bnTriggerMode_CheckedChanged(object sender, EventArgs e)
        {
            // ch:打开触发模式 | en:Open Trigger Mode
            if (bnTriggerMode.Checked)
            {
                m_MyCamera.SetEnumValue("TriggerMode", (uint)MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);

                // ch:触发源选择:0 - Line0; | en:Trigger source select:0 - Line0;
                //           1 - Line1;
                //           2 - Line2;
                //           3 - Line3;
                //           4 - Counter;
                //           7 - Software;

                m_MyCamera.SetEnumValue("TriggerSource", (uint)MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
            }
        }

        private void bnContinuesMode_CheckedChanged(object sender, EventArgs e)
        {
            if (bnContinuesMode.Checked)
            {
                m_MyCamera.SetEnumValue("TriggerMode", (uint)MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);
            }
        }

        public void StartGrab()
        {
            // ch:前置配置 | en:pre-operation
            int nRet = NecessaryOperBeforeGrab();
            if (CErrorDefine.MV_OK != nRet)
            {
                return;
            }

            // ch:开始采集 | en:Start Grabbing
            nRet = m_MyCamera.StartGrabbing();
            if (CErrorDefine.MV_OK != nRet)
            {
                ShowErrorMsg("Start Grabbing Fail!", nRet);
                return;
            }
            _isGrab = true;
        }

        public void StopGrab()
        {
            // ch:停止采集 | en:Stop Grabbing
            int nRet = m_MyCamera.StopGrabbing();
            if (nRet != CErrorDefine.MV_OK)
            {
                ShowErrorMsg("Stop Grabbing Fail!", nRet);
            }
            _isGrab = false;
        }

        public bool DiviceConnecter()
        {
            CCameraInfo cCameraInfo = m_ltDeviceList[cbDeviceList.SelectedIndex];
            if (cCameraInfo.nTLayerType == 0 || !_isGrab)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public CMvdImage TriggerExec()
        {
            try
            {
                if (m_MyCamera.GetCameraHandle() != IntPtr.Zero)
                {
                    int nRet = m_MyCamera.SetEnumValue("TriggerMode", 1);
                    if (CErrorDefine.MV_OK != nRet)
                    {
                        ShowErrorMsg("Set TriggerMode Fail!", nRet);
                    }

                    nRet = m_MyCamera.SetEnumValue("TriggerSource", 7);
                    if (CErrorDefine.MV_OK != nRet)
                    {
                        ShowErrorMsg("Set TriggerSource Fail!", nRet);
                    }

                    nRet = m_MyCamera.SetCommandValue("TriggerSoftware");
                    if (CErrorDefine.MV_OK != nRet)
                    {
                        ShowErrorMsg("Trigger Software Fail!", nRet);
                    }

                    CFrameout pcFrameInfo = new CFrameout();

                    // 使用更短的超时，并处理超时情况
                    nRet = m_MyCamera.GetImageBuffer(ref pcFrameInfo, 1000); // 减少超时时间
                    if (nRet == CErrorDefine.MV_OK)
                    {
                        CMvdImage image = CCDToCMvdImage(pcFrameInfo);
                        m_MyCamera.FreeImageBuffer(ref pcFrameInfo);
                        OnImageCaptured?.Invoke(image);
                        return image;
                    }
                    else
                    {
                        ShowErrorMsg("GetImageBuffer Fail!", nRet);
                        // 确保在失败时释放缓冲区
                        if (pcFrameInfo.Image != null)
                        {
                            m_MyCamera.FreeImageBuffer(ref pcFrameInfo);
                        }
                        return LoadEmbeddedImage();
                    }
                }
                else
                {
                    // 处理相机句柄无效的情况，读取嵌入的图像资源
                    return LoadEmbeddedImage();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMsg("Exception in TriggerExec!", 0);
            }
            finally
            {
                // 确保触发事件后释放资源
                GC.Collect(); // 使用 GC.Collect() 谨慎
            }

            return null;
        }

        private CMvdImage LoadEmbeddedImage()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "VMWireDetection.BaseImg.bmp";
            CMvdImage imgResult = new CMvdImage();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    CMvdImage img = new CMvdImage();
                    Bitmap bitmap = new Bitmap(stream);
                    ConvertBitmap2MVDImage(bitmap, img);
                    OnImageCaptured?.Invoke(img);
                    imgResult = img.Clone();
                }
                else
                {
                    Console.WriteLine("资源未找到!");
                }
            }
            return imgResult;
        }


        public CMvdImage CCDToCMvdImage(CFrameout frame)
        {
            VisionDesigner.CMvdImage cMvdImage = new VisionDesigner.CMvdImage();
            VisionDesigner.MVD_IMAGE_DATA_INFO stImageData = new VisionDesigner.MVD_IMAGE_DATA_INFO();
            stImageData.stDataChannel[0].nLen = (uint)(frame.Image.FrameLen);
            stImageData.stDataChannel[0].nSize = (uint)(frame.Image.FrameLen);
            byte[] m_BufForDriver1 = new byte[frame.Image.FrameLen];
            //数据Copy
            Marshal.Copy(frame.Image.ImageAddr, m_BufForDriver1, 0, (int)frame.Image.FrameLen);
            stImageData.stDataChannel[0].arrDataBytes = m_BufForDriver1;
            if (frame.Image.PixelType == MvGvspPixelType.PixelType_Gvsp_Mono8)
            {
                stImageData.stDataChannel[0].nRowStep = (uint)frame.Image.Width;
                //初始化CMvdImage
                cMvdImage.InitImage((uint)frame.Image.Width, (uint)frame.Image.Height, MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08, stImageData);
            }
            else if (frame.Image.PixelType == MvGvspPixelType.PixelType_Gvsp_RGB8_Packed)
            {
                stImageData.stDataChannel[0].nRowStep = (uint)frame.Image.Width * 3;
                //初始化CMvdImage
                cMvdImage.InitImage((uint)frame.Image.Width, (uint)frame.Image.Height, MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3, stImageData);
            }
            return cMvdImage;
        }

        private Int32 NecessaryOperBeforeGrab()
        {
            // ch:取图像宽 | en:Get Iamge Width
            CIntValue pcWidth = new CIntValue();
            int nRet = m_MyCamera.GetIntValue("Width", ref pcWidth);
            if (CErrorDefine.MV_OK != nRet)
            {
                ShowErrorMsg("Get Width Info Fail!", nRet);
                return nRet;
            }
            // ch:取图像高 | en:Get Iamge Height
            CIntValue pcHeight = new CIntValue();
            nRet = m_MyCamera.GetIntValue("Height", ref pcHeight);
            if (CErrorDefine.MV_OK != nRet)
            {
                ShowErrorMsg("Get Height Info Fail!", nRet);
                return nRet;
            }
            // ch:取像素格式 | en:Get Pixel Format
            CEnumValue pcPixelFormat = new CEnumValue();
            nRet = m_MyCamera.GetEnumValue("PixelFormat", ref pcPixelFormat);
            if (CErrorDefine.MV_OK != nRet)
            {
                ShowErrorMsg("Get Pixel Format Fail!", nRet);
                return nRet;
            }

            // ch:设置bitmap像素格式
            if ((Int32)MvGvspPixelType.PixelType_Gvsp_Undefined == (Int32)pcPixelFormat.CurValue)
            {
                ShowErrorMsg("Unknown Pixel Format!", CErrorDefine.MV_E_UNKNOW);
                return CErrorDefine.MV_E_UNKNOW;
            }
            else if (IsMono((MvGvspPixelType)pcPixelFormat.CurValue))
            {
                m_enBitmapPixelFormat = PixelFormat.Format8bppIndexed;
            }
            else
            {
                m_enBitmapPixelFormat = PixelFormat.Format24bppRgb;
            }

            if (null != m_pcBitmap)
            {
                m_pcBitmap.Dispose();
                m_pcBitmap = null;
            }
            m_pcBitmap = new Bitmap((Int32)pcWidth.CurValue, (Int32)pcHeight.CurValue, m_enBitmapPixelFormat);

            // ch:Mono8格式，设置为标准调色板 | en:Set Standard Palette in Mono8 Format
            if (PixelFormat.Format8bppIndexed == m_enBitmapPixelFormat)
            {
                ColorPalette palette = m_pcBitmap.Palette;
                for (int i = 0; i < palette.Entries.Length; i++)
                {
                    palette.Entries[i] = Color.FromArgb(i, i, i);
                }
                m_pcBitmap.Palette = palette;
            }

            return CErrorDefine.MV_OK;
        }

        private Boolean IsMono(MvGvspPixelType enPixelType)
        {
            switch (enPixelType)
            {
                case MvGvspPixelType.PixelType_Gvsp_Mono1p:
                case MvGvspPixelType.PixelType_Gvsp_Mono2p:
                case MvGvspPixelType.PixelType_Gvsp_Mono4p:
                case MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MvGvspPixelType.PixelType_Gvsp_Mono8_Signed:
                case MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_Mono14:
                case MvGvspPixelType.PixelType_Gvsp_Mono16:
                    return true;
                default:
                    return false;
            }
        }

        private void ShowErrorMsg(string csMessage, int nErrorNum)
        {
            string errorMsg;
            if (nErrorNum == 0)
            {
                errorMsg = csMessage;
            }
            else
            {
                errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
            }

            switch (nErrorNum)
            {
                case CErrorDefine.MV_E_HANDLE: errorMsg += " Error or invalid handle "; break;
                case CErrorDefine.MV_E_SUPPORT: errorMsg += " Not supported function "; break;
                case CErrorDefine.MV_E_BUFOVER: errorMsg += " Cache is full "; break;
                case CErrorDefine.MV_E_CALLORDER: errorMsg += " Function calling order error "; break;
                case CErrorDefine.MV_E_PARAMETER: errorMsg += " Incorrect parameter "; break;
                case CErrorDefine.MV_E_RESOURCE: errorMsg += " Applying resource failed "; break;
                case CErrorDefine.MV_E_NODATA: errorMsg += " No data "; break;
                case CErrorDefine.MV_E_PRECONDITION: errorMsg += " Precondition error, or running environment changed "; break;
                case CErrorDefine.MV_E_VERSION: errorMsg += " Version mismatches "; break;
                case CErrorDefine.MV_E_NOENOUGH_BUF: errorMsg += " Insufficient memory "; break;
                case CErrorDefine.MV_E_UNKNOW: errorMsg += " Unknown error "; break;
                case CErrorDefine.MV_E_GC_GENERIC: errorMsg += " General error "; break;
                case CErrorDefine.MV_E_GC_ACCESS: errorMsg += " Node accessing condition error "; break;
                case CErrorDefine.MV_E_ACCESS_DENIED: errorMsg += " No permission "; break;
                case CErrorDefine.MV_E_BUSY: errorMsg += " Device is busy, or network disconnected "; break;
                case CErrorDefine.MV_E_NETER: errorMsg += " Network error "; break;
            }
            LogManager.WriteError(errorMsg);
            //System.Windows.Forms.MessageBox.Show(errorMsg, "PROMPT");
        }

        private void UpdateUI()
        {
            CamParam param = new CamParam();
            CStringValue stringTemp = new CStringValue();
            CFloatValue floatTemp = new CFloatValue();

            int nRet = m_MyCamera.GetStringValue("DeviceUserID", ref stringTemp);
            if (CErrorDefine.MV_OK != nRet)
            {
                ShowErrorMsg("Get DeviceUserID Fail!", nRet);
                param.userName = "NULL";
            }
            param.userName = stringTemp.CurValue;

            nRet = m_MyCamera.GetFloatValue("ExposureTime", ref floatTemp);
            if (CErrorDefine.MV_OK != nRet)
            {
                ShowErrorMsg("Get ExposureTime Fail!", nRet);
                param.ExposureTime = 5000;
            }
            param.ExposureTime = floatTemp.CurValue;

            CCameraInfo cCameraInfo = m_ltDeviceList[cbDeviceList.SelectedIndex];
            if (cCameraInfo.nTLayerType == CSystem.MV_GIGE_DEVICE)
            {
                CGigECameraInfo gigeInfo = (CGigECameraInfo)cCameraInfo;
                param.SN = gigeInfo.chSerialNumber;
            }
            else if (cCameraInfo.nTLayerType == CSystem.MV_USB_DEVICE)
            {
                CUSBCameraInfo usbInfo = (CUSBCameraInfo)cCameraInfo;
                param.SN = usbInfo.chSerialNumber;
            }


            CEnumValue cEnumValue = new CEnumValue();
            nRet = m_MyCamera.GetEnumValue("TriggerSource", ref cEnumValue);
            if (CErrorDefine.MV_OK != nRet)
            {
                ShowErrorMsg("Get TriggerSource Fail!", nRet);
                param.TriggerSource = 7;
            }
            param.TriggerSource = cEnumValue.CurValue;

            CEnumValue cEnumValue1 = new CEnumValue();
            nRet = m_MyCamera.GetEnumValue("TriggerMode", ref cEnumValue1);
            if (CErrorDefine.MV_OK != nRet)
            {
                ShowErrorMsg("Get TriggerMode Fail!", nRet);
                param.TriggerMode = 1;
            }
            param.TriggerMode = cEnumValue1.CurValue;


            this.tb_CameraSN.Text = param.SN;
            this.tb_CamExposureTime.Text = param.ExposureTime.ToString();
            if (param.TriggerMode == 1)
            {
                bnTriggerMode.Select();
            }
            else if (param.TriggerMode == 0)
            {
                bnContinuesMode.Select();
            }

            switch (param.TriggerSource)
            {
                case 0:
                    this.cb_TriggerSource.SelectedIndex = 0;
                    break;
                case 2:
                    this.cb_TriggerSource.SelectedIndex = 1;
                    break;
                case 7:
                    this.cb_TriggerSource.SelectedIndex = 2;
                    break;
                default:
                    break;
            }
        }

        private void cb_TriggerSource_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (cb_TriggerSource.SelectedIndex)
            {
                case 0:
                    m_MyCamera.SetEnumValue("TriggerSource", 0);
                    break;
                case 1:
                    m_MyCamera.SetEnumValue("TriggerSource", 2);
                    break;
                case 2:
                    m_MyCamera.SetEnumValue("TriggerSource", 7);
                    break;
                default:
                    break;
            }
        }

        CamParam param = new CamParam();
        public void SaveJson(string path)
        {

            CStringValue stringTemp = new CStringValue();
            CFloatValue floatTemp = new CFloatValue();

            int nRet = m_MyCamera.GetStringValue("DeviceUserID", ref stringTemp);
            if (CErrorDefine.MV_OK != nRet)
            {
                //ShowErrorMsg("GEt DeviceUserID Fail!", nRet);
                param.userName = "NULL";
            }
            param.userName = stringTemp.CurValue;

            nRet = m_MyCamera.GetFloatValue("ExposureTime", ref floatTemp);
            if (CErrorDefine.MV_OK != nRet)
            {
                //ShowErrorMsg("GEt ExposureTime Fail!", nRet);
                param.ExposureTime = 5000;
            }
            param.ExposureTime = floatTemp.CurValue;

            CCameraInfo cCameraInfo = m_ltDeviceList[cbDeviceList.SelectedIndex];
            if (cCameraInfo.nTLayerType == CSystem.MV_GIGE_DEVICE)
            {
                CGigECameraInfo gigeInfo = (CGigECameraInfo)cCameraInfo;
                param.SN = gigeInfo.chSerialNumber;
            }
            else if (cCameraInfo.nTLayerType == CSystem.MV_USB_DEVICE)
            {
                CUSBCameraInfo usbInfo = (CUSBCameraInfo)cCameraInfo;
                param.SN = usbInfo.chSerialNumber;
            }

            CEnumValue cEnumValue = new CEnumValue();
            nRet = m_MyCamera.GetEnumValue("TriggerSource", ref cEnumValue);
            if (CErrorDefine.MV_OK != nRet)
            {
                //ShowErrorMsg("GEt TriggerSource Fail!", nRet);
                param.TriggerSource = 7;
            }
            param.TriggerSource = cEnumValue.CurValue;

            CEnumValue cEnumValue1 = new CEnumValue();
            nRet = m_MyCamera.GetEnumValue("TriggerMode", ref cEnumValue1);
            if (CErrorDefine.MV_OK != nRet)
            {
                //ShowErrorMsg("Get TriggerMode Fail!", nRet);
                param.TriggerMode = 1;
            }
            param.TriggerMode = cEnumValue1.CurValue;

            var json = JsonConvert.SerializeObject(param, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public void LoadJson(string path)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var jsonData = JsonConvert.DeserializeObject<CamParam>(json);
                CamParam _param = jsonData;

                // 如果相机序列号不相同，则更新设备列表并设置参数
                if (param.SN != jsonData.SN && jsonData.SN != null)
                {
                    for (int i = 0; i < cbDeviceList.Items.Count; i++)
                    {
                        if (cbDeviceList.Items[i].ToString().IndexOf(_param.SN) != -1)
                        {
                            cbDeviceList.SelectedIndex = i;
                            break;
                        }
                    }

                    // 设置相机参数
                    int nRet = m_MyCamera.SetFloatValue("ExposureTime", _param.ExposureTime);
                    if (CErrorDefine.MV_OK != nRet)
                    {
                        ShowErrorMsg("Set ExposureTime Fail!", nRet);
                    }

                    nRet = m_MyCamera.SetEnumValue("TriggerSource", _param.TriggerSource);
                    if (CErrorDefine.MV_OK != nRet)
                    {
                        ShowErrorMsg("Set TriggerSource Fail!", nRet);
                    }

                    nRet = m_MyCamera.SetEnumValue("TriggerMode", _param.TriggerMode);
                    if (CErrorDefine.MV_OK != nRet)
                    {
                        ShowErrorMsg("Set TriggerMode Fail!", nRet);
                    }

                    // 更新 UI
                    this.UpdateUI();
                }
                // 如果序列号相同，则只更新不同的参数
                else if (param.SN == jsonData.SN && jsonData.SN != null)
                {
                    bool parametersUpdated = false;

                    if (param.ExposureTime != jsonData.ExposureTime)
                    {
                        int nRet = m_MyCamera.SetFloatValue("ExposureTime", _param.ExposureTime);
                        if (CErrorDefine.MV_OK != nRet)
                        {
                            ShowErrorMsg("Set ExposureTime Fail!", nRet);
                        }
                        parametersUpdated = true;
                    }

                    if (param.TriggerSource != jsonData.TriggerSource)
                    {
                        int nRet = m_MyCamera.SetEnumValue("TriggerSource", _param.TriggerSource);
                        if (CErrorDefine.MV_OK != nRet)
                        {
                            ShowErrorMsg("Set TriggerSource Fail!", nRet);
                        }
                        parametersUpdated = true;
                    }

                    if (param.TriggerMode != jsonData.TriggerMode)
                    {
                        int nRet = m_MyCamera.SetEnumValue("TriggerMode", _param.TriggerMode);
                        if (CErrorDefine.MV_OK != nRet)
                        {
                            ShowErrorMsg("Set TriggerMode Fail!", nRet);
                        }
                        parametersUpdated = true;
                    }

                    // 如果有参数更新，更新 UI
                    if (parametersUpdated)
                    {
                        this.UpdateUI();
                    }
                }

                // 更新全局参数
                param = _param;
            }
            else
            {
                // 如果文件不存在，设置默认值
                cbDeviceList.SelectedIndex = 0;

                int nRet = m_MyCamera.SetFloatValue("ExposureTime", 1000);
                if (CErrorDefine.MV_OK != nRet)
                {
                    ShowErrorMsg("Set ExposureTime Fail!", nRet);
                }

                nRet = m_MyCamera.SetEnumValue("TriggerSource", 7);
                if (CErrorDefine.MV_OK != nRet)
                {
                    ShowErrorMsg("Set TriggerSource Fail!", nRet);
                }

                nRet = m_MyCamera.SetEnumValue("TriggerMode", 1);
                if (CErrorDefine.MV_OK != nRet)
                {
                    ShowErrorMsg("Set TriggerMode Fail!", nRet);
                }

                // 更新 UI 并保存新参数
                this.UpdateUI();
                this.SaveJson(path);
            }
        }



        public struct CamParam
        {
            public int Index;
            public float ExposureTime;
            public string userName;
            public string SN;
            public uint TriggerSource;
            public uint TriggerMode;
        }

        private int previousSelectedIndex = -1; // 用于存储上次选择的索引
        private void cbDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 如果当前选择与上次选择相同，则不执行操作
            if (cbDeviceList.SelectedIndex == previousSelectedIndex)
            {
                return;
            }

            // 更新上次选择的索引
            previousSelectedIndex = cbDeviceList.SelectedIndex;
            if (cbDeviceList.SelectedIndex != 0)
            {
                this.StopGrab();
                this.CloseDevices();
                this.OpenDevices();
                this.StartGrab();
                this.UpdateUI();
            }
            else
            {
                this.StopGrab();
                this.CloseDevices();
                this.tb_CameraSN.Text = "";
                this.tb_CamExposureTime.Text = "";
            }
        }


        private static void ConvertBitmap2MVDImage(Bitmap cBitmapImg, CMvdImage cMvdImg)
        {
            // 参数合法性判断
            if (null == cBitmapImg || null == cMvdImg)
            {
                throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_PARAMETER_ILLEGAL);
            }

            // 判断像素格式
            if (PixelFormat.Format8bppIndexed != cBitmapImg.PixelFormat && PixelFormat.Format24bppRgb != cBitmapImg.PixelFormat)
            {
                throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_SUPPORT);
            }

            Int32 nImageWidth = cBitmapImg.Width;
            Int32 nImageHeight = cBitmapImg.Height;
            Int32 nChannelNum = 0;
            BitmapData bitmapData = null;

            try
            {
                // 获取图像信息
                if (PixelFormat.Format8bppIndexed == cBitmapImg.PixelFormat) // 灰度图
                {
                    bitmapData = cBitmapImg.LockBits(new Rectangle(0, 0, nImageWidth, nImageHeight)
                                                                    , ImageLockMode.ReadOnly
                                                                    , PixelFormat.Format8bppIndexed);
                    cMvdImg.InitImage(Convert.ToUInt32(nImageWidth), Convert.ToUInt32(nImageHeight), MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08);
                    nChannelNum = 1;
                }
                else if (PixelFormat.Format24bppRgb == cBitmapImg.PixelFormat) // 彩色图
                {
                    bitmapData = cBitmapImg.LockBits(new Rectangle(0, 0, nImageWidth, nImageHeight)
                                                                , ImageLockMode.ReadOnly
                                                                , PixelFormat.Format24bppRgb);
                    cMvdImg.InitImage(Convert.ToUInt32(nImageWidth), Convert.ToUInt32(nImageHeight), MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3);
                    nChannelNum = 3;
                }

                // 考虑图像是否4字节对齐，bitmap要求4字节对齐，而mvdimage不要求对齐
                if (0 == nImageWidth % 4) // 4字节对齐时，直接拷贝
                {
                    Marshal.Copy(bitmapData.Scan0, cMvdImg.GetImageData().stDataChannel[0].arrDataBytes, 0, nImageWidth * nImageHeight * nChannelNum);
                }
                else // 按步长逐行拷贝
                {
                    // 每行实际占用字节数
                    Int32 nRowPixelByteNum = nImageWidth * nChannelNum + 4 - (nImageWidth * nChannelNum % 4);
                    // 每行首字节首地址
                    IntPtr bitmapDataRowPos = IntPtr.Zero;
                    for (int i = 0; i < nImageHeight; i++)
                    {
                        // 获取每行第一个像素值的首地址
                        bitmapDataRowPos = new IntPtr(bitmapData.Scan0.ToInt64() + nRowPixelByteNum * i);
                        Marshal.Copy(bitmapDataRowPos, cMvdImg.GetImageData().stDataChannel[0].arrDataBytes, i * nImageWidth * nChannelNum, nImageWidth * nChannelNum);
                    }
                }

                // bitmap彩色图按BGR存储，而MVDimg按RGB存储，改变存储顺序
                // 交换R和B
                if (PixelFormat.Format24bppRgb == cBitmapImg.PixelFormat)
                {
                    byte bTemp;
                    byte[] bMvdImgData = cMvdImg.GetImageData().stDataChannel[0].arrDataBytes;
                    for (int i = 0; i < nImageWidth * nImageHeight; i++)
                    {
                        bTemp = bMvdImgData[3 * i];
                        bMvdImgData[3 * i] = bMvdImgData[3 * i + 2];
                        bMvdImgData[3 * i + 2] = bTemp;
                    }
                }
            }
            finally
            {
                cBitmapImg.UnlockBits(bitmapData);
            }
        }


        private static void ConvertMVDImage2Bitmap(CMvdImage cMvdImg, ref Bitmap cBitmapImg)
        {
            // 参数合法性判断
            if (null == cMvdImg)
            {
                throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_PARAMETER_ILLEGAL);
            }

            // 判断像素格式
            if (MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08 != cMvdImg.PixelFormat && MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 != cMvdImg.PixelFormat)
            {
                throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_SUPPORT);
            }

            Int32 nImageWidth = Convert.ToInt32(cMvdImg.Width);
            Int32 nImageHeight = Convert.ToInt32(cMvdImg.Height);
            Int32 nChannelNum = 0;
            BitmapData bitmapData = null;
            byte[] bBitmapDataTemp = null;
            try
            {
                // 获取图像信息
                if (MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08 == cMvdImg.PixelFormat) // 灰度图
                {
                    cBitmapImg = new Bitmap(nImageWidth, nImageHeight, PixelFormat.Format8bppIndexed);

                    // 灰度图需指定调色板
                    ColorPalette colorPalette = cBitmapImg.Palette;
                    for (int j = 0; j < 256; j++)
                    {
                        colorPalette.Entries[j] = Color.FromArgb(j, j, j);
                    }
                    cBitmapImg.Palette = colorPalette;

                    bitmapData = cBitmapImg.LockBits(new Rectangle(0, 0, nImageWidth, nImageHeight)
                                                                    , ImageLockMode.WriteOnly
                                                                    , PixelFormat.Format8bppIndexed);

                    // 灰度图不做深拷贝
                    bBitmapDataTemp = cMvdImg.GetImageData().stDataChannel[0].arrDataBytes;
                    nChannelNum = 1;
                }
                else if (MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == cMvdImg.PixelFormat) // 彩色图
                {
                    cBitmapImg = new Bitmap(nImageWidth, nImageHeight, PixelFormat.Format24bppRgb);
                    bitmapData = cBitmapImg.LockBits(new Rectangle(0, 0, nImageWidth, nImageHeight)
                                                                , ImageLockMode.WriteOnly
                                                                , PixelFormat.Format24bppRgb);
                    // 彩色图做深拷贝
                    bBitmapDataTemp = new byte[cMvdImg.GetImageData().stDataChannel[0].arrDataBytes.Length];
                    Array.Copy(cMvdImg.GetImageData().stDataChannel[0].arrDataBytes, bBitmapDataTemp, bBitmapDataTemp.Length);
                    nChannelNum = 3;
                }

                // bitmap彩色图按BGR存储，而MVDimg按RGB存储，改变存储顺序
                // 交换R和B
                if (MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == cMvdImg.PixelFormat)
                {
                    byte bTemp;
                    for (int i = 0; i < nImageWidth * nImageHeight; i++)
                    {
                        bTemp = bBitmapDataTemp[3 * i];
                        bBitmapDataTemp[3 * i] = bBitmapDataTemp[3 * i + 2];
                        bBitmapDataTemp[3 * i + 2] = bTemp;
                    }
                }

                // 考虑图像是否4字节对齐，bitmap要求4字节对齐，而mvdimage不要求对齐
                if (0 == nImageWidth % 4) // 4字节对齐时，直接拷贝
                {
                    Marshal.Copy(bBitmapDataTemp, 0, bitmapData.Scan0, nImageWidth * nImageHeight * nChannelNum);
                }
                else // 按步长逐行拷贝
                {
                    // 每行实际占用字节数
                    Int32 nRowPixelByteNum = nImageWidth * nChannelNum + 4 - (nImageWidth * nChannelNum % 4);
                    // 每行首字节首地址
                    IntPtr bitmapDataRowPos = IntPtr.Zero;
                    for (int i = 0; i < nImageHeight; i++)
                    {
                        // 获取每行第一个像素值的首地址
                        bitmapDataRowPos = new IntPtr(bitmapData.Scan0.ToInt64() + nRowPixelByteNum * i);
                        Marshal.Copy(bBitmapDataTemp, i * nImageWidth * nChannelNum, bitmapDataRowPos, nImageWidth * nChannelNum);
                    }
                }

                cBitmapImg.UnlockBits(bitmapData);
            }
            catch (MvdException ex)
            {
                if (null != cBitmapImg)
                {
                    cBitmapImg.UnlockBits(bitmapData);
                    cBitmapImg.Dispose();
                    cBitmapImg = null;
                }
                throw ex;
            }
            catch (System.Exception ex)
            {
                if (null != cBitmapImg)
                {
                    cBitmapImg.UnlockBits(bitmapData);
                    cBitmapImg.Dispose();
                    cBitmapImg = null;
                }
                throw ex;
            }
        }


    }
}
