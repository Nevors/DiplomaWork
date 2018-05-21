using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Js;
using Minifying.Common;
using Minifying.Concrete.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minifying.Concrete.Js.Visitors {
    class JsWsSymbolsEditor {
        public void Edit(IParseTree tree) {
            var visitor = new Visitor();
            visitor.Visit(tree);
            var space = new CommonToken(JsParser.WhiteSpaces, " ");
            foreach (var item in visitor.List.AsEnumerable().Reverse()) {
                item.Rule.Insert(item.Index, space);
            }
        }

        private class Visitor : JsParserBaseVisitor<object> {
            public List<PositionRule> List { get; } = new List<PositionRule>();
            public override object VisitVariableStatement([NotNull] JsParser.VariableStatementContext context) {
                List.Add(new PositionRule { Index = 1, Rule = context });
                return base.VisitVariableStatement(context);
            }

            public override object VisitFunctionExpression([NotNull] JsParser.FunctionExpressionContext context) {
                if (context.Identifier() != null) {
                    List.Add(new PositionRule { Index = 1, Rule = context });
                }
                return base.VisitFunctionExpression(context);
            }

            public override object VisitFunctionDeclaration([NotNull] JsParser.FunctionDeclarationContext context) {
                if (context.Identifier() != null) {
                    List.Add(new PositionRule { Index = 1, Rule = context });
                }
                return base.VisitFunctionDeclaration(context);
            }

            public override object VisitReturnStatement([NotNull] JsParser.ReturnStatementContext context) {
                var expr = context.expressionSequence();
                if (expr == null) { return null; }
                var objectLit = expr.GetRuleContext<JsParser.ObjectLiteralContext>(0);
                var firstChar = expr.GetText()[0];
                if (firstChar == '+'
                    || firstChar == '-'
                    || firstChar == '\''
                    || firstChar == '"'
                    || firstChar == '!'
                    || firstChar == '['
                    || firstChar == '{'
                    || firstChar == '('
                    || firstChar == '.') {
                    return base.VisitReturnStatement(context);
                }

                List.Add(new PositionRule { Index = 1, Rule = context });
                return base.VisitReturnStatement(context);
            }

            public override object VisitTypeofExpression([NotNull] JsParser.TypeofExpressionContext context) {
                var chield = context.GetChild(1);
                if (!(chield is JsParser.ParenthesizedExpressionContext) && chield.ChildCount != 0) {
                    List.Add(new PositionRule { Index = 1, Rule = context });
                }
                return base.VisitTypeofExpression(context);
            }
            public override object VisitVoidExpression([NotNull] JsParser.VoidExpressionContext context) {
                var chield = context.GetChild(1);
                if (!(chield is JsParser.ParenthesizedExpressionContext) && chield.ChildCount != 0) {
                    List.Add(new PositionRule { Index = 1, Rule = context });
                }
                return base.VisitVoidExpression(context);
            }

            public override object VisitForVarInStatement([NotNull] JsParser.ForVarInStatementContext context) {
                List.Add(new PositionRule { Index = 3, Rule = context });
                var objectLit = context.expressionSequence()?.GetRuleContext<JsParser.ObjectLiteralExpressionContext>(0);
                var arrayLit = context.expressionSequence()?.GetRuleContext<JsParser.ArrayLiteralExpressionContext>(0);
                if (objectLit != null
                    || arrayLit != null) {
                    return base.VisitForVarInStatement(context);
                }
                List.Add(new PositionRule { Index = 4, Rule = context });
                List.Add(new PositionRule { Index = 5, Rule = context });
                return base.VisitForVarInStatement(context);
            }

            public override object VisitForInStatement([NotNull] JsParser.ForInStatementContext context) {
                List.Add(new PositionRule { Index = 3, Rule = context });
                var objectLit = context.expressionSequence()?.GetRuleContext<JsParser.ObjectLiteralExpressionContext>(0);
                var arrayLit = context.expressionSequence()?.GetRuleContext<JsParser.ArrayLiteralExpressionContext>(0);
                if (objectLit != null
                    || arrayLit != null) {
                    return base.VisitForInStatement(context);
                }
                List.Add(new PositionRule { Index = 4, Rule = context });
                return base.VisitForInStatement(context);
            }

            public override object VisitInExpression([NotNull] JsParser.InExpressionContext context) {
                var firstChild = context.GetChild(0).GetText();
                var lastChar = firstChild[firstChild.Length - 1];
                if (lastChar != '"'
                    && lastChar != '\''
                    && lastChar != ']') {
                    List.Add(new PositionRule { Index = 1, Rule = context });
                }
                var secondChild = context.GetChild(2);
                if (!(secondChild is JsParser.ObjectLiteralExpressionContext)) {
                    List.Add(new PositionRule { Index = 2, Rule = context });
                }
                return base.VisitInExpression(context);
            }
            public override object VisitInstanceofExpression([NotNull] JsParser.InstanceofExpressionContext context) {
                List.Add(new PositionRule { Index = 1, Rule = context });
                List.Add(new PositionRule { Index = 2, Rule = context });
                return base.VisitInstanceofExpression(context);
            }

            public override object VisitForVarStatement([NotNull] JsParser.ForVarStatementContext context) {
                List.Add(new PositionRule { Index = 3, Rule = context });
                return base.VisitForVarStatement(context);
            }

            public override object VisitThrowStatement([NotNull] JsParser.ThrowStatementContext context) {
                List.Add(new PositionRule { Index = 1, Rule = context });
                return base.VisitThrowStatement(context);
            }

            public override object VisitNewExpression([NotNull] JsParser.NewExpressionContext context) {
                List.Add(new PositionRule { Index = 1, Rule = context });
                return base.VisitNewExpression(context);
            }

            public override object VisitDeleteExpression([NotNull] JsParser.DeleteExpressionContext context) {
                List.Add(new PositionRule { Index = 1, Rule = context });
                return base.VisitDeleteExpression(context);
            }

            public override object VisitIfStatement([NotNull] JsParser.IfStatementContext context) {
                var secondStatement = context.GetRuleContext<JsParser.StatementContext>(1);
                if (secondStatement != null && secondStatement.block() == null) {
                    List.Add(new PositionRule { Index = 6, Rule = context });
                }
                return base.VisitIfStatement(context);
            }

            public override object VisitDoStatement([NotNull] JsParser.DoStatementContext context) {
                var block = context.statement()?.block();
                if (block == null) {
                    List.Add(new PositionRule { Index = 1, Rule = context });
                }
                return base.VisitDoStatement(context);
            }

            public override object VisitCaseClause([NotNull] JsParser.CaseClauseContext context) {
                List.Add(new PositionRule { Index = 1, Rule = context });
                return base.VisitCaseClause(context);
            }
        }
    }
}
