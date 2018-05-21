using Minifying.Abstract;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using V = Minifying.Concrete.Html.Visitors;

namespace Minifying.Concrete.Html.Editors {
    class HtmlCommentEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var htmlFiles = valueProvider.GetFiles(FileType.Html);

            var visitor = new V.HtmlCommentEditor();
            Parallel.ForEach(htmlFiles, item => {
                visitor.Edit(item.Tree);
            });
        }
    }
}
