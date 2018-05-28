using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using AntlrGrammars.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Abstract.Visitors.Html
{
    abstract class HtmlStyleVisitor<T> : HtmlParserBaseVisitor<T> {

        public override T VisitHtmlElement([NotNull] HtmlParser.HtmlElementContext context) {
            var tags = context.htmlTagName();
            if (tags.Length == 0) { return base.VisitHtmlElement(context); }

            var tagName = tags[0].TAG_NAME().GetText();
            if (tagName.ToUpper() != "LINK") {
                return base.VisitHtmlElement(context);
            }

            VisitLink(context);
            return default(T);
        }

        public override T VisitStyle([NotNull] HtmlParser.StyleContext context) {
            VisitInteranaStyle(context);
            return default(T);
        }

        public abstract void VisitLink(HtmlParser.HtmlElementContext context);

        public abstract void VisitInteranaStyle(HtmlParser.StyleContext context);

    }
}
