using System.Runtime.InteropServices;

namespace VMWireDetection
{
    public class HighPrecisionTimer
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        private long startTime;
        private long frequency;

        public HighPrecisionTimer()
        {
            QueryPerformanceFrequency(out frequency);
        }

        public void Start()
        {
            QueryPerformanceCounter(out startTime);
        }

        public double Stop()
        {
            long endTime;
            QueryPerformanceCounter(out endTime);
            return (endTime - startTime) / (double)frequency * 1000; // 返回时间，以毫秒为单位
        }
    }
}
