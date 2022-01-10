using System;
using System.Runtime.InteropServices;

namespace LibMobileDevice.CoreFundation
{
    public class API
    {
        public const int NORMAL_PRIORITY_CLASS = 0x20;
        public const int STARTF_FORCEOFFFEEDBACK = 0x80;
        public const int STARTF_FORCEONFEEDBACK = 0x40;
        public const int STARTF_RUNFULLSCREEN = 0x20;
        public const int STARTF_USECOUNTCHARS = 8;
        public const int STARTF_USEFILLATTRIBUTE = 0x10;
        public const int STARTF_USEPOSITION = 4;
        public const int STARTF_USESHOWWINDOW = 1;
        public const int STARTF_USESIZE = 2;
        public const int STARTF_USESTDHANDLES = 0x100;
        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_HIDE = 0;
        public const int SW_MAX = 11;
        public const int SW_MAXIMIZE = 3;
        public const int SW_MINIMIZE = 6;
        public const int SW_NORMAL = 1;
        public const int SW_RESTORE = 9;
        public const int SW_SHOW = 5;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOWNORMAL = 1;

        public delegate bool Close();

        public delegate bool Decode(string sourceFileMp3, string destFileWav);

        public delegate bool Encode(string sourceFileWav, string destFileMp3);

        public delegate int GetDecodePercent();

        public delegate double GetLValue(double secondTime);

        public delegate int GetPercent();

        public delegate bool Open(string fileWavName);

        public delegate bool Split(string fileWavOut, double begin, double end, double in_time, double out_time);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        public static extern bool CreatePipe(ref IntPtr hReadPipe, ref IntPtr hWritePipe,
            SECURITY_ATTRIBUTES lpPipeAttributes, int nSize);

        [DllImport("kernel32.dll")]
        public static extern bool CreateProcess(string lpApplicationName, string lpCommandLine,
            SECURITY_ATTRIBUTES lpProcessAttributes, SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles,
            int dwCreationFlags, string lpEnvironment, string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo,
            ref PROCESS_INFORMATION lpProcessInformation);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        public static Delegate GetAddress(IntPtr dllModule, string functionName, Type t)
        {
            IntPtr procAddress = GetProcAddress(dllModule, functionName);
            if (procAddress == IntPtr.Zero) return null;
            return Marshal.GetDelegateForFunctionPointer(procAddress, t);
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll")]
        public static extern bool ReadFile(IntPtr hFile, byte[] lpBuffer, int nNumberOfBytesToRead,
            ref int lpNumberOfBytesRead, IntPtr lpOverlapped);
    }
}