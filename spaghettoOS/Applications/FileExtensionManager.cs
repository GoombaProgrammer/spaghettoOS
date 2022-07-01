using spaghettoOS.Applications.Apps;
using spaghettoOS.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaghettoOS.Applications {
    public class FileExtensionManager {
        public static Dictionary<string, Action<string>> Extensions = new() {
            {".txt", (string path) => 
                {
                    Kernel.Instance.mDebugger.Send("Launching notepad from txt ext");
                    ProcessManager.StartProcess(new Process(new Notepad()), new object[] { path }); 
                }
            }
        };

        public static void TryLaunch(string path) {
            string ext = Path.GetExtension(path);
            Kernel.Instance.mDebugger.Send("ext: " + ext);

            if (Extensions.ContainsKey(ext)) {
                Kernel.Instance.mDebugger.Send("Extensions Dict has extension, getting it...");
                var ExtMethod = Extensions[ext];
                Kernel.Instance.mDebugger.Send("Got it! Trying to run method!");
                ExtMethod(path);
            }
        }
    }
}
