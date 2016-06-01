using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace ChangeSysTimeFrmApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private DateTime realDate;
        private System.Timers.Timer timer;

        private void MainForm_Load(object sender, EventArgs e)
        {
            nudYear.Value = 2008;
            nudMonth.Value = DateTime.Now.Month;
            nudDay.Value = DateTime.Now.Day;
            nudMonth.Enabled = false;
            nudDay.Enabled = false;

            EventHandler eh = (o, args) =>
            {
                int timelength = (int) nudHour.Value*3600 + (int) nudMinute.Value*60 + (int) nudSecond.Value;
                lbLeftTime.Text = timelength.ToString();
            };

            nudSecond.ValueChanged += eh;
            nudMinute.ValueChanged += eh;
            nudHour.ValueChanged += eh;

            nudSecond.Value = 20;

            realDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        }


        void ProcessMethod(int timeLength,int year)
        {
            int timespan = 20; // 20 毫秒
            int count = timeLength * (1000 / timespan); // 20 秒

            Console.WriteLine("倒计时 " + count / (1000 / timespan) + " 秒；");

            string currentYear = DateTime.Now.Year.ToString();

            timer = new Timer(timespan);
            timer.Elapsed += (sender, args) =>
            {
                DateTime now = DateTime.Now;
                if (now.Year != year)
                {
                    //SysTimePro.SetTimeByCmd("2008",now.Month.ToString(),now.Day.ToString());
                    string timeStr = year + now.ToString("-MM-dd HH:mm:ss");
                    SysTimePro.SetLocalTimeByStr(timeStr);
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
                    if (count % (1000 / timespan) == 0)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lbLeftTime.Text = (count/(1000/timespan)).ToString();
                        }));
                    }
                }
            };
            timer.Start();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int timelength = (int) nudHour.Value*3600 + (int) nudMinute.Value*60 + (int) nudSecond.Value;
            ProcessMethod(timelength, (int)nudYear.Value);
            btnOk.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
            }
            string timeStr = realDate.Year + DateTime.Now.ToString("-MM-dd HH:mm:ss");
            SysTimePro.SetLocalTimeByStr(timeStr);
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer != null && timer.Enabled)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
            
        }



    }
}
