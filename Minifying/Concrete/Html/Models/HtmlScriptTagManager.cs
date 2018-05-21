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
    class HtmlScriptTagManager {
        private readonly ScriptContext scriptContext;
        public File File { get; private set; }

        public HtmlScriptTagManager(ScriptContext scriptContext) {
            this.scriptContext = scriptContext;
            if (scriptContext.htmlContent() is ContentContext content) {
                File = content.File;
            } else {
                File = scriptContext.GetRuleContext<BufferValueContext<File>>(0)?.Value;
            }
        }

        public void Init(IValueProvider valueProvider, IPathProvider pathProvider) {
            var curContent = scriptContext.htmlContent();
            foreach (var attr in scriptContext.htmlAttribute()) {
                if (attr.htmlAttributeName().TAG_NAME().GetText().ToUpper() == "SRC") {
                    HtmlAttributeManager manager = new HtmlAttributeManager(attr);
                    string path = pathProvider.GetPathFile(manager.Value);
                    File = valueProvider.GetFile(path);
                    if (File == null) { return; }
                }
            }
            if (curContent == null) { return; }
            HtmlContentContext content = null;
            if (File == null) {
                var stream = curContent.GetStream();
                File = ParseFile.ToParse(IO.Path.GetRandomFileName(), stream, FileType.Js);
                File.IsInternal = true;
                valueProvider.AddFile(File);

                content = new ContentContext(scriptContext, File);
            } else {
                BufferValueContext<File> bufferValue = new BufferValueContext<File>(scriptContext, File);
                scriptContext.AddChild(bufferValue);
                content = new HtmlContentContext(scriptContext, 0);
                
            }
            scriptContext.Replace(curContent, content);
        }
    }
}
