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
using System.Xml;

namespace CoreFoundation
{
    public class CFDictionary : CFType
    {        
        public CFDictionary() { }

        public CFDictionary(IntPtr dictionary) : base(dictionary)
        {
        }

        unsafe public CFDictionary(string[] keys,IntPtr[] values)
        {
            IntPtr[] keyz = new IntPtr[keys.Length];  
            for (int i = 0; i < keys.Length; i++)
            {
                keyz[i] = new CFString(keys[i]);                
            }
            CFDictionaryKeyCallBacks kcall = new CFDictionaryKeyCallBacks();

            CFDictionaryValueCallBacks vcall = new CFDictionaryValueCallBacks();
            base.typeRef = CFLibrary.CFDictionaryCreate(IntPtr.Zero,keyz,values,keys.Length,ref kcall,ref vcall);            
        }
       
        /// <summary>
       /// Returns the value associated with a given key.
       /// </summary>
       /// <param name="value"></param>
       /// <returns></returns>
        public CFType GetValue(string value)
        {
            try
            {
                return new CFType(CFLibrary.CFDictionaryGetValue(base.typeRef, new CFString(value)));
            }
            catch (Exception ex)
            {
                return new CFType(IntPtr.Zero);
            }

        }
        /// <summary>
        /// Returns the number of key-value pairs in a dictionary
        /// </summary>
        /// <returns></returns>
        public int Length
        {
            get
            {
                return CFLibrary.CFDictionaryGetCount(base.typeRef);
            }
        }    


        public static implicit operator CFDictionary(IntPtr value)
        {
            return new CFDictionary(value);
        }

        public static implicit operator IntPtr(CFDictionary value)
        {
            return value.typeRef;
        }

        public static implicit operator string(CFDictionary value)
        {
            return value.ToString();
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct CFDictionaryKeyCallBacks
        {
            int version;
            CFDictionaryRetainCallBack retain;
            CFDictionaryReleaseCallBack release;
            CFDictionaryCopyDescriptionCallBack copyDescription;
            CFDictionaryEqualCallBack equal;
            CFDictionaryHashCallBack hash;

        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct CFDictionaryValueCallBacks
        {
            int version;
            CFDictionaryRetainCallBack retain;
            CFDictionaryReleaseCallBack release;
            CFDictionaryCopyDescriptionCallBack copyDescription;
            CFDictionaryEqualCallBack equal;
        };

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr CFDictionaryRetainCallBack(IntPtr allocator, IntPtr value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void CFDictionaryReleaseCallBack(IntPtr allocator, IntPtr value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr CFDictionaryCopyDescriptionCallBack(IntPtr value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr CFDictionaryEqualCallBack(IntPtr value1, IntPtr value2);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr CFDictionaryHashCallBack(IntPtr value);
    }

            /*
        public delegate IntPtr CFDictionaryRetainCallBack(IntPtr allocator, IntPtr type); 
        public delegate IntPtr CFDictionaryReleaseCallBack(IntPtr allocator, IntPtr type);
        public delegate IntPtr CFDictionaryCopyDescriptionCallBack(IntPtr type);
        public delegate IntPtr CFDictionaryEqualCallBack(IntPtr type1,IntPtr type2);
        public delegate int CFDictionaryHashCallBack(IntPtr type);
             */
    /*
        public struct CFDictionaryKeyCallBacks
        {
            CFIndex version;
            CFDictionaryRetainCallBack retain;
            CFDictionaryReleaseCallBack release;
            CFDictionaryCopyDescriptionCallBack copyDescription;
            CFDictionaryEqualCallBack equal;
            CFDictionaryHashCallBack hash;
        };

        public struct CFDictionaryValueCallBacks
        {
            CFIndex version;
            CFDictionaryRetainCallBack retain;
            CFDictionaryReleaseCallBack release;
            CFDictionaryCopyDescriptionCallBack copyDescription;
            CFDictionaryEqualCallBack equal;
        };
    
    public interface CFDictionaryRetainCallBack
    {
        IntPtr invoke(IntPtr paramCFAllocator, IntPtr paramCFType);
    }

    public interface CFDictionaryReleaseCallBack
    {
        void invoke(IntPtr paramCFAllocator, IntPtr paramCFType);
    }

    public interface CFDictionaryCopyDescriptionCallBack
    {
        IntPtr invoke(IntPtr paramCFType);
    }

    public interface CFDictionaryEqualCallBack
    {
        bool invoke(IntPtr paramCFType1, IntPtr paramCFType2);
    }

    public interface CFDictionaryHashCallBack
    {
        int invoke(IntPtr paramCFType);
    }
    */
    



}
