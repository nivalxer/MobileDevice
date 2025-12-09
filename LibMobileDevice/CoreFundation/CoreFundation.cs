using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
//using Microsoft.VisualBasic;
//using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace LibMobileDevice.CoreFundation
{
    public class CoreFoundation
    {

        public static IntPtr kCFAllocatorDefault = IntPtr.Zero;
        public static IntPtr kCFBooleanFalse = IntPtr.Zero;
        public static IntPtr kCFBooleanTrue = IntPtr.Zero;
        public static IntPtr kCFStreamPropertyDataWritten = IntPtr.Zero;
        public static IntPtr kCFTypeArrayCallBacks = IntPtr.Zero;
        public static IntPtr kCFTypeDictionaryKeyCallBacks = IntPtr.Zero;
        public static IntPtr kCFTypeDictionaryValueCallBacks = IntPtr.Zero;
        private static IntPtr coreFundationDllIntPtr;

        static CoreFoundation()
        {
            coreFundationDllIntPtr = LoadLibrary("CoreFoundation.dll");
            kCFStreamPropertyDataWritten = EnumToCFEnum("kCFStreamPropertyDataWritten");
            kCFTypeDictionaryKeyCallBacks = ConstToCFConst("kCFTypeDictionaryKeyCallBacks");
            kCFTypeDictionaryValueCallBacks = ConstToCFConst("kCFTypeDictionaryValueCallBacks");
            kCFTypeArrayCallBacks = ConstToCFConst("kCFTypeArrayCallBacks");
            kCFBooleanFalse = CFBoolean.GetCFBoolean(false);
            kCFBooleanTrue = CFBoolean.GetCFBoolean(true);
        }

        internal static IntPtr EnumToCFEnum(string enmName)
        {
            IntPtr zero = IntPtr.Zero;
            if (coreFundationDllIntPtr != IntPtr.Zero)
            {
                zero = GetProcAddress(coreFundationDllIntPtr, enmName);
            }
            if (zero != IntPtr.Zero)
            {
                zero = Marshal.ReadIntPtr(zero, 0);
            }
            return zero;
        }

        public static IntPtr ConstToCFConst(string constName)
        {
            IntPtr zero = IntPtr.Zero;
            if (coreFundationDllIntPtr != IntPtr.Zero)
            {
                zero = GetProcAddress(coreFundationDllIntPtr, constName);
            }
            return zero;
        }

        public static string CreatePlistString(object objPlist)
        {
            string str = string.Empty;
            try
            {
                IntPtr propertyList = CFTypeFromManagedType(objPlist);
                IntPtr stream = CFWriteStreamCreateWithAllocatedBuffers(IntPtr.Zero, IntPtr.Zero);
                if (!(stream != IntPtr.Zero) || !CFWriteStreamOpen(stream))
                {
                    return str;
                }
                IntPtr zero = IntPtr.Zero;
                if (CFPropertyListWriteToStream(propertyList, stream, CFPropertyListFormat.kCFPropertyListXMLFormat_v1_0, ref zero) > 0)
                {
                    IntPtr kCFStreamPropertyDataWritten = CoreFoundation.kCFStreamPropertyDataWritten;
                    IntPtr srcRef = CFWriteStreamCopyProperty(stream, kCFStreamPropertyDataWritten);
                    if (CFDataGetLength(srcRef) > 0)
                    {
                        byte[] bytes = ReadCFDataFromIntPtr(srcRef);
                        str = Encoding.UTF8.GetString(bytes).Replace("\0", string.Empty);
                    }
                    CFRelease(srcRef);
                }
                CFWriteStreamClose(stream);
            }
            catch
            {
            }
            return str;
        }

        [DllImport("Kernel32.dll")]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("CoreFoundation.dll", EntryPoint = "__CFStringMakeConstantString", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFStringMakeConstantString(string cStr);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFAbsoluteTimeGetGregorianDate(ref double at, ref IntPtr tz);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CFArrayGetCount(IntPtr sourceRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFArrayGetTypeID();

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CFArrayGetValues(IntPtr sourceRef, CFRange range, IntPtr values);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFBooleanGetTypeID();

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFDateCreate(IntPtr intptr_2, double double_0);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFDataCreate(IntPtr allocator, IntPtr bytes, int length);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CFBooleanGetValue(IntPtr sourceRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CFDataAppendBytes(IntPtr theData, IntPtr pointer, uint length);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFDataCreateMutable(IntPtr allocator, uint capacity);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFDataGetBytePtr(IntPtr sourceRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CFDataGetBytes(IntPtr theData, CFRange range, byte[] buffer);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CFDataGetLength(IntPtr sourceRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFDataGetTypeID();

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern double CFDateGetAbsoluteTime(IntPtr sourceRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFDateGetTypeID();

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        internal static extern void CFDictionaryAddValue(IntPtr theDict, IntPtr keys, IntPtr values);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        internal static extern IntPtr CFDictionaryCreate(IntPtr allocator, ref IntPtr keys, ref IntPtr values,
            int numValues, IntPtr CFDictionaryKeyCallBacks, IntPtr CFDictionaryValueCallBacks);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFDictionaryCreateMutable(IntPtr allocator, int capacity,
            IntPtr CFDictionaryKeyCallBacks, IntPtr CFDictionaryValueCallBacks);

        public static IntPtr CFDictionaryFromManagedDictionary(Dictionary<object, object> sourceDict)
        {
            if (sourceDict == null)
            {
                return IntPtr.Zero;
            }
            IntPtr zero = IntPtr.Zero;
            try
            {
                zero = CFDictionaryCreateMutable(kCFAllocatorDefault, sourceDict.Count, kCFTypeDictionaryKeyCallBacks, kCFTypeDictionaryValueCallBacks);
                foreach (KeyValuePair<object, object> pair in sourceDict)
                {
                    IntPtr key = CFTypeFromManagedType(pair.Key);
                    IntPtr values = CFTypeFromManagedType(pair.Value);
                    if (key != IntPtr.Zero && values != IntPtr.Zero)
                    {
                        CFDictionaryAddValue(zero, key, values);
                    }
                }
            }
            catch
            {
            }
            return zero;
        }

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFWriteStreamCreateWithAllocatedBuffers(IntPtr intptr_2, IntPtr intptr_3);
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CFPropertyListWriteToStream(IntPtr propertyList, IntPtr stream, CFPropertyListFormat format, ref IntPtr errorString);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CFWriteStreamOpen(IntPtr intptr_2);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFNumberCreate(IntPtr intptr_2, CFNumberType enum20_0, IntPtr intptr_3);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFNumberCreate_Int16(IntPtr number, CFNumberType theType, ref short valuePtr);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberCreate", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFNumberCreate_Int32(IntPtr number, CFNumberType theType, ref int valuePtr);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberCreate", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFNumberCreate_Int64(IntPtr number, CFNumberType theType, ref long valuePtr);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberCreate", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFNumberCreate_Float(IntPtr number, CFNumberType theType, ref float valuePtr);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberCreate", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFNumberCreate_Double(IntPtr number, CFNumberType theType,
            ref double valuePtr);

        internal static IntPtr CFNumberCreateNumbers(object objVals)
        {
            Type type = objVals.GetType();
            if (type == typeof(short))
            {
                short num = Convert.ToInt16(objVals);
                return CFNumberCreate_Int16(kCFAllocatorDefault, CFNumberType.kCFNumberSInt16Type, ref num);
            }
            if (type == typeof(int) || type == typeof(ushort))
            {
                int num2 = Convert.ToInt32(objVals);
                return CFNumberCreate_Int32(kCFAllocatorDefault, CFNumberType.kCFNumberSInt32Type, ref num2);
            }
            if (type == typeof(long) || type == typeof(uint))
            {
                long num3 = Convert.ToInt64(objVals);
                return CFNumberCreate_Int64(kCFAllocatorDefault, CFNumberType.kCFNumberSInt64Type, ref num3);
            }
            if (type == typeof(float))
            {
                float num4 = Convert.ToSingle(objVals);
                return CFNumberCreate_Float(kCFAllocatorDefault, CFNumberType.kCFNumberFloat32Type, ref num4);
            }
            if (type != typeof(double))
            {
                throw new NotImplementedException();
            }
            double valuePtr = Convert.ToDouble(objVals);
            return CFNumberCreate_Double(kCFAllocatorDefault, CFNumberType.kCFNumberFloat64Type, ref valuePtr);
        }

        public static IntPtr CFTypeFromManagedType(object objVal)
        {
            if (objVal == null)
            {
                return IntPtr.Zero;
            }
            IntPtr zero = IntPtr.Zero;
            Type type = objVal.GetType();
            try
            {
                if (type == typeof(string))
                {
                    return StringToCFString((string)objVal);
                }
                if (type == typeof(short) || type == typeof(ushort) || type == typeof(int) || type == typeof(uint) || type == typeof(long) || type == typeof(double) || type == typeof(float))
                {
                    return CFNumberCreateNumbers(objVal);
                }
                if (type == typeof(bool))
                {
                    return CFBoolean.GetCFBoolean(Convert.ToBoolean(objVal));
                }
                if (type == typeof(DateTime))
                {
                    return CFDateCreate(kCFAllocatorDefault, ((DateTime)objVal).Subtract(new DateTime(0x7d1, 1, 1, 0, 0, 0)).TotalSeconds);
                }
                if (type == typeof(byte[]))
                {
                    byte[] arr = (byte[])objVal;
                    IntPtr ptrDest = Marshal.UnsafeAddrOfPinnedArrayElement(arr, 0);
                    zero = CFDataCreate(IntPtr.Zero, ptrDest, arr.Length);
                    return zero;
                }
                if (type == typeof(object[]))
                {
                    return CFArrayFromManageArray((object[])objVal);
                }
                if (type == typeof(ArrayList))
                {
                    return CFArrayFromManageArrayList(objVal as ArrayList);
                }
                if (type == typeof(List<object>))
                {
                    return CFArrayFromManageList(objVal as List<object>);
                }
                if (type == typeof(Dictionary<object, object>))
                {
                    zero = CFDictionaryFromManagedDictionary(objVal as Dictionary<object, object>);
                }
            }
            catch
            {
            }
            return zero;
        }

        public static IntPtr CFArrayFromManageList(IList<object> arrySrc)
        {
            if (arrySrc == null)
            {
                return IntPtr.Zero;
            }
            IntPtr theArray = CFArrayCreateMutable(kCFAllocatorDefault, arrySrc.Count, kCFTypeArrayCallBacks);
            foreach (object obj2 in arrySrc)
            {
                if (obj2 != null)
                {
                    CFArrayAppendValue(theArray, CFTypeFromManagedType(obj2));
                }
            }
            return theArray;
        }

        public static IntPtr CFArrayFromManageArrayList(ArrayList arrySrc)
        {
            if (arrySrc == null)
            {
                return IntPtr.Zero;
            }
            IntPtr theArray = CFArrayCreateMutable(kCFAllocatorDefault, arrySrc.Count, kCFTypeArrayCallBacks);
            IEnumerator enumerator = arrySrc.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    object current = enumerator.Current;
                    if (current != null)
                    {
                        CFArrayAppendValue(theArray, CFTypeFromManagedType(current));
                    }
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            return theArray;
        }

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFArrayCreateMutable(IntPtr allocator, int capacity, IntPtr callback);

        public static IntPtr CFArrayFromManageArray(object[] arrySrc)
        {
            if (arrySrc == null)
            {
                return IntPtr.Zero;
            }
            IntPtr zero = IntPtr.Zero;
            try
            {
                zero = CFArrayCreateMutable(kCFAllocatorDefault, arrySrc.Length, kCFTypeArrayCallBacks);
                foreach (object obj2 in arrySrc)
                {
                    if (obj2 != null)
                    {
                        CFArrayAppendValue(zero, CFTypeFromManagedType(obj2));
                    }
                }
            }
            catch
            {
            }
            return zero;
        }

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFArrayAppendValue(IntPtr intptr_2, IntPtr intptr_3);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CFDictionaryGetCount(IntPtr sourceRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CFDictionaryGetKeysAndValues(IntPtr sourceRef, IntPtr keys, IntPtr values);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFDictionaryGetTypeID();

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFGetTypeID(IntPtr cf);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern CFNumberType CFNumberGetType(IntPtr sourceRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFNumberGetTypeID();

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberGetValue", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        internal static extern bool CFNumberGetValue_Float(IntPtr number, CFNumberType theType, ref float valuePtr);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberGetValue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CFNumberGetValue_Double(IntPtr number, CFNumberType theType, ref double valuePtr);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberGetValue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CFNumberGetValue_Int(IntPtr number, CFNumberType theType, ref int valuePtr);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberGetValue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CFNumberGetValue_Long(IntPtr number, CFNumberType theType, ref long valuePtr);

        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberGetValue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CFNumberGetValue_Single(IntPtr number, CFNumberType theType, ref float valuePtr);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFPropertyListCreateFromXMLData(IntPtr allocator, IntPtr xmlData,
            CFPropertyListMutabilityOptions mutabilityOption, ref IntPtr errorString);
        [DllImport("CoreFoundation.dll", EntryPoint = "CFPropertyListCreateFromXMLData", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFPropertyListCreatePtrFromXMLData(IntPtr allocator, IntPtr datas, CFPropertyListMutabilityOptions option, ref IntPtr errorString);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFPropertyListCreateXMLData(IntPtr allocator, IntPtr theData);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CFPropertyListIsValid(IntPtr theData, CFPropertyListFormat format);

        private static IntPtr CFRangeMake(int loc, int len)
        {
            IntPtr ptr = Marshal.AllocHGlobal(8);
            Marshal.WriteInt32(ptr, 0, loc);
            Marshal.WriteInt32(ptr, 4, len);
            return ptr;
        }

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CFRelease(IntPtr cf);
        [DllImport("CoreFoundation.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFRetain(IntPtr obj);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFStringCreateFromExternalRepresentation(IntPtr allocator, IntPtr data,
            CFStringEncoding encoding);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFStringCreateWithBytes(IntPtr allocator, byte[] data, ulong numBytes,
            CFStringEncoding encoding, bool isExternalRepresentation);
        [DllImport("CoreFoundation.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFStringGetCharacters(IntPtr handle, CFRange range, IntPtr buffer);
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFStringCreateWithCharacters(IntPtr allocator, [MarshalAs(UnmanagedType.LPWStr)] string stingData, int strLength);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFStringCreateWithBytesNoCopy(IntPtr allocator, byte[] data, ulong numBytes,
            CFStringEncoding encoding, bool isExternalRepresentation, IntPtr contentsDeallocator);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFStringCreateWithCString(IntPtr allocator, string data, CFStringEncoding encoding);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern CFRange CFStringFind(IntPtr theString, IntPtr stringToFind, ushort compareOptions);
        [DllImport("CoreFoundation.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CFStringGetCharactersPtr(IntPtr handle);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CFStringGetBytes(IntPtr theString, CFRange range, uint encoding, byte lossByte,
            byte isExternalRepresentation, byte[] buffer, int maxBufLen, ref int usedBufLen);
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CFRunLoopRun();

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern char CFStringGetCharacterAtIndex(IntPtr theString, int idx);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int CFStringGetLength(IntPtr cf);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CFStringGetMaximumSizeForEncoding(int length, uint encoding);
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CFWriteStreamCopyProperty(IntPtr stream, IntPtr propertyName);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CFWriteStreamClose(IntPtr stream);
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint CFStringGetTypeID();

        public static string CFStringToString(byte[] value)
        {
            if (value.Length > 9) return Encoding.UTF8.GetString(value, 9, value[9]);
            return "";
        }

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFTimeZoneCopySystem();

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool CFURLCreateDataAndPropertiesFromResource(IntPtr allocator, IntPtr urlRef,
            ref IntPtr resourceData, ref IntPtr properties, IntPtr desiredProperties, ref int errorCode);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CFURLCreateWithFileSystemPath(IntPtr allocator, IntPtr stringRef,
            CFURLPathStyle pathStyle, bool isDirectory);
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CFURLWriteDataAndPropertiesToResource(IntPtr urlRef, IntPtr dataToWrite, IntPtr propertiesToWrite, ref int errorCode);
        public static object ManagedPropertyListFromXMLData(byte[] inBytes)
        {
            if (inBytes != null)
            {
                IntPtr allocator = CFDataCreateMutable(kCFAllocatorDefault, (uint)inBytes.Length);
                IntPtr ptrDest = Marshal.UnsafeAddrOfPinnedArrayElement(inBytes, 0);
                CFDataAppendBytes(allocator, ptrDest, (uint)inBytes.Length);
                IntPtr zero = IntPtr.Zero;
                IntPtr srcRef = CFPropertyListCreatePtrFromXMLData(kCFAllocatorDefault, allocator, CFPropertyListMutabilityOptions.kCFPropertyListImmutable, ref zero);
                if (srcRef != IntPtr.Zero)
                {
                    return ManagedTypeFromCFType(ref srcRef);
                }
            }
            return null;
        }

        public static object ManagedPropertyListFromXMLData(string fileOnPC)
        {
            byte[] buffer = new byte[0];
            using (FileStream stream = new FileStream(fileOnPC, FileMode.Open))
            {
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
            }
            return ManagedPropertyListFromXMLData(buffer);
        }

        public static object ManagedTypeFromCFType(ref IntPtr srcRef)
        {
            if (srcRef == IntPtr.Zero)
            {
                return string.Empty;
            }
            uint num = CFGetTypeID(srcRef);
            object obj2 = null;
            try
            {
                if (num == CFStringGetTypeID())
                {
                    obj2 = ReadCFStringFromIntPtr(srcRef);
                }
                else if (num == CFArrayGetTypeID())
                {
                    obj2 = ReadCFArrayFromIntPtr(srcRef);
                }
                else if (num == CFNumberGetTypeID())
                {
                    obj2 = ReadCFNumberFromIntPtr(srcRef);
                }
                else if (num == CFDateGetTypeID())
                {
                    obj2 = ReadCFDateFromIntPtr(srcRef);
                }
                else if (num == CFBooleanGetTypeID())
                {
                    obj2 = CFBooleanGetValue(srcRef);
                }
                else if (num == CFDictionaryGetTypeID())
                {
                    obj2 = ReadCFDictionaryFromIntPtr(srcRef);
                }
                else if (num == CFDataGetTypeID())
                {
                    obj2 = ReadCFDataFromIntPtr(srcRef);
                }
                else
                {
                    obj2 = ReadCFStringFromIntPtr(srcRef);
                    if (obj2 == null)
                    {
                        obj2 = ReadCFDateFromIntPtr(srcRef);
                    }
                }
            }
            catch (Exception exception)
            {

            }
            if (obj2 == null)
            {
                return string.Empty;
            }
            return obj2;
        }

        public static string PropertyListToXML(byte[] propertyList)
        {
            try
            {
                int length = propertyList.Length;
                if (propertyList[0] == 0x62 && propertyList[1] == 0x70 && propertyList[2] == 0x6c &&
                    propertyList[3] == 0x69 && propertyList[4] == 0x73 && propertyList[5] == 0x74 &&
                    propertyList[6] == 0x30 && propertyList[7] == 0x30)
                {
                    IntPtr ptr = IntPtr.Zero;
                    IntPtr ptr2 = IntPtr.Zero;
                    ptr2 = CFPropertyListCreateFromXMLData(IntPtr.Zero, ptr2,
                        CFPropertyListMutabilityOptions.kCFPropertyListImmutable, ref ptr);
                    if (ptr2 != IntPtr.Zero)
                    {
                        ptr2 = CFPropertyListCreateXMLData(IntPtr.Zero, ptr2);
                        length = CFDataGetLength(ptr2) - 1;
                        propertyList = new byte[length + 1];
                        var range = new CFRange(0, length);
                        CFDataGetBytes(ptr2, range, propertyList);
                    }
                }
                return Encoding.UTF8.GetString(propertyList);
            }
            catch (Exception exception1)
            {

            }
            return null;
        }

        internal static object ReadCFArrayFromIntPtr(IntPtr srcRef)
        {
            if (srcRef == IntPtr.Zero)
            {
                return null;
            }
            int len = CFArrayGetCount(srcRef);
            if (len <= 0)
            {
                return null;
            }
            object[] objArray = new object[len];
            IntPtr values = Marshal.AllocCoTaskMem(len * 4 * 2);
            CFArrayGetValues(srcRef, new CFRange(0, len), values);
            for (int i = 0; i < len; i++)
            {
                IntPtr ptr2 = Marshal.ReadIntPtr(values, i * 4 * 2);
                objArray[i] = ManagedTypeFromCFType(ref ptr2);
            }
            Marshal.FreeCoTaskMem(values);
            return objArray;
        }

        internal static byte[] ReadCFDataFromIntPtr(IntPtr srcRef)
        {
            if (srcRef == IntPtr.Zero)
            {
                return null;
            }
            int num = CFDataGetLength(srcRef);
            if (num <= 0)
            {
                return null;
            }
            IntPtr ptr = CFDataGetBytePtr(srcRef);
            byte[] buffer = new byte[num];
            for (int i = 0; i < num; i++)
            {
                buffer[i] = Marshal.ReadByte(ptr, i);
            }
            return buffer;
        }

        internal static DateTime ReadCFDateFromIntPtr(IntPtr srcRef)
        {
            DateTime minValue = DateTime.MinValue;
            if (srcRef != IntPtr.Zero)
            {
                double num = CFDateGetAbsoluteTime(srcRef);
                minValue = new DateTime(0x7d1, 1, 1, 0, 0, 0).AddSeconds(num);
            }
            return minValue;
        }

        public static object ReadCFDictionaryFromIntPtr(IntPtr srcRef)
        {
            if (srcRef == IntPtr.Zero)
            {
                return null;
            }
            int num = CFDictionaryGetCount(srcRef);
            if (num <= 0)
            {
                return null;
            }
            IntPtr keys = Marshal.AllocCoTaskMem(num * 4 * 2);
            IntPtr values = Marshal.AllocCoTaskMem(num * 4 * 2);
            CFDictionary.GetKeysAndValues(srcRef, keys, ref values);
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            for (int i = 0; i < num; i++)
            {
                IntPtr ptr3 = Marshal.ReadIntPtr(keys, i * 4 * 2);
                IntPtr ptr4 = Marshal.ReadIntPtr(values, i * 4 * 2);
                object key = ManagedTypeFromCFType(ref ptr3);
                object obj3 = ManagedTypeFromCFType(ref ptr4);
                dictionary.Add(key, obj3);
            }
            return dictionary;
        }

        public static object ReadCFNumberFromIntPtr(IntPtr srcRef)
        {
            if (srcRef == IntPtr.Zero)
            {
                return null;
            }
            object obj2 = null;
            switch (CFNumberGetType(srcRef))
            {
                case CFNumberType.kCFNumberFloat32Type:
                case CFNumberType.kCFNumberFloatType:
                case CFNumberType.kCFNumberCGFloatType:
                    {
                        float valuePtr = 0f;
                        if (CFNumberGetValue_Float(srcRef, CFNumberType.kCFNumberFloatType, ref valuePtr))
                        {
                            obj2 = valuePtr;
                        }
                        return obj2;
                    }
                case CFNumberType.kCFNumberFloat64Type:
                case CFNumberType.kCFNumberDoubleType:
                    {
                        double num2 = 0.0;
                        if (CFNumberGetValue_Double(srcRef, CFNumberType.kCFNumberDoubleType, ref num2))
                        {
                            obj2 = num2;
                        }
                        return obj2;
                    }
                case CFNumberType.kCFNumberSInt8Type:
                case CFNumberType.kCFNumberSInt16Type:
                case CFNumberType.kCFNumberSInt32Type:
                case CFNumberType.kCFNumberCharType:
                case CFNumberType.kCFNumberShortType:
                case CFNumberType.kCFNumberIntType:
                case CFNumberType.kCFNumberLongType:
                case CFNumberType.kCFNumberCFIndexType:
                case CFNumberType.kCFNumberNSIntegerType:
                    {
                        int num3 = 0;
                        if (CFNumberGetValue_Int(srcRef, CFNumberType.kCFNumberIntType, ref num3))
                        {
                            obj2 = num3;
                        }
                        return obj2;
                    }
                case CFNumberType.kCFNumberSInt64Type:
                case CFNumberType.kCFNumberLongLongType:
                    {
                        long num4 = 0L;
                        if (CFNumberGetValue_Long(srcRef, CFNumberType.kCFNumberSInt64Type, ref num4))
                        {
                            obj2 = num4;
                        }
                        break;
                    }
            }
            return obj2;
        }

        public static string ReadCFStringFromIntPtr(IntPtr srcRef)
        {
            if (srcRef == IntPtr.Zero)
            {
                return null;
            }
            return CFString.FetchString(srcRef);
        }

        public static object ReadPlist_managed(string fileOnPC)
        {
            object obj2 = null;
            try
            {
                IntPtr zero = IntPtr.Zero;
                int cb = 0;
                using (FileStream stream = new FileStream(fileOnPC, FileMode.Open, FileAccess.Read))
                {
                    cb = (int)stream.Length;
                    zero = Marshal.AllocCoTaskMem(cb);
                    byte[] buffer = new byte[cb];
                    stream.Read(buffer, 0, buffer.Length);
                    Marshal.Copy(buffer, 0, zero, buffer.Length);
                }
                IntPtr datas = IntPtr.Zero;
                IntPtr srcRef = IntPtr.Zero;
                IntPtr errorString = IntPtr.Zero;
                datas = CFDataCreate(kCFAllocatorDefault, zero, cb);
                if (datas != IntPtr.Zero)
                {
                    srcRef = CFPropertyListCreateFromXMLData(kCFAllocatorDefault, datas, CFPropertyListMutabilityOptions.kCFPropertyListImmutable, ref errorString);
                    if (srcRef == IntPtr.Zero)
                    {
                        return null;
                    }
                }
                if (datas != IntPtr.Zero)
                {
                    try
                    {
                        CFRelease(datas);
                    }
                    catch
                    {
                    }
                }
                if (zero != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(zero);
                }
                obj2 = ManagedTypeFromCFType(ref srcRef);
                if (srcRef != IntPtr.Zero)
                {
                    CFRelease(srcRef);
                }
            }
            catch
            {
            }
            return obj2;
        }

        /*public static string SearchXmlByKey(string xmlText, string xmlKey)
        {
            string str2 = "";
            int start = 0;
            int num2 = 0;
            start = Strings.InStr(xmlText, xmlKey, CompareMethod.Binary);
            if (start > 0)
            {
                num2 = Strings.InStr(start, xmlText, "<string>", CompareMethod.Binary);
                if (num2 > start)
                {
                    num2 += "<string>".Length;
                    start = Strings.InStr(num2, xmlText, "</string>", CompareMethod.Binary);
                    if (start > num2) str2 = Strings.Mid(xmlText, num2, start - num2);
                }
            }
            return str2.Trim();
        }*/
        public static string SearchXmlByKey(string xmlText, string xmlKey)
        {
            if (string.IsNullOrEmpty(xmlText) || string.IsNullOrEmpty(xmlKey))
                return string.Empty;

            int keyIndex = xmlText.IndexOf(xmlKey, StringComparison.Ordinal);
            if (keyIndex < 0)
                return string.Empty;

            int startTagIndex = xmlText.IndexOf("<string>", keyIndex, StringComparison.Ordinal);
            if (startTagIndex < 0)
                return string.Empty;

            startTagIndex += "<string>".Length;

            int endTagIndex = xmlText.IndexOf("</string>", startTagIndex, StringComparison.Ordinal);
            if (endTagIndex < 0 || endTagIndex <= startTagIndex)
                return string.Empty;

            string value = xmlText.Substring(startTagIndex, endTagIndex - startTagIndex);
            return value.Trim();
        }

        public static IntPtr StringToCFString(string values)
        {
            IntPtr zero = IntPtr.Zero;
            if (values != null)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(values);
                IntPtr ptrDest = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
                IntPtr cfData = CFDataCreate(IntPtr.Zero, ptrDest, bytes.Length);
                zero = CFStringCreateFromExternalRepresentation(IntPtr.Zero, cfData, (CFStringEncoding)0x8000100);
                if (cfData != IntPtr.Zero)
                {
                    CFRelease(cfData);
                }
            }
            return zero;
        }

        public static IntPtr StringToHeap(string src, Encoding encode)
        {
            if (string.IsNullOrEmpty(src))
            {
                return IntPtr.Zero;
            }
            if (encode == null)
            {
                return Marshal.StringToCoTaskMemAnsi(src);
            }
            int maxByteCount = encode.GetMaxByteCount(1);
            char[] chars = src.ToCharArray();
            int num2 = encode.GetByteCount(chars) + maxByteCount;
            byte[] bytes = new byte[0];
            bytes = new byte[num2];
            if (encode.GetBytes(chars, 0, chars.Length, bytes, 0) != bytes.Length - maxByteCount)
            {
                throw new NotSupportedException("StringToHeap编码不支持");
            }
            IntPtr destination = Marshal.AllocCoTaskMem(bytes.Length);
            if (destination == IntPtr.Zero)
            {
                throw new OutOfMemoryException();
            }
            bool flag = false;
            try
            {
                Marshal.Copy(bytes, 0, destination, bytes.Length);
                flag = true;
            }
            finally
            {
                if (!flag)
                {
                    Marshal.FreeCoTaskMem(destination);
                }
            }
            return destination;
        }
        public static bool WritePlist(IntPtr dataPtr, string fileOnPC)
        {
            bool flag = false;
            if (dataPtr == IntPtr.Zero) return false;
            IntPtr zero = IntPtr.Zero;
            try
            {
                zero = CFURLCreateWithFileSystemPath(kCFAllocatorDefault, StringToCFString(fileOnPC), CFURLPathStyle.kCFURLWindowsPathStyle, false);
                if (zero != IntPtr.Zero)
                {
                    IntPtr dataToWrite = CFPropertyListCreateXMLData(kCFAllocatorDefault, dataPtr);
                    if (dataToWrite != IntPtr.Zero)
                    {
                        IntPtr ptr3 = IntPtr.Zero;
                        int errorCode = -1;
                        flag = CFURLWriteDataAndPropertiesToResource(zero, dataToWrite, ptr3, ref errorCode);
                        CFRelease(dataToWrite);
                    }
                    else
                        flag = false;
                    CFRelease(zero);
                    return flag;
                }
                flag = false;
            }
            catch (Exception exception1)
            {

            }
            return flag;
        }

        public static bool WritePlist(object dict, string fileOnPC)
        {
            return WritePlist(CFTypeFromManagedType(dict), fileOnPC);
        }
    }
}
