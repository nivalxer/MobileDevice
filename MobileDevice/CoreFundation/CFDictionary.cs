using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDevice.CoreFundation
{
    internal class CFDictionary : IDisposable
    {
        internal IntPtr _handle;

        internal CFDictionary(IntPtr handle, bool owns)
        {
            if (handle == IntPtr.Zero)
            {
                throw new ArgumentNullException("handle");
            }
            this._handle = handle;
            if (!owns)
            {
                CoreFoundation.CFRetain(handle);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._handle != IntPtr.Zero)
            {
                CoreFoundation.CFRelease(this._handle);
                this._handle = IntPtr.Zero;
            }
        }

        ~CFDictionary()
        {
            this.Dispose(false);
        }

        internal static void GetKeysAndValues(IntPtr srcRef, IntPtr keys, ref IntPtr values)
        {
            CoreFoundation.CFDictionaryGetKeysAndValues(srcRef, keys, values);
        }
    }
}
