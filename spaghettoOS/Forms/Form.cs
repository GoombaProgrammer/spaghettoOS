using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using CosmosTTF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Cosmos.System.Graphics.Point;

namespace spaghettoOS.Forms {
    public class Form {
        public string Title { get; set; } = "Untitled Spaghetto Form";
        public Color BackgroundColor { get; set; } = Color.Gray;
        public Point Position { get; set; } = new Point(0, 0);
        public Point Size { get; set; } = new Point(100, 100);
        public bool TitleBarEnabled { get; set; } = true;
        public bool BackgroundEnabled { get; set; } = true;

        public List<FormElement> formElements = new();

        private Pen _titleBarPen = new Pen(Color.Gray, 1);
        private Pen _titleBarTextPen = new Pen(Color.Black, 1);
        private Pen _generalPurposePen = new Pen(Color.Black, 1);

        public Form(string title) {
            Title = title;
        }
        public Form(string title, Color backgroundColor) {
            Title = title;
            BackgroundColor = backgroundColor;
        }

        public Form(string title, Color backgroundColor, Point position, Point size) {
            Title = title;
            BackgroundColor = backgroundColor;
            Position = position;
            Size = size;
        }

        public void Render(Canvas cv) {
            if (TitleBarEnabled) {
                cv.DrawFilledRectangle(_titleBarPen, this.Position, this.Size.X, 20);
                cv.DrawStringTTF(_titleBarTextPen, Title, "default", 16, new Point(this.Position.X, this.Position.Y + 10));
            }

            if (BackgroundEnabled) {
                _generalPurposePen.Color = BackgroundColor;
                cv.DrawFilledRectangle(_generalPurposePen, this.Position.X, this.Position.Y + (TitleBarEnabled ? 20 : 0), this.Size.X, this.Size.Y);
            }

            foreach(FormElement element in formElements) {
                element.Render(cv, this);
            }
        }
    }
}
