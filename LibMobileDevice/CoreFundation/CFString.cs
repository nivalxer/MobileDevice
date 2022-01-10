using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace LibMobileDevice.CoreFundation
{
    internal class CFString : IDisposable
    {
        internal IntPtr _handle;
        internal string _str;

        internal CFString(IntPtr handle) : this(handle, false)
        {
        }

        internal CFString(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            _handle = CoreFoundation.CFStringCreateWithCharacters(IntPtr.Zero, str, str.Length);
            _str = str;
        }

        internal CFString(IntPtr handle, bool owns)
        {
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
            _str = null;
            if (_handle != IntPtr.Zero)
            {
                CoreFoundation.CFRelease(_handle);
                _handle = IntPtr.Zero;
            }
        }

        public override bool Equals(object other)
        {
            CFString str = other as CFString;
            if (str == null)
            {
                return false;
            }
            return str.Handle == _handle;
        }

        internal static string FetchString(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
            {
                return null;
            }
            var len = CoreFoundation.CFStringGetLength(handle);
            var ptr = CoreFoundation.CFStringGetCharactersPtr(handle);
            var zero = IntPtr.Zero;
            if (ptr == IntPtr.Zero)
            {
                var range = new CFRange(0, len);
                zero = Marshal.AllocCoTaskMem(len * 2);
                CoreFoundation.CFStringGetCharacters(handle, range, zero);
                ptr = zero;
            }

            return Marshal.PtrToStringUni(ptr, len);
        }

        ~CFString()
        {
            Dispose(false);
        }

        public override int GetHashCode()
        {
            return _handle.GetHashCode();
        }

        public static bool operator ==(CFString a, CFString b)
        {
            return Equals(a, b);
        }

        public static implicit operator string(CFString other)
        {
            if (string.IsNullOrEmpty(other._str))
            {
                other._str = FetchString(other._handle);
            }
            return other._str;
        }

        public static implicit operator CFString(string s)
        {
            return new CFString(s);
        }

        public static bool operator !=(CFString a, CFString b)
        {
            return !Equals(a, b);
        }

        public IntPtr Handle
        {
            get { return _handle; }
        }

        public char this[int p]
        {
            get
            {
                if (_str != null)
                {
                    return _str[p];
                }
                return CoreFoundation.CFStringGetCharacterAtIndex(_handle, p);
            }
        }

        internal int Length
        {
            get
            {
                if (_str != null)
                {
                    return _str.Length;
                }
                return CoreFoundation.CFStringGetLength(_handle);
            }
        }
    }
}
