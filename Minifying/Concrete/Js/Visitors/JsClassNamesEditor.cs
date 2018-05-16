using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Js;
using Minifying.Concrete.Js.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Js.Visitors
{
    class JsClassNamesEditor
    {
        public void Replace(IParseTree tree, Dictionary<string, string> map) {
            new Visitor(map).Visit(tree);
        }

        class Visitor : JsBaseVisitor<object> {
            private Dictionary<string, string> map;

            public Visitor(Dictionary<string, string> idsMap) {
                this.map = idsMap;
            }

            public override object VisitLiteral([NotNull] JsParser.LiteralContext context) {
                if (context.StringLiteral() == null) { return null; }

                var manager = new StringLiteralManager(context);
                string id = manager.Value;
                if (map.ContainsKey(id)) {
                    manager.Value = map[id];
                    return null;
                }

                var id2 = id.Substring(1);
                if (map.ContainsKey(id2)) {
                    manager.Value = map[id2];
                    return null;
                }

                var keys = map.Keys;

                foreach (var item in keys) {
                    if (id.IndexOf(item) >= 0) {
                        manager.Value = id.Replace(item, map[item]);
                    }
                }

                return base.VisitLiteral(context);
            }
        }
    }
}
