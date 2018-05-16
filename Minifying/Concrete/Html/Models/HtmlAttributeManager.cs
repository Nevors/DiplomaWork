using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Abstract.Managers;
using Minifying.Common;

namespace Minifying.Concrete.Html.Models {
    class HtmlAttributeManager : StringManager {

        public HtmlAttributeManager(HtmlParser.HtmlAttributeContext context)
            : base(
                  () => {
                      return context.htmlAttributeValue().ATTVALUE_VALUE().Symbol.Text;
                  },
                  (value) => {
                      var contextValue = context.htmlAttributeValue();
                      var newToken = new CommonToken(HtmlParser.ATTVALUE_VALUE, value);
                      contextValue.Replace(contextValue.ATTVALUE_VALUE(), newToken);
                  }) {
        }
    }
}
