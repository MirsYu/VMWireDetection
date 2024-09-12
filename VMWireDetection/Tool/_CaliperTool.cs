using VisionDesigner;
using VisionDesigner.Caliper;

namespace VMWireDetection
{
    public class _CaliperTool
    {
        public UserControl_CamShow _Render;

        public CCaliperTool _tool = null;

        public CMvdImage _image = null;

        public CCaliperResult _result = null;

        public _CaliperTool()
        {
            _tool = new CCaliperTool();
        }

        public bool Run()
        {
            bool bResult = false;
            try
            {
                if (_tool == null)
                {
                    _tool = new CCaliperTool();
                }
                _tool.InputImage = _image;
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
