using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MobileDevice.Callback;

namespace MobileDevice.Struct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct AMDeviceNotification
    {
        private readonly uint unknown0;
        private readonly uint unknown1;
        private readonly uint unknown2;
        private readonly DeviceNotificationCallback callback;
        private readonly uint unknown3;
    }
}
