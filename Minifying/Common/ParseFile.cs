using Minifying.Entities;
using System;
using System.Collections.Generic;
using IO = System.IO;
using System.Text;
using Minifying.Abstract;
using Minifying.Concrete.Css.Parsers;
using Minifying.Concrete.Js.Parsers;
using Minifying.Concrete.Html.Parsers;
using System.Diagnostics;

namespace Minifying.Common {
    class ParseFile {
        private Dictionary<FileType, IParser> fileParsers = new Dictionary<FileType, IParser> {
            { FileType.Css,new CssParser() },
            { FileType.Js,new Concrete.Js.Parsers.JsParser() },
            { FileType.Html,new HtmlParser() }
        };

        public File ToParse(string relativeName, IO.Stream stream, FileType type) {
            File result = new File {
                FileName = relativeName,
                SearchName = relativeName,
                Type = type
            };

            if (fileParsers.ContainsKey(type)) {
                result.Tree = fileParsers[type].ToParse(stream);
            } else {
                var ms = new IO.MemoryStream();
                stream.CopyTo(ms);
                ms.Position = 0;
                result.Stream = ms;
            }

            return result;
        }
    }
}
