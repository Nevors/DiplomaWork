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

        public string Optimize(string text) {
            return engine
                .SetValue("text", text)
                .Execute("csso.minify(text).css;")
                .GetCompletionValue()
                .AsString();
        }
    }
}
