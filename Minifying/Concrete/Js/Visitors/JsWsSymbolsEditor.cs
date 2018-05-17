using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Js;
using Minifying.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Js.Visitors {
    class JsWsSymbolsEditor {
        public void Edit(IParseTree tree) {
            var visitor = new Visitor();
            visitor.Visit(tree);
            var space = new CommonToken(JsParser.WhiteSpaces, " ");
            foreach (var item in visitor.List) {
                item.Insert(1, space);
            }
        }

        private class Visitor : JsBaseVisitor<object> {
            public List<ParserRuleContext> List { get; } = new List<ParserRuleContext>();
            public override object VisitVariableStatement([NotNull] JsParser.VariableStatementContext context) {
                List.Add(context);
                return base.VisitVariableStatement(context);
            }

            public override object VisitFunctionExpression([NotNull] JsParser.FunctionExpressionContext context) {
                if (context.Identifier() != null) {
                    List.Add(context);
                }
                return base.VisitFunctionExpression(context);
            }
        }
    }
}
