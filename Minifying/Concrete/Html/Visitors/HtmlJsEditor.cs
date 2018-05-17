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
    class HtmlJsEditor {
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

            public override object VisitScript([NotNull] HtmlParser.ScriptContext context) {
                HtmlScriptTagManager tagManager = new HtmlScriptTagManager(context);
                tagManager.Init(valueProvider, pathProvider);

                //if (tagManager.File == null) { NodesForRemove.Add(context); }
                return null;
            }

        }
    }
}
