using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LibMobileDevice.Struct;

namespace LibMobileDevice.Callback
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DeviceDFUNotificationCallback(ref AMDFUModeDevice callback_info);
}
