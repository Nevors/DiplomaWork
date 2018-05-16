using Antlr4.Runtime;
using Minifying.Abstract.Managers;
using Minifying.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static AntlrGrammars.Js.JsParser;
namespace Minifying.Concrete.Js.Models {
    class StringLiteralManager : StringManager {
        private readonly LiteralContext context;

        public StringLiteralManager(LiteralContext context)
            : base(
                  () => {
                      return context.StringLiteral().Symbol.Text;
                  },
                  (value) => {
                      var stringLiteral = context.StringLiteral();
                      var newToken = new CommonToken(stringLiteral.Symbol.Type, value);
                      context.Replace(stringLiteral, newToken);
                  }) {
            this.context = context;
        }
    }
}
