using JsOptimizer;
using Minifying.Abstract;
using Minifying.Common;
using Minifying.Concrete.Js.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Minifying.Concrete.Js.Editors
{
    class JsMinifyingEditor : IEditor {
        private readonly bool isInternal;

        public JsMinifyingEditor(bool isInternal = true) {
            this.isInternal = isInternal;
        }
        public void ToEdit(IValueProvider valueProvider) {
            var jsFile = valueProvider.GetFiles(Entities.FileType.Js)
                .Where(i=> !isInternal ^ i.IsInternal);

            var remover = new Js.Visitors.JsEofTokenRemover();
            var wsEditor = new Js.Visitors.JsWsSymbolsEditor();
            foreach (var item in jsFile) {
                var jso = new Jso();
                var stream = jso.ToOptimize(item.Tree.GetStream());

                item.Tree = new JsParser().ToParse(stream);
                remover.Remove(item.Tree);
                wsEditor.Edit(item.Tree);
            }
        }
    }
}
