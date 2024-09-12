using VisionDesigner;
using VisionDesigner.L2LMeasure;

namespace VMWireDetection
{
    public class _L2LMeasureTool
    {
        public UserControl_CamShow _Render;

        public CL2LMeasureTool _tool = null;

        public _L2LMeasureTool()
        {
            _tool = new CL2LMeasureTool();
        }

        public bool Run()
        {
            bool bResult = false;
            try
            {
                if (_tool == null)
                {
                    _tool = new CL2LMeasureTool();
                }
                _tool.Run();
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
