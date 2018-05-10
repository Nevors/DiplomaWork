using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using AntlrGrammars.Css;
using Minifying.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Css.Visitors
{
    class CssIdsEditor
    {
        public void Replace(IParseTree tree, Dictionary<string, string> idsMap) {
            new Visitor(idsMap).Visit(tree);
        }

        class Visitor : CssBaseVisitor<object> {
            private Dictionary<string, string> idsMap;
            public Visitor(Dictionary<string, string> classNamesMap) {
                this.idsMap = classNamesMap;
            }

            public override object VisitSimpleSelectorSequence([NotNull] CssParser.SimpleSelectorSequenceContext context) {
                for (int i = 0; i < context.ChildCount; i++) {
                    var id = context.GetChild(i);
                    string text = id.GetText();

                    if (idsMap.ContainsKey(text)) {
                        var newNode = new CommonToken(CssParser.RULE_attrib, idsMap[text]);
                        context.Replace(id, newNode);
                    }
                }
                return base.VisitSimpleSelectorSequence(context);
            }
        }
    }
}
