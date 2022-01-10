using LibMobileDevice.Callback;
using LibMobileDevice.Enumerates;
using LibMobileDevice.Event;
using LibMobileDevice.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMobileDevice.CoreFundation;

namespace LibMobileDevice
{
    /// <summary>
    /// iOS设备管理类
    /// </summary>
    public class iOSDeviceManager
    {
        private bool iSStartListen = false;
        private DeviceNotificationCallback deviceNotificationCallback;
        private DeviceRestoreNotificationCallback deviceRecoveryConnectedNotificationCallback;
        private DeviceRestoreNotificationCallback deviceRecoveryDisConnectedNotificationCallback;
        private DeviceDFUNotificationCallback deviceDFUConnectedNotificationCallback;
        private DeviceDFUNotificationCallback deviceDFUDisConnectedNotificationCallback;
        private List<iOSDevice> currentConnectedDevice = new List<iOSDevice>(); //当前链接设备

        #region 公共变量

        /// <summary>
        /// 普通状态链接设备
        /// </summary>
        public event DeviceCommonConnectEventHandler CommonConnectEvent;

        /// <summary>
        /// 恢复模式链接设备
        /// </summary>
        public event DeviceRecoveryConnectEventHandler RecoveryConnectEvent;

        /// <summary>
        /// 监听错误回调
        /// </summary>
        public event EventHandler<ListenErrorEventHandlerEventArgs> ListenErrorEvent;

        /// <summary>
        /// 获取当前已链接设备
        /// </summary>
        /// <value>The current connected device.</value>
        public List<iOSDevice> CurrentConnectedDevice => currentConnectedDevice;

        #endregion

        /// <summary>
        /// 正常链接回调
        /// </summary>
        /// <param name="callback">The callback.</param>
        private void NotificationCallback(ref AMDeviceNotificationCallbackInfo callback)
        {
            try
            {
                switch (callback.Msg)
                {
                    case ConnectNotificationMessage.Connected:
                        var device = FindConnectedDevice(callback.DevicePtr);
                        if (device == null)
                        {
                            device = new iOSDevice(callback.DevicePtr);
                            currentConnectedDevice.Add(device);
                        }

                        CommonConnectEvent?.Invoke(this, new DeviceCommonConnectEventArgs(device, ConnectNotificationMessage.Connected));

                        break;
                    case ConnectNotificationMessage.Disconnected:
                        var disConnectDevice = FindConnectedDevice(callback.DevicePtr);
                        if (disConnectDevice != null)
                        {
                            currentConnectedDevice.Remove(disConnectDevice);
                        }

                        CommonConnectEvent?.Invoke(this, new DeviceCommonConnectEventArgs(disConnectDevice, ConnectNotificationMessage.Disconnected));

                        break;
                    case ConnectNotificationMessage.Unknown:
                        break;
                }
            }
            catch (Exception ex)
            {
                ListenErrorEvent?.Invoke(this, new ListenErrorEventHandlerEventArgs(ex.Message, ListenErrorEventType.Connect));
            }
        }

        /// <summary>
        /// 从当前已链接设备中查找
        /// </summary>
        /// <param name="devicePtr">The device PTR.</param>
        /// <returns>iOSDevice.</returns>
        private iOSDevice FindConnectedDevice(IntPtr devicePtr)
        {
            return currentConnectedDevice.FirstOrDefault(p => p.DevicePtr == devicePtr);
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
            RecoveryConnectEvent?.Invoke(this, new DeviceRecoveryConnectEventArgs(callback.devicePtr, ConnectNotificationMessage.Connected));
        }

        /// <summary>
        /// 恢复模式断开链接回调
        /// </summary>
        /// <param name="callback">The callback.</param>
        private void RecoveryDisconnectCallback(ref AMRecoveryDevice callback)
        {
            RecoveryConnectEvent?.Invoke(this, new DeviceRecoveryConnectEventArgs(callback.devicePtr, ConnectNotificationMessage.Disconnected));
        }

        /// <summary>
        /// 开始监听链接
        /// 需要使用线程方式调用
        /// </summary>
        public void StartListen()
        {
            if (!iSStartListen)
            {
                iSStartListen = true;
                try
                {
                    deviceNotificationCallback = NotificationCallback;
                    deviceDFUConnectedNotificationCallback = DfuConnectCallback;
                    deviceRecoveryConnectedNotificationCallback = RecoveryConnectCallback;
                    deviceDFUDisConnectedNotificationCallback = DfuDisconnectCallback;
                    deviceRecoveryDisConnectedNotificationCallback = RecoveryDisconnectCallback;
                    IntPtr zero = IntPtr.Zero;
                    kAMDError error = (kAMDError)MobileDevice.AMDeviceNotificationSubscribe(deviceNotificationCallback, 0, 1, 0, ref zero);
                    if (error != kAMDError.kAMDSuccess)
                    {
                        //Check "Apple Mobile Device Service" status
                        ListenErrorEvent?.Invoke(this, new ListenErrorEventHandlerEventArgs("AMDeviceNotificationSubscribe failed with error : " + error, ListenErrorEventType.StartListen));
                    }

                    IntPtr userInfo = IntPtr.Zero;
                    error = (kAMDError)MobileDevice.AMRestoreRegisterForDeviceNotifications(deviceDFUConnectedNotificationCallback, deviceRecoveryConnectedNotificationCallback, deviceDFUDisConnectedNotificationCallback, deviceRecoveryDisConnectedNotificationCallback, 0,
                        ref userInfo);
                    if (error != kAMDError.kAMDSuccess)
                    {
                        ListenErrorEvent?.Invoke(this, new ListenErrorEventHandlerEventArgs("AMRestoreRegisterForDeviceNotifications failed with error : " + error, ListenErrorEventType.StartListen));
                    }
                    //LoopRun
                    CoreFoundation.CFRunLoopRun();
                }
                catch (Exception ex)
                {
                    if (ListenErrorEvent != null)
                    {
                        ListenErrorEvent(this, new ListenErrorEventHandlerEventArgs(ex.Message, ListenErrorEventType.StartListen));
                    }
                }
            }
        }
    }
}