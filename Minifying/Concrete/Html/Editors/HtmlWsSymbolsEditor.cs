using Minifying.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Minifying.Entities;
using V = Minifying.Concrete.Html.Visitors;
using System.Threading.Tasks;

namespace Minifying.Concrete.Html.Editors {
    class HtmlWsSymbolsEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var htmlFiles = valueProvider.GetFiles(FileType.Html);

            var htmlWsSymbols = new V.HtmlWsSymbolsEditor();
            Parallel.ForEach(htmlFiles, item => {
                htmlWsSymbols.Edit(item.Tree);
            });
        }
    }
}
