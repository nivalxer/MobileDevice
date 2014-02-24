namespace MobileDevice
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack=1)]
    internal struct AMDeviceNotificationCallbackInfo
    {
        internal unsafe void* dev_ptr;
        public NotificationMessage msg;
        public unsafe void* dev
        {
            get
            {
                return this.dev_ptr;
            }
        }
    }
}

