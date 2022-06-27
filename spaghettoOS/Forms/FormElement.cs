using Cosmos.System;
using Cosmos.System.Graphics;
using spaghettoOS.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaghettoOS.Forms {
    public abstract class FormElement {
        public abstract string ID { get; set; }
        public abstract Form Form { get; set; }
        public abstract void Render(Canvas cv, Form form);
        public Action<MouseState> OnMouseDown { get; set; } = (MouseState ms) => { }; // for handling in app code
        public Action<MouseState> OnMouseUp { get; set; } = (MouseState ms) => { };
        public Action<KeyEvent> OnKey { get; set; } = (KeyEvent ev) => { };

        public virtual void OnMouseDownHandler(MouseState state) { // for handling in element code
            OnMouseDown(state);
        }

        public virtual void OnMouseUpHandler(MouseState state) {
            OnMouseUp(state);
        }

        public virtual void OnKeyHandler(KeyEvent ev) {
            OnKey(ev);
        }

        public abstract Rect GetBounds();
    }

    public struct Rect {
        public int X, Y, Width, Height;
    }
}
