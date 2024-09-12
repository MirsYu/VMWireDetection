using System;
using System.Collections.Generic;
using System.Numerics;
using VisionDesigner;
using VisionDesigner.ContourPatMatch;
using static VMWireDetection.Form_CasingParam;

namespace VMWireDetection
{
    /// <summary>
    /// 插壳检测流程
    /// </summary>
    public class Flow_Casing
    {
        public string strFlowName = "Casing";

        public UserControl_CamShow _RenderControl = null;

        public bool useLocalImage = false; // 标识是否使用本地图片

        public CMvdImage localImage;       // 存储本地加载的图像

        public bool IsGetColorBase = false;

        // 工具
        public _ColorConver imageColorConverTool1 = new _ColorConver();
        public _ColorConver imageColorConverTool2 = new _ColorConver();
        public _ContourPatMarchTool imgPathTool = new _ContourPatMarchTool();
        public _PositionFixTool fixtureTool1 = new _PositionFixTool();
        public _LineFindTool lineFind1 = new _LineFindTool();
        public _CaliperTool caliperTool1 = new _CaliperTool();
        public _CaliperTool caliperTool2 = new _CaliperTool();
        public _CaliperTool caliperTool3 = new _CaliperTool();
        public _CaliperTool caliperTool4 = new _CaliperTool();
        public _LineFindTool lineFind2 = new _LineFindTool();
        public _PositionFixTool fixtureTool2 = new _PositionFixTool();
        public _P2LMeasureTool P2LTool = new _P2LMeasureTool();
        public _ColorMeasureTool colorMeasureTool1 = new _ColorMeasureTool();
        public _ColorMeasureTool colorMeasureTool2 = new _ColorMeasureTool();
        public _BlobTool blob = new _BlobTool();

        public static string strBaseImg = $"{Global.strProjectPath}\\Casing_BaseImg.bmp";

        // 定义颜色常量
        MVD_COLOR greenColor = new MVD_COLOR(0, byte.MaxValue, 0, byte.MaxValue);
        MVD_COLOR redColor = new MVD_COLOR(byte.MaxValue, 0, 0, byte.MaxValue);


        public bool Init()
        {
            imageColorConverTool1._Render = _RenderControl;
            imageColorConverTool2._Render = _RenderControl;
            imgPathTool._Render = _RenderControl;
            fixtureTool1._Render = _RenderControl;
            lineFind1._Render = _RenderControl;

            lineFind2._Render = _RenderControl;
            fixtureTool2._Render = _RenderControl;
            P2LTool._Render = _RenderControl;
            colorMeasureTool1._Render = _RenderControl;
            colorMeasureTool2._Render = _RenderControl;
            blob._Render = _RenderControl;

            return true;
        }

        public bool Run()
        {
            HighPrecisionTimer totalTimer = new HighPrecisionTimer();
            HighPrecisionTimer stepTimer = new HighPrecisionTimer();

            Global.GlobalData globalData = Global._GlobalData;
            try
            {
                globalData.ShellTotalNum++;
                // 开始总体计时
                totalTimer.Start();

                // 清空渲染控件中的图像
                stepTimer.Start();
                _RenderControl.ClearReaderImg();
                double clearImgTime = stepTimer.Stop();
                LogManager.WriteDebug("清空渲染控件耗时: " + clearImgTime + "ms");

                // 加载光源和相机配置
                stepTimer.Start();
                Global._Light.LoadJson($"{Global.strProjectPath}\\{strFlowName}_Light.json");
                Global._Cam.LoadJson($"{Global.strProjectPath}\\{strFlowName}_Cam.json");
                double loadConfigTime = stepTimer.Stop();
                LogManager.WriteDebug("加载光源和相机配置耗时: " + loadConfigTime + "ms");

                // 获取图像
                CMvdImage img;
                stepTimer.Start();
                if (useLocalImage && localImage != null)
                {
                    img = localImage;
                    useLocalImage = false;
                }
                else
                {
                    img = Global._Cam.TriggerExec();
                    Global._RTU.RTU.Write<Int32>("1884", 1);
                }
                Global._Light.CloseLight();
                double getImageTime = stepTimer.Stop();
                LogManager.WriteDebug("获取图像耗时: " + getImageTime + "ms");

                // 添加原始图像到渲染控件
                stepTimer.Start();
                _RenderControl.AddReaderImg($"Flow_Casing.相机原始图", img);
                double addOriginalImgTime = stepTimer.Stop();
                LogManager.WriteDebug("添加原始图像到渲染控件耗时: " + addOriginalImgTime + "ms");

                // 图像处理：颜色转换
                stepTimer.Start();
                imageColorConverTool1._Image = img;
                imageColorConverTool1.Run();
                double colorConversionTime = stepTimer.Stop();
                LogManager.WriteDebug("图像颜色转换耗时: " + colorConversionTime + "ms");
                CMvdImage img_Mono = imageColorConverTool1._ResultImage;
                _RenderControl.AddReaderImg($"Flow_Casing.相机黑白图", img_Mono);


                // 图像匹配
                stepTimer.Start();
                imgPathTool._Image = img_Mono;
                if (imgPathTool._tool.Pattern.ModelData == null)
                {
                    imgPathTool._tool.Pattern.ImportPattern($"{Global.strProjectPath}\\{strFlowName}.contourmxml");
                }
                imgPathTool.Run();
                double imageMatchingTime = stepTimer.Stop();
                LogManager.WriteDebug("图像匹配耗时: " + imageMatchingTime + "ms");

                // 端子插到位检测和测量点计算
                stepTimer.Start();
                CasingParam param = Global._CasingParam.param;
                lineFind1._Image = img_Mono;
                caliperTool1._image = img_Mono;
                caliperTool2._image = img_Mono;

                List<MeasureInfo> Infos = new List<MeasureInfo>();
                List<TitleMeasureInfo> titleMeasureInfos = new List<TitleMeasureInfo>();

                if (param.isTitleCheck == 1)
                {
                    if (imgPathTool._Result.MatchInfoList.Count == param.CasingNum)
                    {
                        for (int i = 0; i < param.CasingNum; i++)
                        {
                            // 查找直线
                            lineFind1._tool.ROI = imgPathTool._Result.MatchInfoList[i].MatchBox;
                            lineFind1.Run();
                            float LineCenterX = (lineFind1._result.LineStartPoint.fX + lineFind1._result.LineEndPoint.fX) / 2;
                            float LineCenterY = (lineFind1._result.LineStartPoint.fY + lineFind1._result.LineEndPoint.fY) / 2;
                            float LineAngle = lineFind1._result.LineAngle;
                            float Width = Math.Abs(lineFind1._result.LineStartPoint.fX - lineFind1._result.LineEndPoint.fX);

                            // 设置卡尺ROI并执行检测
                            CMvdRectangleF rect = new CMvdRectangleF(LineCenterX, LineCenterY + param.CaliperYOffset, Width, param.CaliperHeight);
                            rect.Angle = LineAngle;
                            caliperTool1._tool.ROI = rect;
                            caliperTool1.Run();

                            // 遍历卡尺结果并计算测量信息
                            if (caliperTool1._result.EdgePairResult.EdgePairInfo.Count == param.TitleNum)
                            {
                                for (int j = 0; j < caliperTool1._result.EdgePairResult.EdgePairInfo.Count; j++)
                                {
                                    MVD_POINT_F point = caliperTool1._result.EdgePairResult.EdgePairInfo[j].Center;
                                    point.fY += param.TitleYOffset;

                                    CMvdRectangleF pointRect = new CMvdRectangleF(
                                        point.fX, point.fY,
                                        caliperTool1._result.EdgePairResult.EdgePairInfo[j].Distance,
                                        param.TitleHeight
                                    );
                                    pointRect.Angle = LineAngle;

                                    MeasureInfo info = new MeasureInfo
                                    {
                                        rect = pointRect,
                                        line = new CMvdLineSegmentF(lineFind1._result.LineStartPoint, lineFind1._result.LineEndPoint)
                                    };
                                    Infos.Add(info);
                                }
                            }
                        }

                        // 检测端子测量点
                        foreach (var info in Infos)
                        {
                            caliperTool2._tool.ROI = info.rect;
                            caliperTool2.Run();

                            // 计算端子测量距离
                            MVD_POINT_F point = caliperTool2._result.SingleEdgeFindResult.SingleEdgeInfo[0].EdgePoint;
                            CMvdLineSegmentF line = info.line;

                            P2LTool.point = point;
                            P2LTool.line = line;
                            P2LTool.Run();

                            TitleMeasureInfo titleInfo = new TitleMeasureInfo
                            {
                                line = new CMvdLineSegmentF(P2LTool._result.ProjectPoint, point),
                                data = P2LTool._result.VerticalDist
                            };
                            if (titleInfo.data > param.MinTitle && titleInfo.data < param.MaxTitle)
                            {
                                titleInfo.color = greenColor;
                            }
                            else
                            {
                                titleInfo.color = redColor;
                                LogManager.WriteError($"端子到位测量失败:标准值:{param.MinTitle}~{param.MaxTitle};测量值:{titleInfo.data}");
                            }
                            titleMeasureInfos.Add(titleInfo);
                        }
                    }
                }
                double detectionAndMeasurementTime = stepTimer.Stop();
                LogManager.WriteDebug("端子插到位检测和测量点计算耗时: " + detectionAndMeasurementTime + "ms");

                // 检测线序和间距
                stepTimer.Start();
                lineFind2._Image = img_Mono;
                caliperTool3._image = img_Mono;
                caliperTool4._image = img_Mono;
                List<CMvdRectangleF> ColorRect = new List<CMvdRectangleF>();
                List<TitleMeasureInfo> GapLine = new List<TitleMeasureInfo>();
                List<ColorMeasureInfo> colorInfos = new List<ColorMeasureInfo>();
                colorInfos.Clear();

                if (imgPathTool._Result.MatchInfoList.Count == param.CasingNum)
                {
                    for (int i = 0; i < param.CasingNum; i++)
                    {
                        CMvdRectangleF RoiTemp = imgPathTool._Result.MatchInfoList[i].MatchBox;
                        float angle = RoiTemp.Angle;
                        angle += 180;
                        if (angle > 180)
                        {
                            angle -= 360;
                        }
                        if (angle <= -180)
                        {
                            angle += 360;
                        }
                        RoiTemp.Angle = angle;
                        // 查找直线
                        lineFind2._tool.ROI = RoiTemp;
                        lineFind2.Run();
                        CMvdLineSegmentF lineShow = new CMvdLineSegmentF(lineFind2._result.LineStartPoint, lineFind2._result.LineEndPoint);
                        float LineCenterX = (lineFind2._result.LineStartPoint.fX + lineFind2._result.LineEndPoint.fX) / 2;
                        float LineCenterY = (lineFind2._result.LineStartPoint.fY + lineFind2._result.LineEndPoint.fY) / 2;
                        float LineAngle = lineFind2._result.LineAngle;
                        float Width = Math.Abs(lineFind2._result.LineStartPoint.fX - lineFind2._result.LineEndPoint.fX);

                        // 设置卡尺ROI
                        CMvdRectangleF rect = new CMvdRectangleF(LineCenterX, LineCenterY + param.CaliperYOffset, Width, param.CaliperHeight);
                        rect.Angle = LineAngle;
                        if (param.isLineCheck == 1)
                        {
                            // 执行卡尺检测
                            caliperTool3._tool.ROI = rect;
                            caliperTool3.Run();

                            if (caliperTool3._result.EdgePairResult.EdgePairInfo.Count == param.TitleNum)
                            {
                                for (int j = 0; j < caliperTool3._result.EdgePairResult.EdgePairInfo.Count; j++)
                                {
                                    MVD_POINT_F point = caliperTool3._result.EdgePairResult.EdgePairInfo[j].Center;
                                    point.fY += param.LineYOffset;

                                    CMvdRectangleF pointRect = new CMvdRectangleF(
                                        point.fX, point.fY,
                                        caliperTool3._result.EdgePairResult.EdgePairInfo[j].Distance,
                                        param.LineHeight
                                    );
                                    ColorRect.Add(pointRect);
                                }
                            }
                        }

                        if (param.isLineGapCheck == 1)
                        {
                            // 计算线间距
                            caliperTool4._tool.ROI = rect;
                            caliperTool4.Run();

                            for (int j = 0; j < caliperTool4._result.EdgePairResult.EdgePairInfo.Count; j++)
                            {
                                CMvdLineSegmentF line = new CMvdLineSegmentF(
                                    caliperTool4._result.EdgePairResult.EdgePairInfo[j].Edge0Info.EdgePoint,
                                    caliperTool4._result.EdgePairResult.EdgePairInfo[j].Edge1Info.EdgePoint
                                );

                                TitleMeasureInfo gapInfo = new TitleMeasureInfo
                                {
                                    line = line,
                                    data = caliperTool4._result.EdgePairResult.EdgePairInfo[j].Distance
                                };
                                if (gapInfo.data > param.MinLineGap && gapInfo.data < param.MaxLineGap)
                                {
                                    gapInfo.color = greenColor;
                                }
                                else
                                {
                                    gapInfo.color = redColor;
                                    LogManager.WriteError($"线间距检测失败:标准值{param.MinLineGap}~{param.MaxLineGap};测量值{gapInfo.data}");
                                }

                                GapLine.Add(gapInfo);
                            }
                        }
                    }


                    if (param.isLineCheck == 1)
                    {
                        // 执行颜色测量
                        CasingParam paramTemp = Global._CasingParam.param;
                        colorMeasureTool1._Image = img;
                        ColorMeasureInfo colorinfo = new ColorMeasureInfo();
                        if (IsGetColorBase)
                        {
                            IsGetColorBase = false;
                            foreach (var rect in ColorRect)
                            {
                                colorMeasureTool1._Roi = rect;
                                colorMeasureTool1.Run();

                                colorinfo.rect = rect;

                                Vector3 var = new Vector3();
                                var.X = colorMeasureTool1._result.HistInfo[0].MeanVal;
                                var.Y = colorMeasureTool1._result.HistInfo[1].MeanVal;
                                var.Z = colorMeasureTool1._result.HistInfo[2].MeanVal;
                                paramTemp.ChannelValue.Add(var);
                                Global._CasingParam.param = paramTemp;
                                Global._CasingParam.SaveJson($"{Global.strProjectPath}\\{strFlowName}_Flow.json");
                                colorinfo.color = greenColor;
                                colorinfo.data = new List<float>();
                                colorInfos.Add(colorinfo);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < ColorRect.Count; i++)
                            {
                                colorMeasureTool1._Roi = ColorRect[i];
                                colorMeasureTool1.Run();

                                colorinfo.rect = ColorRect[i];

                                if (param.ColorGap > Math.Abs(paramTemp.ChannelValue[i].X - colorMeasureTool1._result.HistInfo[0].MeanVal) &&
                                param.ColorGap > Math.Abs(paramTemp.ChannelValue[i].Y - colorMeasureTool1._result.HistInfo[1].MeanVal) &&
                                param.ColorGap > Math.Abs(paramTemp.ChannelValue[i].Z - colorMeasureTool1._result.HistInfo[2].MeanVal))
                                {
                                    colorinfo.color = greenColor;
                                    colorinfo.data = new List<float>();
                                }
                                else
                                {
                                    colorinfo.color = redColor;
                                    colorinfo.data = new List<float>();
                                    colorinfo.data.Add(Math.Abs(paramTemp.ChannelValue[i].X - colorMeasureTool1._result.HistInfo[0].MeanVal));
                                    colorinfo.data.Add(Math.Abs(paramTemp.ChannelValue[i].Y - colorMeasureTool1._result.HistInfo[1].MeanVal));
                                    colorinfo.data.Add(Math.Abs(paramTemp.ChannelValue[i].Z - colorMeasureTool1._result.HistInfo[2].MeanVal));
                                    LogManager.WriteError($"线序检测失败,标准值{paramTemp.ChannelValue[i].X},{paramTemp.ChannelValue[i].Y},{paramTemp.ChannelValue[i].Z};" +
                                        $"测量值:{colorMeasureTool1._result.HistInfo[0].MeanVal},{colorMeasureTool1._result.HistInfo[1].MeanVal},{colorMeasureTool1._result.HistInfo[2].MeanVal}");
                                }
                                colorInfos.Add(colorinfo);
                            }
                        }
                    }
                }
                double lineDetectionAndColorMeasurementTime = stepTimer.Stop();
                LogManager.WriteDebug("检测线序和间距耗时: " + lineDetectionAndColorMeasurementTime + "ms");

                // 发送结果
                int iResult = 0;
                foreach (var item in titleMeasureInfos)
                {
                    if (item.color.Equals(redColor))
                    {
                        iResult = 0;
                        break;
                    }
                    else
                    {
                        iResult = 1;
                    }
                }
                foreach (var item in colorInfos)
                {
                    if (item.color.Equals(redColor))
                    {
                        iResult = 0;
                        break;
                    }
                    else
                    {
                        iResult = 1;
                    }
                }
                foreach (var item in GapLine)
                {
                    if (item.color.Equals(redColor))
                    {
                        iResult = 0;
                        break;
                    }
                    else
                    {
                        iResult = 1;
                    }
                }
                Global._RTU.RTU.Write<Int32>("1882", iResult);

                // 计数

                if (iResult == 0)
                {
                    globalData.ShellNGNum++;
                }
                else
                {
                    globalData.ShellOKNum++;
                }

                if (Global._Main.IsRunMode)
                {
                    Global._GlobalData = globalData;
                }

                // 渲染结果
                stepTimer.Start();
                if (param.isTitleCheck == 1)
                {
                    foreach (var rect in Infos)
                    {
                        UpdateShapeColor(rect.line, greenColor);
                        UpdateShapeColor(rect.rect, greenColor);
                    }

                    // 渲染端子插到位结果
                    foreach (var item in titleMeasureInfos)
                    {
                        CMvdTextF textF = new CMvdTextF(item.line.StartPoint.fX, item.line.StartPoint.fY, item.data.ToString("F2"))
                        {
                            FontWidth = 20
                        };
                        UpdateShapeColor(textF, item.color);
                        UpdateShapeColor(item.line, item.color);
                    }
                }

                if (param.isLineCheck == 1)
                {
                    // 渲染颜色测量结果
                    foreach (var item in colorInfos)
                    {
                        UpdateShapeColor(item.rect, item.color);
                        if (item.data.Count != 0)
                        {
                            CMvdTextF textF = new CMvdTextF(item.rect.CenterX, item.rect.CenterY, $"{item.data[0].ToString("F2")}\r\n" +
                                $"{item.data[1].ToString("F2")}\r\n{item.data[2].ToString("F2")}")
                            {
                                FontWidth = 20
                            };
                            UpdateShapeColor(textF, redColor);
                        }
                    }
                }

                if (param.isLineGapCheck == 1)
                {
                    // 渲染线间距测量结果
                    foreach (var item in GapLine)
                    {
                        CMvdTextF textF = new CMvdTextF(item.line.StartPoint.fX, item.line.StartPoint.fY, item.data.ToString("F2"))
                        {
                            FontWidth = 20
                        };
                        UpdateShapeColor(textF, item.color);
                        UpdateShapeColor(item.line, item.color);
                    }
                }
                double renderingTime = stepTimer.Stop();
                LogManager.WriteDebug("渲染结果耗时: " + renderingTime + "ms");

                // 总体计时结束
                double totalTime = totalTimer.Stop();
                LogManager.WriteDebug("总体运行时间: " + totalTime + "ms");

                // 存图
                if (Global._GlobalData.SaveOrgInOK == 1 && iResult == 1)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    _RenderControl.SaveOrgImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\OK_ORG\\胶壳\\{timestamp}.bmp");
                }

                if (Global._GlobalData.SaveOrgInNG == 1 && iResult == 0)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    _RenderControl.SaveOrgImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\NG_ORG\\胶壳\\{timestamp}.bmp");
                }

                if (Global._GlobalData.SaveRenderInOK == 1 && iResult == 1)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    _RenderControl.SaveRenderImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\OK_Rendel\\胶壳\\{timestamp}.bmp");
                }

                if (Global._GlobalData.SaveRenderInNG == 1 && iResult == 0)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    _RenderControl.SaveRenderImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\NG_Rendel\\胶壳\\{timestamp}.bmp");
                }

                return true; // 成功返回
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
                    _RenderControl.SaveOrgImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\NG_ORG_Error\\胶壳\\{timestamp}.bmp");
                }

                if (Global._GlobalData.SaveRenderInNG == 1)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    _RenderControl.SaveRenderImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\NG_Rendel_Error\\胶壳\\{timestamp}.bmp");
                }

                globalData.ShellNGNum++;
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
                    _RenderControl.SaveOrgImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\NG_ORG_Error\\{timestamp}.bmp");
                }

                if (Global._GlobalData.SaveRenderInNG == 1)
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    _RenderControl.SaveRenderImg($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\NG_Rendel_Error\\{timestamp}.bmp");
                }

                globalData.ShellNGNum++;
                if (Global._Main.IsRunMode)
                {
                    Global._GlobalData = globalData;
                }
                return false;
            }
        }

        public bool CreateMode(CMvdRectangleF rect)
        {
            CMvdImage modeImg = _RenderControl.imgCam.Clone();
            modeImg.ConvertImagePixelFormat(MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08);
            imgPathTool._ModeImage = modeImg;

            imgPathTool._Modelroi.Clear();
            CPatMatchRegion region = new CPatMatchRegion(rect, true);
            imgPathTool._Modelroi.Add(region);

            imgPathTool.GenterMode();
            return true;
        }

        void UpdateShapeColor(CMvdShape shape, MVD_COLOR color)
        {
            if (shape != null)
            {
                shape.BorderColor = color;
                _RenderControl.CusAddShape(shape);
            }
        }

        public struct MeasureInfo
        {
            public CMvdRectangleF rect;
            public CMvdLineSegmentF line;
        }

        public struct TitleMeasureInfo
        {
            public CMvdLineSegmentF line;
            public float data;
            public MVD_COLOR color;
        }

        public struct ColorMeasureInfo
        {
            public CMvdRectangleF rect;
            public MVD_COLOR color;
            public List<float> data;
        }
    }
}
