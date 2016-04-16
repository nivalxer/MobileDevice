using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace MobileDevice.CoreFundation
{
    public class CFBoolean
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);
        public static IntPtr GetCFBoolean(bool flag)
        {
            string strEnumName = flag ? "kCFBooleanTrue" : "kCFBooleanFalse";

            IntPtr modulePtr = GetModuleHandle("CoreFoundation.dll");
            if (modulePtr == IntPtr.Zero)
            {
                string appleApplicationSupportFolder = Helper.DLLHelper.GetAppleApplicationSupportFolder();
                if (!string.IsNullOrWhiteSpace(appleApplicationSupportFolder))
                {
                    modulePtr = LoadLibrary(Path.Combine(appleApplicationSupportFolder, "CoreFoundation.dll"));
                }
            }
            IntPtr zero = IntPtr.Zero;
            if (modulePtr != IntPtr.Zero) zero = GetProcAddress(modulePtr, strEnumName);
            return Marshal.ReadIntPtr(zero, 0);
        }
    }
}