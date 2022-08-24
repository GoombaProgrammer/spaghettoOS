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
    public class PhotoViewer : IApp {

        public string Name => "PhotoViewer";

        private Form form;
        private TextBox textBox;

        public void OnStart(object[] args) {
            if(args.Length < 1) {
                Kernel.Instance.mDebugger.Send("No path provided!");
                // todo: show error alert
                return;
            }

            try {
                form = new("PhotoViewer", Color.White, new Point(100, 100), new Point(500, 500));
                form.TitleBarEnabled = true;
                form.BackgroundEnabled = true;

                PictureBox newPictureBox = new("", new Point(0, 0));
                newPictureBox.Image = Resources.ResourceManager.GenericIcon;
                newPictureBox.Form = form;

                form.formElements.Add(textBox);

                WindowManager.Instance.RegisterForm(form);
            }catch(Exception e) {
                Kernel.Instance.mDebugger.Send("Starting notepad app failed! Exception: " + e.Message);
            }
        }

        public void Update() {
            
        }
    }
}
