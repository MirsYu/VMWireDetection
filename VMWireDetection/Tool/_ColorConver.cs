using VisionDesigner;
using VisionDesigner.ColorConvert;

namespace VMWireDetection
{
    public class _ColorConver
    {
        public UserControl_CamShow _Render;
        public CMvdImage _Image = new CMvdImage();
        public CMvdShape _Roi;
        public CMvdImage _ResultImage;

        public CColorConvertTool _tool = null;


        public _ColorConver()
        {
            _tool = new CColorConvertTool();
        }

        public bool Run()
        {
            bool bResult = false;
            try
            {
                if (_tool == null)
                {
                    _tool = new CColorConvertTool();
                }
                _tool.InputImage = _Image;
                _tool.ROI = _Roi;
                _tool.Run();
                _ResultImage = _tool.Result.OutputImage;
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
