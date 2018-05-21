using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Common.Models
{
    class PositionRule
    {
        public int Index { get; set; }
        public ParserRuleContext Rule { get; set; }
    }
}
