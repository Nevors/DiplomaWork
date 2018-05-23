using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Jint;

namespace CssOptimizer {
    public class Csso {
        Engine engine;
        public Csso() {
            engine = new Engine();
            var text = Properties.Resources.ResourceManager.GetString("csso");
            engine.Execute(text);
        }

        public Stream ToOptimize(Stream stream) {
            var text = new StreamReader(stream).ReadToEnd();
            var result =  engine
                .SetValue("text", text)
                .Execute("csso.minify(text).css;")
                .GetCompletionValue()
                .AsString();

            var ms = new MemoryStream(result.Length);
            var sw = new StreamWriter(ms);
            sw.Write(result);
            sw.Flush();
            ms.Position = 0;

            return ms;
        }
    }
}
