using Antlr4.Runtime;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static AntlrGrammars.Html.HtmlParser;

namespace Minifying.Concrete.Html.Models.Contexts {
    class InternalStyleContext : StyleContext {
        public StyleContext Style { get; }
        public File File { get; }

        public InternalStyleContext(ParserRuleContext parent, int invokingState, File file, StyleContext style) : base(parent, invokingState) {
            File = file;
            Style = style;
            style.STYLE_OPEN();
        }

        public override string GetText() {
            if (File.Tree != null) {
                return File.Tree.GetText();
            }
            return new System.IO.StreamReader(File.Stream).ReadToEnd();
        }
    }
}
