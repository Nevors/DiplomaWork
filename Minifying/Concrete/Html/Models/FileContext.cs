using Antlr4.Runtime;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Html.Models
{
    class FileContext
    {
        public File File { get; set; }
        public ParserRuleContext Context { get; set; }

        public override bool Equals(object obj) {
            return File == (obj as FileContext).File;
        }

        public override int GetHashCode() {
            return File.GetHashCode();
        }
    }
}
