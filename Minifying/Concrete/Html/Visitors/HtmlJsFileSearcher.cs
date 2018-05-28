using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Concrete.Html.Models;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Html.Visitors
{
    class HtmlJsFileSearcher
    {
        public List<FileContext> Search(IParseTree tree) {
            var visitor = new Visitor();
            visitor.Visit(tree);
            return visitor.Files;
        }

        class Visitor : HtmlParserBaseVisitor<object> {
            public List<FileContext> Files { get; private set; }
            public Visitor() {
                Files = new List<FileContext>();
            }

            public override object VisitScript([NotNull] HtmlParser.ScriptContext context) {
                var manager = new HtmlScriptTagManager(context);
                if (manager.File != null) {
                    Files.Add(new FileContext { File = manager.File, Context = context });
                }
                return null;
            }
        }
    }
}
