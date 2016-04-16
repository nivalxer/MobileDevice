using System.Runtime.InteropServices;

namespace MobileDevice.CoreFundation
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct CFRange
    {
        internal long Location;
        internal long Length;
        internal CFRange(int l, int len)
        {
            this.Location = l;
            this.Length = len;
        }
    }
}
