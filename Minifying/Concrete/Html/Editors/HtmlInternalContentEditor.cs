using Minifying.Abstract;
using Minifying.Concrete.Common;
using Minifying.Concrete.Html.Visitors;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Html.Editors
{
    class HtmlInternalContentEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var htmlFiles = valueProvider.GetFiles(FileType.Html);

            HtmlStyleEditor htmlStyleEditor = new HtmlStyleEditor();
            HtmlJsEditor htmlJsEditor = new HtmlJsEditor();
            foreach (var item in htmlFiles) {
                var pathProvider = new PathProvider(item.FileName);
                htmlStyleEditor.Edit(item.Tree, valueProvider, pathProvider);
                htmlJsEditor.Edit(item.Tree, valueProvider, pathProvider);
            }
        }
    }
}
