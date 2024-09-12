using System;
using System.Windows.Forms;

namespace VMWireDetection
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool ret;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out ret);
            if (ret)
            {
                // 初始化 log4net
                log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
                //log4net.Util.LogLog.InternalDebugging = true;

                if (!HslCommunication.Authorization.SetAuthorizationCode("cf897f12-e779-4966-b0a3-bd4877c24bf6"))
                {
                    Console.WriteLine("Authorization failed! The current program can only be used for 8 hours!");
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                // 设置未捕获异常处理器
                MiniDumpHelper.SetupUnhandledExceptionHandler();
                Application.Run(new Form_Main());
            }
            else
            {
                MessageBox.Show(null, "有一个和本程序相同的应用程序已经在运行，请不要同时运行多个本程序。\n\n这个程序即将退出。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //   提示信息，可以删除。   
                Application.Exit();//退出程序   
            }
        }
    }
}
