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

namespace Minifying.Concrete.Common.Editors {
    class IdsNameEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var cssFiles = valueProvider.GetFiles(FileType.Css);
            var jsFiles = valueProvider.GetFiles(FileType.Js);
            var htmlFiles = valueProvider.GetFiles(FileType.Html);

            var idsFreqList = new FreqList<string>();
            var classNamesFreqList = new FreqList<string>();

            foreach (var item in cssFiles) {
                new CssV.CssIdsSearcher().Search(item.Tree, idsFreqList);
                new CssV.CssClassNamesSearcher().Search(item.Tree, classNamesFreqList);
            }

            foreach (var item in htmlFiles) {
                new HtmlV.HtmlIdsSercher().Search(item.Tree, idsFreqList);
                new HtmlV.HtmlClassNamesSearcher().Search(item.Tree, classNamesFreqList);
            }

            INameGenerator nameGen = new NameGenerator();
            IFactoryNames factoryNames = new FactoryNames(nameGen);

            var idsMap = GetMapNames(idsFreqList.GetOrderedList(), factoryNames);
            var classNamesMap = GetMapNames(classNamesFreqList.GetOrderedList(), factoryNames);

            foreach (var item in cssFiles) {
                new CssV.CssIdsEditor().Replace(item.Tree, idsMap);
                new CssV.CssClassNamesEditor().Replace(item.Tree, classNamesMap);
            }

            foreach (var item in htmlFiles) {
                new HtmlV.HtmlIdsEditor().Replace(item.Tree, idsMap);
                new HtmlV.HtmlClassNamesEditor().Replace(item.Tree, classNamesMap);
            }

            foreach (var item in jsFiles) {
                new JsV.JsIdsEditor().Replace(item.Tree, idsMap);
                new JsV.JsClassNamesEditor().Replace(item.Tree, idsMap);
            }
        }

        public Dictionary<string, string> GetMapNames(IEnumerable<string> list, IFactoryNames factoryNames) {
            return list.ToDictionary(key => key, value => factoryNames.GetShortName(value));
        }
    }
}
