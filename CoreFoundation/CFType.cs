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

namespace CoreFoundation
{
    public class CFType
    {
        internal const int _CFArray = 18;
        internal const int _CFBoolean = 21;
        internal const int _CFData = 19; 
        internal const int _CFNumber = 22;
        internal const int _CFDictionary = 17;
        internal const int _CFString=7;

        internal IntPtr typeRef;

        public CFType() { }
        
        public CFType(IntPtr handle){this.typeRef = handle;}
        
        /// <summary>
        /// Returns the unique identifier of an opaque type to which a CoreFoundation object belongs
        /// </summary>
        /// <returns></returns>
        public int GetTypeID()
        {
            return CFLibrary.CFGetTypeID(typeRef);
        }
        /// <summary>
        /// Returns a textual description of a CoreFoundation object
        /// </summary>
        /// <returns></returns>
        public string GetDescription()
        {
            return new CFString(CFLibrary.CFCopyDescription(typeRef)).ToString();
        }       
                
        private string CFString()
        {
            if (typeRef == IntPtr.Zero)
                return null;
            
                string str;
                int length = CFLibrary.CFStringGetLength(typeRef);        
                IntPtr u = CFLibrary.CFStringGetCharactersPtr(typeRef);
                IntPtr buffer = IntPtr.Zero;
                if (u == IntPtr.Zero)
                {
                    CFRange range = new CFRange(0, length);
                    buffer = Marshal.AllocCoTaskMem(length * 2);
                    CFLibrary.CFStringGetCharacters(typeRef, range, buffer);
                    u = buffer;
                }
                unsafe
                {
                    str = new string((char*)u, 0, length);
                }
                if (buffer != IntPtr.Zero)
                    Marshal.FreeCoTaskMem(buffer);
                return str;
        }       
        private string CFNumber()
        {
            IntPtr buffer = Marshal.AllocCoTaskMem(CFLibrary.CFNumberGetByteSize(typeRef));
            bool scs = CFLibrary.CFNumberGetValue(typeRef, CFLibrary.CFNumberGetType(typeRef), buffer);
            if (scs != true)
            {
                return string.Empty;
            }
            int type = (int)CFLibrary.CFNumberGetType(typeRef);
            switch (type)
            {
                case 1:
                    return Marshal.ReadInt16(buffer).ToString();
                case 2:
                    return Marshal.ReadInt16(buffer).ToString();
                case 3:
                    return Marshal.ReadInt32(buffer).ToString();
                case 4:
                    return Marshal.ReadInt64(buffer).ToString();
                default:
                    return Enum.GetName(typeof(CFNumber.CFNumberType), type) + " is not supported yet!";
            }
        }
        private string CFBoolean()
        {
            return CFLibrary.CFBooleanGetValue(typeRef).ToString();
        }
        private string CFData()
        {
            CFData typeArray = new CFData(typeRef);
            return Convert.ToBase64String(typeArray.ToByteArray());
        }
        private string CFPropertyList()
        {
            return Encoding.UTF8.GetString(new CFData(CFLibrary.CFPropertyListCreateXMLData(IntPtr.Zero, typeRef)).ToByteArray());
        }
        public override string ToString()
        {
            switch (CFLibrary.CFGetTypeID(typeRef))
            {
                case _CFString:                                
                    return CFString();                
                case _CFDictionary:
                    return CFPropertyList();
                case _CFArray:
                    return CFPropertyList();
                case _CFData:
                    return CFData();
                case _CFBoolean:
                    return CFBoolean();
                case _CFNumber:
                    return CFNumber();                
            }
            return null;
        }
        public static implicit operator IntPtr(CFType value)
        {
            return value.typeRef;
        }
        public static implicit operator CFType(IntPtr value)
        {
            return new CFType(value);
        }
    }
}
