using MobileDevice.Enum;
using MobileDevice.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDevice.Event
{
    public unsafe class ConnectEventArgs : EventArgs
    {
        private readonly IntPtr device;
        private readonly ConnectNotificationMessage message;

        internal ConnectEventArgs(AMDeviceNotificationCallbackInfo cbi)
        {
            message = cbi.Msg;
            device = cbi.DevicePtr;
        }

        public IntPtr Device
        {
            get { return device; }
        }

        public ConnectNotificationMessage Message
        {
            get { return message; }
        }
    }
}
