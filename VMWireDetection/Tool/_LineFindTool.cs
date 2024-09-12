using VisionDesigner;
using VisionDesigner.LineFind;

namespace VMWireDetection
{
    public class _LineFindTool
    {
        public UserControl_CamShow _Render;
        public CMvdImage _Image;
        public CLineFindTool _tool = null;
        public CLineFindResult _result = null;

        public _LineFindTool()
        {
            _tool = new CLineFindTool();
        }

        public bool Run()
        {
            bool bResult = false;
            try
            {
                if (_tool == null)
                {
                    _tool = new CLineFindTool();
                }
                _tool.InputImage = _Image;
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
