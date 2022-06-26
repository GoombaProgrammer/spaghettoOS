﻿using Cosmos.Debug.Kernel;
using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using spaghettoOS.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = System.Console;
using Point = Cosmos.System.Graphics.Point;

namespace spaghettoOS {
    public class Panic {
        public static void KernelPanic(string currentAction, Exception ex) {
            try {
                GraphicsManager.Instance.cv.Clear(Color.Red);
                GraphicsManager.Instance.cv.DrawString("***** KERNEL PANIC *****", PCScreenFont.Default, new Pen(Color.White), new Point(0, 0));
                GraphicsManager.Instance.cv.DrawString("Current action: " + currentAction, PCScreenFont.Default, new Pen(Color.White), new Point(0, 16));
                GraphicsManager.Instance.cv.DrawString("Exception message: " + ex.Message, PCScreenFont.Default, new Pen(Color.White), new Point(0, 32));
                GraphicsManager.Instance.cv.DrawString("Press any key to shutdown SpaghettoOS", PCScreenFont.Default, new Pen(Color.White), new Point(0, 64));

                GraphicsManager.Instance.cv.Display();

                Console.ReadKey();
                Power.Shutdown();
            }catch(Exception ex2) {
                Kernel.Instance.mDebugger.SendMessageBox("Panic ironically caused an exception: " + ex2.Message + " | Panic caused by exception: " + ex.Message);
            }
        }
    }
}
