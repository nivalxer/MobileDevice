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
    public class CFPropertyList : CFType 
    {
        public CFPropertyList() { }
        public CFPropertyList(IntPtr PropertyList)
            : base(PropertyList)
        {
        }
        public CFPropertyList(string plistlocation) 
        {            
            IntPtr inputfilename;
            inputfilename = new CFString(plistlocation);

            IntPtr ifile_IntPtr = CFLibrary.CFURLCreateWithFileSystemPath(IntPtr.Zero, inputfilename, 2, false);
            IntPtr ifile_CFReadStreamRef = CFLibrary.CFReadStreamCreateWithFile(IntPtr.Zero, ifile_IntPtr);
            if ((CFLibrary.CFReadStreamOpen(ifile_CFReadStreamRef)) == false)
            {
                typeRef = IntPtr.Zero;
            }
            IntPtr PlistRef = CFLibrary.CFPropertyListCreateFromStream(IntPtr.Zero, ifile_CFReadStreamRef, 0, 2, 0, IntPtr.Zero);
            CFLibrary.CFReadStreamClose(ifile_CFReadStreamRef);
            typeRef = PlistRef;
        }

        public static implicit operator CFPropertyList(IntPtr value)
        {
            return new CFPropertyList(value);
        }

        public static implicit operator IntPtr(CFPropertyList value)
        {
            return value.typeRef;
        }

        public static implicit operator string(CFPropertyList value)
        {
            return value.ToString();
        }

        public static implicit operator CFPropertyList(string value)
        {
            return new CFPropertyList(value);
        }               
    }
}
