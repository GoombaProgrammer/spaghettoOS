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

        public TextBox(string id, Point position, Point size) : base(id) {
            this.Position = position;
            this.Size = size;
        }

        public override void Render(Canvas cv, Form form) {
            string[] lines = WordWrap(Text, Size.X).Split(Environment.NewLine);

            int i = 0;
            foreach(string line in lines) {
                cv.DrawStringTTF(foregroundPen, line, "default", FontSize, Position.Add(form.Position).Add(new Point(0, (2+i) * FontSize)));
                i++;
            }
        }

        public override Rect GetBounds() {
            // Todo: add proper bounds calculation
            Point off = Form.Position.Add(Position);
            return new Rect() { X = off.X, Y = off.Y, Width = Size.X, Height = Size.Y };
        }

        public override void OnKeyHandler(KeyEvent ev) {
            base.OnKeyHandler(ev);

            Kernel.Instance.mDebugger.Send("Adding key " + (int)ev.KeyChar);
            if (ev.Key != ConsoleKeyEx.Enter && (ev.KeyChar < 32 || (ev.KeyChar >= 127 && ev.KeyChar < 160))) return; // Do not accept unicode control chars
            Text += (ev.Key == ConsoleKeyEx.Enter ? Environment.NewLine : ev.KeyChar);
        }

        // Improved WordWrap algorithm by https://stackoverflow.com/a/17635/11877986
        static char[] splitChars = new char[] { ' ', '-', '\t' };

        private string WordWrap(string str, int width) {
            string[] words = Explode(str, splitChars);

            int curLineLength = 0;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < words.Length; i += 1) {
                string word = words[i];
                // If adding the new word to the current line would be too long,
                // then put it on a new line (and split it up if it's too long).
                if (curLineLength + word.GetTTFWidth("default", FontSize) > width) {
                    // Only move down to a new line if we have text on the current line.
                    // Avoids situation where wrapped whitespace causes emptylines in text.
                    if (curLineLength > 0) {
                        strBuilder.Append(Environment.NewLine);
                        curLineLength = 0;
                    }

                    // If the current word is too long to fit on a line even on it's own then
                    // split the word up.
                    while (word.Length > width) {
                        strBuilder.Append(word.Substring(0, width - 1) + "-");
                        word = word.Substring(width - 1);

                        strBuilder.Append(Environment.NewLine);
                    }

                    // Remove leading whitespace from the word so the new line starts flush to the left.
                    word = word.TrimStart();
                }
                strBuilder.Append(word);
                curLineLength += word.GetTTFWidth("default", FontSize);
            }

            return strBuilder.ToString();
        }

        private string[] Explode(string str, char[] splitChars) {
            List<string> parts = new List<string>();
            int startIndex = 0;
            while (true) {
                int index = str.IndexOfAny(splitChars, startIndex);

                if (index == -1) {
                    parts.Add(str.Substring(startIndex));
                    return parts.ToArray();
                }

                string word = str.Substring(startIndex, index - startIndex);
                char nextChar = str.Substring(index, 1)[0];
                // Dashes and the likes should stick to the word occuring before it. Whitespace doesn't have to.
                if (char.IsWhiteSpace(nextChar)) {
                    parts.Add(word);
                    parts.Add(nextChar.ToString());
                } else {
                    parts.Add(word + nextChar);
                }

                startIndex = index + 1;
            }
        }
    }
}
