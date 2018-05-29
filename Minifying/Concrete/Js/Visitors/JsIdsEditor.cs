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
    class JsIdsEditor {
        public void Replace(IParseTree tree, IValueProvider valueProvider, Dictionary<string, string> idsMap) {
            new Visitor(idsMap, valueProvider).Visit(tree);
        }

        class Visitor : JsParserBaseVisitor<object> {
            private Dictionary<string, string> idsMap;
            private readonly IValueProvider valueProvider;
            FactoryOutputMessage factoryMessage = new FactoryOutputMessage();
            public Visitor(Dictionary<string, string> idsMap, IValueProvider valueProvider) {
                this.idsMap = idsMap;
                this.valueProvider = valueProvider;
            }

            public override object VisitLiteral([NotNull] JsParser.LiteralContext context) {
                if (context.StringLiteral() == null) { return null; }

                var manager = new StringLiteralManager(context);

                string id = manager.Value;
                if (id.Length <= 1) { return null; }

                if (idsMap.ContainsKey(id)) {
                    Answer(idsMap[id], manager, context);
                    return null;
                }

                var id2 = id.Substring(1);
                if (idsMap.ContainsKey(id2)) {
                    string will = id[0] + idsMap[id2];
                    if (!id.StartsWith("#")) {
                        Answer(will, manager, context);
                    } else {
                        manager.Value = will;
                    }
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
