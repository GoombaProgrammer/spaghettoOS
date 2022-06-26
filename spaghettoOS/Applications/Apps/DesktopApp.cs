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
    public class DesktopApp : IApp {

        public string Name => "DesktopApp";

        private Form form;
        private PictureBox pictureBox;

        public CustomDict<string, Image> icons = new();

        public void OnStart(params object[] args) {
            if(!Directory.Exists("0:\\System\\Desktop")) {
                Directory.CreateDirectory("0:\\System\\Desktop");

                File.WriteAllText("0:\\System\\Desktop\\test0.txt", "");
                File.WriteAllText("0:\\System\\Desktop\\test1.txt", "");
                File.WriteAllText("0:\\System\\Desktop\\test2.txt", "");
                File.WriteAllText("0:\\System\\Desktop\\test3.txt", "");
                File.WriteAllText("0:\\System\\Desktop\\test4.txt", "");
                File.WriteAllText("0:\\System\\Desktop\\test5.txt", "");

            }

            form = new("DesktopApp", Color.Black, new Point(0, 0), new Point(1280, 720));
            form.TitleBarEnabled = false;
            form.BackgroundEnabled = false;

            pictureBox = new(new Point(0, 0));
            pictureBox.Image = Resources.ResourceManager.BackgroundImage;
            pictureBox.Form = form;

            form.formElements.Add(pictureBox);

            string[] files = Directory.GetFiles("0:\\System\\Desktop");

            int offX = 0, offY = 0;

            foreach (string file in files) {
                string fileName = Path.GetFileName(file);

                PictureBox newPictureBox = new(new Point(offX, offY));
                newPictureBox.Image = Resources.ResourceManager.GenericIcon;
                newPictureBox.WithAlpha = true;
                newPictureBox.Form = form;

                Label newLabel = new(new Point(offX, offY + 48));
                newLabel.Text = fileName;
                newLabel.FontSize = 12;
                newLabel.Form = form;

                form.formElements.Add(newPictureBox);
                form.formElements.Add(newLabel);

                offY += 64;

                if (offY > 680) {
                    offY = 0;
                    offX += 64;
                }
            }

            GraphicsManager.Instance.RegisterForm(form);
        }

        public void Update() {
            
        }
    }
}
