using Cosmos.System;
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
    public class TextBox : FormElement {
        public override string ID { get; set; }
        public override Form Form { get; set; }

        public Point Position { get; set; }
        public Point Size { get; set; }

        public Color ForegroundColor { get => foregroundPen.Color; set => foregroundPen.Color = value; }
        private Pen foregroundPen = new Pen(Color.White, 1);
        public string Text { get; set; } = "New text box";
        public int FontSize { get; set; } = 16;

        public TextBox(Point position, Point size) {
            this.Position = position;
            this.Size = size;
        }

        public override void Render(Canvas cv, Form form) {
            string[] words = Text.Split(' ');
            List<string> lines = new();

            int lineWidth = 0;
            string currentLine = "";
            
            for(var i = 0; i < words.Length; i++) {
                int wordWidth = (words[i] + " ").GetTTFWidth("default", FontSize);
                
                if(lineWidth + wordWidth > Size.X) {
                    lines.Add(currentLine);
                    currentLine = words[i] + " ";
                    lineWidth = 0;
                }else {
                    lineWidth += wordWidth;
                    currentLine += words[i] + " ";
                }
            }

            int lineIdx = 0;
            foreach(string line in lines) {
                cv.DrawStringTTF(foregroundPen, line, "default", FontSize, Position.Add(form.Position).Add(new Point(0, lineIdx*FontSize)));
                lineIdx++;
            }
        }

        public override Rect GetBounds() {
            // Todo: add proper bounds calculation
            Point off = Form.Position.Add(Position);
            return new Rect() { X = off.X, Y = off.Y, Width = 1, Height = FontSize };
        }

        public override void OnKeyHandler(KeyEvent ev) {
            base.OnKeyHandler(ev);

            Text += ev.KeyChar;
        }
    }
}
