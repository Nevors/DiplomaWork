using Antlr4.Runtime;
using AntlrGrammars.Css;
using Minifying.Abstract;
using Minifying.Common;
using Minifying.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CssV = Minifying.Concrete.Css.Visitors;
using JsV = Minifying.Concrete.Js.Visitors;
using HtmlV = Minifying.Concrete.Html.Visitors;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Minifying.Concrete.Common.Editors {
    class NamesEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var cssFiles = valueProvider.GetFiles(FileType.Css);
            var jsFiles = valueProvider.GetFiles(FileType.Js);
            var htmlFiles = valueProvider.GetFiles(FileType.Html);

            var idsFreqList = new FreqList<string>();
            var classNamesFreqList = new FreqList<string>();

            var cssIdsSearcher = new CssV.CssIdsSearcher();
            var cssClassNamesSearcher = new CssV.CssClassNamesSearcher();

            foreach (var item in cssFiles) {
                cssIdsSearcher.Search(item.Tree, idsFreqList);
                cssClassNamesSearcher.Search(item.Tree, classNamesFreqList);
                Trace.TraceInformation("1");
            }


            var htmlIdsSercher = new HtmlV.HtmlIdsSercher();
            var htmlClassNamesSearcher = new HtmlV.HtmlClassNamesSearcher();
            foreach (var item in htmlFiles) {
                htmlIdsSercher.Search(item.Tree, idsFreqList);
                htmlClassNamesSearcher.Search(item.Tree, classNamesFreqList);
                Trace.TraceInformation("2");

            }

            INameGenerator nameGen = new NameGenerator();
            IFactoryNames factoryNames = new FactoryNames(nameGen);

            var idsMap = GetMapNames(idsFreqList.GetOrderedList(), factoryNames);
            var classNamesMap = GetMapNames(classNamesFreqList.GetOrderedList(), factoryNames);

            var cssIdsEditor = new CssV.CssIdsEditor();
            var cssClassNamesEditor = new CssV.CssClassNamesEditor();
            Parallel.ForEach(cssFiles, item => {
                cssIdsEditor.Replace(item.Tree, idsMap);
                cssClassNamesEditor.Replace(item.Tree, classNamesMap);
                Trace.TraceInformation("3");
            });

            var htmlIdsEditor = new HtmlV.HtmlIdsEditor();
            var htmlClassNamesEditor = new HtmlV.HtmlClassNamesEditor();
            Parallel.ForEach(htmlFiles, item => {
                htmlIdsEditor.Replace(item.Tree, idsMap);
                htmlClassNamesEditor.Replace(item.Tree, classNamesMap);
                Trace.TraceInformation("4");
            });

            var jsIdsEditor = new JsV.JsIdsEditor();
            var jsClassNamesEditor = new JsV.JsClassNamesEditor();
            Parallel.ForEach(jsFiles, item => {
                jsIdsEditor.Replace(item.Tree, idsMap);
                jsClassNamesEditor.Replace(item.Tree, classNamesMap);
                Trace.TraceInformation("5");
            });
        }

        public Dictionary<string, string> GetMapNames(IEnumerable<string> list, IFactoryNames factoryNames) {
            return list.ToDictionary(key => key, value => factoryNames.GetShortName(value));
        }
    }
}
