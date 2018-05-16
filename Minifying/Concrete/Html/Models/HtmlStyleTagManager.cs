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
    class HtmlStyleTagManager {
        private readonly StyleContext styleContext;
        public File File { get; private set; }
        public HtmlStyleTagManager(StyleContext styleContext) {
            this.styleContext = styleContext;
            if (styleContext.htmlContent() is ContentContext content) {
                File = content.File;
            }
        }

        public void Init(IValueProvider valueProvider) {
            var curContent = styleContext.htmlContent();
            var stream = curContent.GetStream();

            File = ParseFile.ToParse(IO.Path.GetRandomFileName(), stream, FileType.Css);
            File.IsInternal = true;

            ContentContext content = new ContentContext(styleContext, File);
            styleContext.Replace(curContent, content);

            valueProvider.AddFile(File);
        }
    }
}
