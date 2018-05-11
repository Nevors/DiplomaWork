using Minifying.Common;
using Minifying.Concrete.Common;
using Minifying.Concrete.Common.Editors;
using Minifying.Concrete.Html.Editors;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Minifying {
    public static class Manager {
        public static Dictionary<string, Stream> ToMinimize(Dictionary<string, Stream> files, MinimizationType minimizations) {
            var valueProvider = new ValueProvider();
            foreach (var item in files) {
                valueProvider.AddFile(ParseFile.ToParse(item.Key, item.Value));
            }

            IdsNameEditor idsNameEditor = new IdsNameEditor();
            idsNameEditor.ToEdit(valueProvider);

            ReplaceInternalCode replaceInternalCode = new ReplaceInternalCode();
            replaceInternalCode.ToEdit(valueProvider);

            var resultFiles = valueProvider.GetFiles();
            foreach (var file in resultFiles) {
                if (file.Tree != null) {
                    Console.WriteLine(file.Tree.GetText());
                    var ms = new MemoryStream();
                    var stream = new StreamWriter(ms);
                    stream.Write(file.Tree.GetText());
                    ms.Position = 0;
                    file.Stream = ms;
                }
            }

            var result = resultFiles.ToDictionary(k => k.FileName, v => v.Stream);

            return result;
        }
    }
}
