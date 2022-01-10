using LibMobileDevice.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMobileDevice.Event
{
    public class DeviceCommonConnectEventArgs : EventArgs
    {
        private readonly iOSDevice device;
        private readonly ConnectNotificationMessage message;

        internal DeviceCommonConnectEventArgs(iOSDevice device, ConnectNotificationMessage message)
        {
            this.device = device;
            this.message = message;
        }

        public iOSDevice Device
        {
            get { return device; }
        }

        public ConnectNotificationMessage Message
        {
            get { return message; }
        }
    }
}
