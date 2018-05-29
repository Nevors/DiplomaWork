using JsOptimizer;
using Minifying.Abstract;
using Minifying.Common;
using Minifying.Concrete.Common;
using Minifying.Concrete.Js.Parsers;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Minifying.Concrete.Js.Models {
    class JsOptimizerFacade {
        public Stream ToOptimize(string fileName, Stream stream, List<IOutputMessage> messages) {
            var jso = new Jso();
            try {
                return  jso.ToOptimize(stream);
            } catch (Exception e) {
                string text = $"Ошибка при оптимизации файла {fileName}. Файл не будет оптимизирован.\n";
                text += e.Message;
                var mesage = new BaseOutputMessage(text);
                messages.Add(mesage);
                return null;
            }
        }
    }
}
