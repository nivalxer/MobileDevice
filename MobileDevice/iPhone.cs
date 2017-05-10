using MobileDevice.Callback;
using MobileDevice.Enum;
using MobileDevice.Event;
using MobileDevice.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDevice
{
    public class iPhone
    {
        #region 私有变量
        private DeviceNotificationCallback deviceNotificationCallback;
        private DeviceRestoreNotificationCallback deviceRecoveryConnectedNotificationCallback;
        private DeviceRestoreNotificationCallback deviceRecoveryDisConnectedNotificationCallback;
        private DeviceDFUNotificationCallback deviceDFUConnectedNotificationCallback;
        private DeviceDFUNotificationCallback deviceDFUDisConnectedNotificationCallback;
        private bool wasAFC2;
        private IntPtr devicePtr;//当前链接设备句柄
        #endregion

        #region 公共变量
        public event ConnectEventHandler Connect;
        public event ConnectEventHandler Disconnect;
        #endregion

        /// <summary>
        /// 初始化iPhone
        /// </summary>
        /// <param name="myConnectHandler">链接</param>
        /// <param name="myDisconnectHandler">断开链接</param>
        public iPhone(ConnectEventHandler connectHandler, ConnectEventHandler disconnectHandler)
        {
            wasAFC2 = false;
            Connect = (ConnectEventHandler)Delegate.Combine(Connect, disconnectHandler);
            Disconnect = (ConnectEventHandler)Delegate.Combine(Disconnect, disconnectHandler);
            DoRegConnect();
        }

        /// <summary>
        /// 正常链接回调
        /// </summary>
        /// <param name="callback">The callback.</param>
        private void NotificationCallback(ref AMDeviceNotificationCallbackInfo callback)
        {
            switch (callback.Msg)
            {
                case ConnectNotificationMessage.Connected:
                    this.devicePtr = callback.DevicePtr;
                    break;
                case ConnectNotificationMessage.Disconnected:
                    break;
                case ConnectNotificationMessage.Unknown:
                    break;
            }
        }

        /// <summary>
        /// DFU模式链接回调
        /// </summary>
        /// <param name="callback">The callback.</param>
        private void DfuConnectCallback(ref AMDFUModeDevice callback)
        {

        }

        /// <summary>
        /// DFU断开链接回调
        /// </summary>
        /// <param name="callback">The callback.</param>
        private void DfuDisconnectCallback(ref AMDFUModeDevice callback)
        {

        }

        /// <summary>
        /// 恢复模式链接回调
        /// </summary>
        /// <param name="callback">The callback.</param>
        private void RecoveryConnectCallback(ref AMRecoveryDevice callback)
        {

        }

        /// <summary>
        /// 恢复模式断开链接回调
        /// </summary>
        /// <param name="callback">The callback.</param>
        private void RecoveryDisconnectCallback(ref AMRecoveryDevice callback)
        {

        }

        /// <summary>
        /// 配置链接，注册通知回调
        /// </summary>
        /// <exception cref="Exception">
        /// AMDeviceNotificationSubscribe failed with error : " + error.ToString()
        /// or
        /// AMRestoreRegisterForDeviceNotifications failed with error : " + error.ToString()
        /// </exception>
        private void DoRegConnect()
        {
            deviceNotificationCallback = NotificationCallback;
            deviceDFUConnectedNotificationCallback = DfuConnectCallback;
            deviceRecoveryConnectedNotificationCallback = RecoveryConnectCallback;
            deviceDFUDisConnectedNotificationCallback = DfuDisconnectCallback;
            deviceRecoveryDisConnectedNotificationCallback = RecoveryDisconnectCallback;
            IntPtr zero = IntPtr.Zero;
            kAMDError error = (kAMDError)MobileDevice.AMDeviceNotificationSubscribe(this.deviceNotificationCallback, 0, 1, 0, ref zero);
            if (error != kAMDError.kAMDSuccess)
            {
                throw new Exception("AMDeviceNotificationSubscribe failed with error : " + error.ToString());
            }
            IntPtr userInfo = IntPtr.Zero;
            error = (kAMDError)MobileDevice.AMRestoreRegisterForDeviceNotifications(this.deviceDFUConnectedNotificationCallback, this.deviceRecoveryConnectedNotificationCallback, this.deviceDFUDisConnectedNotificationCallback,this.deviceRecoveryDisConnectedNotificationCallback ,0, ref userInfo);
            if (error != kAMDError.kAMDSuccess)
            {
                throw new Exception("AMRestoreRegisterForDeviceNotifications failed with error : " + error.ToString());
            }
        }
    }
}
