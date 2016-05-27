using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ChangeSysTimeApp
{
    internal class Program
    {
        /*
         * 手动更改系统时间
         */

        private static void Main(string[] args)
        {
        
                /** 
                * 当前用户是管理员的时候，直接启动应用程序 
                * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行 
                */

                //获得当前登录的Windows用户标示 
            System.Security.Principal.WindowsIdentity identity =
                System.Security.Principal.WindowsIdentity.GetCurrent();
 
            System.Security.Principal.WindowsPrincipal principal =
                new System.Security.Principal.WindowsPrincipal(identity);

                //判断当前登录用户是否为管理员 
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                //如果是管理员，则直接运行 
                ProcessMethod();
            }
            else
            {
                //创建启动对象 
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //设置运行文件 
                startInfo.FileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                //设置启动参数 
                startInfo.Arguments = String.Join(" ", args);
                //设置启动动作,确保以管理员身份运行 
                startInfo.Verb = "runas";
                //如果不是管理员，则启动UAC 
                System.Diagnostics.Process.Start(startInfo);
                //退出 
                //System.Windows.Forms.Application.Exit();
                
            }
         
        }

        static void ProcessMethod()
        {


            int timespan = 20; // 20 毫秒
            int count = 20*(1000/timespan); // 20 秒

            Console.WriteLine("倒计时 " + count / (1000 / timespan) + " 秒；");

            string currentYear = DateTime.Now.Year.ToString();

            System.Timers.Timer timer = new Timer(timespan);
            timer.Elapsed += (sender, args) =>
            {
                DateTime now = DateTime.Now;
                if (now.Year != 2008)
                {
                    //SysTimePro.SetTimeByCmd("2008",now.Month.ToString(),now.Day.ToString());
                    string timeStr = "2008" + now.ToString("-MM-dd HH:mm:ss");
                    SysTimePro.SetLocalTimeByStr(timeStr);
                }

                Console.Write("-");
                if (count%10 == 0)
                {
                    Console.WriteLine();
                }

                if (--count == 0)
                {
                    timer.Stop();
                    string timeStr = currentYear + now.ToString("-MM-dd HH:mm:ss");
                    SysTimePro.SetLocalTimeByStr(timeStr);
                    System.Environment.Exit(1);
                }
                else
                {
                    if (count%(1000/timespan) == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("倒计时 " + count/(1000/timespan) + " 秒；");
                    }
                }
            };
            timer.Start();

            while (true)
            {
                
            }

        }



    }
}
