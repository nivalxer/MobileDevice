namespace MobileDevice
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate uint t_sendCommandToDevice(void* conn, void* cfs, int block);
}

