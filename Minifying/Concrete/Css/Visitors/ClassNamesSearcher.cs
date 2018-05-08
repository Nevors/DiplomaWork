using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;
using AntlrGrammars.Css;
using Antlr4.Runtime.Misc;

namespace Minifying.Concrete.Css.Visitors {
    class ClassNamesSearcher {

        public void Search(IParseTree tree, Dictionary<string, int> freqList) {
            new Visitor(freqList).Visit(tree);
        }

        class Visitor : CssBaseVisitor<object> {
            private Dictionary<string, int> freqList;
            public Visitor(Dictionary<string, int> freqList){
                this.freqList = freqList;
            }
            public override object VisitClassName([NotNull] CssParser.ClassNameContext context) {
                var ident = context.ident();
                var nameIdent = ident.Ident();
                if (nameIdent != null) {
                    string text = nameIdent.Symbol.Text;
                    if (freqList.ContainsKey(text)) {
                        freqList[text]++;
                    } else {
                        freqList.Add(text, 0);
                    }
                }
                return base.VisitClassName(context);
            }
        }
    }
}
