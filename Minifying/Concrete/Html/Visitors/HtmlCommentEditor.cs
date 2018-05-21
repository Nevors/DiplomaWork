using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AntlrGrammars.Html.HtmlParser;

namespace Minifying.Concrete.Html.Visitors {
    class HtmlCommentEditor {
        public void Edit(IParseTree tree) {
            var visitor = new Visitor();
            visitor.Visit(tree);
            var list = visitor.List;
            foreach (var item in list) {
                item.Remove();
            }
        }
        class Visitor : HtmlParserBaseVisitor<object> {
            public List<ParserRuleContext> List { get; } = new List<ParserRuleContext>();

            public override object VisitHtmlComment([NotNull] HtmlCommentContext context) {
                if (context.HTML_CONDITIONAL() != null
                    || context.HTML_COMMENT_CONDITIONAL() != null) {
                    return base.VisitHtmlComment(context);
                }
                List.Add(context);
                return base.VisitHtmlComment(context);
            }
        }
    }
}
