using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Minifying.Entities {
    class File {
        public IParseTree Tree { get; set; }
        public string FileName { get; set; }
        public string SearchName { get; set; }
        public FileType Type { get; set; }
        public bool IsInternal { get; set; }
        public bool IsExternal { get; set; }
        public Stream Stream { get; set; }
    }
}
