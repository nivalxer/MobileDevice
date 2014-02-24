namespace MobileDevice
{
    using System;

    public class DeviceNotificationEventArgs : EventArgs
    {
        private AMRecoveryDevice device;

        internal DeviceNotificationEventArgs(AMRecoveryDevice device)
        {
            this.device = device;
        }

        internal AMRecoveryDevice Device
        {
            get
            {
                return this.device;
            }
        }
    }
}

