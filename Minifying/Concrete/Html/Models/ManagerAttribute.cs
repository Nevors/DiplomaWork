using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Common;

namespace Minifying.Concrete.Html.Models {
    class ManagerAttribute {
        private readonly HtmlParser.HtmlAttributeContext context;
        private readonly HtmlParser.HtmlAttributeValueContext contextValue;
        private readonly ITerminalNode contextValueToken;

        private char symbol = '\0';

        private string value;

        public string Value {
            get {
                return value;
            }
            set {
                this.value = value;
                if (symbol == '\0') {
                    value = symbol + value + symbol;
                }
                var newNode = new CommonToken(HtmlParser.ATTVALUE_VALUE, value);
                contextValue.Replace(contextValueToken, newNode);
            }
        }

        public ManagerAttribute(HtmlParser.HtmlAttributeContext context) {
            this.context = context;
            contextValue = context.htmlAttributeValue();
            contextValueToken = contextValue.ATTVALUE_VALUE();
            initValue();
        }

        private void initValue() {
            string text = contextValueToken.Symbol.Text;
            int index = 0;
            int length = text.Length - 1;
            if (text[index] == '"') {
                index++;
                length--;
                symbol = '"';
            } else if (text[index] == '\'') {
                index++;
                length--;
                symbol = '\'';
            }
            value = text.Substring(index, length);
        }
    }
}
