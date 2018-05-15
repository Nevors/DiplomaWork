using Minifying.Abstract;
using Minifying.Concrete.Common;
using Minifying.Concrete.Html.Visitors;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Html.Editors
{
    class InternalContentEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var htmlFiles = valueProvider.GetFiles(FileType.Html);

            foreach (var item in htmlFiles) {
                var pathProvider = new PathProvider(item.FileName);
                new HtmlStyleEditor().Edit(item.Tree, valueProvider, pathProvider);
                new HtmlJsEditor().Edit(item.Tree, valueProvider, pathProvider);
            }
        }
    }
}
