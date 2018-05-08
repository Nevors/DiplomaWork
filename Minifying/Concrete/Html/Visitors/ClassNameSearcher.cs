using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Html.Visitors {
    class ClassNameSearcher {
        public void Search(IParseTree tree, Dictionary<string, int> freqList) {
            new Visitor(freqList).Visit(tree);
        }

        class Visitor : HtmlParserBaseVisitor<object> {
            private Dictionary<string, int> freqList;
            public Visitor(Dictionary<string, int> freqList) {
                this.freqList = freqList;
            }
            public override object VisitHtmlAttribute([NotNull] HtmlParser.HtmlAttributeContext context) {
                var contextName = context.htmlAttributeName();
                var name = contextName.GetText();

                bool isClass = string.Equals("class", name, StringComparison.OrdinalIgnoreCase);
                if (isClass) {
                    var contextValue = context.htmlAttributeValue();
                    var token = contextValue.ATTVALUE_VALUE();
                    string text = token.Symbol.Text;

                    foreach (var className in text.Split(' ')) {
                        if (freqList.ContainsKey(className)) {
                            freqList[className]++;
                        } else {
                            freqList.Add(className, 0);
                        }
                    }
                }
                return base.VisitHtmlAttribute(context);
            }
        }
    }
}
