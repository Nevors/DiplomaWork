using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Abstract.Visitors.Html;
using Minifying.Concrete.Html.Models;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Html.Visitors {
    class HtmlStyleFilesSearcher {
        public List<FileContext> Search(IParseTree tree) {
            var visitor = new Visitor();
            visitor.Visit(tree);
            return visitor.Files;
        }

        class Visitor : HtmlStyleVisitor<object> {
            public List<FileContext> Files { get; private set; }
            public Visitor() {
                Files = new List<FileContext>();
            }

            public override void VisitLink(HtmlParser.HtmlElementContext context) {
                var manager = new HtmlLinkStyleTagManager(context);
                if (manager.File != null) {
                    Files.Add(new FileContext { File = manager.File, Context = context });
                }
            }

            public override void VisitInteranaStyle(HtmlParser.StyleContext context) {
                var manager = new HtmlStyleTagManager(context);
                if (manager.File != null) {
                    Files.Add(new FileContext { File = manager.File, Context = context });
                }
            }
        }
    }
}
