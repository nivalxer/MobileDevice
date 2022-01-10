using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibMobileDevice.Struct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AMRecoveryDevice
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x80 * 2)]
        public byte[] devicePtr;
    }
}
