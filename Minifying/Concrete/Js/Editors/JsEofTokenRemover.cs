using Minifying.Abstract;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using V = Minifying.Concrete.Js.Visitors;
using static AntlrGrammars.Js.JsParser;
using Antlr4.Runtime;
using System.Threading.Tasks;

namespace Minifying.Concrete.Js.Editors {
    class JsEofTokenRemover : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var jsFiles = valueProvider.GetFiles(FileType.Js);

            var remover = new V.JsEofTokenRemover();
            Parallel.ForEach(jsFiles, item => {
                remover.Remove(item.Tree);
            });
        }
    }
}
