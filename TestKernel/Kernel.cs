using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using spaghetto;
using Cosmos.System.ScanMaps;
using System.Collections;
using System.Linq;

namespace TestKernel {
    public class Kernel : Sys.Kernel {

        protected override void BeforeRun() {

        }

        protected override void Run() {
            Console.WriteLine("Setting keyboard scan map to DE_Standard");
            SetKeyboardScanMap(new DE_Standard());

            Console.Clear();

            while (true) {
                Console.Write("spaghetto > ");
                string text = Console.ReadLine();

                if (text.Trim() == String.Empty) return;

                try {
                    (RuntimeResult res, SpaghettoException err) = Intepreter.Run("<spaghetto_cli>", text);

                    if (err != null) throw err;
                    if (res.error != null) throw res.error;

                    if (res.value != null) {
                        Console.WriteLine(((res.value as ListValue).value.Count == 1 ? ((res.value as ListValue).value[0]?.Represent()) : res.value.Represent()));
                    } else {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        //                          Console.WriteLine("\x001B[3mNothing was returned");
                        Console.ResetColor();
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    if (ex.Data != null) Console.WriteLine(DictToString(ex.Data, " = ", "\n"));
                }
            }
        }

        public string DictToString(IDictionary source, string keyValueSeparator,
                                                       string sequenceSeparator) {
            string o = "";

            foreach (DictionaryEntry entry in source) {
                o += entry.Key.ToString() + keyValueSeparator + entry.Value.ToString() + sequenceSeparator;
            }

            return o;
        }
    }
}
