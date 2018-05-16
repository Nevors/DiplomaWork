using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Abstract;
using Minifying.Common;
using Minifying.Concrete.Html.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Html.Visitors {
    class HtmlStyleEditor {
        public void Edit(IParseTree tree, IValueProvider valueProvider, IPathProvider pathProvider) {
            var visitor = new Visitor(valueProvider, pathProvider);
            visitor.Visit(tree);
            visitor.NodesForRemove.ForEach(i => i.Remove());
        }
        class Visitor : HtmlParserBaseVisitor<object> {
            private readonly IValueProvider valueProvider;
            private readonly IPathProvider pathProvider;
            public List<ParserRuleContext> NodesForRemove { get; } = new List<ParserRuleContext>();

            public Visitor(IValueProvider valueProvider, IPathProvider pathProvider) {
                this.valueProvider = valueProvider;
                this.pathProvider = pathProvider;
            }

            public override object VisitHtmlElement([NotNull] HtmlParser.HtmlElementContext context) {
                var tags = context.htmlTagName();
                if (tags.Length == 0) { return base.VisitHtmlElement(context); }

                var tagName = tags[0].TAG_NAME().GetText();
                if (tagName.ToUpper() != "LINK") {
                    return base.VisitHtmlElement(context);
                }

                var linkStyle = new HtmlLinkStyleTagManager(context);
                linkStyle.Init(valueProvider,pathProvider);
                if (linkStyle.File == null) { NodesForRemove.Add(context); }
                return null;
            }

            public override object VisitStyle([NotNull] HtmlParser.StyleContext context) {
                HtmlStyleTagManager htmlStyleTagManager = new HtmlStyleTagManager(context);
                htmlStyleTagManager.Init(valueProvider);
                return null;
            }

        }
    }
}
