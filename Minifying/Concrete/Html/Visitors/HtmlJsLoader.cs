using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using AntlrGrammars.Html;
using Minifying.Abstract;
using Minifying.Common;
using Minifying.Concrete.Html.Models;
using Minifying.Concrete.Js.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Minifying.Concrete.Html.Visitors {
    class HtmlJsLoader {
        public void Replace(IParseTree tree, IValueProvider valueProvider) {
            new Visitor(valueProvider).Visit(tree);
        }

        class Visitor : HtmlParserBaseVisitor<object> {
            private readonly IValueProvider valueProvider;

            public Visitor(IValueProvider valueProvider) {
                this.valueProvider = valueProvider;
            }

            public override object VisitScript([NotNull] HtmlParser.ScriptContext context) {
                foreach (var item in context.htmlAttribute()) {
                    var name = item.htmlAttributeName().TAG_NAME().GetText();
                    if (name.ToUpper() != "SRC") {
                        continue;
                    }
                    var manager = new HtmlAttributeManager(item);
                    var path = manager.Value;
                    Entities.File searchFile = valueProvider.GetFile(path);
                    if (searchFile != null) {
                        if (searchFile.IsExternal) {
                            manager.Value = searchFile.FileName;
                        }
                        return null;
                    }

                    HttpWebRequest req = WebRequest.CreateHttp(path);
                    req.Method = "GET";
                    try {
                        var result = req.GetResponse();
                        var fileName = Path.GetRandomFileName()+".js";
                        Entities.File file = new ParseFile().ToParse(fileName, result.GetResponseStream(), Entities.FileType.Js);
                        valueProvider.AddFile(file);

                        file = new Entities.File {
                            Type = file.Type,
                            FileName = file.FileName,
                            SearchName = path,
                            IsExternal = true,
                        };
                        valueProvider.AddFile(file);

                        manager.Value = fileName;
                    } catch {
                        return null;
                    }
                }
                return null;
            }
        }
    }
}
