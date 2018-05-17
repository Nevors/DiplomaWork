using Minifying.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using V = Minifying.Concrete.Js.Visitors;
namespace Minifying.Concrete.Js.Editors
{
    class JsWsSymbolEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var jsFiles = valueProvider.GetFiles();

            var jsWsEditor = new V.JsWsSymbolsEditor();
            foreach (var item in jsFiles) {
                jsWsEditor.Edit(item.Tree);
            }
        }
    }
}
