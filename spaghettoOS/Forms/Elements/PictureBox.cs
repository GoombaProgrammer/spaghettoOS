using Cosmos.System.Graphics;
using CosmosTTF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Cosmos.System.Graphics.Point;

namespace spaghettoOS.Forms.Elements {
    public class PictureBox : IFormElement {
        public string ID { get; set; }
        public Form Form { get; set; }

        public Point Position { get; set; }
        public Image Image { get; set; }
        public bool WithAlpha { get; set; } = false;

        public PictureBox(Point position) {
            this.Position = position;
        }

        public void Render(Canvas cv, Form form) {
            if (Image == null) return;

            Point finalPos = Form.Position.Add(Position);

            if (WithAlpha) {
                cv.DrawImageAlpha(Image, finalPos.X, finalPos.Y);
            } else {
                cv.DrawImage(Image, finalPos.X, finalPos.Y);
            }
        }
    }
}
