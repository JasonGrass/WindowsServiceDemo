using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace ChangeSysTime
{
    /*
     * .net version : 2.0
     * service cannot run in winxp while .net version is 4.0
     */

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ChangeTimeService(),  
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
