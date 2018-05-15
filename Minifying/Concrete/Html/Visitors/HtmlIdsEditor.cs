using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Concrete.Html.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Html.Visitors {
    class HtmlIdsEditor {
        public void Replace(IParseTree tree, Dictionary<string, string> idsMap) {
            new Visitor(idsMap).Visit(tree);
        }

        class Visitor : HtmlParserBaseVisitor<object> {
            private Dictionary<string, string> idsMap;

            public Visitor(Dictionary<string, string> idsMap) {
                this.idsMap = idsMap;
            }
            public override object VisitHtmlAttribute([NotNull] HtmlParser.HtmlAttributeContext context) {
                var contextName = context.htmlAttributeName();
                var name = contextName.GetText();

                bool isId = string.Equals("id", name, StringComparison.OrdinalIgnoreCase);
                if (isId) {
                    var mangerAttr = new AttributeManager(context);
                    string id = mangerAttr.Value;
                    if (idsMap.ContainsKey(id)) {
                        mangerAttr.Value = idsMap[id];
                    }
                }
                return null;
            }
        }
    }
}
