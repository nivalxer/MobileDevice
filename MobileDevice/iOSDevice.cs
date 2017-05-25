using MobileDevice.Callback;
using MobileDevice.CoreFundation;
using MobileDevice.Enumerates;
using MobileDevice.Event;
using MobileDevice.Struct;
using MobileDevice.Unitiy;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace MobileDevice
{
    /// <summary>
    /// iOS设备操作类
    /// </summary>
    public class iOSDevice
    {
        #region 私有变量
        private bool isConnected = false;
        private bool isSessionOpen = false;
        private string activationState = string.Empty;
        private string basebandBootloaderVersion = string.Empty;
        private string basebandSerialNumber = string.Empty;
        private string basebandVersion = string.Empty;
        private string bluetoothAddress = string.Empty;
        private string buildVersion = string.Empty;
        private string cpuArchitecture = string.Empty;
        private DeviceColorKey deviceColor;
        private string deviceColorString = string.Empty;
        private string deviceColorBgString = string.Empty;
        private string deviceName = string.Empty;
        private string firmwareVersion = string.Empty;
        private string hardwareModel = string.Empty;
        private string modelNumber = string.Empty;
        private string phoneNumber = string.Empty;
        private string productType = string.Empty;
        private string integratedCircuitCardIdentity = string.Empty;
        private string internationalMobileEquipmentIdentity = string.Empty;
        private string serialNumber = string.Empty;
        private string simStatus = string.Empty;
        private string uniqueDeviceID = string.Empty;
        private string wiFiAddress = string.Empty;
        private string productVersion = string.Empty;
        private int versionNumber;
        #endregion
        #region 公共变量
        /// <summary>
        /// 当前设备链接句柄
        /// </summary>
        public IntPtr DevicePtr;
        /// <summary>
        /// 是否连接
        /// </summary>
        /// <value><c>true</c> if this instance is connected; otherwise, <c>false</c>.</value>
        public bool IsConnected
        {
            get
            {
                return this.isConnected;
            }
        }
        
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="devicePtr"></param>
        public iOSDevice(IntPtr devicePtr)
        {
            this.DevicePtr = devicePtr;
            ConnectAndInitService();
        }

        #region 连接与服务
        /// <summary>
        /// 链接到设备，用于设备链接失效或者手动推出后重链接操作，此函数只要设备还在USB树上，是无需用户插拔的
        /// </summary>
        /// <returns></returns>
        public kAMDError Connect()
        {
            kAMDError kAMDSuccess = kAMDError.kAMDSuccess;
            try
            {
                if (!this.isConnected)
                {
                    kAMDSuccess = (kAMDError)MobileDevice.AMDeviceConnect(this.DevicePtr);
                    if (kAMDSuccess == kAMDError.kAMDSuccess)
                    {
                        this.isConnected = true;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            return kAMDSuccess;
        }

        /// <summary>
        /// 断开设备链接，但不会断开USB树链接，因而可以通过调用Connect来重新链接到设备
        /// </summary>
        /// <returns></returns>
        public kAMDError Disconnect()
        {
            var kAMDSuccess = kAMDError.kAMDSuccess;
            this.isConnected = false;
            try
            {
                kAMDSuccess = (kAMDError)MobileDevice.AMDeviceDisconnect(DevicePtr);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            return kAMDSuccess;
        }

        /// <summary>
        /// 连接设备并初始化基础服务
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool ConnectAndInitService()
        {
            bool result = false;
            kAMDError kAMDUndefinedError = kAMDError.kAMDUndefinedError;
            try
            {
                if (this.Connect() != (int)kAMDError.kAMDSuccess)
                {
                    return false;
                }
                //TODO:初始化设备信息
                if (MobileDevice.AMDeviceValidatePairing(this.DevicePtr) != (int)kAMDError.kAMDSuccess)
                {
                    kAMDUndefinedError = (kAMDError)MobileDevice.AMDevicePair(this.DevicePtr);
                    if (kAMDUndefinedError != kAMDError.kAMDSuccess)
                    {
                        this.Disconnect();
                        return false;
                    }
                }
                this.isSessionOpen = false;//确保Session已打开
                if (this.StartSession(false) == (int)kAMDError.kAMDSuccess)
                {
                    //TODO:初始化服务，填充基础数据
                    this.isConnected = true;
                    this.StopSession();
                    result = true;
                }
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// 开启Session。Session需要在调用服务前先行打开，然后完成服务操作后自行关闭（函数会自动关闭），否则可能会遇到无法打开服务的错误
        /// </summary>
        /// <returns></returns>
        public kAMDError StartSession(bool isRretry = false)
        {
            var kAMDSuccess = kAMDError.kAMDSuccess;
            try
            {
                if (!this.isSessionOpen)
                {
                    kAMDSuccess = (kAMDError)MobileDevice.AMDeviceStartSession(this.DevicePtr);
                    if (kAMDSuccess != kAMDError.kAMDInvalidHostIDError)
                    {
                        if (kAMDSuccess != (int)kAMDError.kAMDSuccess)
                        {
                            //修复:Session关闭后一段时间无法再次打开的问题
                            if (!isRretry)
                            {
                                this.Disconnect();
                                this.Connect();
                                return StartSession(true);
                            }
                            return kAMDSuccess;
                        }
                        this.isSessionOpen = true;
                        return kAMDSuccess;
                    }
                    if ((MobileDevice.AMDeviceUnpair(this.DevicePtr) == (int)kAMDError.kAMDSuccess) &&
                        (MobileDevice.AMDevicePair(this.DevicePtr) == (int)kAMDError.kAMDSuccess))
                    {
                        kAMDSuccess = (kAMDError)MobileDevice.AMDeviceStartSession(this.DevicePtr);
                        if (kAMDSuccess != kAMDError.kAMDSuccess)
                        {
                            return kAMDSuccess;
                        }
                        this.isSessionOpen = true;
                        return kAMDSuccess;
                    }
                }
                return kAMDSuccess;
            }
            catch
            {
                kAMDSuccess = kAMDError.kAMDUndefinedError;
            }
            return kAMDSuccess;
        }

        /// <summary>
        /// 关闭Session，此函数无需外界单独调用，对应功能模块调用已经和开启Session成对存在
        /// </summary>
        /// <returns></returns>
        private kAMDError StopSession()
        {
            this.isSessionOpen = false;
            try
            {
                return (kAMDError)MobileDevice.AMDeviceStopSession(this.DevicePtr);
            }
            catch
            {
                return kAMDError.kAMDUndefinedError;
            }
        }

        /// <summary>
        /// 开启Socket服务
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="serviceSocket">Socket链接</param>
        /// <returns></returns>
        private bool StartSocketService(string serviceName, ref int serviceSocket)
        {
            var kAMDSuccess = kAMDError.kAMDSuccess;
            if (serviceSocket > 0)//已经开启服务
            {
                return true;
            }
            if (!this.isConnected)
            {
                if (Connect() != (int)kAMDError.kAMDSuccess)
                {
                    Console.WriteLine("StartService()执行Connect()失败");
                    return false;
                }
            }
            bool openSession = false;
            if(!this.isSessionOpen)
            {
                kAMDSuccess = StartSession();
                if (kAMDSuccess == kAMDError.kAMDSuccess)
                {
                    openSession = true;
                }
                else
                {
                    return false;
                }
            }
            var result = false;
            var zero = IntPtr.Zero;
            if ((MobileDevice.AMDeviceSecureStartService(this.DevicePtr, CoreFoundation.StringToCFString(serviceName),IntPtr.Zero, ref zero) == (int)kAMDError.kAMDSuccess))
            {
                serviceSocket = MobileDevice.AMDServiceConnectionGetSocket(zero);
                result = true;
            }
            else if (MobileDevice.AMDeviceStartService(this.DevicePtr, CoreFoundation.StringToCFString(serviceName),ref serviceSocket, IntPtr.Zero) == (int)kAMDError.kAMDSuccess)
            {
                result = true;
            }
            if(openSession)
            {
                StopSession();
            }
            return result;
        }

        /// <summary>
        /// 停止Socket服务
        /// </summary>
        /// <param name="inSocket"></param>
        /// <returns></returns>
        private bool StopSocketService(ref int socket)
        {
            kAMDError kAMDSuccess = kAMDError.kAMDSuccess;
            if (socket > 0)
            {
                try
                {
                    kAMDSuccess = (kAMDError)MobileDevice.closesocket(socket);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            socket = 0;
            return kAMDSuccess != kAMDError.kAMDSuccess;
        }

        /// <summary>
        /// 发送消息通过Socket，大部分用途为发送plist文件（指令）给设备
        /// </summary>
        /// <param name="sock"></param>
        /// <param name="message"></param>
        /// <returns></returns>

        public bool SendMessageToSocket(int sock, IntPtr message)
        {
            if ((sock < 1) || (message == IntPtr.Zero))
            {
                return false;
            }
            var flag = false;
            var stream = CoreFoundation.CFWriteStreamCreateWithAllocatedBuffers(IntPtr.Zero, IntPtr.Zero);
            if (stream != IntPtr.Zero)
            {
                if (!CoreFoundation.CFWriteStreamOpen(stream))
                {
                    return false;
                }
                var zero = IntPtr.Zero;
                if (CoreFoundation.CFPropertyListWriteToStream(message, stream,CFPropertyListFormat.kCFPropertyListBinaryFormat_v1_0, ref zero) > 0)
                {
                    var propertyName = CoreFoundation.kCFStreamPropertyDataWritten;
                    var srcRef = CoreFoundation.CFWriteStreamCopyProperty(stream, propertyName);
                    var buffer = CoreFoundation.CFDataGetBytePtr(srcRef);
                    var bufferlen = CoreFoundation.CFDataGetLength(srcRef);
                    var structure = MobileDevice.htonl((uint)bufferlen);
                    var num3 = Marshal.SizeOf(structure);
                    if (MobileDevice.send_UInt32(sock, ref structure, num3, 0) != num3)
                    {
                        Console.WriteLine("could not send message size");
                    }
                    else if (MobileDevice.send(sock, buffer, bufferlen, 0) != bufferlen)
                    {
                        Console.WriteLine("Could not send message.");
                    }
                    else
                    {
                        flag = true;
                    }
                    CoreFoundation.CFRelease(srcRef);
                }
                CoreFoundation.CFWriteStreamClose(stream);
            }
            return flag;
        }

        /// <summary>
        /// 发送Socket消息
        /// </summary>
        /// <param name="sock"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        public bool SendMessageToSocket(int sock, object dict)
        {
            var message = CoreFoundation.CFTypeFromManagedType(RuntimeHelpers.GetObjectValue(dict));
            return SendMessageToSocket(sock, message);
        }

        /// <summary>
        /// 接收Socket消息，主要为接收设备返回的指令结果，大部分为Plist，所以函数内将会自行作转换
        /// </summary>
        /// <param name="sock"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        public object ReceiveMessageFromSocket(int sock)
        {
            if (sock < 0)
            {
                return null;
            }
            var recvCount = -1;
            uint dataSize = 0;
            uint buffer = 0;
            uint reviceSize = 0;
            var zero = IntPtr.Zero;
            do
            {
                recvCount = MobileDevice.recv_UInt32(sock, ref buffer, 4, 0);
            } while (recvCount < 0);
            if (recvCount != 4)
            {
                return null;
            }
            dataSize = MobileDevice.ntohl(buffer);//获取数据总长度
            if (dataSize <= 0)
            {
                Console.WriteLine("receive size error, dataSize:" + dataSize);
                return null;
            }
            zero = Marshal.AllocCoTaskMem((int)dataSize);
            if (zero == IntPtr.Zero)
            {
                Console.WriteLine("Could not allocate message buffer.");
                return null;
            }
            var tempPtr = zero;
            while (reviceSize < dataSize)
            {
                recvCount = MobileDevice.recv(sock, tempPtr, (int)(dataSize - reviceSize), 0);
                if (recvCount <= -1)
                {
                    Console.WriteLine("Could not receive secure message: " + recvCount);
                    reviceSize = dataSize + 1;
                }
                else
                {
                    if (recvCount == 0)
                    {
                        Console.WriteLine("receive size is zero. ");
                        break;
                    }
                    tempPtr = new IntPtr(tempPtr.ToInt64() + recvCount);
                    reviceSize += (uint)recvCount;
                }
            }
            var datas = IntPtr.Zero;
            var srcRef = IntPtr.Zero;
            if (reviceSize == dataSize)
            {
                datas = CoreFoundation.CFDataCreate(CoreFoundation.kCFAllocatorDefault, zero, (int)dataSize);
                if (datas == IntPtr.Zero)
                {
                    Console.WriteLine("Could not create CFData for message");
                }
                else
                {
                    var errorString = IntPtr.Zero;
                    srcRef = CoreFoundation.CFPropertyListCreateFromXMLData(CoreFoundation.kCFAllocatorDefault, datas,
                        CFPropertyListMutabilityOptions.kCFPropertyListImmutable, ref errorString);
                    if (srcRef == IntPtr.Zero)
                    {
                        Console.WriteLine("Could not convert raw xml into a dictionary: " + Convert.ToString(CoreFoundation.ManagedTypeFromCFType(ref errorString)));
                        return null;
                    }
                }
            }
            if (datas != IntPtr.Zero)
            {
                try
                {
                    CoreFoundation.CFRelease(datas);
                }
                catch
                {
                }
            }
            if (zero != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(zero);
            }
            var result = CoreFoundation.ManagedTypeFromCFType(ref srcRef);
            if (srcRef != IntPtr.Zero)
            {
                CoreFoundation.CFRelease(srcRef);
            }
            return result;
        }
        #endregion

        #region 设备信息获取       

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        public object GetDeviceValue(DeviceInfoKey key)
        {
            return this.GetDeviceValue(null, key);
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        public object GetDeviceValue(string domain, DeviceInfoKey key)
        {
            return this.GetDeviceValue(domain, key.ToString());
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        public object GetDeviceValue(string domain, string key)
        {
            object resultValue = null;
            try
            {
                var isReconnect = false;
                var isReOpenSession = false;
                if (!this.isConnected)
                {
                    if (Connect() != (int)kAMDError.kAMDSuccess)
                    {
                        return null;
                    }
                    isReconnect = true;
                }
                if (!this.isSessionOpen)
                {
                    if (StartSession(false) == (int)kAMDError.kAMDSuccess)
                    {
                        isReOpenSession = true;
                    }
                    else
                    {
                        if (isReconnect)
                        {
                            Disconnect();
                        }
                    }
                }
                resultValue = MobileDevice.AMDeviceCopyValue(this.DevicePtr, domain, key);
                if (isReOpenSession)
                {
                    StopSession();
                }
                if (isReconnect)
                {
                    Disconnect();
                }
            }
            catch
            {
            }
            return resultValue;
        }

        /// <summary>
        /// 获取设备当前电量
        /// </summary>
        /// <returns></returns>
        public int GetBatteryCurrentCapacity()
        {
            try
            {
                string s = Convert.ToString(this.GetDeviceValue("com.apple.mobile.battery", DeviceInfoKey.BatteryCurrentCapacity)) + string.Empty;
                if (s.Length > 0)
                {
                    return SafeConvert.ToInt32(s);
                }
            }
            catch (Exception ex)
            {
            }
            return -1;
        }

        /// <summary>
        /// 获取设备当前是否充电
        /// </summary>
        /// <returns></returns>
        public bool GetBatteryIsCharging()
        {
            try
            {
                object deviceValue = this.GetDeviceValue("com.apple.mobile.battery", DeviceInfoKey.BatteryIsCharging);
                if ((deviceValue != null) && (deviceValue is bool))
                {
                    return Convert.ToBoolean(deviceValue);
                }
            }
            catch
            {
            }
            return false;
        }

        /// <summary>
        /// 获取电池信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<object, object> GetBatteryInfoFormDiagnostics()
        {
            try
            {
                var diagnosticsInfo = GetDiagnosticsInfo();
                if (diagnosticsInfo == null)
                {
                    return null;
                }
                var dicResult = diagnosticsInfo as Dictionary<object, object>;
                if (dicResult["Status"].ToString() != "Success")
                {
                    return null;
                }
                var diagnosticsData = dicResult["Diagnostics"] as Dictionary<object, object>;
                if (diagnosticsData == null)
                {
                    return null;
                }
                return diagnosticsData["GasGauge"] as Dictionary<object, object>;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取诊断信息
        /// </summary>
        /// <returns></returns>
        public object GetDiagnosticsInfo()
        {
            int socket = 0;
            var startSocketResult = StartSocketService("com.apple.mobile.diagnostics_relay", ref socket);
            if(!startSocketResult)
            {
                return null;
            }
            var dict = new Dictionary<object,object>();
            dict.Add("Request", "All");
            if (SendMessageToSocket(socket, dict))
            {
                var result = ReceiveMessageFromSocket(socket);
                return result;
            }
            return null;
        }

        #region 设备信息字段        
        public string ActivationState
        {
            get
            {
                if (this.activationState.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.ActivationState);
                    if (deviceValue != null)
                    {
                        this.activationState = deviceValue.ToString();
                    }
                }
                return this.activationState;
            }
        }

        public string BasebandBootloaderVersion
        {
            get
            {
                if (this.basebandBootloaderVersion.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.BasebandBootloaderVersion);
                    if (deviceValue != null)
                    {
                        this.basebandBootloaderVersion = deviceValue.ToString();
                    }
                }
                return this.basebandBootloaderVersion;
            }
        }

        public string BasebandSerialNumber
        {
            get
            {
                if (this.basebandSerialNumber.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.BasebandSerialNumber);
                    if (deviceValue != null)
                    {
                        this.basebandSerialNumber = deviceValue.ToString();
                    }
                }
                return this.basebandSerialNumber;
            }
        }

        public string BasebandVersion
        {
            get
            {
                if (this.basebandVersion.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.BasebandVersion);
                    if (deviceValue != null)
                    {
                        this.basebandVersion = deviceValue.ToString();
                    }
                }
                return this.basebandVersion;
            }
        }

        public string BluetoothAddress
        {
            get
            {
                if (this.bluetoothAddress.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.BluetoothAddress);
                    if (deviceValue != null)
                    {
                        this.bluetoothAddress = deviceValue.ToString();
                    }
                }
                return this.bluetoothAddress;
            }
        }

        public string BuildVersion
        {
            get
            {
                if (this.buildVersion.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.BuildVersion);
                    if (deviceValue != null)
                    {
                        this.buildVersion = deviceValue.ToString();
                    }
                }
                return this.buildVersion;
            }
        }

        public string CPUArchitecture
        {
            get
            {
                if (this.cpuArchitecture.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.CPUArchitecture);
                    if (deviceValue != null)
                    {
                        this.cpuArchitecture = deviceValue.ToString();
                    }
                }
                return this.cpuArchitecture;
            }
        }

        public DeviceColorKey DeviceColor
        {
            get
            {
                if(this.deviceColor == DeviceColorKey.Default)
                {
                    var deviceColorValue = GetDeviceValue(DeviceInfoKey.DeviceColor);
                    if (deviceColorValue != null)
                    {
                        var deviceColorString = deviceColorValue.ToString();
                        if (!string.IsNullOrWhiteSpace(deviceColorString))
                        {
                            switch(deviceColorString.ToLower())
                            {
                                case "black":
                                case "#99989b":
                                case "1":
                                    this.deviceColor = DeviceColorKey.Black;
                                    break;
                                case "silver":
                                case "#d7d9d8":
                                case "2":
                                    this.deviceColor = DeviceColorKey.Silver;
                                    break;
                                case "gold":
                                case "#d4c5b3":
                                case "3":
                                    this.deviceColor = DeviceColorKey.Gold;
                                    break;
                                case "rose gold":
                                case "#e1ccb5":
                                case "4":
                                    this.deviceColor = DeviceColorKey.Rose_Gold;
                                    break;
                                case "jet black":
                                case "#0a0a0a":
                                case "5":
                                    this.deviceColor = DeviceColorKey.Jet_Black;
                                    break;
                                default:
                                    this.deviceColor = DeviceColorKey.Unknown;
                                    break;
                            }
                        }
                    }
                }
                return this.deviceColor;
            }
        }

        public string DeviceName
        {
            get
            {
                if (this.deviceName.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.DeviceName);
                    if (deviceValue != null)
                    {
                        this.deviceName = deviceValue.ToString();
                    }
                }
                return this.deviceName;
            }
        }

        public string FirmwareVersion
        {
            get
            {
                if (this.firmwareVersion.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.FirmwareVersion);
                    if (deviceValue != null)
                    {
                        this.firmwareVersion = deviceValue.ToString();
                    }
                }
                return this.firmwareVersion;
            }
        }

        public string HardwareModel
        {
            get
            {
                if (this.hardwareModel.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.HardwareModel);
                    if (deviceValue != null)
                    {
                        this.hardwareModel = deviceValue.ToString();
                    }
                }
                return this.hardwareModel;
            }
        }
        public string ModelNumber
        {
            get
            {
                if (this.modelNumber.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.ModelNumber);
                    if (deviceValue != null)
                    {
                        this.modelNumber = deviceValue.ToString();
                    }
                }
                return this.modelNumber;
            }
        }

        public string PhoneNumber
        {
            get
            {
                if (this.phoneNumber.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.PhoneNumber);
                    if (deviceValue != null)
                    {
                        this.phoneNumber = deviceValue.ToString();
                    }
                }
                return this.phoneNumber;
            }
        }

        public string ProductType
        {
            get
            {
                if (this.productType.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.ProductType);
                    if (deviceValue != null)
                    {
                        this.productType = deviceValue.ToString();
                    }
                }
                return this.productType;
            }
        }

        public string IntegratedCircuitCardIdentity
        {
            get
            {
                if (this.integratedCircuitCardIdentity.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.IntegratedCircuitCardIdentity);
                    if (deviceValue != null)
                    {
                        this.integratedCircuitCardIdentity = deviceValue.ToString();
                    }
                }
                return this.integratedCircuitCardIdentity;
            }
        }

        public string InternationalMobileEquipmentIdentity
        {
            get
            {
                if (this.internationalMobileEquipmentIdentity.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.InternationalMobileEquipmentIdentity);
                    if (deviceValue != null)
                    {
                        this.internationalMobileEquipmentIdentity = deviceValue.ToString();
                    }
                }
                return this.internationalMobileEquipmentIdentity;
            }
        }

        public string SerialNumber
        {
            get
            {
                if (this.serialNumber.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.SerialNumber);
                    if (deviceValue != null)
                    {
                        this.serialNumber = deviceValue.ToString();
                    }
                }
                return this.serialNumber;
            }
        }

        public string SIMStatus
        {
            get
            {
                if (this.simStatus.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.SIMStatus);
                    if (deviceValue != null)
                    {
                        this.simStatus = deviceValue.ToString();
                    }
                }
                return this.simStatus;
            }
        }

        public string UniqueDeviceID
        {
            get
            {
                if (this.uniqueDeviceID.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.UniqueDeviceID);
                    if (deviceValue != null)
                    {
                        this.uniqueDeviceID = deviceValue.ToString();
                    }
                }
                return this.uniqueDeviceID;
            }
        }

        public string WiFiAddress
        {
            get
            {
                if (this.wiFiAddress.Length == 0)
                {
                    object deviceValue = this.GetDeviceValue(DeviceInfoKey.WiFiAddress);
                    if (deviceValue != null)
                    {
                        this.wiFiAddress = deviceValue.ToString();
                    }
                }
                return this.wiFiAddress;
            }
        }
        public string ProductVersion
        {
            get
            {
                if (string.IsNullOrEmpty(this.productVersion))
                {
                    this.productVersion = Convert.ToString(this.GetDeviceValue(DeviceInfoKey.ProductVersion)) + string.Empty;
                }
                return this.productVersion;
            }
        }

        public int VersionNumber
        {
            get
            {
                if (this.versionNumber == 0)
                {
                    this.versionNumber = Convert.ToInt32(this.ProductVersion.Replace(".", string.Empty).PadRight(3, '0'));
                }
                return this.versionNumber;
            }
        }

        public string Identifier
        {
            get
            {
                string str = string.Empty;
                IntPtr zero = IntPtr.Zero;
                try
                {
                    zero = MobileDevice.AMDeviceCopyDeviceIdentifier(this.DevicePtr);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
                finally
                {
                    if (zero != IntPtr.Zero)
                    {
                        str = Convert.ToString(CoreFoundation.ManagedTypeFromCFType(ref zero));
                        CoreFoundation.CFRelease(zero);
                    }
                }
                return str;
            }
        }
        #endregion
        #endregion
    }
}
