using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ChangeSysTimeFrmApp
{
    internal class SysTimePro
    {
        [DllImport("Kernel32.dll")]
        public static extern bool SetLocalTime(ref SystemTime sysTime);

        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMiliseconds;
        }

        /// <summary>
        /// 调用系统API更改时间
        /// </summary>
        /// <param name="timestr"></param>
        /// <returns></returns>
        public static bool SetLocalTimeByStr(string timestr)
        {
            bool flag = false;
            SystemTime sysTime = new SystemTime();
            DateTime dt = Convert.ToDateTime(timestr);
            sysTime.wYear = Convert.ToUInt16(dt.Year);
            sysTime.wMonth = Convert.ToUInt16(dt.Month);
            sysTime.wDay = Convert.ToUInt16(dt.Day);
            sysTime.wHour = Convert.ToUInt16(dt.Hour);
            sysTime.wMinute = Convert.ToUInt16(dt.Minute);
            sysTime.wSecond = Convert.ToUInt16(dt.Second);
            flag = SetLocalTime(ref sysTime);
            return flag;
        }


        /// <summary>
        /// 采用cmd命令更改时间
        /// </summary>
        public static void SetTimeByCmd(string year, string month, string day)
        {
            //实例一个Process类，启动一个独立进程 
            Process p = new Process();
            //Process类有一个StartInfo属性 
            //设定程序名 
            p.StartInfo.FileName = "cmd.exe";
            //设定程式执行参数    “/C”表示执行完命令后马上退出
            p.StartInfo.Arguments = string.Format("/c date {0}-{1}-{2}", year, month, day);
            //关闭Shell的使用   
            p.StartInfo.UseShellExecute = false;
            //重定向标准输入      
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出   
            p.StartInfo.RedirectStandardError = true;
            //设置不显示doc窗口  
            p.StartInfo.CreateNoWindow = true;
            //启动 
            p.Start();

            //从输出流取得命令执行结果 
            p.StandardOutput.ReadToEnd();
        }
    }
}
