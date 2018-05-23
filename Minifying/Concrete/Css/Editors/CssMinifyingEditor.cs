using Minifying.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using CssOptimizer;
using Minifying.Concrete.Css.Parsers;
using System.IO;
using System.Threading.Tasks;
using Minifying.Common;
using System.Linq;

namespace Minifying.Concrete.Css.Editors {
    class CssMinifyingEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var cssFiles = valueProvider.GetFiles(Entities.FileType.Css)
                .Where(i => i.IsInternal);
            CssParser cssParser = new CssParser();

            Parallel.ForEach(cssFiles, item => {
                var csso = new Csso();
                var stream = csso.ToOptimize(item.Tree.GetStream());

                item.Tree = cssParser.ToParse(stream);
            });
        }
    }
}
