using Cosmos.System.Graphics;
using spaghettoOS.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunarLabs.Fonts;
using spaghettoOS.Resources;
using Point = Cosmos.System.Graphics.Point;
using Cosmos.System.Graphics.Fonts;
using Font = LunarLabs.Fonts.Font;
using CosmosTTF;
using Cosmos.System;
using Console = System.Console;
using spaghettoOS.Utils;

namespace spaghettoOS.Graphics {
    public class WindowManager {
        public static WindowManager Instance { get => _instance; }
        private static WindowManager _instance;

        public FormElement focusedElement;

        public Canvas cv;
        public List<Form> registeredForms;

        public WindowManager() {
            registeredForms = new();
        }

        public void Initiliaze() {
            try {
                Console.WriteLine("[GraphicsManager] Setting instance");
                SetInstance(this);

                Console.WriteLine("[GraphicsManager] Adding Inconsolata to font registry");
                TTFManager.RegisterFont("default", ResourceManager.defaultFont);

                Console.WriteLine("[GraphicsManager] Get Canvas");
                this.cv = FullScreenCanvas.GetFullScreenCanvas(new Mode(1280, 720, ColorDepth.ColorDepth32));
            }catch(Exception ex) {
                Console.WriteLine("[GraphicsManager] Failure: " + ex.Message);
                Panic.KernelPanic("Loading Graphics Manager", ex);
            }
        }

        public void RenderFrame() {
            cv.Clear(Color.Black);

            HandleInputs();
            foreach (Form form in registeredForms) {
                form.Render(cv);
                Cosmos.Core.Memory.Heap.Collect();
            }

            cv.DrawImageAlpha(ResourceManager.CursorImage, (int)MouseManager.X, (int)MouseManager.Y);
            cv.Display();
        }

        public FormElement FindElementAt(int x, int y) {
            FormElement currentElement = null;

            foreach(Form form in registeredForms) {
                foreach(FormElement el in form.formElements) {
                    if ((x, y).IsInBounds(el.GetBounds())) currentElement = el;
                }
            }

            return currentElement;
        }

        private CustomDict<int, bool> mbDown = new() {
            { (int)MouseState.Left, false },
            { (int)MouseState.Right, false },
            { (int)MouseState.Middle, false },
        };

        public void OnMouseButtonDown(MouseState mb) {
            var el = FindElementAt((int)MouseManager.X, (int)MouseManager.Y);

            if (el != null) el.OnMouseDownHandler(mb);
            focusedElement = el;
        }

        public void OnMouseButtonUp(MouseState mb) {
            var el = FindElementAt((int)MouseManager.X, (int)MouseManager.Y);

            if (el != null) el.OnMouseUpHandler(mb);
            focusedElement = el;
        }

        public void HandleInputs() {
            if(KeyboardManager.KeyAvailable) {
                KeyboardManager.TryReadKey(out KeyEvent key);
                if (focusedElement != null) focusedElement.OnKeyHandler(key);
            }

            foreach(MouseState ms in mbDown.AsKeyList()) {
                if (MouseManager.MouseState == ms) {
                    if (!mbDown[(int)ms]) {
                        mbDown[(int)ms] = true;
                        OnMouseButtonDown(ms);
                    }
                } else {
                    if (mbDown[(int)ms]) {
                        mbDown[(int)ms] = false;
                        OnMouseButtonUp(ms);
                    }
                }
            }
        }

        public void RegisterForm(Form form) {
            this.registeredForms.Add(form);
        }

        private void SetInstance(WindowManager instance) {
            _instance = instance;
        }
    }

    public enum MouseButton {
        Left, Right, Middle
    }
}
