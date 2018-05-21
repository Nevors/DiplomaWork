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
    static class ParseFile {
        private static Dictionary<FileType, IParser> fileParsers = new Dictionary<FileType, IParser> {
            { FileType.Css,new CssParser() },
            { FileType.Js,new Concrete.Js.Parsers.JsParser() },
            { FileType.Html,new HtmlParser() }
        };

        public static File ToParse(string relativeName, IO.Stream stream, FileType type = FileType.None) {
            if(type == FileType.None) {
                type = GetFileType(relativeName);
            }

            File result = new File {
                FileName = relativeName,
                Type = type
            };

            if (fileParsers.ContainsKey(type)) {
                result.Tree = fileParsers[type].ToParse(stream);
            }

            result.Stream = stream;
            
            return result;
        }

        static public FileType GetFileType(string name) {
            int indexPoint = name.LastIndexOf('.');
            if (indexPoint == -1) {
                return FileType.None;
            }

            string typeS = name.Substring(indexPoint + 1);
            switch (typeS.ToUpper()) {
                case "CSS":
                    return FileType.Css;
                case "HTML":
                    return FileType.Html;
                case "JS":
                    return FileType.Js;
                default:
                    return FileType.None;
            }
        }
    }
}
