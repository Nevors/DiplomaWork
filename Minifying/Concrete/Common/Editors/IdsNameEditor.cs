using Minifying.Abstract;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using CssV = Minifying.Concrete.Css.Visitors;
//using Js = Minifying.Concrete.Js.Visitors;
using HtmlV = Minifying.Concrete.Html.Visitors;

namespace Minifying.Concrete.Common.Editors
{
    class IdsNameEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var cssFiles = valueProvider.GetFiles(FileType.Css);
            var jsFiles = valueProvider.GetFiles(FileType.Js);
            var htmlFiles = valueProvider.GetFiles(FileType.Html);

            var freqList = new Dictionary<string, int>();

            foreach (var item in cssFiles) {
                new CssV.IdsSearcher().Search(item.Tree, freqList);
            }

            foreach (var item in htmlFiles) {
                new HtmlV.IdsSercher().Search(item.Tree, freqList);
            }
        }
    }
}
