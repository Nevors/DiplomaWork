using Minifying.Abstract;
using Minifying.Concrete.Html.Models;
using Minifying.Concrete.Html.Visitors;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO = System.IO;
using static AntlrGrammars.Css.CssParser;
using Minifying.Common;

namespace Minifying.Concrete.Html.Editors
{
    class HtmlUnionCssEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var htmlFiles = valueProvider.GetFiles(Entities.FileType.Html);
            List<FileContext> files = new List<FileContext>();

            var searcher = new HtmlStyleFilesSearcher();

            foreach (var item in htmlFiles) {
                var list = searcher.Search(item.Tree);
                files.AddRange(list);
            }

            var uniqFiles = files.Distinct();

            File commonFile = new File();
            commonFile.FileName = IO.Path.GetRandomFileName() + ".css";
            commonFile.SearchName = commonFile.FileName;
            commonFile.Type = FileType.Css;

            StylesheetContext context = new StylesheetContext(null, 0);
            foreach (var item in uniqFiles) {
                var style = item.File.Tree as StylesheetContext;
                var nesteds = style.nestedStatement();
                foreach (var nested in nesteds) {
                    nested.parent = context;
                    context.AddChild(nested);
                }
                valueProvider.RemoveFile(item.File.FileName);
            }

            commonFile.Tree = context;

            valueProvider.AddFile(commonFile);

            HtmlAddStyleEditor htmlAddStyle = new HtmlAddStyleEditor();

            foreach (var item in htmlFiles) {
                htmlAddStyle.Add(item.Tree, new File[] { commonFile });
            }

            foreach (var item in files) {
                item.Context.Remove();
            }
        }
    }
}
