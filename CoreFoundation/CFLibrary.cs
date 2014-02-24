// Software License Agreement (BSD License)
// 
// Copyright (c) 2011, iSn0wra1n <isn0wra1ne@gmail.com>
// All rights reserved.
// 
// Redistribution and use of this software in source and binary forms, with or without modification are
// permitted without the copyright owner's permission provided that the following conditions are met:
// 
// * Redistributions of source code must retain the above
//   copyright notice, this list of conditions and the
//   following disclaimer.
// 
// * Redistributions in binary form must reproduce the above
//   copyright notice, this list of conditions and the
//   following disclaimer in the documentation and/or other
//   materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR
// TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
// ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;
using System.Runtime.CompilerServices;

namespace CoreFoundation
{
    public class CFLibrary
    {
        #region CFArray

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFArrayCreate(IntPtr allocator, IntPtr[] keys , int numValues, IntPtr callbacks);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFArrayGetCount(IntPtr theArray);
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CFArrayGetValues(IntPtr intptr, CFRange cfrange, IntPtr intptr_1);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFArrayGetValueAtIndex(IntPtr theArray, int index);
        #endregion
        #region CFBoolean

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern bool CFBooleanGetValue(IntPtr theBoolean);
        #endregion
        #region CFData

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFDataGetLength(IntPtr theData);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFDataGetBytePtr(IntPtr theData);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void CFDataGetBytes(IntPtr theData, CFRange range, IntPtr buffer);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFDataCreate(IntPtr theAllocator, IntPtr bytes, int bytelength);
        #endregion
        #region CFDictionary

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFDictionaryGetValue(IntPtr theDict, IntPtr key);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFDictionaryGetCount(IntPtr theDict);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFDictionaryGetKeysAndValues(IntPtr theDict, IntPtr[] keys, IntPtr[] values);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFDictionaryCreate(IntPtr allocator, IntPtr[] keys, IntPtr[] values, int numValues, ref CFDictionary.CFDictionaryKeyCallBacks kcall, ref CFDictionary.CFDictionaryValueCallBacks vcall);
        #endregion
        #region CFNumber

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFNumberCreate(IntPtr theAllocator, CFNumber.CFNumberType theType, int* valuePtr);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern bool CFNumberGetValue(IntPtr theNumber, IntPtr pint, IntPtr valuePtr);
        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberGetValue", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CFNumberGetValue_1(IntPtr number, int theType, ref long valuePtr);
        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberGetValue", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CFNumberGetValue_2(IntPtr number, int theType, ref int valuePtr);
        [DllImport("CoreFoundation.dll", EntryPoint = "CFNumberGetValue", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CFNumberGetValue_3(IntPtr number, int theType, ref double valuePtr);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFNumberGetType(IntPtr theNumber);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFNumberGetByteSize(IntPtr theNumber);
        #endregion
        #region CFPropertyList

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern bool CFPropertyListIsValid(IntPtr theList, IntPtr theFormat);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFPropertyListCreateXMLData(IntPtr theAllocator, IntPtr theList);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFPropertyListCreateWithData(IntPtr theAllocator, IntPtr theData, int options, int format,IntPtr errorString);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFPropertyListCreateFromStream(IntPtr allocator,IntPtr stream, int length,int options,int format,IntPtr errorString);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFPropertyListWriteToStream(IntPtr propertyList, IntPtr stream, int format,IntPtr errorString);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern bool CFPropertyListIsValid(IntPtr plist,int format);
        #endregion
        #region CFString

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr __CFStringMakeConstantString(string cstring);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFStringGetLength(IntPtr handle);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFStringGetCharactersPtr(IntPtr handle);
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CFStringGetBytes(IntPtr theString, CFRange range, uint encoding, byte lossByte, byte isExternalRepresentation, byte[] buffer, int maxBufLen, ref int usedBufLen);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFStringGetCharacters(IntPtr handle, CFRange range, IntPtr buffer);
        #endregion
        #region CFReadStream
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFReadStreamCreateWithFile(IntPtr alloc, IntPtr fileURL);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern bool CFReadStreamOpen(IntPtr stream);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void CFReadStreamClose(IntPtr stream);
        #endregion
        #region CFType
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern int CFGetTypeID(IntPtr typeRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]        
        public static unsafe extern IntPtr CFCopyDescription(IntPtr typeRef);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFCopyTypeIDDescription(int typeID);
        #endregion        
        #region CFURL
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFURLCreateWithFileSystemPath(IntPtr allocator, IntPtr filePath, int pathStyle, bool isDirectory);
        #endregion
        #region CFWriteStream
        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr CFWriteStreamCreateWithFile(IntPtr allocator, IntPtr fileURL);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern bool CFWriteStreamOpen(IntPtr stream);

        [DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void CFWriteStreamClose(IntPtr stream);
        #endregion
        #region CFFuncation
  
        #endregion
    }
}
