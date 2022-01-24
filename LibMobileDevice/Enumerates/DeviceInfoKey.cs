using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMobileDevice.Enumerates
{
    /// <summary>
    /// 设备基础信息枚举
    /// </summary>
    public enum DeviceInfoKey
    {
        [Description("激活公钥")]
        ActivationPublicKey,
        [Description("激活状态")]
        ActivationState,
        [Description("BasebandBootloader版本")]
        BasebandBootloaderVersion,
        [Description("BasebandBootloaderChipId")]
        BasebandChipId,
        BasebandGoldCertId,
        BasebandCertId,
        [Description("基带序列号")]
        BasebandSerialNumber,
        [Description("基带状态")]
        BasebandStatus,
        [Description("基带版本")]
        BasebandVersion,
        [Description("蓝牙地址")]
        BluetoothAddress,
        [Description("内部版本")]
        BuildVersion,
        [Description("CPU构架")]
        CPUArchitecture,
        DeviceCertificate,
        DeviceClass,
        [Description("设备颜色")]
        DeviceColor,
        [Description("外壳颜色")]
        DeviceEnclosureColor,
        [Description("设备名")]
        DeviceName,
        DevicePublicKey,
        DieID,
        [Description("iBoot")]
        FirmwareVersion,
        [Description("硬件型号")]
        HardwareModel,
        HardwarePlatform,
        HostAttached,
        IMLockdownEverRegisteredKey,
        [Description("ICCID")]
        IntegratedCircuitCardIdentity,
        [Description("卡1-IMEI码")]
        InternationalMobileEquipmentIdentity,
        [Description("IMSI码")]
        InternationalMobileSubscriberIdentity,
        [Description("MLB序列号")]
        MLBSerialNumber,
        MobileSubscriberCountryCode,
        MobileSubscriberNetworkCode,
        [Description("设备型号")]
        ModelNumber,
        [Description("分区类型")]
        PartitionType,
        [Description("密码保护")]
        PasswordProtected,
        [Description("手机号")]
        PhoneNumber,
        [Description("产品SOC")]
        ProductionSOC,
        [Description("产品版本")]
        ProductType,
        [Description("系统版本")]
        ProductVersion,
        ProtocolVersion,
        ProximitySensorCalibration,
        RegionInfo,
        SBLockdownEverRegisteredKey,
        SDIOManufacturerTuple,
        [Description("序列号")]
        SerialNumber,
        SoftwareBehavior,
        [Description("软件版本")]
        SoftwareBundleVersion,
        SomebodySetTimeZone,
        SupportedDeviceFamilies,
        TelephonyCapability,
        TimeIntervalSince1970,
        [Description("时区")]
        TimeZone,
        [Description("时区差")]
        TimeZoneOffsetFromUTC,
        TrustedHostAttached,
        UniqueChipID,
        [Description("UDID")]
        UniqueDeviceID,
        UseActivityURL,
        UseRaptorCerts,
        [Description("24小时制")]
        Uses24HourClock,
        WeDelivered,
        [Description("WIFI地址")]
        WiFiAddress,
        [Description("当前电量")]
        BatteryCurrentCapacity,
        [Description("设备是否充电")]
        BatteryIsCharging,
        [Description("键盘")]
        Keyboard,
        [Description("AppleID")]
        AppleID, 
        CallsInProgress,
        IOSDIOManufacturerID,
        IOSDIOProductID,
        SDIOProductInfo,
        SIMGID1,
        SIMGID2,
        ActivationStateAcknowledged,
        iTunesHasConnected,
        [Description("SIM卡状态")]
        SIMStatus,
        /*
         * added by vaske
         */
        ActivationInfo,
        [Description("卡2-IMEI码")]
        InternationalMobileEquipmentIdentity2,
        [Description("MEID")]
        MobileEquipmentIdentifier,
        [Description("SIM卡槽状态")]
        SIMTrayStatus
    }
}
