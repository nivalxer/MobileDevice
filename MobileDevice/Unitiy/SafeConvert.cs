using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MobileDevice.Unitiy
{
    /// <summary>
    /// 用于把字符串转换为对于的数据类型,作用类似系统内置的Convert.ToXXX,
    /// 特点是当转换失败时,不会抛出异常而是返回默认,返回值.
    /// </summary>
    public static class SafeConvert
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ToBoolean(string s)
        {
            bool result;
            bool succeeded = bool.TryParse(s, out result);
            return succeeded ? result : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool[] ToBoolean(string[] s)
        {
            return Array.ConvertAll<string, bool>(s, ToBoolean);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte ToByte(string s)
        {
            byte result;
            bool succeeded = byte.TryParse(s, out result);
            return succeeded ? result : (byte)0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] ToByte(string[] s)
        {
            return Array.ConvertAll<string, byte>(s, ToByte);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static sbyte ToSbyte(string s)
        {
            sbyte result;
            bool succeeded = sbyte.TryParse(s, out result);
            return succeeded ? result : (sbyte)0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static sbyte[] ToSbyte(string[] s)
        {
            return Array.ConvertAll<string, sbyte>(s, ToSbyte);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static short ToInt16(string s)
        {
            return ToInt16(s, 0);
        }

        public static short ToInt16(string s, short defaultValue)
        {
            short result;
            bool succeeded = short.TryParse(s, out result);
            return succeeded ? result : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static short[] ToInt16(string[] s)
        {
            return Array.ConvertAll<string, short>(s, ToInt16);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ushort ToUInt16(string s)
        {
            return ToUInt16(s, 0);
        }

        public static ushort ToUInt16(string s, ushort defaultValue)
        {
            ushort result;
            bool succeeded = ushort.TryParse(s, out result);
            return succeeded ? result : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ushort[] ToUInt16(string[] s)
        {
            return Array.ConvertAll<string, ushort>(s, ToUInt16);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt32(string s)
        {
            return ToInt32(s, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt32(string s, int defaultValue)
        {
            int result;
            bool succeeded = int.TryParse(s, out result);
            return succeeded ? result : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int[] ToInt32(string[] s)
        {
            return Array.ConvertAll<string, int>(s, ToInt32);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static uint ToUInt32(string s)
        {
            uint result;
            bool succeeded = uint.TryParse(s, out result);
            return succeeded ? result : 0;
        }

        public static uint ToUInt32(string s, uint defalutValue)
        {
            uint result;
            bool succeeded = uint.TryParse(s, out result);
            return succeeded ? result : defalutValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static uint[] ToUInt32(string[] s)
        {
            return Array.ConvertAll<string, uint>(s, ToUInt32);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defalutValue"></param>
        /// <returns></returns>
        public static long ToInt64(string s, long defalutValue)
        {
            long result;
            bool succeeded = long.TryParse(s, out result);
            return succeeded ? result : defalutValue;
        }

        public static long ToInt64(string s)
        {
            return ToInt64(s, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long[] ToInt64(string[] s)
        {
            return Array.ConvertAll<string, long>(s, ToInt64);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ulong ToUInt64(string s)
        {
            return ToUInt64(s, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong ToUInt64(string s, uint defaultValue)
        {
            ulong result;
            bool succeeded = ulong.TryParse(s, out result);
            return succeeded ? result : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ulong[] ToUInt64(string[] s)
        {
            return Array.ConvertAll<string, ulong>(s, ToUInt64);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ToSingle(string s, float defaultValue)
        {
            float result;
            bool succeeded = float.TryParse(s, out result);
            return succeeded ? result : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float ToSingle(string s)
        {
            return ToSingle(s, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float[] ToSingle(string[] s)
        {
            return Array.ConvertAll<string, float>(s, ToSingle);
        }


        public static double ToDouble(string s, double defaultValue)
        {
            double result;
            bool succeeded = double.TryParse(s, out result);
            return succeeded ? result : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double ToDouble(string s)
        {
            return ToDouble(s, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double[] ToDouble(string[] s)
        {
            return Array.ConvertAll<string, double>(s, ToDouble);
        }

        public static float ToFloat(string s, float defaultValue = 0)
        {
            float result;
            bool succeeded = float.TryParse(s, out result);
            return succeeded ? result : defaultValue;
        }

        public static decimal ToDecimal(string s, decimal defaultValue)
        {
            decimal result;
            bool succeeded = decimal.TryParse(s, out result);
            return succeeded ? result : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal ToDecimal(string s)
        {
            return ToDecimal(s, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal[] ToDecimal(string[] s)
        {
            return Array.ConvertAll<string, decimal>(s, ToDecimal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string s, DateTime defaultValue)
        {
            DateTime result;
            bool succeeded = DateTime.TryParse(s, out result);
            return succeeded ? result : defaultValue;
        }

        /// <summary>
        /// 不成功则返回DateTime.MinValue
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string s)
        {
            return ToDateTime(s, DateTime.MinValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime[] ToDateTime(string[] s)
        {
            return Array.ConvertAll<string, DateTime>(s, ToDateTime);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(string s, TimeSpan defaultValue)
        {
            TimeSpan result;
            bool succeeded = TimeSpan.TryParse(s, out result);
            return succeeded ? result : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(string s)
        {
            return ToTimeSpan(s, TimeSpan.Zero);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static TimeSpan[] ToTimeSpan(string[] s)
        {
            return Array.ConvertAll<string, TimeSpan>(s, ToTimeSpan);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Object ToEnum(object obj, Type enumType)
        {
            if (System.Enum.IsDefined(enumType, obj))
            {
                string[] names = System.Enum.GetNames(enumType);
                string name = obj.ToString();
                for (int i = 0; i < names.Length; i++)
                {
                    if (name == names[i])
                    {
                        return System.Enum.Parse(enumType, name);
                    }
                }
                return System.Enum.ToObject(enumType, obj);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ToEnum<T>(object obj) where T : struct
        {
            int intObj = 0;
            bool result = int.TryParse(obj.ToString(), out intObj);
            if (System.Enum.IsDefined(typeof(T), obj))
            {
                string[] names = System.Enum.GetNames(typeof(T));
                string name = obj.ToString();
                for (int i = 0; i < names.Length; i++)
                {
                    if (name == names[i])
                    {
                        return (T)System.Enum.Parse(typeof(T), name);
                    }
                }
                return (T)System.Enum.ToObject(typeof(T), obj);
            }
            else if (result)
            {
                return (T)System.Enum.ToObject(typeof(T), intObj);
            }
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T[] ToEnum<T>(object[] s) where T : struct
        {
            return Array.ConvertAll<object, T>(s, ToEnum<T>);
        }

        public static string ToString(string p)
        {
            return p;
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(long timeStamp)
        {
            if (timeStamp == 0) return null;
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp.ToString() + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (long)(time - startTime).TotalSeconds;
        }
    }
}