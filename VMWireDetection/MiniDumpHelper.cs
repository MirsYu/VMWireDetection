using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VMWireDetection
{
    public static class MiniDumpHelper
    {
        [Flags]
        public enum MiniDumpType
        {
            MiniDumpNormal = 0x00000000,
            MiniDumpWithDataSegs = 0x00000001,
            MiniDumpWithFullMemory = 0x00000002,
            MiniDumpWithHandleData = 0x00000004,
            MiniDumpFilterMemory = 0x00000008,
            MiniDumpScanMemory = 0x00000010,
            MiniDumpWithUnloadedModules = 0x00000020,
            MiniDumpWithIndirectlyReferencedMemory = 0x00000040,
            MiniDumpFilterModulePaths = 0x00000080,
            MiniDumpWithProcessThreadData = 0x00000100,
            MiniDumpWithPrivateReadWriteMemory = 0x00000200,
            MiniDumpWithoutOptionalData = 0x00000400,
            MiniDumpWithFullMemoryInfo = 0x00000800,
            MiniDumpWithThreadInfo = 0x00001000,
            MiniDumpWithCodeSegs = 0x00002000,
            MiniDumpWithoutAuxiliaryState = 0x00004000,
            MiniDumpWithFullAuxiliaryState = 0x00008000,
            MiniDumpWithPrivateWriteCopyMemory = 0x00010000,
            MiniDumpIgnoreInaccessibleMemory = 0x00020000,
            MiniDumpWithTokenInformation = 0x00040000,
            MiniDumpWithModuleHeaders = 0x00080000,
            MiniDumpFilterTriage = 0x00100000,
        }

        [DllImport("dbghelp.dll", SetLastError = true)]
        static extern bool MiniDumpWriteDump(
            IntPtr hProcess,
            uint processId,
            IntPtr hFile,
            MiniDumpType dumpType,
            IntPtr exceptionParam,
            IntPtr userStreamParam,
            IntPtr callbackParam);

        public static void WriteDump(string dumpDirectory, MiniDumpType dumpType = MiniDumpType.MiniDumpWithFullMemory)
        {
            // 生成带有时间戳的唯一文件名
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string dumpFilePath = Path.Combine(dumpDirectory, $"crash_dump_{timestamp}.dmp");

            using (FileStream fs = new FileStream(dumpFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Process currentProcess = Process.GetCurrentProcess();
                IntPtr hProcess = currentProcess.Handle;
                uint processId = (uint)currentProcess.Id;
                IntPtr hFile = fs.SafeFileHandle.DangerousGetHandle();

                if (!MiniDumpWriteDump(hProcess, processId, hFile, dumpType, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero))
                {
                    throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        public static void SetupUnhandledExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                string dumpDirectory = AppDomain.CurrentDomain.BaseDirectory; // 存放dmp文件的路径

                try
                {
                    WriteDump(dumpDirectory);
                    MessageBox.Show($"应用程序崩溃，生成Dump文件: {dumpDirectory}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"生成Dump文件时发生错误: {ex.Message}");
                }

                Environment.Exit(1); // 退出应用程序
            };
        }

    }
}
