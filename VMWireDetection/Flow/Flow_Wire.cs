using System;
using System.Collections.Generic;
using System.Linq;
using VisionDesigner;
using VisionDesigner.BlobFind;
using static VMWireDetection.Form_WireParam;

namespace VMWireDetection
{
    /// <summary>
    /// 剥皮检测流程(线芯)
    /// </summary>
    public class Flow_Wire
    {
        public string strFlowName = "Wire";

        public UserControl_CamShow _RenderControl = null;

        public _ColorConver imageColorConverTool = new _ColorConver();
        public _ImageMorphTool imageMorphTool1 = new _ImageMorphTool();
        public _ImageFilterTool imageFilterTool = new _ImageFilterTool();
        public _ImageArithmeticTool imageArithmeticTool = new _ImageArithmeticTool();
        public _ImageBinaryTool imageBinaryTool = new _ImageBinaryTool();
        public _ImageMorphTool imageMorphTool2 = new _ImageMorphTool();
        public _BlobTool blob1 = new _BlobTool();

        public bool useLocalImage = false; // 标识是否使用本地图片
        public CMvdImage localImage;       // 存储本地加载的图像

        // 定义颜色常量
        MVD_COLOR greenColor = new MVD_COLOR(0, byte.MaxValue, 0, byte.MaxValue);
        MVD_COLOR redColor = new MVD_COLOR(byte.MaxValue, 0, 0, byte.MaxValue);

        public bool Init()
        {
            imageColorConverTool._Render = _RenderControl;
            imageMorphTool1._Render = _RenderControl;
            imageFilterTool._Render = _RenderControl;
            imageArithmeticTool._Render = _RenderControl;
            imageBinaryTool._Render = _RenderControl;
            imageMorphTool2._Render = _RenderControl;
            blob1._Render = _RenderControl;
            return true;
        }

        public bool Run()
        {
            HighPrecisionTimer timer = new HighPrecisionTimer();
            Global.GlobalData globalData = Global._GlobalData;
            try
            {

                globalData.lineTotalNum++;
                // 开始总体计时
                timer.Start();

                // 清空渲染
                HighPrecisionTimer stepTimer = new HighPrecisionTimer();
                stepTimer.Start();
                _RenderControl.ClearReaderImg();
                double clearImgTime = stepTimer.Stop();
                LogManager.WriteDebug("清空渲染耗时: " + clearImgTime + "ms");

                // 加载灯光和相机配置
                stepTimer.Start();
                Global._Light.LoadJson($"{Global.strProjectPath}\\{strFlowName}_Light.json");
                Global._Cam.LoadJson($"{Global.strProjectPath}\\{strFlowName}_Cam.json");
                double loadConfigTime = stepTimer.Stop();
                LogManager.WriteDebug("加载配置耗时: " + loadConfigTime + "ms");

                CMvdImage img = new CMvdImage();
                if (useLocalImage && localImage != null)
                {
                    // 使用本地加载的图像
                    img = localImage;
                    useLocalImage = false;
                }
                else
                {
                    // 使用相机触发获取的图像
                    img = Global._Cam.TriggerExec();
                    Global._RTU.RTU.Write<Int32>("1884", 1);
                }
                Global._Light.CloseLight();
                // 添加原始图像
                stepTimer.Start();
                _RenderControl.AddReaderImg($"Flow_Wire.相机原始图", img);
                double addOriginalImgTime = stepTimer.Stop();
                LogManager.WriteDebug("添加原始图像耗时: " + addOriginalImgTime + "ms");

                // 处理图像
                stepTimer.Start();
                imageColorConverTool._Image = img;
                imageColorConverTool.Run();
                CMvdImage img_Mono = imageColorConverTool._ResultImage != null ? imageColorConverTool._ResultImage : img;

                _RenderControl.AddReaderImg($"Flow_Wire.相机黑白图", img_Mono);
                Global._WireParam.LoadJson($"{Global.strProjectPath}\\{strFlowName}_Flow.json");
                double imageProcessingTime = stepTimer.Stop();
                LogManager.WriteDebug("图像处理耗时: " + imageProcessingTime + "ms");

                // 继续处理
                stepTimer.Start();
                imageMorphTool1._Image = img_Mono;
                imageFilterTool._Image = img_Mono;
                imageMorphTool1.Run();
                imageFilterTool.Run();
                double filterMorphTime = stepTimer.Stop();
                LogManager.WriteDebug("形态学和滤波处理耗时: " + filterMorphTime + "ms");

                // 算术操作
                stepTimer.Start();
                imageArithmeticTool._Image2 = imageMorphTool1._ResultImage;
                imageArithmeticTool._Image1 = imageFilterTool._ResultImage;
                imageArithmeticTool.Run();
                double arithmeticTime = stepTimer.Stop();
                LogManager.WriteDebug("算术处理耗时: " + arithmeticTime + "ms");

                // 二值化处理
                stepTimer.Start();
                imageBinaryTool._Image = imageArithmeticTool._ResultImage;
                imageBinaryTool.Run();
                double binaryTime = stepTimer.Stop();
                LogManager.WriteDebug("二值化处理耗时: " + binaryTime + "ms");

                // 形态学处理2
                stepTimer.Start();
                imageMorphTool2._Image = imageBinaryTool._ResultImage;
                imageMorphTool2.Run();
                double morph2Time = stepTimer.Stop();
                LogManager.WriteDebug("形态学处理2耗时: " + morph2Time + "ms");

                // 结果判断
                stepTimer.Start();
                blob1._Image = imageMorphTool2._ResultImage;
                blob1.Run();
                bool overallResultOk = false;
                if (blob1._tool.Result != null)
                {
                    overallResultOk = IsOverallResultOk(blob1._tool.Result.BlobInfo, Global._WireParam.param);
                }
                else
                {
                    overallResultOk = false;
                }

                if (overallResultOk)
                {
                    Global._RTU.RTU.Write<Int32>("1882", 1);
                }
                else
                {
                    Global._RTU.RTU.Write<Int32>("1882", 0);
                }
                double resultCheckTime = stepTimer.Stop();
                LogManager.WriteDebug("结果判断耗时: " + resultCheckTime + "ms");



                // 计数
                if (overallResultOk)
                {
                    globalData.lineOKNum++;
                }
                else
                {
                    globalData.lineNGNum++;
                }

                if (Global._Main.IsRunMode)
                {
                    Global._GlobalData = globalData;
                }

                // 渲染
                stepTimer.Start();
                foreach (var item in blob1._tool.Result.BlobInfo)
                {
                    WireParam parm = Global._WireParam.param;

                    // 默认颜色为绿色
                    MVD_COLOR borderColor = greenColor;

                    // 创建字符对象，调整位置以避免重叠
                    float textXOffset = 0; // X偏移量
                    float textYOffset = item.BoxInfo.Height / 2 + 20; // Y偏移量（字符在图形上方）

                    // 将宽度和高度格式化为保留两位小数的字符串
                    string widthText = item.BoxInfo.Width.ToString("F2");
                    string heightText = item.BoxInfo.Height.ToString("F2");

                    CMvdTextF textWidth = new CMvdTextF(item.BoxInfo.CenterX + textXOffset, item.BoxInfo.CenterY - textYOffset, widthText);
                    textWidth.FontWidth = 10;

                    CMvdTextF textHeight = new CMvdTextF(item.BoxInfo.CenterX + textXOffset, item.BoxInfo.CenterY + textYOffset, heightText);
                    textHeight.FontWidth = 10;

                    // 判断宽度
                    if (item.BoxInfo.Width < parm.MinLineHeight || item.BoxInfo.Width > parm.MaxLineHeight)
                    {
                        // 宽度超出范围，设置红色
                        borderColor = redColor;
                    }
                    textWidth.BorderColor = borderColor;

                    // 判断高度
                    if (item.BoxInfo.Height < parm.MinLineWidth || item.BoxInfo.Height > parm.MaxLineWidth)
                    {
                        // 高度超出范围，设置红色
                        borderColor = redColor;
                    }
                    textHeight.BorderColor = borderColor;

                    // 更新 BoxInfo 的颜色
                    if (item.BoxInfo.Width < parm.MinLineHeight || item.BoxInfo.Width > parm.MaxLineHeight ||
                        item.BoxInfo.Height < parm.MinLineWidth || item.BoxInfo.Height > parm.MaxLineWidth)
                    {
                        // 如果宽度或高度超出范围，则 BoxInfo 为红色
                        item.BoxInfo.BorderColor = redColor;
                    }
                    else
                    {
                        // 否则，BoxInfo 为绿色
                        item.BoxInfo.BorderColor = greenColor;
                    }

                    // 添加形状到渲染控件
                    UpdateShapeColor(textWidth, textWidth.BorderColor);
                    UpdateShapeColor(textHeight, textHeight.BorderColor);
                    UpdateShapeColor(item.BoxInfo, item.BoxInfo.BorderColor);
                }
                double renderTime = stepTimer.Stop();
                LogManager.WriteDebug("渲染耗时: " + renderTime + "ms");

                double totalElapsedMilliseconds = timer.Stop();
                LogManager.WriteDebug("总体处理耗时: " + totalElapsedMilliseconds + "ms");

                // 存图
                if (Global._GlobalData.SaveOrgInOK == 1 && overallResultOk)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    _RenderControl.SaveOrgImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\OK_ORG\\线芯\\{timestamp}.bmp");
                }

                if (Global._GlobalData.SaveOrgInNG == 1 && !overallResultOk)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    _RenderControl.SaveOrgImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\NG_ORG\\线芯\\{timestamp}.bmp");
                }

                if (Global._GlobalData.SaveRenderInOK == 1 && overallResultOk)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    _RenderControl.SaveRenderImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\OK_Rendel\\线芯\\{timestamp}.bmp");
                }
                if (Global._GlobalData.SaveRenderInNG == 1 && !overallResultOk)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    _RenderControl.SaveRenderImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\NG_Rendel\\线芯\\{timestamp}.bmp");
                }

                return true;
            }
            catch (MvdException ex)
            {
                // 捕获MvdException，处理特定错误
                Global._RTU.RTU.Write<Int32>("1882", 0);
                LogManager.WriteError("An error occurred while running train. ErrorCode: 0x" + ex.ErrorCode.ToString("X")
                                        + ", message is : " + ex.Message + "\r\n");
                // 存图
                if (Global._GlobalData.SaveOrgInNG == 1)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    _RenderControl.SaveOrgImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\NG_ORG_Error\\线芯\\{timestamp}.bmp");
                }

                if (Global._GlobalData.SaveRenderInNG == 1)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    _RenderControl.SaveRenderImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\NG_Rendel_Error\\线芯\\{timestamp}.bmp");
                }


                globalData.lineNGNum++;


                if (Global._Main.IsRunMode)
                {
                    Global._GlobalData = globalData;
                }
                return false;
            }
            catch (Exception ex)
            {
                // 捕获所有其他异常
                Global._RTU.RTU.Write<Int32>("1882", 0);
                LogManager.WriteError("An error occurred while running train with ' " + ex.Message + " ', ex stack trace is : " + ex.StackTrace + ".\r\n");

                // 存图
                if (Global._GlobalData.SaveOrgInNG == 1)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    _RenderControl.SaveOrgImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\NG_ORG_Error\\线芯\\{timestamp}.bmp");
                }

                if (Global._GlobalData.SaveRenderInNG == 1)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    _RenderControl.SaveRenderImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\NG_Rendel_Error\\线芯\\{timestamp}.bmp");
                }
                globalData.lineNGNum++;


                if (Global._Main.IsRunMode)
                {
                    Global._GlobalData = globalData;
                }
                return false;
            }
        }


        // 设置 BoxInfo 的颜色和添加到渲染控件
        void UpdateShapeColor(CMvdShape shape, MVD_COLOR color)
        {
            if (shape != null)
            {
                shape.BorderColor = color;
                _RenderControl.CusAddShape(shape);
            }
        }

        private bool IsOverallResultOk(IEnumerable<CBlobInfo> blobInfoList, WireParam param)
        {
            if (blobInfoList.Count() != 0)
            {
                foreach (var item in blobInfoList)
                {
                    if (!IsResultOk(item.BoxInfo, param))
                    {
                        LogManager.WriteError($"铜丝检测失败,标准值W:{param.MinLineWidth}~{param.MaxLineWidth};H:{param.MinLineHeight}~{param.MaxLineWidth}" +
                            $",测量值W:{item.BoxInfo.Width},H:{item.BoxInfo.Height}");
                        return false; // 一旦发现不符合条件的项，返回 false
                    }
                }
                return true; // 如果所有项都符合条件，则返回 true
            }
            else
            {
                return false;
            }
        }

        private bool IsResultOk(CMvdRectangleF boxInfo, WireParam param)
        {
            return boxInfo.Width >= param.MinLineHeight && boxInfo.Width <= param.MaxLineHeight &&
                   boxInfo.Height >= param.MinLineWidth && boxInfo.Height <= param.MaxLineWidth;
        }

    }
}
