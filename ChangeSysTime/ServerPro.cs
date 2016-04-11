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
            System.Timers.Timer timer = new Timer(1000);
            timer.Elapsed += TimerOnElapsed;
            timer.Start();
        }

        private static int timeCount = 0;

        private static void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            timeCount++;
            DateTime n = DateTime.Now;

            if (n.Year == 2008)
            {
                // 服务至少运行较长一段时间，因为在系统开机之后，可能自动对时，将时间改回原来的值
                if (timeCount > 100)
                {
                    if (CanStop != null)
                    {
                        CanStop();
                    }
                }
                return;
            }

            // 将年份改为2008年                  
            string dtstr = string.Format("2008-{0}-{1} {2}:{3}:{4}", n.Month, n.Day, n.Hour, n.Minute, n.Second);

            SysTimePro.SetLocalTimeByStr(dtstr);

        }

    }
}
