using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Js;
namespace Minifying.Concrete.Js.Visitors {
    class JsEofTokenRemover {
        public void Remove(IParseTree tree) {
            new Visitor().Visit(tree);
        }

        class Visitor : JsParserBaseVisitor<object> {
            public override object VisitProgram([NotNull] JsParser.ProgramContext context) {
                if (context.Eof() != null) {
                    context.RemoveLastChild();
                }
                return base.VisitProgram(context);
            }

            public override object VisitEos([NotNull] JsParser.EosContext context) {
                if (context.Eof() != null) {
                    context.RemoveLastChild();
                }
                return null;
            }
        }
    }
}
