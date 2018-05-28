using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using static AntlrGrammars.Html.HtmlParser;

namespace Minifying.Concrete.Html.Models {
    class HtmlElemFactory {
        public HtmlAttributeContext CreateAttribute(ParserRuleContext parent, string name, string value, char symbol = '"') {
            HtmlAttributeContext attribute = new HtmlAttributeContext(parent, 0);

            HtmlAttributeNameContext attributeName = new HtmlAttributeNameContext(attribute, 0);
            attributeName.AddChild(new CommonToken(TAG_NAME, name));

            HtmlAttributeValueContext attributeValue = new HtmlAttributeValueContext(attribute, 0);
            attributeValue.AddChild(new CommonToken(ATTVALUE_VALUE, symbol + value + symbol));

            attribute.AddChild(attributeName);
            attribute.AddChild(new CommonToken(TAG_EQUALS, "="));
            attribute.AddChild(attributeValue);

            return attribute;
        }

        public HtmlTagNameContext CreateTagName(ParserRuleContext parent, string name) {
            HtmlTagNameContext tagName = new HtmlTagNameContext(parent,0);
            tagName.AddChild(new CommonToken(TAG_NAME, name));
            return tagName;
        }
    }
}
