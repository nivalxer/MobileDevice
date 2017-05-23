using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using MobileDevice.Callback;
using MobileDevice.CoreFundation;
using MobileDevice.Enumerates;

namespace MobileDevice
{
    internal class MobileDevice
    {

        static MobileDevice()
        {
            string mobileDeviceDirectoryName = Helper.DLLHelper.GetiTunesMobileDeviceDllPath();
            string applicationSupportDirectoryName = Helper.DLLHelper.GetAppleApplicationSupportFolder();
            if (!string.IsNullOrWhiteSpace(mobileDeviceDirectoryName) && !string.IsNullOrWhiteSpace(applicationSupportDirectoryName))
            {
                Environment.SetEnvironmentVariable("Path", string.Join(";", new[]
                    {Environment.GetEnvironmentVariable("Path"), mobileDeviceDirectoryName, applicationSupportDirectoryName}));
            }
        }

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCConnectionClose(IntPtr conn);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCConnectionGetFSBlockSize(IntPtr conn);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCConnectionInvalidate(IntPtr conn);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCConnectionIsValid(IntPtr conn);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError AFCConnectionSetSecureContext(IntPtr device, IntPtr intResult);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCConnectionOpen(int socket, uint io_timeout, ref IntPtr conn);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCDeviceInfoOpen(IntPtr conn, ref IntPtr dict);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceInstallApplication(int socket, IntPtr path, IntPtr options, IntPtr callback,
            IntPtr unknow1);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceTransferApplication(int socket, IntPtr path, IntPtr options,
            DeviceInstallApplicationCallback callback, IntPtr unknow1);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceUninstallApplication(IntPtr conn, IntPtr bundleIdentifier,
            IntPtr installOption, IntPtr unknown0, IntPtr unknown1);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceArchiveApplication(IntPtr conn, IntPtr bundleIdentifier, IntPtr options,
            DeviceInstallApplicationCallback callback);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDPostNotification(int socket, IntPtr NoticeMessage, uint uint_0);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDObserveNotification(int socket, IntPtr NoticeMessage);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceSecureStartService(IntPtr device, IntPtr service_name, IntPtr unknown,
            ref IntPtr socket);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDServiceConnectionGetSocket(IntPtr socket);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDServiceConnectionGetSecureIOContext(IntPtr socket);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceLookupApplicationArchives(IntPtr conn, IntPtr AppType, ref IntPtr result);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceLookupApplications(IntPtr conn, IntPtr AppType, ref IntPtr result);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCDirectoryClose(IntPtr conn, IntPtr dir);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCDirectoryCreate(IntPtr conn, string path);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefLock(IntPtr conn, long long_0);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCDirectoryOpen(IntPtr conn, byte[] path, ref IntPtr dir);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError AFCDirectoryOpen(IntPtr afcHandle, IntPtr path, ref IntPtr dir);

        public static int AFCDirectoryOpen(IntPtr conn, string path, ref IntPtr dir)
        {
            return AFCDirectoryOpen(conn, Encoding.UTF8.GetBytes(path), ref dir);
        }

        public static int AFCDirectoryRead(IntPtr conn, IntPtr dir, ref string buffer)
        {
            IntPtr zero = IntPtr.Zero;
            int error = AFCDirectoryRead(conn, dir, ref zero);
            if (((kAMDError)error == kAMDError.kAMDSuccess) && (zero != IntPtr.Zero))
            {
                int ofs = 0;
                List<byte> list = new List<byte>();
                while (Marshal.ReadByte(zero, ofs) != 0)
                {
                    list.Add(Marshal.ReadByte(zero, ofs));
                    ofs++;
                }
                buffer = Encoding.UTF8.GetString(list.ToArray());
                return (int)error;
            }
            buffer = null;
            return error;
        }

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError AFCGetFileInfo(IntPtr afcHandle, IntPtr path, ref IntPtr buffer, ref uint length);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCDirectoryRead(IntPtr afcHandle, IntPtr dir, ref IntPtr dirent);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileInfoOpen(IntPtr conn, byte[] path, ref IntPtr dict);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError AFCFileInfoOpen(IntPtr afcHandle, IntPtr path, ref IntPtr info);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefClose(IntPtr conn, long handle);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefOpen(IntPtr conn, byte[] path, int mode, ref long handle);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError AFCFileRefOpen(IntPtr afcHandle, IntPtr path, int mode, ref long handle);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefRead(IntPtr conn, long handle, byte[] buffer, ref uint len);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefSeek(IntPtr conn, long handle, uint pos, uint origin);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError AFCFileRefSeek(IntPtr afcHandle, long handle, long pos, uint origin);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError AFCFileRefUnlock(IntPtr afcHandle, long handle);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefSetFileSize(IntPtr conn, long handle, uint size);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefTell(IntPtr conn, long handle, ref uint position);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFileRefWrite(IntPtr conn, long handle, byte[] buffer, uint len);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCFlushData(IntPtr conn, long handle);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCKeyValueClose(IntPtr dict);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCKeyValueRead(IntPtr dict, ref IntPtr key, ref IntPtr val);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCRemovePath(IntPtr conn, byte[] path);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError AFCRemovePath(IntPtr afcHandle, IntPtr path);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AFCRenamePath(IntPtr conn, byte[] old_path, byte[] new_path);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceActivate(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceConnect(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AMDeviceCopyDeviceIdentifier(IntPtr device);

        public static object AMDeviceCopyValue(IntPtr device, string domain, string name)
        {
            object obj2 = string.Empty;
            IntPtr ptr = CoreFoundation.StringToCFString(domain);
            IntPtr cfstring = CoreFoundation.StringToCFString(name);
            IntPtr srcRef = AMDeviceCopyValue_Int(device, ptr, cfstring);
            if (ptr != IntPtr.Zero)
            {
                CoreFoundation.CFRelease(ptr);
            }
            if (cfstring != IntPtr.Zero)
            {
                CoreFoundation.CFRelease(cfstring);
            }
            if (srcRef != IntPtr.Zero)
            {
                obj2 = RuntimeHelpers.GetObjectValue(CoreFoundation.ManagedTypeFromCFType(ref srcRef));
                CoreFoundation.CFRelease(srcRef);
            }
            return obj2;
        }

        [DllImport("iTunesMobileDevice.dll", EntryPoint = "AMDeviceCopyValue", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AMDeviceCopyValue_Int(IntPtr device, IntPtr domain, IntPtr cfstring);
        public static bool AMDeviceRemoveValue(IntPtr device, string domain, string name)
        {
            bool flag = true;
            try
            {
                IntPtr ptr = CoreFoundation.StringToCFString(domain);
                IntPtr cfsKey = CoreFoundation.StringToCFString(name);
                if (AMDeviceRemoveValue_UInt32(device, ptr, cfsKey) != 0)
                {
                    flag = false;
                }
                if (ptr != IntPtr.Zero)
                {
                    CoreFoundation.CFRelease(ptr);
                }
                if (cfsKey != IntPtr.Zero)
                {
                    CoreFoundation.CFRelease(cfsKey);
                }
            }
            catch (Exception exception)
            {
                
            }
            return flag;
        }

        [DllImport("iTunesMobileDevice.dll", EntryPoint = "AMDeviceRemoveValue", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceRemoveValue_UInt32(IntPtr device, IntPtr domain, IntPtr cfsKey);
        public static bool AMDeviceSetValue(IntPtr device, string domain, string name, object value)
        {
            bool flag = true;
            IntPtr ptr = CoreFoundation.StringToCFString(domain);
            IntPtr cfsKey = CoreFoundation.StringToCFString(name);
            IntPtr cfsValue = CoreFoundation.CFTypeFromManagedType(value);
            if (AMDeviceSetValue_Int(device, ptr, cfsKey, cfsValue) != (int)kAMDError.kAMDSuccess)
            {
                flag = false;
            }
            if (ptr != IntPtr.Zero)
            {
                CoreFoundation.CFRelease(ptr);
            }
            if (cfsKey != IntPtr.Zero)
            {
                CoreFoundation.CFRelease(cfsKey);
            }
            if (cfsValue != IntPtr.Zero)
            {
                CoreFoundation.CFRelease(cfsValue);
            }
            return flag;
        }
        [DllImport("iTunesMobileDevice.dll", EntryPoint = "AMDeviceSetValue", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceSetValue_Int(IntPtr device, IntPtr domain, IntPtr cfsKey, IntPtr cfsValue);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceSetValue(IntPtr device, IntPtr mbz, IntPtr cfstringkey, IntPtr cfstringname);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceDeactivate(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceDisconnect(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceEnterRecovery(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceGetConnectionID(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceIsPaired(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceNotificationSubscribe(DeviceNotificationCallback callback, uint unused1,
            uint unused2, uint unused3, ref IntPtr am_device_notification_ptr);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError AMDeviceNotificationUnsubscribe(IntPtr am_device_notification_ptr);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceRelease(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceStartService(IntPtr device, IntPtr service_name, ref int socket,
            IntPtr unknown);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void AMDeviceRetain(IntPtr device);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceStartSession(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceStopSession(IntPtr device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceValidatePairing(IntPtr device);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDevicePair(IntPtr device);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError AMDevicePairWithOptions(IntPtr device, IntPtr ptrOption);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceUnpair(IntPtr device);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError AMDListenForNotifications(int socket, DeviceEventSink callback, IntPtr userdata);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AMRecoveryModeDeviceCopyIMEI(byte[] device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRecoveryModeDeviceGetLocationID(byte[] device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRecoveryModeDeviceGetProductID(byte[] device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRecoveryModeDeviceGetProductType(byte[] device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRecoveryModeDeviceGetTypeID();

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRecoveryModeGetSoftwareBuildVersion();

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AMRecoveryModeDeviceCopySerialNumber(byte[] device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AMRecoveryModeDeviceCopyAuthlnstallPreflightOptions(byte[] byte_0, IntPtr intptr_0,
            IntPtr intptr_1);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRecoveryModeDeviceReboot(byte[] device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRecoveryModeDeviceSetAutoBoot(byte[] device, byte paramByte);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern string AMRestoreModeDeviceCopyIMEI(byte[] device);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRestoreModeDeviceCreate(uint unknown0, int connection_id, uint unknown1);

        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRestoreRegisterForDeviceNotifications(
            DeviceDFUNotificationCallback dfu_connect, DeviceRestoreNotificationCallback recovery_connect,
            DeviceDFUNotificationCallback dfu_disconnect, DeviceRestoreNotificationCallback recovery_disconnect,
            uint unknown0, ref IntPtr user_info);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDeviceGetInterfaceType(IntPtr device);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDServiceConnectionReceive(IntPtr socket, ref uint uint_0, int int_1);
        [DllImport("iTunesMobileDevice.dll", EntryPoint = "AMDServiceConnectionReceive", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDServiceConnectionReceive_1(IntPtr socket, IntPtr intptr_0, int int_1);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMRestorePerformRecoveryModeRestore(byte[] device, IntPtr dicopts, DeviceInstallApplicationCallback callback, IntPtr user_info);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr AMRestoreCreateDefaultOptions(IntPtr allocator);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDServiceConnectionReceive(int inSocket, IntPtr buffer, int bufferlen);
        [DllImport("iTunesMobileDevice.dll", EntryPoint = "AMDServiceConnectionReceive", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDServiceConnectionReceive_UInt32(int inSocket, ref uint buffer, int bufferlen);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDServiceConnectionSend(int inSocket, IntPtr buffer, int bufferlen);
        [DllImport("iTunesMobileDevice.dll", EntryPoint = "AMDServiceConnectionSend", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMDServiceConnectionSend_UInt32(int inSocket, ref uint buffer, int bufferlen);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int USBMuxConnectByPort(int connectionID, uint iPhone_port_network_byte_order, ref int outSocket);

        [DllImport("Ws2_32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint htonl(uint hostlong);
        [DllImport("Ws2_32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint htons(uint hostshort);

        [DllImport("Ws2_32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint ntohl(uint netlong);
        [DllImport("Ws2_32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int send(int inSocket, IntPtr buffer, int bufferlen, int flags);
        [DllImport("Ws2_32.dll", EntryPoint = "send", CallingConvention = CallingConvention.StdCall)]
        public static extern int send_UInt32(int inSocket, ref uint buffer, int bufferlen, int flags);

        [DllImport("Ws2_32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int recv(int inSocket, IntPtr buffer, int bufferlen, int flags);
        [DllImport("Ws2_32.dll", EntryPoint = "recv", CallingConvention = CallingConvention.StdCall)]
        public static extern int recv_UInt32(int inSocket, ref uint buffer, int bufferlen, int flags);
        [DllImport("Ws2_32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int closesocket(int inSocket);
        public static IntPtr ATCFMessageCreate(int sesssion, string strMessageType, Dictionary<object, object> dictParams)
        {
            IntPtr hMessageType = CoreFoundation.StringToCFString(strMessageType);
            IntPtr hParams = CoreFoundation.CFTypeFromManagedType(dictParams);
            IntPtr ptr3 = ATCFMessageCreate_IntPtr(sesssion, hMessageType, hParams);
            CoreFoundation.CFRelease(hMessageType);
            CoreFoundation.CFRelease(hParams);
            return ptr3;
        }

        [DllImport("AirTrafficHost.dll", EntryPoint = "ATCFMessageCreate", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ATCFMessageCreate_IntPtr(int sesssion, IntPtr hMessageType, IntPtr hParams);
        public static IntPtr ATHostConnectionCreateWithLibrary(string strPrefsValue, string strUUID)
        {
            IntPtr hPrefsValue = CoreFoundation.StringToCFString(strPrefsValue);
            IntPtr hUUID = CoreFoundation.StringToCFString(strUUID);
            IntPtr ptr3 = ATHostConnectionCreateWithLibrary_IntPtr(hPrefsValue, hUUID, 0);
            CoreFoundation.CFRelease(hUUID);
            CoreFoundation.CFRelease(hPrefsValue);
            return ptr3;
        }

        [DllImport("AirTrafficHost.dll", EntryPoint = "ATHostConnectionCreateWithLibrary", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ATHostConnectionCreateWithLibrary_IntPtr(IntPtr hPrefsValue, IntPtr hUUID, int unkonwn);
        [DllImport("AirTrafficHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ATHostConnectionGetCurrentSessionNumber(IntPtr hATHost);
        [DllImport("AirTrafficHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ATHostConnectionGetGrappaSessionId(IntPtr hATHost);
        [DllImport("AirTrafficHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ATHostConnectionReadMessage(IntPtr hATHost);
        [DllImport("AirTrafficHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ATHostConnectionRelease(IntPtr hATHost);
        public static int ATHostConnectionSendAssetCompleted(IntPtr hATHost, string strPid, string strMediaType, string strFilePath)
        {
            IntPtr hPid = CoreFoundation.CFStringMakeConstantString(strPid);
            IntPtr hMediaType = CoreFoundation.CFStringMakeConstantString(strMediaType);
            IntPtr hFilePath = CoreFoundation.CFStringMakeConstantString(strFilePath);
            IntPtr ptr4 = ATHostConnectionSendAssetCompleted_IntPtr(hATHost, hPid, hMediaType, hFilePath);
            CoreFoundation.CFRelease(hPid);
            CoreFoundation.CFRelease(hMediaType);
            CoreFoundation.CFRelease(hFilePath);
            return ptr4.ToInt32();
        }

        [DllImport("AirTrafficHost.dll", EntryPoint = "ATHostConnectionSendAssetCompleted", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ATHostConnectionSendAssetCompleted_IntPtr(IntPtr hATHost, IntPtr hPid, IntPtr hMediaType, IntPtr hFilePath);
        public static int ATHostConnectionSendFileError(IntPtr hATHost, string strPid, string strMediaType, int intType)
        {
            IntPtr hPid = CoreFoundation.CFStringMakeConstantString(strPid);
            IntPtr hMediaType = CoreFoundation.CFStringMakeConstantString(strMediaType);
            IntPtr ptr3 = (IntPtr)ATHostConnectionSendFileError_IntPtr(hATHost, hPid, hMediaType, intType);
            CoreFoundation.CFRelease(hPid);
            CoreFoundation.CFRelease(hMediaType);
            return ptr3.ToInt32();
        }

        [DllImport("AirTrafficHost.dll", EntryPoint = "ATHostConnectionSendFileError", CallingConvention = CallingConvention.Cdecl)]
        private static extern kAMDError ATHostConnectionSendFileError_IntPtr(IntPtr hATHost, IntPtr hPid, IntPtr hMediaType, int intType);
        public static int ATHostConnectionSendFileProgress(IntPtr hATHost, string strPid, string strMediaType, double fileProgress, double totalProgress)
        {
            IntPtr hPid = CoreFoundation.CFStringMakeConstantString(strPid);
            IntPtr hMediaType = CoreFoundation.CFStringMakeConstantString(strMediaType);
            byte[] bytes = BitConverter.GetBytes(fileProgress);
            byte[] buffer2 = BitConverter.GetBytes(totalProgress);
            int num = BitConverter.ToInt32(bytes, 0);
            int num2 = BitConverter.ToInt32(bytes, 4);
            int num3 = BitConverter.ToInt32(buffer2, 0);
            int num4 = BitConverter.ToInt32(buffer2, 4);
            IntPtr ptr3 = (IntPtr)ATHostConnectionSendFileProgress_IntPtr(hATHost, hPid, hMediaType, num, num2, num3, num4);
            CoreFoundation.CFRelease(hPid);
            CoreFoundation.CFRelease(hMediaType);
            return ptr3.ToInt32();
        }

        [DllImport("AirTrafficHost.dll", EntryPoint = "ATHostConnectionSendFileProgress", CallingConvention = CallingConvention.Cdecl)]
        private static extern kAMDError ATHostConnectionSendFileProgress_IntPtr(IntPtr hATHost, IntPtr hPid, IntPtr hMediaType, int proc1L, int proc1H, int proc2L, int proc2H);
        [DllImport("AirTrafficHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError ATHostConnectionSendHostInfo(IntPtr hATHost, IntPtr hDictInfo);
        [DllImport("AirTrafficHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError ATHostConnectionSendMetadataSyncFinished(IntPtr hATHost, IntPtr hKeybag, IntPtr hMedia);
        [DllImport("AirTrafficHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError ATHostConnectionSendPowerAssertion(IntPtr hATHost, IntPtr allocator);
        [DllImport("AirTrafficHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern kAMDError ATHostConnectionSendSyncRequest(IntPtr hATHost, IntPtr hArrDataclasses, IntPtr hDictDataclassAnchors, IntPtr hDictHostInfo);
        [DllImport("AirTrafficHost.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ATProcessLinkSendMessage(IntPtr hATHost, IntPtr hATCFMessage);
        public static IntPtr CFStringMakeConstantString(string s)
        {
            return CoreFundation.CoreFoundation.StringToCFString(s);
        }

    }
}
