using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MobileDevice.Unitiy
{
    public class EnumHelper
    {
        #region##获得Enum类型description
        ///<summary>        
        /// 获得Enum类型description        
        ///</summary>        
        ///<param name="enumType">枚举的类型</param>        
        ///<param name="val">枚举值</param>        
        ///<returns>string</returns>        
        public static string GetEnumDescription(Type enumType, object val)
        {
            string enumvalue = System.Enum.GetName(enumType, val);
            if (string.IsNullOrEmpty(enumvalue))
            {
                return string.Empty;
            }

            System.Reflection.FieldInfo finfo = enumType.GetField(enumvalue);
            object[] enumAttr = finfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (enumAttr.Length > 0)
            {
                DescriptionAttribute desc = enumAttr[0] as DescriptionAttribute;
                if (desc != null)
                {
                    return desc.Description;
                }
            }
            return enumvalue;
        }

        #endregion



        #region##获取某个枚举的全部信息
        ///<summary>        
        /// 获取某个枚举的全部信息        
        ///</summary>        
        ///<typeparam name="T">枚举</typeparam>        
        ///<returns>枚举的全部信息</returns>        
        public static IList<EnumInfo> GetEnumList<T>()
        {
            IList<EnumInfo> list = new List<EnumInfo>();
            EnumInfo re = null;
            Type type = typeof(T);
            foreach (int enu in System.Enum.GetValues(typeof(T)))
            {
                re = new EnumInfo();
                re.Text = GetEnumDescription(type, enu);
                re.Value = GetModel<T>(enu).ToString();
                re.Value2 = enu;
                list.Add(re);
            }
            return list;
        }


        #endregion
        #region##根据值返回枚举对应的内容
        ///<summary>        
        /// 根据值返回枚举对应的内容         
        ///</summary>        
        ///<typeparam name="T">枚举</typeparam>       
        //////<param name="value">值(int)</param>        
        ///<returns></returns>        
        public static T GetModel<T>(int value)
        {
            T myEnum = (T)System.Enum.Parse(typeof(T), value.ToString(), true);
            return myEnum;
        }

        ///<summary>        
        /// 根据值返回枚举对应的内容         
        ///</summary>        
        ///<typeparam name="T">枚举</typeparam>       
        //////<param name="value">值(int)</param>        
        ///<returns></returns>        
        public static T GetModel<T>(string value)
        {
            T myEnum = (T)System.Enum.Parse(typeof(T), value.ToString(), true);
            return myEnum;
        }


        #endregion
        
    }
    public class EnumInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Value2 { get; set; }
    }
}
