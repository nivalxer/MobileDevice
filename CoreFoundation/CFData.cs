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
    public class CFData :  CFType 
    {      
        public CFData(){}
        public CFData(IntPtr Data)
            : base(Data)
        {
        }
        unsafe public CFData(byte[] Data)
        {            
            byte[] buffer = Data;            
            int len = buffer.Length;            
            fixed (byte* bytePtr = buffer)
                
                base.typeRef = CFLibrary.CFDataCreate(IntPtr.Zero, (IntPtr)bytePtr,len);            
        }
        /// <summary>
        /// Returns the number of bytes contained by the CFData object
        /// </summary>
        /// <returns></returns>
        public int Length()
        {
            return CFLibrary.CFDataGetLength(typeRef);
        }
        /// <summary>
        /// Checks if the object is a valid CFData object
        /// </summary>
        /// <returns></returns>
        public bool isData()
        {
            return CFLibrary.CFGetTypeID(typeRef) == _CFData;
        }
        /// <summary>
        /// Returns the CFData object as a byte array
        /// </summary>
        /// <returns></returns>
        unsafe public byte[] ToByteArray()
        {            
            int len = Length();
            byte[] buffer = new byte[len];
            fixed (byte* bufPtr = buffer)
                CFLibrary.CFDataGetBytes(typeRef, new CFRange(0, len), (IntPtr)bufPtr);
            return buffer;            
        }

        public static implicit operator CFData(IntPtr value)
        {
            return new CFData(value);
        }

        public static implicit operator IntPtr(CFData value)
        {
            return value.typeRef;
        }       
    }
}
