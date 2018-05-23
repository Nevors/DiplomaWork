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
            var fileIdentifying = new FileIdentifying();
            var parseFile = new ParseFile();

            var csso = new CssOptimizer.Csso();
            var jso = new JsOptimizer.Jso();

            //Parallel.ForEach(files, item => {
            foreach (var item in files) {
                var type = fileIdentifying.GetType(item.Key, item.Value);
                Stream stream;
                switch (type) {
                    case FileType.Css:
                        stream = csso.ToOptimize(item.Value);
                        break;
                    case FileType.Js:
                        stream = jso.ToOptimize(item.Value);
                        break;
                    default:
                        stream = item.Value;
                        break;
                }

                valueProvider.AddFile(parseFile.ToParse(item.Key, stream, type));
            }
           // });
           

            HtmlInternalContentEditor replaceInternalCode = new HtmlInternalContentEditor();
            replaceInternalCode.ToEdit(valueProvider);

            NamesEditor idsNameEditor = new NamesEditor();
            //idsNameEditor.ToEdit(valueProvider);

            JsWsSymbolsEditor jsWsSymbolEditor = new JsWsSymbolsEditor();
            jsWsSymbolEditor.ToEdit(valueProvider);

            JsEofTokenRemover jsEofTokenRemover = new JsEofTokenRemover();
            jsEofTokenRemover.ToEdit(valueProvider);

            JsMinifyingEditor jsMinifying = new JsMinifyingEditor();
            jsMinifying.ToEdit(valueProvider);

            HtmlWsSymbolsEditor htmlWsSymbolsEditor = new HtmlWsSymbolsEditor();
            htmlWsSymbolsEditor.ToEdit(valueProvider);

            HtmlCommentEditor htmlCommentEditor = new HtmlCommentEditor();
            htmlCommentEditor.ToEdit(valueProvider);

            CssMinifyingEditor cssMinifying = new CssMinifyingEditor();
            cssMinifying.ToEdit(valueProvider);
            
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
