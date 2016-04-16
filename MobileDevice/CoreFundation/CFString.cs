using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MobileDevice.CoreFundation
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
            this._handle = CoreFoundation.CFStringCreateWithCharacters(IntPtr.Zero, str, str.Length);
            this._str = str;
        }

        internal CFString(IntPtr handle, bool owns)
        {
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
            this._str = null;
            if (this._handle != IntPtr.Zero)
            {
                CoreFoundation.CFRelease(this._handle);
                this._handle = IntPtr.Zero;
            }
        }

        public override bool Equals(object other)
        {
            CFString str = other as CFString;
            if (str == null)
            {
                return false;
            }
            return (str.Handle == this._handle);
        }

        internal static unsafe string FetchString(IntPtr handle)
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
                zero = Marshal.AllocCoTaskMem(len*2);
                CoreFoundation.CFStringGetCharacters(handle, range, zero);
                ptr = zero;
            }
            return new string((char*) ptr, 0, len);
        }

        ~CFString()
        {
            this.Dispose(false);
        }

        public override int GetHashCode()
        {
            return this._handle.GetHashCode();
        }

        public static bool operator ==(CFString a, CFString b)
        {
            return object.Equals(a, b);
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
            return !object.Equals(a, b);
        }

        public IntPtr Handle
        {
            get { return this._handle; }
        }

        public char this[int p]
        {
            get
            {
                if (this._str != null)
                {
                    return this._str[p];
                }
                return CoreFoundation.CFStringGetCharacterAtIndex(this._handle, p);
            }
        }

        internal int Length
        {
            get
            {
                if (this._str != null)
                {
                    return this._str.Length;
                }
                return CoreFoundation.CFStringGetLength(this._handle);
            }
        }
    }
}
