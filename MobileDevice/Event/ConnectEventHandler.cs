using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDevice.Event
{
    /// <summary>
    /// 设备连接事件
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The <see cref="ConnectEventArgs"/> instance containing the event data.</param>
    public delegate void ConnectEventHandler(object sender, ConnectEventArgs args);
}
