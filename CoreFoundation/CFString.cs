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

namespace CoreFoundation
{
   
    public sealed class CFString : CFType
    {
        public CFString()
        {            
        }             
        /// <summary>
        /// Creates an immutable string from a constant compile-time string
        /// </summary>
        /// <param name="str"></param>
        unsafe public CFString(string str) 
        {
            base.typeRef = CFLibrary.__CFStringMakeConstantString(str);
        }
        public CFString(IntPtr myHandle) : base(myHandle)
        {    
        }
        /// <summary>
        /// Checks if the current object is a valid CFString object
        /// </summary>
        /// <returns></returns>
        public bool isString()
        {
            return CFLibrary.CFGetTypeID(typeRef) == _CFString;
        }
        public static implicit operator CFString(IntPtr value)
        {
            return new CFString(value);
        }

        public static implicit operator IntPtr(CFString value)
        {
            return value.typeRef;
        }

        public static implicit operator string(CFString value)
        {
            return value.ToString();
        }

        public static implicit operator CFString(string value)
        {            
            return new CFString(value);
        }            
    }         
}
