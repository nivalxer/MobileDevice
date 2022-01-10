using LibMobileDevice.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMobileDevice.Event
{
    public class DeviceRecoveryConnectEventArgs : EventArgs
    {
        private byte[] devicePtr;
        private ConnectNotificationMessage message;

        public byte[] DevicePtr
        {
            get
            {
                return devicePtr;
            }
        }

        public ConnectNotificationMessage Message
        {
            get
            {
                return message;
            }
        }

        public DeviceRecoveryConnectEventArgs(byte[] devicePtr, ConnectNotificationMessage message)
        {
            this.devicePtr = devicePtr;
            this.message = message;
        }
    }
}
