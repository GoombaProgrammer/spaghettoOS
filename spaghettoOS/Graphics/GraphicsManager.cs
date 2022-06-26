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

namespace spaghettoOS.Graphics {
    public class GraphicsManager {
        public static GraphicsManager Instance { get => _instance; }
        private static GraphicsManager _instance;

        public Canvas cv;
        public List<Form> registeredForms;

        public GraphicsManager() {
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

            foreach (Form form in registeredForms) {
                form.Render(cv);
                Cosmos.Core.Memory.Heap.Collect();
            }

            cv.DrawImageAlpha(ResourceManager.CursorImage, (int)MouseManager.X, (int)MouseManager.Y);
            cv.Display();
        }

        public void RegisterForm(Form form) {
            this.registeredForms.Add(form);
        }

        private void SetInstance(GraphicsManager instance) {
            _instance = instance;
        }
    }
}
