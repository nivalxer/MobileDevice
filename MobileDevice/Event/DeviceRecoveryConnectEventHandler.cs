using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDevice.Event
{
    /// <summary>
    /// 设备恢复模式下连接事件
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The <see cref="DeviceCommonConnectEventArgs"/> instance containing the event data.</param>
    public delegate void DeviceRecoveryConnectEventHandler(object sender, DeviceRecoveryConnectEventArgs args);
}
