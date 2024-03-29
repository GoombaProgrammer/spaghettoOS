﻿using System;
using Sys = Cosmos.System;
using spaghettoOS.Graphics;
using Cosmos.System.Graphics;
using System.Drawing;
using Point = Cosmos.System.Graphics.Point;
using Cosmos.HAL;
using spaghettoOS.Applications;
using spaghettoOS.Applications.Apps;
using spaghettoOS.Resources;
using Cosmos.System;
using Console = System.Console;
using System.IO;

namespace spaghettoOS {
    public class Kernel : Sys.Kernel {
        public static Kernel Instance { get; set; }
        public Sys.FileSystem.CosmosVFS fs;

        WindowManager graphics;

        // fps stuff
        int frameCounter = 0;
        int previousSecond = -1;
        public int FramesPerSecond { get; set; } = 0;  

        protected override void BeforeRun() {
            try {
                Instance = this;

                Console.WriteLine("Initiliaze resources...");
                ResourceManager.Init();
                Console.WriteLine("Done!");

                MouseManager.ScreenWidth = 1280;
                MouseManager.ScreenHeight = 720;

                Console.WriteLine("Initiliaze file system...");

                fs = new Sys.FileSystem.CosmosVFS();
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

                if(!Directory.Exists("0:\\System")) {
                    Directory.CreateDirectory("0:\\System");
                }

                Console.WriteLine("Done!");

                Console.WriteLine("Initiliaze graphics engine...");
                graphics = new();
                graphics.Initiliaze();
                Console.WriteLine("Done!");

                KeyboardManager.SetKeyLayout(new Sys.ScanMaps.DE_Standard());

            } catch (Exception ex) {
                mDebugger.Send(ex.Message);
                Panic.KernelPanic("KernelBeforeRun", ex);
            }
        }

        protected override void Run() {
            try {
                ProcessManager.StartProcess(new Process(new DesktopApp()));
                ProcessManager.StartProcess(new Process(new TaskbarApp()));

                /*if (!File.Exists("0:\\System\\BasicConfig.cfg")) {
                    ProcessManager.StartProcess(new Process(new FirstTimeSetup()));
                }*/

                while (true) {
                    Cosmos.Core.Memory.Heap.Collect();
                    foreach (Process proc in ProcessManager.GetProcesses()) {
                        proc.app.Update();
                    }

                    graphics.RenderFrame();
                    frameCounter++;

                    if(previousSecond != RTC.Second) {
                        previousSecond = RTC.Second;
                        FramesPerSecond = frameCounter;
                        frameCounter = 0;
                    }
                }
            }catch(Exception ex) {
                mDebugger.Send(ex.Message);
                Panic.KernelPanic("GENERIC_RunningKernel", ex);
            }
        }

        const long secondsInMinute = 60;
        const long secondsInHour = secondsInMinute * 60;

        /// <summary>
        /// only returning current day offset
        /// </summary>
        /// <returns></returns>
        public static long DayOffset() {
            return RTC.Second + RTC.Minute * secondsInMinute + RTC.Hour * secondsInHour;
        }

        public static void DebugUIPrint(string txt, int offY = 0) {
            WindowManager.Instance.cv.DrawFilledRectangle(new Pen(Color.Black), new Point(0, offY), 1000, 16);
            WindowManager.Instance.cv.DrawString(txt, Sys.Graphics.Fonts.PCScreenFont.Default, new Pen(Color.White, 2), new Point(16, offY));
            WindowManager.Instance.cv.Display();
        }
    }
}
