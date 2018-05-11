using System;
using System.Collections.Generic;
using System.Text;
using static AntlrGrammars.Html.HtmlParser;
using Antlr4.Runtime;
using Minifying.Entities;

namespace Minifying.Concrete.Html.Models.Contexts {
    class InternalScriptContext : ScriptContext {
        public File File { get; }
        public ScriptContext Script { get; }

        public InternalScriptContext(ParserRuleContext parent, int invokingState, File file, ScriptContext script) : base(parent, invokingState) {
            File = file;
            Script = script;
        }
    }
}
