namespace MobileDevice
{
    using System;
    using System.IO;

    public class iPhoneFile : Stream
    {
        private long handle;
        private OpenMode mode;
        private iPhone phone;

        private iPhoneFile(iPhone phone, long handle, OpenMode mode)
        {
            this.phone = phone;
            this.mode = mode;
            this.handle = handle;
        }

        protected override unsafe void Dispose(bool disposing)
        {
            if (disposing && (this.handle != 0L))
            {
                MobileDevice.AFCFileRefClose(this.phone.AFCHandle, this.handle);
                this.handle = 0L;
            }
            base.Dispose(disposing);
        }

        public override unsafe void Flush()
        {
            MobileDevice.AFCFlushData(this.phone.AFCHandle, this.handle);
        }

        public static unsafe iPhoneFile Open(iPhone phone, string path, FileAccess openmode)
        {
            long num2;
            OpenMode none = OpenMode.None;
            switch (openmode)
            {
                case FileAccess.Read:
                    none = OpenMode.Read;
                    break;

                case FileAccess.Write:
                    none = OpenMode.Write;
                    break;

                case FileAccess.ReadWrite:
                    throw new NotImplementedException("Read+Write not (yet) implemented");
            }
            string str = phone.FullPath(phone.CurrentDirectory, path);
            int num = MobileDevice.AFCFileRefOpen(phone.AFCHandle,System.Text.Encoding.UTF8.GetBytes(str), (int) none, 0, out num2);
            if (num != 0)
            {
                phone.ReConnect();
                throw new IOException("AFCFileRefOpen failed with error " + num.ToString());
            }
            return new iPhoneFile(phone, num2, none);
        }

        public static iPhoneFile OpenRead(iPhone phone, string path)
        {
            return Open(phone, path, FileAccess.Read);
        }

        public static iPhoneFile OpenWrite(iPhone phone, string path)
        {
            return Open(phone, path, FileAccess.Write);
        }

        public override unsafe int Read(byte[] buffer, int offset, int count)
        {
            byte[] buffer2;
            if (!this.CanRead)
            {
                throw new NotImplementedException("Stream open for writing only");
            }
            if (offset == 0)
            {
                buffer2 = buffer;
            }
            else
            {
                buffer2 = new byte[count];
            }
            uint len = (uint) count;
            int num2 = MobileDevice.AFCFileRefRead(this.phone.AFCHandle, this.handle, buffer2, ref len);
            if (num2 != 0)
            {
                throw new IOException("AFCFileRefRead error = " + num2.ToString());
            }
            if (buffer2 != buffer)
            {
                Buffer.BlockCopy(buffer2, 0, buffer, offset, (int) len);
            }
            return (int) len;
        }

        public override unsafe long Seek(long offset, SeekOrigin origin)
        {
            int num = MobileDevice.AFCFileRefSeek(this.phone.AFCHandle, this.handle, (uint) offset, 0);
            Console.WriteLine("ret = {0}", num);
            return offset;
        }

        public override unsafe void SetLength(long value)
        {
            int num = MobileDevice.AFCFileRefSetFileSize(this.phone.AFCHandle, this.handle, (uint) value);
        }

        public override unsafe void Write(byte[] buffer, int offset, int count)
        {
            byte[] buffer2;
            if (!this.CanWrite)
            {
                throw new NotImplementedException("Stream open for reading only");
            }
            if (offset == 0)
            {
                buffer2 = buffer;
            }
            else
            {
                buffer2 = new byte[count];
                Buffer.BlockCopy(buffer, offset, buffer2, 0, count);
            }
            int num = MobileDevice.AFCFileRefWrite(this.phone.AFCHandle, this.handle, buffer2, (uint) count);
        }

        public override bool CanRead
        {
            get
            {
                return (this.mode == OpenMode.Read);
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        public override bool CanTimeout
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return (this.mode == OpenMode.Write);
            }
        }

        public override long Length
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public unsafe override long Position
        {
            get
            {
                uint position = 0;
                MobileDevice.AFCFileRefTell(this.phone.AFCHandle, this.handle, ref position);
                return (long) position;
            }
            set
            {
                this.Seek(value, SeekOrigin.Begin);
            }
        }

        private enum OpenMode
        {
            None = 0,
            Read = 2,
            Write = 3
        }
    }
}

