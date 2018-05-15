using Minifying.Abstract;
using Minifying.Common;
using Minifying.Concrete.Html.Models.Contexts;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static AntlrGrammars.Html.HtmlParser;
using IO = System.IO;

namespace Minifying.Concrete.Html.Models {
    class StyleTagManager {
        private readonly StyleContext styleContext;
        public File File { get; }
        public StyleTagManager(StyleContext styleContext) {
            this.styleContext = styleContext;
            if (styleContext.htmlContent() is ContentContext content) {
                File = content.File;
            }
        }

        public void Init(IValueProvider valueProvider) {
            var curContent = styleContext.htmlContent();
            var ms = new IO.MemoryStream();
            new IO.StreamWriter(ms).Write(curContent.GetText());
            ms.Position = 0;
            File file = ParseFile.ToParse(IO.Path.GetRandomFileName(), ms, FileType.Css);
            ContentContext content = new ContentContext(styleContext, file);
            styleContext.Replace(curContent, content);
        }
    }
}
