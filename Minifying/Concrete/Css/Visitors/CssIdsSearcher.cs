using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Css;
using Minifying.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Css.Visitors {
    class CssIdsSearcher {

        public void Search(IParseTree tree, FreqList<string> freqList) {
            new Visitor(freqList).Visit(tree);
        }

        class Visitor : CssBaseVisitor<object> {
            private FreqList<string> freqList;
            public Visitor(FreqList<string> freqList) {
                this.freqList = freqList;
            }
            public override object VisitSimpleSelectorSequence([NotNull] CssParser.SimpleSelectorSequenceContext context) {
                var item = context.GetChild(0);
                if (item is TerminalNodeImpl token) {
                    string text = token.Symbol.Text.Substring(1);
                    freqList.Increment(text);
                }
                return base.VisitSimpleSelectorSequence(context);
            }
        }
    }
}
