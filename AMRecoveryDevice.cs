namespace MobileDevice
{
    using System;
    using System.Runtime.InteropServices;

    /*[StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct AMRecoveryDevice
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
        public byte[] unknown0;
        public DeviceRestoreNotificationCallback callback;
        public IntPtr user_info;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=12)]
        public byte[] unknown1;
        public uint readwrite_pipe;
        public byte read_pipe;
        public byte write_ctrl_pipe;
        public byte read_unknown_pipe;
        public byte write_file_pipe;
        public byte write_input_pipe;
    }*/
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct AMRecoveryDevice
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x80)]
        public byte[] byte_0;
    }


}

