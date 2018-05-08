using Minifying.Common;
using Minifying.Concrete.Common;
using Minifying.Concrete.Common.Editors;
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

            var result = valueProvider.GetFiles().ToDictionary(k => k.FileName, v => v.Stream);

            return result;
        }
    }
}
