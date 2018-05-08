using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;
using AntlrGrammars.Html;
using Antlr4.Runtime.Misc;

namespace Minifying.Concrete.Html.Visitors {
    class IdsSercher {
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

                bool isId = string.Equals("id", name, StringComparison.OrdinalIgnoreCase);
                if (isId) {
                    var contextValue = context.htmlAttributeValue();
                    var token = contextValue.ATTVALUE_VALUE();
                    //contextValue.ch
                    string text = token.Symbol.Text;

                    if (freqList.ContainsKey(text)) {
                        freqList[text]++;
                    } else {
                        freqList.Add(text, 0);
                    }
                }
                return base.VisitHtmlAttribute(context);
            }
        }
    }
}
