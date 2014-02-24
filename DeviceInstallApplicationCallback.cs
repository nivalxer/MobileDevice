namespace MobileDevice
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    /// <summary>
    /// 安装回调，暂未实现
    /// </summary>
    /// <param name="dict"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DeviceInstallApplicationCallback(ref IntPtr dict);
}
