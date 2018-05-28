using Minifying.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Minifying.Concrete.Html.Visitors;
namespace Minifying.Concrete.Html.Editors
{
    class HtmlLoadExtJsEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var htmlFiles = valueProvider.GetFiles(Entities.FileType.Html);
            var editor = new HtmlJsLoader();
            foreach (var item in htmlFiles) {
                editor.Replace(item.Tree, valueProvider);
            }

            var jsFiles = valueProvider.GetFiles(Entities.FileType.Js);
            foreach (var item in jsFiles) {
                if (item.IsExternal) {
                    valueProvider.RemoveFile(item.SearchName);
                }
            }
        }
    }
}
