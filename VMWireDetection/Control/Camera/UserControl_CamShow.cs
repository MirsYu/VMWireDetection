using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using VisionDesigner;

namespace VMWireDetection
{
    public partial class UserControl_CamShow : UserControl
    {

        public CMvdImage imgCam;
        public string strCamSN = "";

        private List<CMvdShape> cMvdShapes = new List<CMvdShape>();

        public UserControl_CamShow()
        {
            InitializeComponent();
            try
            {
                this.mvdRenderActivex1.SetMenuState(Convert.ToUInt32(MVD_MENU_ID.MvdPan), Convert.ToUInt32(MVD_MENU_CMD.MvdMenuCmdMin), string.Empty);
            }
            catch (MvdException ex)
            {
                LogManager.WriteError(string.Format("Fail to set menu state as pointer, module: {0}, error code: 0x{1}, message: {2}", ex.ModuleType, ex.ErrorCode.ToString("X"), ex.Message));
            }
            catch (Exception ex2)
            {
                LogManager.WriteError(string.Format("Fail to set menu state as pointer with error: {0}, stack trace is: {1}", ex2.Message, ex2.StackTrace));
            }
        }

        public void SetImage()
        {
            this.mvdRenderActivex1.LoadImageFromObject(imgCam);
            this.mvdRenderActivex1.Display();
            GC.Collect();
        }

        public void CusAddShape(CMvdShape shape)
        {
            this.mvdRenderActivex1.AddShape(shape);
            this.mvdRenderActivex1.Display();
            cMvdShapes.Add(shape);
        }

        public void CusClearShape()
        {
            this.mvdRenderActivex1.ClearShapes();
            cMvdShapes.Clear();
        }

        public void CusUpdateShape()
        {
            foreach (var item in cMvdShapes)
            {
                this.mvdRenderActivex1.AddShape(item);
            }
            this.mvdRenderActivex1.Display();
        }

        // 存储Name和对应的CMvdImage
        private Dictionary<string, CMvdImage> imageDictionary = new Dictionary<string, CMvdImage>();


        bool isClear = false;
        // 清空ComboBox列表
        public void ClearReaderImg()
        {
            this.comboBox1.Items.Clear();
            imageDictionary.Clear(); // 清空存储的图片
            CusClearShape();
            isClear = true;
        }

        // 向ComboBox添加Name和对应的img
        public void AddReaderImg(string Name, CMvdImage img)
        {
            string originalName = Name;
            int index = 1;

            // 如果名称重复，附加索引
            while (imageDictionary.ContainsKey(Name))
            {
                Name = $"{originalName} ({index++})";
            }

            // 添加Name到ComboBox
            this.comboBox1.Items.Add(Name);

            // 存储Name和img的对应关系
            imageDictionary[Name] = img;
            // 默认选择第一个项
            this.comboBox1.SelectedIndex = 0;
        }


        // 添加一个字段来保存上一个选择的名称
        private string _lastSelectedName;
        // 当ComboBox切换选项时，更新显示的图像
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 获取当前选择的名称
            string selectedName = comboBox1.SelectedItem?.ToString();

            // 如果当前选择为空或与上一个选择相同，则不更新
            if (selectedName == _lastSelectedName && !isClear)
            {
                return;
            }

            if (selectedName != null && imageDictionary.ContainsKey(selectedName))
            {
                if (isClear)
                {
                    isClear = false;
                }
                imgCam = imageDictionary[selectedName];
                // 更新显示的图像
                SetImage();
                CusUpdateShape();

                // 更新上一个选择的名称
                _lastSelectedName = selectedName;
            }
        }


        private void mvdRenderActivex1_MVDMouseEvent(MVDMouseEventType enMouseEventType, int nPointX, int nPointY, short nZDelta)
        {
            //用户想要实现自定义交互需通过SetConfiguration接口启用自定义交互
            //用户可根据enMouseEventType判断鼠标事件类型，编写对应的响应函数
            //示例：实时显示鼠标所在位置的ClassMap渲染图像坐标,标签和像素值
            try
            {
                //窗口坐标转图像坐标
                float fImgX = 0.0f, fImgY = 0.0f;
                mvdRenderActivex1.TransformCoordinate(nPointX, nPointY, ref fImgX, ref fImgY, MVDCoordTransType.Wnd2Img);

                //获取像素信息显示
                do
                {
                    if (null == imgCam)
                    {
                        break;
                    }

                    int nImagePointX = (int)fImgX;
                    int nImagePointY = (int)fImgY;
                    int nWidth = (int)imgCam.Width;
                    int nHeight = (int)imgCam.Height;
                    if (nImagePointX < 0 || nImagePointX >= nWidth
                        || nImagePointY < 0 || nImagePointY >= nHeight)
                    {
                        break;
                    }

                    string pixelInfo = string.Empty;
                    List<byte> pixelValue = imgCam.GetPixel(nImagePointX, nImagePointY);
                    MVD_PIXEL_FORMAT enPixelFormat = imgCam.PixelFormat;
                    if (MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08 == enPixelFormat)
                    {
                        pixelInfo = string.Format("X:{0:D4} Y:{1:D4} | CameraSN:{2:D2} | R:{3:D3} G:{4:D3} B:{5:D3}", nImagePointX, nImagePointY, strCamSN, pixelValue[0], pixelValue[0], pixelValue[0]);
                    }
                    else if (MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == enPixelFormat)
                    {
                        pixelInfo = string.Format("X:{0:D4} Y:{1:D4} | CameraSN:{2:D2} | R:{3:D3} G:{4:D3} B:{5:D3}", nImagePointX, nImagePointY, strCamSN, pixelValue[0], pixelValue[1], pixelValue[2]);
                    }
                    else
                    {
                        throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_SUPPORT, "Unsupported pixel format.");
                    }
                    this.tbPixelInfo.Text = pixelInfo;
                } while (false);
            }
            catch (MvdException ex)
            {
                LogManager.WriteError(String.Format("Fail to respond to mouse event! Module : {0}, ErrorCode : 0x{1}, Message : {2}.\r\n", ex.ModuleType.ToString(), ex.ErrorCode.ToString("X"), ex.Message));
            }
            catch (System.Exception ex)
            {
                LogManager.WriteError(String.Format("Fail to respond to mouse event! Message : {0}, StackTrace : {1}.\r\n", ex.Message, ex.StackTrace));
            }
        }

        private void MvdRenderActivex1_MVDShapeChangedEvent(MVDRenderActivex.MVD_SHAPE_EVENT_TYPE enEventType, MVD_SHAPE_TYPE enShapeType, CMvdShape cShapeObj)
        {
            if (enShapeType == MVD_SHAPE_TYPE.MvdShapeAnnularSector || enShapeType == MVD_SHAPE_TYPE.MvdShapeRectangle || enShapeType == MVD_SHAPE_TYPE.MvdShapeCircle)
            {
            }
        }

        private void btn_Zoom_Click(object sender, EventArgs e)
        {
            this.mvdRenderActivex1.ResetDisplayState();
        }

        public CMvdRectangleF DrawRectangle(string Name)
        {
            this.mvdRenderActivex1.ClearShapes();
            CMvdRectangleF rect = new CMvdRectangleF(100, 100, 100, 100);
            try
            {
                if (!imgCam.IsEmpty)
                {
                    rect = new CMvdRectangleF(imgCam.Width / 2, imgCam.Height / 2, imgCam.Width / 2, imgCam.Height / 2);
                    rect.BorderColor = new MVD_COLOR(0, 0, byte.MaxValue, byte.MaxValue);
                    rect.FillColor = new MVD_COLOR(0, 0, byte.MaxValue, 128);
                    rect.Interaction = true;
                }
                // 计算矩形左上角的位置
                float leftX = rect.CenterX - rect.Width / 2 - 100;
                float topY = rect.CenterY - rect.Height / 2 - 100;
                CMvdTextF Text = new CMvdTextF(leftX, topY, Name);
                Text.BorderColor = new MVD_COLOR(0x7f, 0xff, 00);
                Text.FontWidth = 20;
                this.mvdRenderActivex1.AddShape(Text);
                this.mvdRenderActivex1.AddShape(rect);
                this.mvdRenderActivex1.Display();
                return rect;
                //this.mvdRenderActivex1.SetMenuState(Convert.ToUInt32(MVD_MENU_ID.MvdRectangle), Convert.ToUInt32(MVD_MENU_CMD.MvdMenuCmdMin), string.Empty);
            }
            catch (MvdException ex)
            {
                LogManager.WriteError(string.Format("Fail to set menu state as rectangle, module: {0}, error code: 0x{1}, message: {2}", ex.ModuleType, ex.ErrorCode.ToString("X"), ex.Message));
                return rect;
            }
            catch (Exception ex2)
            {
                LogManager.WriteError(string.Format("Fail to set menu state as rectangle with error: {0}, stack trace is: {1}", ex2.Message, ex2.StackTrace));
                return rect;
            }
        }

        public CMvdRectangleF GetRectangle(CMvdShape rect)
        {
            try
            {
                this.mvdRenderActivex1.UpdateShapeInfo(ref rect, false);
                CMvdRectangleF rectResult = (CMvdRectangleF)rect;
                this.mvdRenderActivex1.ClearShapes();
                rect.Interaction = false;
                this.mvdRenderActivex1.AddShape(rect);
                this.mvdRenderActivex1.Display();
                return rectResult;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SaveOrgImg(string path)
        {
            // 提取目录路径
            string directory = Path.GetDirectoryName(path);

            // 判断目录是否存在，如果不存在则创建
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            this.mvdRenderActivex1.SaveImage(path, MVD_FILE_FORMAT.MVD_FILE_BMP, 100, MVD_SAVE_TYPE.MVD_SAVE_ORIGIANAL_IMAGE);
        }

        public void SaveRenderImg(string path)
        {
            // 提取目录路径
            string directory = Path.GetDirectoryName(path);

            // 判断目录是否存在，如果不存在则创建
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            this.mvdRenderActivex1.SaveImage(path, MVD_FILE_FORMAT.MVD_FILE_BMP, 100, MVD_SAVE_TYPE.MVD_SAVE_RESULT_IMAGE);
        }
    }
}
