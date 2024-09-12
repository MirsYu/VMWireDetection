using System.Collections.Generic;
using VisionDesigner;
using VisionDesigner.ContourPatMatch;

namespace VMWireDetection
{
    public class _ContourPatMarchTool
    {
        public UserControl_CamShow _Render;
        public CMvdImage _Image;
        public CMvdImage _ModeImage;
        public List<CPatMatchRegion> _Modelroi = new List<CPatMatchRegion>();
        public CMvdShape _Roi;
        public CContourPatMatchResult _Result;

        public CContourPatMatchTool _tool = null;

        public _ContourPatMarchTool()
        {
            _tool = new CContourPatMatchTool();
            _tool.Pattern = new CContourPattern();
        }

        public bool GenterMode(bool isShowRender = false)
        {
            try
            {
                _tool.Pattern.InputImage = _ModeImage;

                // region更新
                _tool.Pattern.RegionList.Clear();
                _tool.Pattern.RegionList = _Modelroi;

                _tool.Pattern.Train();
                if (isShowRender && _tool.Result != null)
                {
                    _Render.mvdRenderActivex1.ClearImages();
                    List<CPatMatchOutline> patmatchOutline = _tool.Pattern.Result.OutlineList;
                    if (null != _ModeImage)
                    {
                        _Render.mvdRenderActivex1.LoadImageFromObject(_ModeImage);
                        //同步刷新，避免图形显示偏移
                        _Render.mvdRenderActivex1.Display(MVDRenderActivex.MVD_REFRESH_MODE.Sync);

                        if (null != patmatchOutline)
                        {
                            foreach (var item in patmatchOutline)
                            {
                                CMvdPointSetF pointSetG = new CMvdPointSetF();
                                CMvdPointSetF pointSetY = new CMvdPointSetF();
                                CMvdPointSetF pointSetR = new CMvdPointSetF();

                                foreach (var point in item.EdgePointListOrigin)
                                {
                                    if (0 == point.Score)
                                    {
                                        pointSetG.AddPoint(point.Position.fX, point.Position.fY);
                                    }
                                    else if (1 == point.Score)
                                    {
                                        pointSetY.AddPoint(point.Position.fX, point.Position.fY);
                                    }
                                    else if (2 == point.Score)
                                    {
                                        pointSetR.AddPoint(point.Position.fX, point.Position.fY);
                                    }
                                }

                                pointSetG.BorderColor = new MVD_COLOR(0, 255, 0, 255);
                                pointSetY.BorderColor = new MVD_COLOR(255, 255, 0, 255);
                                pointSetR.BorderColor = new MVD_COLOR(255, 0, 0, 255);

                                if (0 != pointSetG.PointsList.Count)
                                {
                                    _Render.CusAddShape(pointSetG);
                                }

                                if (0 != pointSetY.PointsList.Count)
                                {
                                    _Render.CusAddShape(pointSetY);
                                }

                                if (0 != pointSetR.PointsList.Count)
                                {
                                    _Render.CusAddShape(pointSetR);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (MvdException ex)
            {
                LogManager.WriteError("An error occurred while running train. ErrorCode: 0x" + ex.ErrorCode.ToString("X")
                                        + ", message is : " + ex.Message + "\r\n");
                return false;
            }
            catch (System.Exception ex)
            {
                LogManager.WriteError("An error occurred while running train with ' " + ex.Message + " ', ex stack trace is : " + ex.StackTrace + ".\r\n");
                return false;
            }
        }

        public bool Run()
        {
            bool bResult = false;
            try
            {
                if (_tool == null)
                {
                    _tool = new CContourPatMatchTool();
                }
                _tool.InputImage = _Image;
                _tool.ROI = _Roi;
                _tool.Run();
                _Result = _tool.Result;
                return bResult;
            }
            catch (MvdException ex)
            {
                LogManager.WriteError("An error occurred while running train. ErrorCode: 0x" + ex.ErrorCode.ToString("X")
                                        + ", message is : " + ex.Message + "\r\n");
                return bResult;
            }
            catch (System.Exception ex)
            {
                LogManager.WriteError("An error occurred while running train with ' " + ex.Message + " ', ex stack trace is : " + ex.StackTrace + ".\r\n");
                return bResult;
            }
        }
    }
}
