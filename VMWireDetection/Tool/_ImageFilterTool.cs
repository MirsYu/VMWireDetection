using System.Diagnostics;
using VisionDesigner;
using VisionDesigner.ImageFilter;

namespace VMWireDetection
{
    public class _ImageFilterTool
    {
        public UserControl_CamShow _Render;
        public CMvdImage _Image;
        public CMvdShape _Roi;
        public CMvdImage _ResultImage;
        public CImageFilterTool _tool = null;

        public _ImageFilterTool()
        {
            _tool = new CImageFilterTool();
        }

        public bool Run()
        {
            bool bResult = false;
            try
            {
                if (_tool == null)
                {
                    _tool = new CImageFilterTool();
                }
                _tool.InputImage = _Image;
                _tool.ROI = _Roi;
                _tool.Run();
                _ResultImage = _tool.Result.OutputImage;
                _Render.AddReaderImg($"{GetCallingClassName()}.{this.GetType().Name}", _ResultImage);
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

        private string GetCallingClassName()
        {
            // 获取调用者的栈信息
            StackTrace stackTrace = new StackTrace();
            // 栈帧2表示调用此方法的类（栈帧0是当前方法，栈帧1是调用当前方法的类）
            StackFrame frame = stackTrace.GetFrame(2);

            if (frame != null)
            {
                // 获取调用者的方法信息
                var method = frame.GetMethod();
                if (method != null)
                {
                    // 获取调用者的类
                    return method.DeclaringType.Name;
                }
            }
            return "未知";
        }
    }
}
