using Minifying.Abstract;
using Minifying.Common;
using Minifying.Concrete.Html.Models.Contexts;
using Minifying.Entities;
using System;
using IO = System.IO;
using System.Collections.Generic;
using System.Text;
using static AntlrGrammars.Html.HtmlParser;
namespace Minifying.Concrete.Html.Models {
    class ScriptTagManager {
        private readonly ScriptContext scriptContext;
        public File File { get; }

        public ScriptTagManager(ScriptContext scriptContext) {
            this.scriptContext = scriptContext;
            if(scriptContext.htmlContent() is ContentContext content) {
                File = content.File;
            }
        }

        public void Init(IValueProvider valueProvider, IPathProvider pathProvider) {
            File file = null;
            var curContent = scriptContext.htmlContent();
            foreach (var attr in scriptContext.htmlAttribute()) {
                if(attr.htmlAttributeName().TAG_NAME().GetText().ToUpper() == "SRC") {
                    AttributeManager manager = new AttributeManager(attr);
                    string path = pathProvider.GetPathFile(manager.Value);
                    file = valueProvider.GetFile(path);
                }
            }
            if(file == null) {
                var ms = new IO.MemoryStream();
                new IO.StreamWriter(ms).Write(curContent.GetText());
                ms.Position = 0;
                file = ParseFile.ToParse(IO.Path.GetRandomFileName(), ms, FileType.Js);
            }
            ContentContext content = new ContentContext(scriptContext, file);
            scriptContext.Replace(curContent, content);
        }
    }
}
