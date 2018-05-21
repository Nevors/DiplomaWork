using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AntlrGrammars.Html.HtmlParser;
using Minifying.Common;

namespace Minifying.Concrete.Html.Visitors {
    class HtmlWsSymbolsEditor {
        public void Edit(IParseTree tree) {
            var visitor = new Visitor();
            visitor.Visit(tree);
            var list = visitor.List.AsEnumerable().Reverse();
            var space = new CommonToken(SEA_WS, " ");
            foreach (var item in list) {
                item.Item2.Insert(item.Item1, space);
            }
        }
        class Visitor : HtmlParserBaseVisitor<object> {
            public List<Tuple<int, ParserRuleContext>> List { get; } = new List<Tuple<int, ParserRuleContext>>();

            public override object VisitHtmlChardata([NotNull] HtmlChardataContext context) {
                var ws = context.SEA_WS();
                if (ws != null) {
                    context.Replace(ws, new CommonToken(SEA_WS, " "));
                }
                return base.VisitHtmlChardata(context);
            }

            public override object VisitHtmlElement([NotNull] HtmlElementContext context) {
                ProcessingNode(context);
                return base.VisitHtmlElement(context);
            }
            public override object VisitStyle([NotNull] StyleContext context) {
                ProcessingNode(context);
                return base.VisitStyle(context);
            }

            public override object VisitScript([NotNull] ScriptContext context) {
                ProcessingNode(context);
                return base.VisitScript(context);
            }

            private void ProcessingNode(ParserRuleContext context) {
                for (int i = 0; i < context.ChildCount; i++) {
                    var node = context.GetChild(i);
                    if (node is HtmlAttributeContext node2) {
                        List.Add(Tuple.Create(i, context));
                    }
                }
            }
        }
    }
}
