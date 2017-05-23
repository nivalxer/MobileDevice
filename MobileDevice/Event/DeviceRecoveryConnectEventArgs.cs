using MobileDevice.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDevice.Event
{
    public class DeviceRecoveryConnectEventArgs : EventArgs
    {
        private byte[] devicePtr;
        private ConnectNotificationMessage message;

        public byte[] DevicePtr
        {
            get
            {
                return this.devicePtr;
            }
        }

        public ConnectNotificationMessage Message
        {
            get
            {
                return this.message;
            }
        }

        public DeviceRecoveryConnectEventArgs(byte[] devicePtr, ConnectNotificationMessage message)
        {
            this.devicePtr = devicePtr;
            this.message = message;
        }
    }
}
