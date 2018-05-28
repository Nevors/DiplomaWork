using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Concrete.Html.Models;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static AntlrGrammars.Html.HtmlParser;

namespace Minifying.Concrete.Html.Visitors
{
    class HtmlAddStyleEditor
    {
        public void Add(IParseTree tree, IEnumerable<File> files) {
            new Visitor(files).Visit(tree);
        }

        class Visitor : HtmlParserBaseVisitor<object> {
            private readonly IEnumerable<File> files;

            public Visitor(IEnumerable<File> files) {
                this.files = files;
            }

            public override object VisitHtmlElement([NotNull] HtmlElementContext context) {
                var content = context.htmlContent();
                var name = context.htmlTagName(0)?.GetText();
                if (name == null || name.ToUpper() != "HEAD") {
                    return base.VisitHtmlElement(context);
                }
                HtmlElemFactory factory = new HtmlElemFactory();
                var space = new CommonToken(SEA_WS, " ");
                foreach (var item in files) {
                    HtmlElementContext elem = new HtmlElementContext(content,0);
                    elem.AddChild(new CommonToken(TAG_OPEN, "<"));
                    elem.AddChild(factory.CreateTagName(elem, "link"));

                    elem.AddChild(space);
                    var type = factory.CreateAttribute(elem, "type", "text/css");
                    elem.AddChild(type);

                    elem.AddChild(space);
                    var rel = factory.CreateAttribute(elem, "rel", "styleSheet");
                    elem.AddChild(rel);

                    elem.AddChild(space);
                    var href = factory.CreateAttribute(elem, "href", item.FileName);
                    elem.AddChild(href);
                    
                    elem.AddChild(new CommonToken(TAG_SLASH_CLOSE, "/>"));

                    content.AddChild(elem);
                }
                return null;
            }
        }
    }
}
