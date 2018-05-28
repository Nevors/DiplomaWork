using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Abstract;
using Minifying.Abstract.Visitors.Html;
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
        class Visitor : HtmlStyleVisitor<object> {
            private readonly IValueProvider valueProvider;
            private readonly IPathProvider pathProvider;
            public List<ParserRuleContext> NodesForRemove { get; } = new List<ParserRuleContext>();

            public Visitor(IValueProvider valueProvider, IPathProvider pathProvider) {
                this.valueProvider = valueProvider;
                this.pathProvider = pathProvider;
            }

            public override void VisitLink(HtmlParser.HtmlElementContext context) {
                var linkStyle = new HtmlLinkStyleTagManager(context);
                linkStyle.Init(valueProvider, pathProvider);
            }

            public override void VisitInteranaStyle(HtmlParser.StyleContext context) {
                HtmlStyleTagManager htmlStyleTagManager = new HtmlStyleTagManager(context);
                htmlStyleTagManager.Init(valueProvider);
            }
        }
    }
}
