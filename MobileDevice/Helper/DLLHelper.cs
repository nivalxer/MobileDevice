using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace MobileDevice.Helper
{
    public class DLLHelper
    {
        /// <summary>
        /// 获取iTunesMobileDeviceDll位置
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetiTunesMobileDeviceDllPath()
        {
            //判断注册表
            RegistryKey subkey = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Apple Inc.\Apple Mobile Device Support\Shared");
            if (subkey != null)
            {
                string path = subkey.GetValue("iTunesMobileDeviceDLL") as string;
                if (!string.IsNullOrWhiteSpace(path))
                {
                    return path;
                }
            }
            //判断常用路径
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles) + @"\Apple\Mobile Device Support\bin";//判断64位
            if (!File.Exists(directory + @"\iTunesMobileDevice.dll"))
            {
                return directory;
            }
            directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86) + @"\Apple\Mobile Device Support\bin";//针对老版本的iTunes64位
            if (!File.Exists(directory + @"\iTunesMobileDevice.dll"))
            {
                return directory;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取AppleApplicationSupport目录
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetAppleApplicationSupportFolder()
        {
            //判断注册表
            RegistryKey subkey = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Apple Inc.\Apple Application Support");
            if (subkey != null)
            {
                string path = subkey.GetValue("InstallDir") as string;
                if (!string.IsNullOrWhiteSpace(path))
                {
                    return path;
                }
            }
            //判断常用路径
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles) + @"\Apple\Apple Application Support\";//判断64位
            if (!File.Exists(directory + @"\CoreFoundation.dll"))
            {
                return directory;
            }
            directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86) + @"\Apple\Apple Application Support\";//针对老版本的iTunes64位
            if (!File.Exists(directory + @"\CoreFoundation.dll"))
            {
                return directory;
            }
            return string.Empty;
        }
    }
}
