using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Minifying.Common {
    static class ExtensionParseTree {
        public static void Replace(this ParserRuleContext parent, IParseTree cur, IParseTree node) {
            int index = parent.children.IndexOf(cur);
            parent.children[index] = node;
        }
        public static void Replace(this ParserRuleContext parent, IParseTree cur, IToken token) {
            parent.Replace(cur, new TerminalNodeImpl(token));
        }

        public static void Remove(this ParserRuleContext parent, IParseTree node) {
            parent.children.Remove(node);
        }

        public static void Insert(this ParserRuleContext parent, int index, IParseTree node) {
            parent.children.Insert(index, node);
        }

        public static void Insert(this ParserRuleContext parent, int index, IToken token) {
            parent.Insert(index, new TerminalNodeImpl(token));
        }

        public static void Remove(this ParserRuleContext node) {
            ((ParserRuleContext)node.parent).children.Remove(node);
            node.parent = null;
        }

        public static Stream GetStream(this IParseTree node) {
            var ms = new MemoryStream();
            if (node == null)
                return ms;
            var sw = new StreamWriter(ms);
            sw.Write(node.GetText());
            sw.Flush();

            ms.Position = 0;

            return ms;
        }

        public static IParseTree CloseSt<T>(this IParseTree node) where T:class{
            var parent = node.Parent;
            while(!(parent is T)) {
                parent = parent.Parent;
            }

            return parent;
        }
    }
}
