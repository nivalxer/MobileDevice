using System;
using System.Runtime.InteropServices;

namespace LibMobileDevice.CoreFundation
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct CFRange
    {
        internal IntPtr Location, Length;

        internal CFRange(int location, int len) : this((IntPtr) location, (IntPtr) len)
        {
        }

        internal CFRange(IntPtr location, IntPtr len)
        {
            Location = location;
            Length = len;
        }
    }
}
