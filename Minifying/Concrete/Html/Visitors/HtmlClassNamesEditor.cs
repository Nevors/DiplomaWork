using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Common;
using Minifying.Concrete.Html.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minifying.Concrete.Html.Visitors {
    class HtmlClassNamesEditor {
        public void Replace(IParseTree tree, Dictionary<string, string> classNamesMap) {
            new Visitor(classNamesMap).Visit(tree);
        }

        class Visitor : HtmlParserBaseVisitor<object> {
            private Dictionary<string, string> classNamesMap;
            public Visitor(Dictionary<string, string> classNamesMap) {
                this.classNamesMap = classNamesMap;
            }
            public override object VisitHtmlAttribute([NotNull] HtmlParser.HtmlAttributeContext context) {
                var contextName = context.htmlAttributeName();
                var name = contextName.GetText();

                bool isClass = string.Equals("class", name, StringComparison.OrdinalIgnoreCase);
                if (isClass) {
                    var mangerAttr = new HtmlAttributeManager(context);
                    string text = mangerAttr.Value;

                    StringBuilder sb = new StringBuilder();

                    foreach (var className in text.Split(' ')) {
                        if (classNamesMap.ContainsKey(className)) {
                            sb.Append(classNamesMap[className]);
                        } else {
                            sb.Append(className);
                        }
                        sb.Append(" ");
                    }
                    sb.Length--;

                    mangerAttr.Value = sb.ToString();
                }
                return null;
            }
        }
    }
}
