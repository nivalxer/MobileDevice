using System.Runtime.InteropServices;

namespace LibMobileDevice.CoreFundation
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct CFRange
    {
        internal long Location;
        internal long Length;
        internal CFRange(int l, int len)
        {
            Location = l;
            Length = len;
        }
    }
}
