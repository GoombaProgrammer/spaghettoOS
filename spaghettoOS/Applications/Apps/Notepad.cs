using Cosmos.HAL;
using spaghettoOS.Forms;
using spaghettoOS.Forms.Elements;
using spaghettoOS.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Cosmos.System.Graphics;
using Point = Cosmos.System.Graphics.Point;
using spaghettoOS.Utils;

namespace spaghettoOS.Applications.Apps {
    public class Notepad : IApp {

        public string Name => "Notepad";

        private Form form;
        private TextBox textBox;

        public void OnStart(object[] args) {
            if(args.Length < 1) {
                Kernel.Instance.mDebugger.Send("No path provided!");
                // todo: show error alert
                return;
            }

            try {
                Kernel.Instance.mDebugger.Send("Starting notepad!!! yay!! Path: " + args[0]);
                form = new("NotepadApp", Color.White, new Point(100, 100), new Point(500, 500));
                form.TitleBarEnabled = true;
                form.BackgroundEnabled = true;

                Kernel.Instance.mDebugger.Send("notepad!!! 1!!");
                textBox = new("textBox", new Point(0, 0), new Point(500, 500));
                textBox.Text = File.ReadAllText((string)args[0]);
                textBox.ForegroundColor = Color.Black;
                textBox.Form = form;

                Kernel.Instance.mDebugger.Send("notepad!!! 2!!");
                form.formElements.Add(textBox);

                Kernel.Instance.mDebugger.Send("notepad!!! 3!!");
                WindowManager.Instance.RegisterForm(form);
            }catch(Exception e) {
                Kernel.Instance.mDebugger.Send("Starting notepad app failed! Exception: " + e.Message);
            }
        }

        public void Update() {
            
        }
    }
}
