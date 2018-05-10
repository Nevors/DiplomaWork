using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using AntlrGrammars.Css;
using Minifying.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Css.Visitors {
    class CssClassNamesEditor {
        public void Replace(IParseTree tree, Dictionary<string, string> classNamesMap) {
            new Visitor(classNamesMap).Visit(tree);
        }

        class Visitor : CssBaseVisitor<object> {
            private Dictionary<string, string> classNamesMap;
            public Visitor(Dictionary<string, string> classNamesMap) {
                this.classNamesMap = classNamesMap;
            }
            public override object VisitClassName([NotNull] CssParser.ClassNameContext context) {
                var ident = context.ident();
                var nameIdent = ident.Ident();
                if (nameIdent != null) {
                    string text = nameIdent.Symbol.Text;

                    if (classNamesMap.ContainsKey(text)) {
                        var newNode = new CommonToken(CssParser.Ident, classNamesMap[text]);
                        ident.Replace(nameIdent, newNode);
                    }
                }
                return base.VisitClassName(context);
            }
        }
    }
}
