using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using static AntlrGrammars.Html.HtmlParser;
using System.Text;
using Antlr4.Runtime;
using Minifying.Common;
using Minifying.Concrete.Html.Models;

namespace Minifying.Concrete.Html.Visitors {
    class HtmlAddScriptEditor {
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
                if (name == null || name.ToUpper() != "BODY") {
                    return base.VisitHtmlElement(context);
                }

                HtmlElemFactory factory = new HtmlElemFactory();
                foreach (var item in files) {
                    HtmlElementContext elem = new HtmlElementContext(content, 0);
                    ScriptContext script = new ScriptContext(elem, 0);
                    script.AddChild(new CommonToken(RULE_script, "<script"));
                    script.AddChild(new CommonToken(SEA_WS, " "));

                    var attribute = factory.CreateAttribute(script, "src", item.FileName);
                    script.AddChild(attribute);

                    script.AddChild(new CommonToken(TAG_CLOSE, ">"));

                    script.AddChild(new HtmlContentContext(script, 0));
                    script.AddChild(new CommonToken(TAG_OPEN, "<"));
                    script.AddChild(new CommonToken(TAG_SLASH, "/"));
                    script.AddChild(factory.CreateTagName(script,"script"));
                    script.AddChild(new CommonToken(TAG_CLOSE, ">"));

                    elem.AddChild(script);

                    content.AddChild(elem);
                }
                return null;
            }
        }
    }
}

