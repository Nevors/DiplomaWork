using JsOptimizer;
using Minifying.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Minifying.Concrete.Js.Editors
{
    class JsMinifyingEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var jsFile = valueProvider.GetFiles(Entities.FileType.Js);
            foreach (var item in jsFile) {
                var jso = new Jso();
                var newText = jso.ToOptimize(item.Tree.GetText());

                var ms = new MemoryStream();
                var sw = new StreamWriter(ms);
                sw.Write(newText);
                sw.Flush();
                ms.Position = 0;

            }
        }
    }
}
