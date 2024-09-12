using VisionDesigner;
using VisionDesigner.ColorMeasure;

namespace VMWireDetection
{
    public class _ColorMeasureTool
    {
        public UserControl_CamShow _Render;
        public CMvdImage _Image;
        public CMvdShape _Roi;
        public CColorMeasureResult _result;

        public CColorMeasureTool _tool = null;


        public _ColorMeasureTool()
        {
            _tool = new CColorMeasureTool();
        }

        public bool Run()
        {
            bool bResult = false;
            try
            {
                if (_tool == null)
                {
                    _tool = new CColorMeasureTool();
                }
                _tool.InputImage = _Image;
                _tool.ROI = _Roi;
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
