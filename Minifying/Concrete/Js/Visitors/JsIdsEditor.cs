using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Js;
using Minifying.Concrete.Js.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minifying.Concrete.Js.Visitors {
    class JsIdsEditor {
        public void Replace(IParseTree tree, Dictionary<string, string> idsMap) {
            new Visitor(idsMap).Visit(tree);
        }

        class Visitor : JsParserBaseVisitor<object> {
            private Dictionary<string, string> idsMap;

            public Visitor(Dictionary<string, string> idsMap) {
                this.idsMap = idsMap;
            }

            public override object VisitLiteral([NotNull] JsParser.LiteralContext context) {
                if (context.StringLiteral() == null) { return null; }

                var manager = new StringLiteralManager(context);

                string id = manager.Value;
                if (id.Length <= 1) { return null; }

                if (idsMap.ContainsKey(id)) {
                    manager.Value = idsMap[id];
                    return null;
                }

                var id2 = id.Substring(1);
                if (idsMap.ContainsKey(id2)) {
                    manager.Value = id[0] + idsMap[id2];
                    return null;
                }

                var ids = idsMap.Keys;

                foreach (var item in ids) {
                    if (id.IndexOf('#' + item) >= 0) {
                        manager.Value = id.Replace(item, idsMap[item]);
                    }
                }

                return null;
            }
        }
    }
}
