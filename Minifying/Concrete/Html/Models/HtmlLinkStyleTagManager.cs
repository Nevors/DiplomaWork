using Minifying.Abstract;
using Minifying.Common;
using Minifying.Concrete.Html.Models.Contexts;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static AntlrGrammars.Html.HtmlParser;
using IO = System.IO;
using System.Linq;

namespace Minifying.Concrete.Html.Models {
    class HtmlLinkStyleTagManager {
        private readonly HtmlElementContext htmlElement;
        public File File { get; private set; }
        public HtmlLinkStyleTagManager(HtmlElementContext htmlElement) {
            this.htmlElement = htmlElement;
            var buffer = htmlElement.GetRuleContext<BufferValueContext<File>>(0);
            if (buffer != null) {
                File = buffer.Value;
            }
        }

        public void Init(IValueProvider valueProvider, IPathProvider pathProvider) {
            var needAttr = htmlElement.htmlAttribute()
                .ToDictionary(
                    key => key.htmlAttributeName().TAG_NAME().GetText().ToUpper(),
                    value => new HtmlAttributeManager(value).Value
                );
            if (needAttr["REL"]?.ToUpper() != "STYLESHEET") {
                return;
            }

            var fileName = needAttr["HREF"];
            if (fileName == null) { return; }

            string path = pathProvider.GetPathFile(fileName);
            File = valueProvider.GetFile(path);
            if (File == null) { return; }

            htmlElement.AddChild(new BufferValueContext<File>(htmlElement, File));
        }
    }
}
