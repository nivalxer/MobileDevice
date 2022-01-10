using System;
using System.Runtime.InteropServices;

namespace LibMobileDevice.CoreFundation
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_INFORMATION
    {
        public IntPtr hProcess;
        public IntPtr hThread;
        public int dwProcessId;
        public int dwThreadId;

        public bool IsEmpty
        {
            get { return hProcess == IntPtr.Zero && hThread == IntPtr.Zero && dwProcessId == 0 && dwThreadId == 0; }
        }
    }
}