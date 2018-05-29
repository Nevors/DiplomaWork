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
            }


            var htmlIdsSercher = new HtmlV.HtmlIdsSercher();
            var htmlClassNamesSearcher = new HtmlV.HtmlClassNamesSearcher();
            foreach (var item in htmlFiles) {
                htmlIdsSercher.Search(item.Tree, idsFreqList);
                htmlClassNamesSearcher.Search(item.Tree, classNamesFreqList);
            }

            INameGenerator nameGen = new NameGenerator();
            IFactoryNames factoryNames = new FactoryNames(nameGen);

            var idsMap = GetMapNames(idsFreqList.GetOrderedList(), factoryNames);
            var classNamesMap = GetMapNames(classNamesFreqList.GetOrderedList(), factoryNames);

            var cssIdsEditor = new CssV.CssIdsEditor();
            var cssClassNamesEditor = new CssV.CssClassNamesEditor();
            foreach (var item in cssFiles){
                cssIdsEditor.Replace(item.Tree, idsMap);
                cssClassNamesEditor.Replace(item.Tree, classNamesMap);
            }

            var htmlIdsEditor = new HtmlV.HtmlIdsEditor();
            var htmlClassNamesEditor = new HtmlV.HtmlClassNamesEditor();
            foreach(var item in htmlFiles){
                htmlIdsEditor.Replace(item.Tree, idsMap);
                htmlClassNamesEditor.Replace(item.Tree, classNamesMap);
            }

            var jsIdsEditor = new JsV.JsIdsEditor();
            var jsClassNamesEditor = new JsV.JsClassNamesEditor();
            foreach (var item in jsFiles){
                jsIdsEditor.Replace(item.Tree, valueProvider, idsMap);
                jsClassNamesEditor.Replace(item.Tree, valueProvider, classNamesMap);
            }
        }

        public Dictionary<string, string> GetMapNames(IEnumerable<string> list, IFactoryNames factoryNames) {
            return list.ToDictionary(key => key, value => factoryNames.GetShortName(value));
        }
    }
}
