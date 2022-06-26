using spaghettoOS.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaghettoOS.Applications {
    public static class ProcessManager {
        /// <summary>
        ///     int: ProcessId<br></br>
        ///     Process: Process instance
        /// </summary>
        private static CustomDict<int, Process> processes = new();
        private static int pidCounter = 0;

        public static void StartProcess(Process process, params object[] startParams) {
            process.pid = pidCounter;
            process.app.OnStart(startParams);
            processes.Add(pidCounter, process);
            pidCounter++;
        }

        /// <summary>
        ///     Returns the process with the specified id
        /// </summary>
        /// <param name="pid">The process id to search for</param>
        public static Process FindProcess(int pid) {
            foreach(Process process in processes) {
                if (process.pid == pid) return process;
            }

            return null;
        }

        /// <summary>
        ///     Kills the process with the specified id
        /// </summary>
        /// <param name="pid">The process id to remove</param>
        /// <exception cref="Exception">Thrown if there is no process active with the specified process id</exception>
        public static void KillProcess(int pid) {
            int idx = processes.IndexOf(pid);
            if (idx == -1) throw new Exception("Process not found");

            processes[pid].Kill();
        }

        /// <summary>
        ///     <b>WARNING</b>: This should not be called from anything apart the process.
        ///     The process would probably no longer do anything, or might even get garbage
        ///     collected but there could be unexpected consequences!
        /// </summary>
        /// <param name="pid">The process id to remove</param>
        /// <exception cref="Exception">Thrown if there is no process active with the specified process id</exception>
        public static void RemoveProcessWithPid(int pid) {
            int idx = processes.IndexOf(pid);
            if (idx == -1) throw new Exception("Process not found");

            processes.RemoveAt(idx);
        }

        public static List<Process> GetProcesses() {
            return processes.AsValueList();
        }
    }
}
