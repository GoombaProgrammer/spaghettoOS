using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IL2CPU.API.Attribs;

namespace Cosmos.System.Plugs.System.Diagnostics {
    [Plug(Target = typeof(global::System.Diagnostics.Debug))]
    public class Debug {
        public static void WriteLine(string? message) {
            // do nothing cause idk how to use cosmos debugger and i dont really need it anyways
        }
    }
}
