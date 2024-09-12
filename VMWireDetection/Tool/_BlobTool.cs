using VisionDesigner;
using VisionDesigner.BlobFind;

namespace VMWireDetection
{
    public class _BlobTool
    {
        public UserControl_CamShow _Render;
        public CMvdImage _Image;
        public CMvdShape _Roi;

        public CBlobFindTool _tool = null;

        public _BlobTool()
        {
            _tool = new CBlobFindTool();
        }

        public bool Run()
        {
            bool bResult = false;
            try
            {
                if (_tool == null)
                {
                    _tool = new CBlobFindTool();
                }
                _tool.InputImage = _Image;
                _tool.ROI = _Roi;
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
