using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading;
using LibMobileDevice.CoreFundation;
using LibMobileDevice.Unitiy;
using LibMobileDevice.Enumerates;

namespace LibMobileDevice
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
        private string deviceColor = string.Empty;
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
            get { return isConnected; }
        }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="devicePtr"></param>
        public iOSDevice(IntPtr devicePtr)
        {
            DevicePtr = devicePtr;
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
                if (!isConnected)
                {
                    kAMDSuccess = (kAMDError)MobileDevice.AMDeviceConnect(DevicePtr);
                    if (kAMDSuccess == kAMDError.kAMDSuccess)
                    {
                        isConnected = true;
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
            isConnected = false;
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
                if (Connect() != (int)kAMDError.kAMDSuccess)
                {
                    return false;
                }

                //TODO:初始化设备信息
                if (MobileDevice.AMDeviceValidatePairing(DevicePtr) != (int)kAMDError.kAMDSuccess)
                {
                    kAMDUndefinedError = (kAMDError)MobileDevice.AMDevicePair(DevicePtr);
                    if (kAMDUndefinedError != kAMDError.kAMDSuccess)
                    {
                        Disconnect();
                        return false;
                    }
                }

                isSessionOpen = false; //确保Session已打开
                if (StartSession(false) == (int)kAMDError.kAMDSuccess)
                {
                    //TODO:初始化服务，填充基础数据
                    isConnected = true;
                    StopSession();
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
                if (!isSessionOpen)
                {
                    kAMDSuccess = (kAMDError)MobileDevice.AMDeviceStartSession(DevicePtr);
                    if (kAMDSuccess != kAMDError.kAMDInvalidHostIDError)
                    {
                        if (kAMDSuccess != (int)kAMDError.kAMDSuccess)
                        {
                            //修复:Session关闭后一段时间无法再次打开的问题
                            if (!isRretry)
                            {
                                Disconnect();
                                Connect();
                                return StartSession(true);
                            }

                            return kAMDSuccess;
                        }

                        isSessionOpen = true;
                        return kAMDSuccess;
                    }

                    if (MobileDevice.AMDeviceUnpair(DevicePtr) == (int)kAMDError.kAMDSuccess &&
                        MobileDevice.AMDevicePair(DevicePtr) == (int)kAMDError.kAMDSuccess)
                    {
                        kAMDSuccess = (kAMDError)MobileDevice.AMDeviceStartSession(DevicePtr);
                        if (kAMDSuccess != kAMDError.kAMDSuccess)
                        {
                            return kAMDSuccess;
                        }

                        isSessionOpen = true;
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
            isSessionOpen = false;
            try
            {
                return (kAMDError)MobileDevice.AMDeviceStopSession(DevicePtr);
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
            if (serviceSocket > 0) //已经开启服务
            {
                return true;
            }

            if (!isConnected)
            {
                if (Connect() != (int)kAMDError.kAMDSuccess)
                {
                    Console.WriteLine("StartService()执行Connect()失败");
                    return false;
                }
            }

            bool openSession = false;
            if (!isSessionOpen)
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
            if (MobileDevice.AMDeviceSecureStartService(DevicePtr, CoreFoundation.StringToCFString(serviceName), IntPtr.Zero, ref zero) == (int)kAMDError.kAMDSuccess)
            {
                serviceSocket = MobileDevice.AMDServiceConnectionGetSocket(zero);
                result = true;
            }
            else if (MobileDevice.AMDeviceStartService(DevicePtr, CoreFoundation.StringToCFString(serviceName), ref serviceSocket, IntPtr.Zero) == (int)kAMDError.kAMDSuccess)
            {
                result = true;
            }

            if (openSession)
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
            if (sock < 1 || message == IntPtr.Zero)
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
                if (CoreFoundation.CFPropertyListWriteToStream(message, stream, CFPropertyListFormat.kCFPropertyListBinaryFormat_v1_0, ref zero) > 0)
                {
                    IntPtr srcRef = CoreFoundation.CFWriteStreamCopyProperty(stream, CoreFoundation.kCFStreamPropertyDataWritten);
                    IntPtr buffer = CoreFoundation.CFDataGetBytePtr(srcRef);
                    var bufferlen = CoreFoundation.CFDataGetLength(srcRef);
                    var structure = MobileDevice.htonl((uint)bufferlen);
                    var structureSize = Marshal.SizeOf(structure);
                    if (MobileDevice.send_UInt32(sock, ref structure, structureSize, 0) != structureSize)
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
            var message = CoreFoundation.CFTypeFromManagedType(dict);
            return SendMessageToSocket(sock, message);
        }

        /// <summary>
        /// 接收Socket消息，主要为接收设备返回的指令结果，大部分为Plist，所以函数内将会自行作转换
        /// </summary>
        /// <param name="sock"></param>
        /// <returns></returns>
        public object ReceiveMessageFromSocket(int sock)
        {
            if (sock < 0)
            {
                return null;
            }

            int recvCount;
            uint buffer = 0;
            uint reviceSize = 0;
            if (MobileDevice.recv_UInt(sock, ref buffer, 4, 0) != 4)
            {
                return null;
            }

            var dataSize = MobileDevice.ntohl(buffer); //获取数据总长度
            if (dataSize <= 0)
            {
                Console.WriteLine("receive size error, dataSize:" + dataSize);
                return null;
            }

            IntPtr zero = Marshal.AllocCoTaskMem((int)dataSize);
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
                else if (recvCount == 0)
                {
                    Console.WriteLine("receive size is zero. ");
                    break;
                }

                tempPtr = new IntPtr(tempPtr.ToInt64() + recvCount);
                reviceSize += (uint)recvCount;
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

        /// <summary>
        /// 开启AFC服务
        /// </summary>
        /// <param name="inSocket">socket句柄</param>
        /// <returns></returns>
        public bool OpenConnection(int inSocket, out IntPtr connPtr)
        {
            connPtr = IntPtr.Zero;
            try
            {
                if (MobileDevice.AFCConnectionOpen(new IntPtr(inSocket), 0, ref connPtr) == (int)kAMDError.kAMDSuccess)
                {
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }

        #endregion

        #region USBMuxConnect

        /// <summary>
        /// USBMux端口映射
        /// </summary>
        /// <param name="localServeIp">本地服务器IP</param>
        /// <param name="localPort">本地监听端口</param>
        /// <param name="remotePort">远端（iOS）端口</param>
        public void CreateUSBMuxConnect(string localServeIp = "127.0.0.1", int localPort = 2222, uint remotePort = 22)
        {
            //连接打开成功，建立双向通道负责数据转发
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //创建Socket对象
            IPAddress serverIP = IPAddress.Parse(localServeIp);
            IPEndPoint server = new IPEndPoint(serverIP, localPort); //实例化服务器的IP和端口
            s.Bind(server);
            s.Listen(0);
            int socket = 0;
            Socket cSocket;
            cSocket = s.Accept(); //用cSocket来代表该客户端连接
            int ret = MobileDevice.USBMuxConnectByPort(MobileDevice.AMDeviceGetConnectionID(DevicePtr),
                MobileDevice.htons(remotePort), ref socket);
            if (ret != (int)kAMDError.kAMDSuccess)
            {
                //连接错误，跳出
                throw new Exception($"USBMuxConnectByPort Error.Code:{ret}");
            }

            IntPtr conn = IntPtr.Zero;
            if (OpenConnection(socket, out conn))//socket链接通过AFC函数来打开，然后剩下工作交给映射的recv和send函数，函数模型和winsock相同
            {
                if (cSocket.Connected) //测试是否连接成功
                {
                    Thread serverToRemoteThread = new Thread(() => { ConnForwardingThread(cSocket.Handle.ToInt32(), socket); });
                    serverToRemoteThread.Start();
                    Thread remoteToServerThread = new Thread(() => { ConnForwardingThread(socket, cSocket.Handle.ToInt32()); });
                    remoteToServerThread.Start();
                }
            }
        }

        /// <summary>
        /// Connections the forwarding thread.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        private void ConnForwardingThread(int from, int to)
        {
            byte[] recv_buf = new byte[256];
            int bytes_recv, bytes_send;

            while (true)
            {
                bytes_recv = MobileDevice.recv(from, recv_buf, 256, 0);
                if (bytes_recv == -1)
                {
                    //string errorMsg = new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error()).Message;//Winsock错误获取
                    int errorno = Marshal.GetLastWin32Error();
                    if (errorno == 10035) //10035 Socket无数据报错，可能函数原型为异步非阻塞，这里作为阻塞式调用而导致异常，可以忽略
                    {
                        continue;
                    }

                    MobileDevice.closesocket(from);
                    MobileDevice.closesocket(to);
                    break;
                }

                bytes_send = MobileDevice.send(to, recv_buf, bytes_recv, 0);

                if (bytes_recv == 0 || bytes_recv == -1 || bytes_send == 0 || bytes_send == -1)
                {
                    MobileDevice.closesocket(from);
                    MobileDevice.closesocket(to);

                    break;
                }
            }
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
            return GetDeviceValue(null, key);
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        public object GetDeviceValue(string domain, DeviceInfoKey key)
        {
            return GetDeviceValue(domain, key.ToString());
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
                if (!isConnected)
                {
                    if (Connect() != (int)kAMDError.kAMDSuccess)
                    {
                        return null;
                    }

                    isReconnect = true;
                }

                if (!isSessionOpen)
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

                resultValue = MobileDevice.AMDeviceCopyValue(DevicePtr, domain, key);
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
                string s = Convert.ToString(GetDeviceValue("com.apple.mobile.battery", DeviceInfoKey.BatteryCurrentCapacity)) + string.Empty;
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
                object deviceValue = GetDeviceValue("com.apple.mobile.battery", DeviceInfoKey.BatteryIsCharging);
                if (deviceValue != null && deviceValue is bool)
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
                var diagnosticsInfo = GetBatteryInfo();
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

                return diagnosticsData["IORegistry"] as Dictionary<object, object>;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取电池信息
        /// </summary>
        /// <returns></returns>
        public object GetBatteryInfo()
        {
            int socket = 0;
            var startSocketResult = StartSocketService("com.apple.mobile.diagnostics_relay", ref socket);
            if (!startSocketResult)
            {
                return null;
            }

            var dict = new Dictionary<object, object>
            {
                {
                    "Request",
                    "IORegistry"
                },
                {
                    "EntryClass",
                    "IOPMPowerSource"
                }
            };
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
                if (activationState.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.ActivationState);
                    if (deviceValue != null)
                    {
                        activationState = deviceValue.ToString();
                    }
                }

                return activationState;
            }
        }

        public string BasebandBootloaderVersion
        {
            get
            {
                if (basebandBootloaderVersion.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.BasebandBootloaderVersion);
                    if (deviceValue != null)
                    {
                        basebandBootloaderVersion = deviceValue.ToString();
                    }
                }

                return basebandBootloaderVersion;
            }
        }

        public string BasebandSerialNumber
        {
            get
            {
                if (basebandSerialNumber.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.BasebandSerialNumber);
                    if (deviceValue != null)
                    {
                        basebandSerialNumber = deviceValue.ToString();
                    }
                }

                return basebandSerialNumber;
            }
        }

        public string BasebandVersion
        {
            get
            {
                if (basebandVersion.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.BasebandVersion);
                    if (deviceValue != null)
                    {
                        basebandVersion = deviceValue.ToString();
                    }
                }

                return basebandVersion;
            }
        }

        public string BluetoothAddress
        {
            get
            {
                if (bluetoothAddress.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.BluetoothAddress);
                    if (deviceValue != null)
                    {
                        bluetoothAddress = deviceValue.ToString();
                    }
                }

                return bluetoothAddress;
            }
        }

        public string BuildVersion
        {
            get
            {
                if (buildVersion.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.BuildVersion);
                    if (deviceValue != null)
                    {
                        buildVersion = deviceValue.ToString();
                    }
                }

                return buildVersion;
            }
        }

        public string CPUArchitecture
        {
            get
            {
                if (cpuArchitecture.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.CPUArchitecture);
                    if (deviceValue != null)
                    {
                        cpuArchitecture = deviceValue.ToString();
                    }
                }

                return cpuArchitecture;
            }
        }

        public string DeviceColor
        {
            get
            {
                if (string.IsNullOrWhiteSpace(deviceColor))
                {
                    var deviceColorValue = GetDeviceValue(DeviceInfoKey.DeviceColor);
                    if (deviceColorValue != null)
                    {
                        deviceColor = deviceColorValue.ToString();
                    }
                }

                return deviceColor;
            }
        }

        public string DeviceName
        {
            get
            {
                if (deviceName.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.DeviceName);
                    if (deviceValue != null)
                    {
                        deviceName = deviceValue.ToString();
                    }
                }

                return deviceName;
            }
        }

        public string FirmwareVersion
        {
            get
            {
                if (firmwareVersion.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.FirmwareVersion);
                    if (deviceValue != null)
                    {
                        firmwareVersion = deviceValue.ToString();
                    }
                }

                return firmwareVersion;
            }
        }

        public string HardwareModel
        {
            get
            {
                if (hardwareModel.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.HardwareModel);
                    if (deviceValue != null)
                    {
                        hardwareModel = deviceValue.ToString();
                    }
                }

                return hardwareModel;
            }
        }

        public string ModelNumber
        {
            get
            {
                if (modelNumber.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.ModelNumber);
                    if (deviceValue != null)
                    {
                        modelNumber = deviceValue.ToString();
                    }
                }

                return modelNumber;
            }
        }

        public string PhoneNumber
        {
            get
            {
                if (phoneNumber.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.PhoneNumber);
                    if (deviceValue != null)
                    {
                        phoneNumber = deviceValue.ToString();
                    }
                }

                return phoneNumber;
            }
        }

        public string ProductType
        {
            get
            {
                if (productType.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.ProductType);
                    if (deviceValue != null)
                    {
                        productType = deviceValue.ToString();
                    }
                }

                return productType;
            }
        }

        public string IntegratedCircuitCardIdentity
        {
            get
            {
                if (integratedCircuitCardIdentity.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.IntegratedCircuitCardIdentity);
                    if (deviceValue != null)
                    {
                        integratedCircuitCardIdentity = deviceValue.ToString();
                    }
                }

                return integratedCircuitCardIdentity;
            }
        }

        public string InternationalMobileEquipmentIdentity
        {
            get
            {
                if (internationalMobileEquipmentIdentity.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.InternationalMobileEquipmentIdentity);
                    if (deviceValue != null)
                    {
                        internationalMobileEquipmentIdentity = deviceValue.ToString();
                    }
                }

                return internationalMobileEquipmentIdentity;
            }
        }

        public string SerialNumber
        {
            get
            {
                if (serialNumber.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.SerialNumber);
                    if (deviceValue != null)
                    {
                        serialNumber = deviceValue.ToString();
                    }
                }

                return serialNumber;
            }
        }

        public string SIMStatus
        {
            get
            {
                if (simStatus.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.SIMStatus);
                    if (deviceValue != null)
                    {
                        simStatus = deviceValue.ToString();
                    }
                }

                return simStatus;
            }
        }

        public string UniqueDeviceID
        {
            get
            {
                if (uniqueDeviceID.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.UniqueDeviceID);
                    if (deviceValue != null)
                    {
                        uniqueDeviceID = deviceValue.ToString();
                    }
                }

                return uniqueDeviceID;
            }
        }

        public string WiFiAddress
        {
            get
            {
                if (wiFiAddress.Length == 0)
                {
                    object deviceValue = GetDeviceValue(DeviceInfoKey.WiFiAddress);
                    if (deviceValue != null)
                    {
                        wiFiAddress = deviceValue.ToString();
                    }
                }

                return wiFiAddress;
            }
        }

        public string ProductVersion
        {
            get
            {
                if (string.IsNullOrEmpty(productVersion))
                {
                    productVersion = Convert.ToString(GetDeviceValue(DeviceInfoKey.ProductVersion)) + string.Empty;
                }

                return productVersion;
            }
        }

        public int VersionNumber
        {
            get
            {
                if (versionNumber == 0)
                {
                    versionNumber = Convert.ToInt32(ProductVersion.Replace(".", string.Empty).PadRight(3, '0'));
                }

                return versionNumber;
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
                    zero = MobileDevice.AMDeviceCopyDeviceIdentifier(DevicePtr);
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