using VisionDesigner;
using VisionDesigner.PositionFix;

namespace VMWireDetection
{
    public class _PositionFixTool
    {
        public UserControl_CamShow _Render;

        public CPositionFixTool _tool = null;

        public _PositionFixTool()
        {
            _tool = new CPositionFixTool();
        }

        public bool Run()
        {
            bool bResult = false;
            try
            {
                if (_tool == null)
                {
                    _tool = new CPositionFixTool();
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
