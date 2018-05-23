using Minifying.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using ChakraCore.NET;
using ChakraCore.NET.Hosting;
using CssOptimizer;
using Minifying.Concrete.Css.Parsers;
using System.IO;
using System.Threading.Tasks;

namespace Minifying.Concrete.Css.Editors {
    class CssMinifyingEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var cssFiles = valueProvider.GetFiles(Entities.FileType.Css);
            CssParser cssParser = new CssParser();

            Parallel.ForEach(cssFiles, item => {
                var csso = new Csso();
                var newText = csso.Optimize(item.Tree.GetText());

                var ms = new MemoryStream();
                var sw = new StreamWriter(ms);
                sw.Write(newText);
                sw.Flush();
                ms.Position = 0;

                item.Tree = cssParser.ToParse(ms);
            });
        }
    }
}
