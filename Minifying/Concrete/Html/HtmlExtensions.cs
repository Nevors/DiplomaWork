using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;
using static AntlrGrammars.Html.HtmlParser;

namespace Minifying.Concrete.Html {
    static class HtmlExtensions {
        public static string GetTextWithoutQuote(this string text,out bool IsHaveQuote, out char symbol) {
            symbol = '\0';
            IsHaveQuote = false;

            int index = 0;
            int lastIndex = text.Length - 1;
            if (text[index] == '"') {
                index++;
                symbol = '"';
                IsHaveQuote = true;
            } else if(text[index] == '\'') {
                index++;
                symbol = '\'';
                IsHaveQuote = true;
            }

            return text.Substring(index, lastIndex);
        }

        public static string SetQuote(this string text, char symbol) {
            return symbol + text + symbol;
        }
    }
}
