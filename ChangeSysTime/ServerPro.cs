using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ChangeSysTime
{
    /// <summary>
    /// service running code
    /// </summary>
    class ServerPro
    {
        public delegate void StopEventHandler();

        /// <summary>
        /// 通知服务主程序，服务已处理完毕，可以退出服务
        /// </summary>
        public static event StopEventHandler CanStop;

        public static void StartService()
        {
            System.Timers.Timer timer = new Timer(200);
            timer.Elapsed += TimerOnElapsed;
            timer.Start();
        }

        private static void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            DateTime n = DateTime.Now;

            if (n.Year == 2008)
            {
                return;
            }

            // 将年份改为2008年                  
            string dtstr = string.Format("2008-{0}-{1} {2}:{3}:{4}", n.Month, n.Day, n.Hour, n.Minute, n.Second);
            SysTimePro.SetLocalTimeByStr(dtstr);

        }

    }
}
