using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaghettoOS.Applications {
    public class Process {
        public IApp app;
        public int pid;

        public void Kill() {
            app.OnExit();
            ProcessManager.RemoveProcessWithPid(pid);
        }

        public Process(IApp app) {
            this.app = app;
        }
    }
}
