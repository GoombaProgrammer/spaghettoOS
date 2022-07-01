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
    public class PictureBox : FormElement {
        public override string ID { get; set; }
        public override Form Form { get; set; }

        public Point Position { get; set; }
        public Image Image { get; set; }
        public bool WithAlpha { get; set; } = false;

        public PictureBox(string id, Point position) : base(id) {
            this.Position = position;
        }

        public override void Render(Canvas cv, Form form) {
            if (Image == null) return;

            Point finalPos = Form.Position.Add(Position);

            if (WithAlpha) {
                cv.DrawImageAlpha(Image, finalPos.X, finalPos.Y);
            } else {
                cv.DrawImage(Image, finalPos.X, finalPos.Y);
            }
        }

        public override Rect GetBounds() {
            Point off = Form.Position.Add(Position);
            return new Rect() { X = off.X, Y = off.Y, Width = (int)Image.Width, Height = (int)Image.Height };
        }
    }
}
