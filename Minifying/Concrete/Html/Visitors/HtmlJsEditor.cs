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
            new Visitor(valueProvider, pathProvider).Visit(tree);
        }

        class Visitor : HtmlParserBaseVisitor<object> {
            private readonly IValueProvider valueProvider;
            private readonly IPathProvider pathProvider;

            public Visitor(IValueProvider valueProvider, IPathProvider pathProvider) {
                this.valueProvider = valueProvider;
                this.pathProvider = pathProvider;
            }

            public override object VisitScript([NotNull] HtmlParser.ScriptContext context) {
                new ScriptTagManager(context).Init(valueProvider, pathProvider);
                return null;
            }

        }
    }
}
