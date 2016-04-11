using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeSysTimeApp
{
    class Program
    {
        /*
         * 手动更改系统时间
         */

        static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            SysTimePro.SetTimeByCmd("2008",now.Month.ToString(),now.Day.ToString());
        }
    }
}
