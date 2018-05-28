using Minifying.Abstract;
using Minifying.Concrete.Html.Models;
using Minifying.Concrete.Html.Visitors;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO = System.IO;
using static AntlrGrammars.Js.JsParser;
using Minifying.Common;

namespace Minifying.Concrete.Html.Editors {
    class HtmlUnionJsEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var htmlFiles = valueProvider.GetFiles(Entities.FileType.Html);
            List<FileContext> files = new List<FileContext>();

            var searcher = new HtmlJsFileSearcher();

            foreach (var item in htmlFiles) {
                var list = searcher.Search(item.Tree);
                files.AddRange(list);
            }

            var uniqFiles = files.Distinct();

            File commonFile = new File();
            commonFile.FileName = IO.Path.GetRandomFileName();
            commonFile.SearchName = commonFile.FileName;
            commonFile.Type = FileType.Js;

            ProgramContext program = new ProgramContext(null, 0);
            SourceElementsContext elems = new SourceElementsContext(program, 0);
            foreach (var item in uniqFiles) {
                var tree = item.File.Tree as ProgramContext;
                var treeElems = tree.sourceElements().sourceElement();
                foreach (var elem in treeElems) {
                    elems.AddChild(elem);
                }

                valueProvider.RemoveFile(item.File.FileName);
            }
            program.AddChild(elems);

            commonFile.Tree = program;

            valueProvider.AddFile(commonFile);

            HtmlAddScriptEditor htmlAdsScript = new HtmlAddScriptEditor();

            foreach (var item in htmlFiles) {
                htmlAdsScript.Add(item.Tree, new File[] { commonFile });
            }

            foreach (var item in files) {
                item.Context.Remove();
            }
        }


    }
}
