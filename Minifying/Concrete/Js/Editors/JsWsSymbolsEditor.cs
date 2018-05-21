using Minifying.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using V = Minifying.Concrete.Js.Visitors;
namespace Minifying.Concrete.Js.Editors {
    class JsWsSymbolsEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var jsFiles = valueProvider.GetFiles(Entities.FileType.Js);

            var jsWsEditor = new V.JsWsSymbolsEditor();
            Parallel.ForEach(jsFiles, item => {
                jsWsEditor.Edit(item.Tree);
            });
        }
    }
}
