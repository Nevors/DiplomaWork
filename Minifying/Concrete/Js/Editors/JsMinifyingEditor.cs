using Minifying.Abstract;
using Minifying.Common;
using Minifying.Concrete.Common;
using Minifying.Concrete.Js.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Minifying.Concrete.Js.Models;

namespace Minifying.Concrete.Js.Editors {
    class JsMinifyingEditor : IEditor {
        private readonly bool isInternal;

        public JsMinifyingEditor(bool isInternal = true) {
            this.isInternal = isInternal;
        }
        public void ToEdit(IValueProvider valueProvider) {
            var jsFile = valueProvider.GetFiles(Entities.FileType.Js)
                .Where(i => !isInternal ^ i.IsInternal);

            var remover = new Js.Visitors.JsEofTokenRemover();
            var wsEditor = new Js.Visitors.JsWsSymbolsEditor();
            foreach (var item in jsFile) {
                var jso = new JsOptimizerFacade();
                var result = jso.ToOptimize(item.FileName, item.Tree.GetStream(), valueProvider.GetOutputMessages());
                if (result != null) {
                    item.Tree = new JsParser().ToParse(result);
                    remover.Remove(item.Tree);
                    wsEditor.Edit(item.Tree);
                }
            }
        }
    }
}
