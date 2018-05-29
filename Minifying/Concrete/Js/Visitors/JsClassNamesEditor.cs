using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Js;
using Minifying.Abstract;
using Minifying.Common;
using Minifying.Concrete.Js.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minifying.Concrete.Js.Visitors {
    class JsClassNamesEditor {
        public void Replace(IParseTree tree, IValueProvider valueProvider, Dictionary<string, string> map) {
            new Visitor(map, valueProvider).Visit(tree);
        }

        class Visitor : JsParserBaseVisitor<object> {
            private Dictionary<string, string> map;
            FactoryOutputMessage factoryMessage = new FactoryOutputMessage();
            private readonly IValueProvider valueProvider;

            public Visitor(Dictionary<string, string> map, IValueProvider valueProvider) {
                this.map = map;
                this.valueProvider = valueProvider;
            }

            public override object VisitLiteral([NotNull] JsParser.LiteralContext context) {
                if (context.StringLiteral() == null) { return null; }

                var manager = new StringLiteralManager(context);
                string name = manager.Value;
                if (name.Length <= 1) { return null; }
                if (map.ContainsKey(name)) {
                    Answer(map[name], manager, context);
                    return null;
                }

                var id2 = name.Substring(1);
                if (map.ContainsKey(id2)) {
                    string will = "." + map[id2];

                    if (!name.StartsWith(".")) {
                        Answer(will, manager, context);
                    } else {
                        manager.Value = will;
                    }
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

            void Answer(string will, StringLiteralManager manager, IParseTree tree) {
                var statement = tree.CloseSt<JsParser.StatementContext>();
                var message = factoryMessage
                          .CreateMessage($"Заменить {manager.Value}? \r\n{statement.GetText()}")
                          .AddAction(
                                External.Models.AnswerType.Ok,
                                () => {
                                    manager.Value = will;
                                })
                          .GetMessage();
                valueProvider.GetOutputMessages().Add(message);
            }
        }
    }
}
