using Antlr4.Runtime.Misc;
using AntlrGrammars.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Html.Visitors
{
    class Test : HtmlParserBaseVisitor<object> {
        public override object VisitStyle([NotNull] HtmlParser.StyleContext context) {
            return base.VisitStyle(context);
        }
    }
}
