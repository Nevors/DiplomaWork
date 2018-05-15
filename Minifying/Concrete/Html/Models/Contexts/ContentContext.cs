using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;
using Minifying.Entities;
using static AntlrGrammars.Html.HtmlParser;

namespace Minifying.Concrete.Html.Models.Contexts {
    class ContentContext : HtmlContentContext {
        public File File { get; }
        public ContentContext(ParserRuleContext parent, File file) : base(parent, 0) {
            File = file;
        }

        public override string GetText() {
            return File.Tree.GetText();
        }
    }
}
