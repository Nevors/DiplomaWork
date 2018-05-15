using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Html.Models.Contexts {
    class BufferValueContext<T> : ParserRuleContext {
        public BufferValueContext(ParserRuleContext parent,T value) : base(parent, 0) {
            Value = value;
        }

        public T Value { get; }

    }
}
