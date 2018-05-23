using Minifying.Common;
using Minifying.Concrete.Common;
using Minifying.Concrete.Common.Editors;
using Minifying.Concrete.Html.Editors;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Minifying.Concrete.Js.Editors;
using System.Threading.Tasks;
using Minifying.Concrete.Css.Editors;

namespace Minifying {
    public static class Manager {
        public static Dictionary<string, Stream> ToMinimize(Dictionary<string, Stream> files, MinimizationType minimizations) {
            var valueProvider = new ValueProvider();
            Parallel.ForEach(files, item => {
                valueProvider.AddFile(ParseFile.ToParse(item.Key, item.Value));
            });

            HtmlInternalContentEditor replaceInternalCode = new HtmlInternalContentEditor();
            replaceInternalCode.ToEdit(valueProvider);

            JsEofTokenRemover jsEofTokenRemover = new JsEofTokenRemover();
            jsEofTokenRemover.ToEdit(valueProvider);

            NamesEditor idsNameEditor = new NamesEditor();
            //idsNameEditor.ToEdit(valueProvider);

            CssMinifyingEditor cssMinifyingEditor = new CssMinifyingEditor();
            //cssMinifyingEditor.ToEdit(valueProvider);

            JsWsSymbolsEditor jsWsSymbolEditor = new JsWsSymbolsEditor();
            jsWsSymbolEditor.ToEdit(valueProvider);

            HtmlWsSymbolsEditor htmlWsSymbolsEditor = new HtmlWsSymbolsEditor();
            htmlWsSymbolsEditor.ToEdit(valueProvider);

            HtmlCommentEditor htmlCommentEditor = new HtmlCommentEditor();
            htmlCommentEditor.ToEdit(valueProvider);

            JsMinifyingEditor jsMinifyingEditor = new JsMinifyingEditor();
            jsMinifyingEditor.ToEdit(valueProvider);

            var resultFiles = valueProvider.GetFiles().Where(f => !f.IsInternal);
            foreach (var file in resultFiles) {
                if (file.Tree != null) {
                    file.Stream = file.Tree.GetStream();
                }
            }

            var result = resultFiles.ToDictionary(k => k.FileName, v => v.Stream);

            return result;
        }
    }
}
