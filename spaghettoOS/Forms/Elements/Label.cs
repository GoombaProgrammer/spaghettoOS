using Cosmos.System.Graphics;
using CosmosTTF;
using spaghettoOS.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Cosmos.System.Graphics.Point;

namespace spaghettoOS.Forms.Elements {
    public class Label : FormElement {
        public override string ID { get; set; }
        public override Form Form { get; set; }
        
        public Point Position { get; set; }
        public Color ForegroundColor { get => foregroundPen.Color; set => foregroundPen.Color = value; }
        private Pen foregroundPen = new Pen(Color.White, 1);
        public string Text { get; set; } = "New label";
        public int FontSize { get; set; } = 16;

        public Label(string id, Point position) : base(id) {
            this.Position = position;
        }

        public override void Render(Canvas cv, Form form) {
            cv.DrawStringTTF(foregroundPen, Text, "default", FontSize, Form.Position.Add(Position).Add(new Point(0, FontSize)));
        }

        public override Rect GetBounds() {
            // Todo: add proper bounds calculation
            Point off = Form.Position.Add(Position);
            return new Rect() { X = off.X, Y = off.Y, Width = 1, Height = FontSize };
        }
    }
}
