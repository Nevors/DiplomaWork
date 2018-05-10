using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;
using AntlrGrammars.Html;
using Antlr4.Runtime.Misc;
using Minifying.Common;
using System.Linq;
using Antlr4.Runtime;
using Minifying.Concrete.Html.Models;

namespace Minifying.Concrete.Html.Visitors {
    class HtmlIdsSercher {
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

                bool isId = string.Equals("id", name, StringComparison.OrdinalIgnoreCase);
                if (isId) {
                    var mangerAttr = new ManagerAttribute(context);
                    freqList.Increment(mangerAttr.Value);
                }
                return null;
            }
        }
    }
}
