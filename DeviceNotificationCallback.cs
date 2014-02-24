namespace MobileDevice
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DeviceNotificationCallback(ref AMDeviceNotificationCallbackInfo callback_info);
}

