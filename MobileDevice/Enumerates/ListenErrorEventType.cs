using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDevice.Enumerates
{
    /// <summary>
    /// 监听错误类型
    /// </summary>
    public enum ListenErrorEventType
    {
        /// <summary>
        /// 开始监听阶段
        /// </summary>
        StartListen = 0,
        /// <summary>
        /// 设备链接处理阶段
        /// </summary>
        Connect = 1
    }
}
