using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using Minifying.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using G = AntlrGrammars.Css;

namespace Minifying.Concrete.Css.Parsers
{
    class CssParser : IParser {
        public IParseTree ToParse(Stream stream) {
            var inputStream = new AntlrInputStream(stream);
            var lexer = new G.CssLexer(inputStream);
            var parser = new G.CssParser(new CommonTokenStream(lexer));
            return parser.stylesheet();
        }
    }
}
