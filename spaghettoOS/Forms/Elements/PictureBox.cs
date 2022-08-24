using Cosmos.System.Graphics;
using CosmosTTF;
using spaghettoOS.Utils;
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
        public Point Size { get; set; }

        public Image Image { get; set; }
        public bool WithAlpha { get; set; } = false;

        private string cacheIdentifier = "";
        private int[] resizeCache;

        public PictureBox(string id, Point position) : base(id) {
            this.Position = position;
        }

        public override void Render(Canvas cv, Form form) {
            if (Image == null) return;

            Point finalPos = Form.Position.Add(Position);

            if (Size.X <= 0 || Size.Y <= 0) Size = new((int)Image.Width, (int)Image.Height);

            if ((Size.X != Image.Width && Size.Y != Image.Height) && cacheIdentifier != Size.X.ToString() + Size.Y.ToString()) {
                cacheIdentifier = Size.X.ToString() + Size.Y.ToString();
                resizeCache = Utils.Utils.ScaleImage(Image, Size.X, Size.Y);
            }

            if (WithAlpha) {
                if (Size.X == Image.Width && Size.Y == Image.Height) {
                    cv.DrawImageAlpha(Image, Position);
                } else {
                    cv.DrawImageFromIntArr(resizeCache, finalPos.X, finalPos.Y, Size.X, Size.Y);
                }
                cv.DrawImageFromIntArr(resizeCache, finalPos.X, finalPos.Y, Size.X, Size.Y);
            } else {
                if (Size.X == Image.Width && Size.Y == Image.Height) {
                    cv.DrawImage(Image, Position);
                }else {
                    cv.DrawImageFromIntArr(resizeCache, finalPos.X, finalPos.Y, Size.X, Size.Y);
                }
            }
        }

        public override Rect GetBounds() {
            Point off = Form.Position.Add(Position);
            return new Rect() { X = off.X, Y = off.Y, Width = (int)Image.Width, Height = (int)Image.Height };
        }
    }
}
