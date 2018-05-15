using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Abstract;
using Minifying.Common;
using Minifying.Concrete.Html.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Html.Visitors
{
    class HtmlStyleEditor {
        public void Edit(IParseTree tree, IValueProvider valueProvider, IPathProvider pathProvider) {
            new Visitor(valueProvider, pathProvider).Visit(tree);
        }
        class Visitor : HtmlParserBaseVisitor<object> {
            private readonly IValueProvider valueProvider;
            private readonly IPathProvider pathProvider;

            public Visitor(IValueProvider valueProvider, IPathProvider pathProvider) {
                this.valueProvider = valueProvider;
                this.pathProvider = pathProvider;
            }

            public override object VisitHtmlElement([NotNull] HtmlParser.HtmlElementContext context) {
                var tags = context.htmlTagName();
                if (tags.Length == 0) { return null; }

                var tagName = tags[0].TAG_NAME().GetText();
                if (tagName.ToUpper() != "LINK") {
                    return base.VisitHtmlElement(context);
                }

                new LinkStyleTagManager(context).Init(valueProvider,pathProvider);

                return null;
            }

            public override object VisitStyle([NotNull] HtmlParser.StyleContext context) {
                new StyleTagManager(context).Init(valueProvider);
                return null;
            }

        }
    }
}
