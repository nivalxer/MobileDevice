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
    public class CFNumber : CFType 
    {
        public CFNumber() { }
        public CFNumber(IntPtr Number)
            : base(Number)
        {
        }
        unsafe public CFNumber(int Number) 
        {
            int* pNumber=&Number;
            base.typeRef = CFLibrary.CFNumberCreate(IntPtr.Zero, CFNumberType.kCFNumberIntType, pNumber);
        }       
        public enum CFNumberType 
        { 
            kCFNumberSInt8Type = 1, 
            kCFNumberSInt16Type = 2, 
            kCFNumberSInt32Type = 3, 
            kCFNumberSInt64Type = 4, 
            kCFNumberFloat32Type = 5, 
            kCFNumberFloat64Type = 6, 
            kCFNumberCharType = 7, 
            kCFNumberShortType = 8, 
            kCFNumberIntType = 9, 
            kCFNumberLongType = 10, 
            kCFNumberLongLongType = 11, 
            kCFNumberFloatType = 12, 
            kCFNumberDoubleType = 13, 
            kCFNumberCFIndexType = 14, 
            kCFNumberNSIntegerType = 15,            
            kCFNumberCGFloatType = 16, 
            kCFNumberMaxType = 16 
        };        
           
        }
    }

