using Minifying.Abstract;
using Minifying.Concrete.Html.Visitors;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Html.Editors
{
    class ReplaceInternalCode : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var htmlFiles = valueProvider.GetFiles(FileType.Html);

            foreach (var item in htmlFiles) {
                new HtmlStyleTagEditor().Edit(item.Tree, valueProvider);
            }
            foreach (var item in htmlFiles) {
                new Test().Visit(item.Tree);
            }
        }
    }
}
