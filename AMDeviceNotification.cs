namespace MobileDevice
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack=1)]
    internal struct AMDeviceNotification
    {
        private uint unknown0;
        private uint unknown1;
        private uint unknown2;
        private DeviceNotificationCallback callback;
        private uint unknown3;
    }
}

