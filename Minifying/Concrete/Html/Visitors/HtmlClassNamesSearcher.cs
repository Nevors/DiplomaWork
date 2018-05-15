using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Common;
using Minifying.Concrete.Html.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Html.Visitors {
    class HtmlClassNamesSearcher {
        public void Search(IParseTree tree, FreqList<string> freqList) {
            new Visitor(freqList).Visit(tree);
        }

        class Visitor : HtmlParserBaseVisitor<object> {
            private FreqList<string> freqList;
            public Visitor(FreqList<string> freqList) {
                this.freqList = freqList;
            }
            public override object VisitHtmlAttribute([NotNull] HtmlParser.HtmlAttributeContext context) {
                var contextName = context.htmlAttributeName();
                var name = contextName.GetText();

                bool isClass = string.Equals("class", name, StringComparison.OrdinalIgnoreCase);
                if (isClass) {
                    var mangerAttr = new AttributeManager(context);

                    foreach (var className in mangerAttr.Value.Split(' ')) {
                        freqList.Increment(className);
                    }
                }
                return null;
            }
        }
    }
}
