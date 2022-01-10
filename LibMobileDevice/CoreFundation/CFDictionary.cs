using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMobileDevice.CoreFundation
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
            _handle = handle;
            if (!owns)
            {
                CoreFoundation.CFRetain(handle);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_handle != IntPtr.Zero)
            {
                CoreFoundation.CFRelease(_handle);
                _handle = IntPtr.Zero;
            }
        }

        ~CFDictionary()
        {
            Dispose(false);
        }

        internal static void GetKeysAndValues(IntPtr srcRef, IntPtr keys, ref IntPtr values)
        {
            CoreFoundation.CFDictionaryGetKeysAndValues(srcRef, keys, values);
        }
    }
}
