using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Minifying.Abstract;
using G = AntlrGrammars.Js;
namespace Minifying.Concrete.Js.Parsers
{
    class JsParser : IParser {
        public IParseTree ToParse(Stream stream) {
            var inputStream = new AntlrInputStream(stream);
            var lexer = new G.JsLexer(inputStream);
            var parser = new G.JsParser(new CommonTokenStream(lexer));
            return parser.program();
        }
    }
}
