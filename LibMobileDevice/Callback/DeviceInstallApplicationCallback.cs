using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibMobileDevice.Callback
{
    /// <summary>
    /// 安装IPA回调
    /// </summary>
    /// <param name="dict">The dictionary.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DeviceInstallApplicationCallback(IntPtr dict);
}
