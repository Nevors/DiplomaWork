using Minifying.Abstract;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using V = Minifying.Concrete.Js.Visitors;
namespace Minifying.Concrete.Js.Editors
{
    class JsEofTokenRemover : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var jsFiles = valueProvider.GetFiles(FileType.Js);

            var remover = new V.JsEofTokenRemover();
            foreach (var item in jsFiles) {
                remover.Remove(item.Tree);
            }
        }
    }
}
