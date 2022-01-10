using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LibMobileDevice.Enumerates;

namespace LibMobileDevice.Struct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct AMDeviceNotificationCallbackInfo
    {
        internal IntPtr DevicePtr;
        public ConnectNotificationMessage Msg;
        public uint NotificationId;
    }
}
