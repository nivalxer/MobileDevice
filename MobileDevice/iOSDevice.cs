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
    /// <summary>
    /// iOS设备操作类
    /// </summary>
    public class iOSDevice
    {
        #region 私有变量
        #endregion

        #region 公共变量
        /// <summary>
        /// 当前设备链接句柄
        /// </summary>
        public IntPtr DevicePtr;
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="devicePtr"></param>
        public iOSDevice(IntPtr devicePtr)
        {
            this.DevicePtr = devicePtr;
        }
    }
}
