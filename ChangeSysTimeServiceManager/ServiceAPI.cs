using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using Microsoft.Win32;

namespace ChangeSysTimeServiceManager
{
    public class ServiceAPI
    {
        /// <summary>  
        /// 检查服务存在的存在性  
        /// </summary>  
        /// <param name="serviceName">服务名</param>
        /// <returns>存在返回 true,否则返回 false;</returns>  
        public static bool IsServiceIsExisted(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            return services.Any(s => string.Equals(s.ServiceName, serviceName, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>  
        /// 安装Windows服务  
        /// </summary>  
        /// <param name="stateSaver">集合</param>  
        /// <param name="filepath">程序文件路径</param>  
        public static void InstallService(IDictionary stateSaver, string filepath)
        {
            AssemblyInstaller installer = new AssemblyInstaller();
            installer.UseNewContext = true;
            installer.Path = filepath;
            installer.Install(stateSaver);
            installer.Commit(stateSaver);
            installer.Dispose();
        }

        /// <summary>  
        /// 卸载Windows服务  
        /// </summary>  
        /// <param name="filepath">程序文件路径</param>  
        public static void UnInstallService(string filepath)
        {
            AssemblyInstaller installer = new AssemblyInstaller();
            installer.UseNewContext = true;
            installer.Path = filepath;
            installer.Uninstall(null);
            installer.Dispose();
        }

        /// <summary>  
        /// 启动服务  
        /// </summary>  
        /// <returns>存在返回 true,否则返回 false;</returns>  
        public static bool RunService(string serviceName)
        {
            bool success = true;
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                if (sc.Status.Equals(ServiceControllerStatus.Stopped) || sc.Status.Equals(ServiceControllerStatus.StopPending))
                {
                    sc.Start();
                }
            }
            catch (Exception ex)
            {
                success = false;
                //LogAPI.WriteLog(ex.Message);
            }

            return success;
        }

        /// <summary>  
        /// 停止服务  
        /// </summary>  
        /// <returns>存在返回 true,否则返回 false;</returns>  
        public static bool StopService(string serviceName)
        {
            bool success = true;
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                if (!sc.Status.Equals(ServiceControllerStatus.Stopped))
                {
                    sc.Stop();
                }
            }
            catch (Exception ex)
            {
                success = false;
                // LogAPI.WriteLog(ex.Message);
            }

            return success;
        }

        /// <summary>  
        /// 获取服务状态  
        /// </summary>  
        /// <returns>返回服务状态</returns>  
        public static int GetServiceStatus(string serviceName)
        {
            int ret = 0;
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                ret = Convert.ToInt16(sc.Status);
            }
            catch (Exception ex)
            {
                ret = 0;
                // LogAPI.WriteLog(ex.Message);
            }
            return ret;
        }

        /// <summary>  
        /// 获取服务安装路径  
        /// </summary>  
        /// <returns></returns>  
        public static string GetWindowsServiceInstallPath(string serviceName)
        {
            string path = "";
            try
            {
                string key = @"SYSTEM\CurrentControlSet\Services\" + serviceName;
                path = Registry.LocalMachine.OpenSubKey(key).GetValue("ImagePath").ToString();

                path = path.Replace("\"", string.Empty);//替换掉双引号    

                FileInfo fi = new FileInfo(path);
                path = fi.Directory.ToString();
            }
            catch (Exception ex)
            {
                path = "";
                //LogAPI.WriteLog(ex.Message);
            }
            return path;
        }

        /// <summary>  
        /// 获取指定服务的版本号  
        /// </summary>  
        /// <returns></returns>  
        public static string GetServiceVersion(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
            {
                return string.Empty;
            }
            try
            {
                string path = GetWindowsServiceInstallPath(serviceName) + "\\" + serviceName + ".exe";
                Assembly assembly = Assembly.LoadFile(path);
                AssemblyName assemblyName = assembly.GetName();
                Version version = assemblyName.Version;
                return version.ToString();
            }
            catch (Exception ex)
            {
                // LogAPI.WriteLog(ex.Message);
                return string.Empty;
            }
        }
    }  
}
