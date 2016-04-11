using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace ChangeSysTime
{
    partial class ChangeTimeService : ServiceBase
    {
        public ChangeTimeService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ServerPro.StartService();
            ServerPro.CanStop += Stop;
            EventLog.WriteEntry("ServiceChangeSysTimeStarted");
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("ServiceChangeSysTimeStoped");
        }
    }
}
