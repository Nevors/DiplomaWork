using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Js;
using Minifying.Concrete.Js.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minifying.Concrete.Js.Visitors {
    class JsClassNamesEditor {
        public void Replace(IParseTree tree, Dictionary<string, string> map) {
            new Visitor(map).Visit(tree);
        }

        class Visitor : JsParserBaseVisitor<object> {
            private Dictionary<string, string> map;

            public Visitor(Dictionary<string, string> map) {
                this.map = map;
            }

            public override object VisitLiteral([NotNull] JsParser.LiteralContext context) {
                if (context.StringLiteral() == null) { return null; }

                var manager = new StringLiteralManager(context);
                string name = manager.Value;
                if (name.Length <= 1) { return null; }
                if (map.ContainsKey(name)) {
                    manager.Value = map[name];
                    return null;
                }

                var id2 = name.Substring(1);
                if (map.ContainsKey(id2)) {
                    manager.Value = "." + map[id2];
                    return null;
                }

                var keys = map.Keys;

                foreach (var item in keys) {
                    if (name.IndexOf('.' + item) >= 0) {
                        manager.Value = name.Replace(item, map[item]);
                    }
                }

                return base.VisitLiteral(context);
            }
        }
    }
}
