using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Minifying.Abstract {
    interface IParser {
        IParseTree ToParse(Stream stream);
    }
}
