using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;
using AntlrGrammars.Css;
using Antlr4.Runtime.Misc;
using Minifying.Common;

namespace Minifying.Concrete.Css.Visitors {
    class CssClassNamesSearcher {

        public void Search(IParseTree tree, FreqList<string> freqList) {
            new Visitor(freqList).Visit(tree);
        }

        class Visitor : CssBaseVisitor<object> {
            private FreqList<string> freqList;
            public Visitor(FreqList<string> freqList) {
                this.freqList = freqList;
            }
            public override object VisitClassName([NotNull] CssParser.ClassNameContext context) {
                var ident = context.ident();
                var nameIdent = ident.Ident();
                if (nameIdent != null) {
                    string text = nameIdent.Symbol.Text;
                    freqList.Increment(text);
                }
                return base.VisitClassName(context);
            }
        }
    }
}
