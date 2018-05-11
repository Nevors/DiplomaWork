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
    class HtmlStyleTagEditor {
        public void Edit(IParseTree tree, IValueProvider valueProvider) {
            new Visitor(valueProvider).Visit(tree);
        }
        class Visitor : HtmlParserBaseVisitor<object> {
            private readonly IValueProvider valueProvider;

            public Visitor(IValueProvider valueProvider) {
                this.valueProvider = valueProvider;
            }

            public override object VisitHtmlElement([NotNull] HtmlParser.HtmlElementContext context) {
                var content = context.style();
                
                if (content == null) {
                    return base.VisitHtmlElement(context);
                }
                /*var a = new InternalContentContext(context, 0, content);
                context.Replace(content, a);*/
                return null;
            }

        }
    }
}
