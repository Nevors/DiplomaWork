using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Css;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Css.Visitors {
    class IdsSearcher {

        public void Search(IParseTree tree, Dictionary<string, int> freqList) {
            new Visitor(freqList).Visit(tree);
        }

        class Visitor : CssBaseVisitor<object> {
            private Dictionary<string, int> freqList;
            public Visitor(Dictionary<string, int> freqList) {
                this.freqList = freqList;
            }
            public override object VisitSimpleSelectorSequence([NotNull] CssParser.SimpleSelectorSequenceContext context) {
                var item = context.GetChild(0);
                if (item is TerminalNodeImpl token) {
                    string text = token.Symbol.Text;
                    if (freqList.ContainsKey(text)) {
                        freqList[text]++;
                    } else {
                        freqList.Add(text, 0);
                    }
                }
                return base.VisitSimpleSelectorSequence(context);
            }
        }
    }
}
