using Cosmos.HAL;
using Cosmos.System;
using spaghettoOS.Forms;
using spaghettoOS.Forms.Elements;
using spaghettoOS.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Cosmos.System.Graphics.Point;

namespace spaghettoOS.Applications.Apps {
    public class TaskbarApp : IApp {

        public string Name => "TaskbarApp";

        private Form form;

        Label labelFPS;
        Label labelDate;
        Label labelTime;

        public void OnStart(object[] args) {
            form = new("taskbar", Color.FromArgb(36, 36, 36), new Point(0, 720 - 40), new Point(1280, 40));
            form.TitleBarEnabled = false;

            labelTime = new("time", new Point(10, 0));
            labelTime.Text = "00:00";
            labelTime.FontSize = 15;
            labelTime.Form = form;

            labelDate = new("date", new Point(10, 20));
            labelDate.Text = "01/01/1970";
            labelDate.FontSize = 15;
            labelDate.Form = form;

            labelFPS = new("fps", new Point(1213, 9));
            labelFPS.Text = "FPS: 00";
            labelFPS.FontSize = 15;
            labelFPS.Form = form;

            form.formElements.Add(labelTime);
            form.formElements.Add(labelDate);
            form.formElements.Add(labelFPS);

            WindowManager.Instance.RegisterForm(form);
        }

        public void Update() {
            labelTime.Text = RTC.Hour.ToString("00") + ":" + RTC.Minute.ToString("00") + ":" + RTC.Second.ToString("00");
            labelDate.Text = RTC.DayOfTheMonth.ToString("00") + "/" + RTC.Month.ToString("00") + "/" + RTC.Year.ToString("00");
            labelFPS.Text = "FPS: " + Kernel.Instance.FramesPerSecond.ToString("00");
        }
    }
}
