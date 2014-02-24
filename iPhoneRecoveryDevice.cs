using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace MobileDevice
{
    public class iPhoneRecoveryDevice
    {
        private byte[] RecoveryHandle = null;
        private string SerialNum = "";
        private string IMEINum = "";

        /*public iPhoneRecoveryDevice(AMRecoveryDevice device)
        {
            this.RecoveryHandle = device.byte_0;
        }*/

        public iPhoneRecoveryDevice(byte[] device)
        {
            this.RecoveryHandle = device;
        }

        public bool exitRecovery()
        {
            if(this.setAutoBoot(true)==(int)kAMDError.kAMDSuccess)
            {
                this.reboot();
                return true;
            }
            return false;
        }

        public void reboot()
        {
            MobileDevice.AMRecoveryModeDeviceReboot(this.RecoveryHandle);
        }

        public int setAutoBoot(bool value)
        {
            return MobileDevice.AMRecoveryModeDeviceSetAutoBoot(this.RecoveryHandle, Conversions.ToByte(Interaction.IIf(value, 1, 0)));
        }
        /// <summary>
        /// 暂未完善
        /// </summary>
        public object AuthlnstallPreflightOptions
        {
            get
            {
                object objectValue = null;
                try
                {
                    IntPtr sourceRef = MobileDevice.AMRecoveryModeDeviceCopyAuthlnstallPreflightOptions(this.RecoveryHandle, IntPtr.Zero, IntPtr.Zero);
                    //if (sourceRef != IntPtr.Zero) objectValue = RuntimeHelpers.GetObjectValue(CoreFoundation.ManagedTypeFromCFType(ref sourceRef));
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    ProjectData.ClearProjectError();
                }
                return objectValue;
            }
        }
        public string IMEI
        {
            get
            {
                if (this.IMEINum.Length == 0)
                {
                    IntPtr zero = IntPtr.Zero;
                    try
                    {
                        zero = MobileDevice.AMRecoveryModeDeviceCopyIMEI(this.RecoveryHandle);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        if (zero != IntPtr.Zero)
                        {
                             CoreFoundation.CFString cf=new CoreFoundation.CFString(zero);
                             this.IMEINum = cf.ToString();
                        }
                        else
                            this.IMEINum = "";
                    }
                }
                return this.IMEINum;
            }
        }

        public string LocationID
        {
            get
            {
                return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetLocationID(this.RecoveryDevice));
            }
        }

        public string ProductID
        {
            get
            {
                return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetProductID(this.RecoveryDevice));
            }
        }

        public string ProductType
        {
            get
            {
                return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetProductType(this.RecoveryDevice));
            }
        }

        public byte[] RecoveryDevice
        {
            get
            {
                return this.RecoveryHandle;
            }
        }

        public string SerialNumber
        {
            get
            {
                if (this.SerialNum.Length == 0)
                {
                    IntPtr zero = IntPtr.Zero;
                    try
                    {
                        zero =MobileDevice.AMRecoveryModeDeviceCopySerialNumber(this.RecoveryDevice);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        if (zero != IntPtr.Zero)
                        {
                            CoreFoundation.CFString cf = new CoreFoundation.CFString(zero);
                            this.SerialNum = cf.ToString();
                        }
                        else
                            this.SerialNum = "";
                    }
                }
                return this.SerialNum;
            }
        }

        public string TypeID
        {
            get
            {
                return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetTypeID());
            }
        }

        public string Version
        {
            get
            {
                return Conversions.ToString(MobileDevice.AMRecoveryModeGetSoftwareBuildVersion());
            }
        }
    }
}
