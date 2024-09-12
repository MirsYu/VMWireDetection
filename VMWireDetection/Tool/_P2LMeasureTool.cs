using VisionDesigner;
using VisionDesigner.P2LMeasure;

namespace VMWireDetection
{
    public class _P2LMeasureTool
    {
        public UserControl_CamShow _Render;

        public CP2LMeasureTool _tool = null;

        public MVD_POINT_F point;

        public CMvdLineSegmentF line;

        public CP2LMeasureResult _result;

        public _P2LMeasureTool()
        {
            _tool = new CP2LMeasureTool();
        }

        public bool Run()
        {
            bool bResult = false;
            try
            {
                if (_tool == null)
                {
                    _tool = new CP2LMeasureTool();
                }
                _tool.BasicParam.Point = point;
                _tool.BasicParam.Line = line;
                _tool.Run();
                _result = _tool.Result;
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
