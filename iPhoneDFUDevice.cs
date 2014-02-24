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
    public class iPhoneDFUDevice
    {
        private byte[] DFUHandle = null;

        /*public iPhoneDFUDevice(AMRecoveryDevice device)
        {
            this.DFUHandle = device.byte_0;
        }*/

        public iPhoneDFUDevice(byte[] device)
        {
            this.DFUHandle = device;
        }


        public string LocationID
        {
            get
            {
                return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetLocationID(this.DFUDevice));
            }
        }

        public string ProductID
        {
            get
            {
                return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetProductID(this.DFUDevice));
            }
        }

        public string ProductType
        {
            get
            {
                return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetProductType(this.DFUDevice));
            }
        }

        public byte[] DFUDevice
        {
            get
            {
                return this.DFUHandle;
            }
        }


        public string TypeID
        {
            get
            {
                return Conversions.ToString(MobileDevice.AMRecoveryModeDeviceGetTypeID());
            }
        }

    }
}
