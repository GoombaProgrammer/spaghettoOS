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

        public CustomDict<string, Image> icons = new();

        public void OnStart(params object[] args) {
            form = new("NotepadApp", Color.White, new Point(100, 100), new Point(500, 500));
            form.TitleBarEnabled = true;
            form.BackgroundEnabled = true;

            textBox = new(new Point(0, 0), new Point(500, 500));
            textBox.Text = File.ReadAllText((string)args[0]);
            textBox.Form = form;

            form.formElements.Add(textBox);

            WindowManager.Instance.RegisterForm(form);
        }

        public void Update() {
            
        }
    }
}
