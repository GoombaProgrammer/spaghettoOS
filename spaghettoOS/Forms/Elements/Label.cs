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
    public class Label : IFormElement {
        public string ID { get; set; }
        public Form Form { get; set; }

        public Point Position { get; set; }
        public Color ForegroundColor { get => foregroundPen.Color; set => foregroundPen.Color = value; }
        private Pen foregroundPen = new Pen(Color.White, 1);
        public string Text { get; set; } = "New label";
        public int FontSize { get; set; } = 16;

        public Label(Point position) {
            this.Position = position;
        }

        public void Render(Canvas cv, Form form) {
            cv.DrawStringTTF(foregroundPen, Text, "default", FontSize, Form.Position.Add(Position).Add(new Point(0, FontSize)));
        }
    }
}
