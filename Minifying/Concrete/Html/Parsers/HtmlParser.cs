using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Minifying.Abstract;   
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using G = AntlrGrammars.Html;

namespace Minifying.Concrete.Html.Parsers {
    class HtmlParser : IParser {
        public IParseTree ToParse(Stream stream) {
            var inputStream = new AntlrInputStream(stream);
            var lexer = new G.HtmlLexer(inputStream);
            var parser = new G.HtmlParser(new CommonTokenStream(lexer));
            return parser.htmlDocument();
        }
    }
}
