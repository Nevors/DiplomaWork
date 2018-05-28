﻿using Minifying.Common;
using Minifying.Concrete.Common;
using Minifying.Concrete.Common.Editors;
using Minifying.Concrete.Html.Editors;
using Entities = Minifying.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Minifying.Concrete.Js.Editors;
using System.Threading.Tasks;
using Minifying.Concrete.Css.Editors;
using Minifying.External.Abstract;
using Minifying.External.Models;

namespace Minifying {
    public class Manager {
        Dictionary<string, Stream> files;
        ValueProvider valueProvider = new ValueProvider();
        FileIdentifying fileIdentifying = new FileIdentifying();
        ParseFile parseFile = new ParseFile();

        public Manager(Dictionary<string, Stream> files) {
            this.files = files;
        }

        public List<IOutputMessage> ToMinimize(MinimizationType min) {
            valueProvider = new ValueProvider();

            var csso = new CssOptimizer.Csso();
            var jso = new JsOptimizer.Jso();
            
            Parallel.ForEach(files, item => {
                var type = fileIdentifying.GetType(item.Key, item.Value);
                Stream stream = item.Value;

                if (type == Entities.FileType.Css
                    && min.IsCommonCss) {
                    stream = csso.ToOptimize(item.Value);
                } else if (type == Entities.FileType.Js
                             && min.IsCommonJs) {
                    stream = jso.ToOptimize(item.Value);
                }

                valueProvider.AddFile(parseFile.ToParse(item.Key, stream, type));
            });

            List<Abstract.IEditor> listOpt = new List<Abstract.IEditor>();

            if (min.IsLoadExtJsFile) {
                listOpt.Add(new HtmlLoadExtJsEditor());
            }
           
            listOpt.Add(new HtmlInternalContentEditor());

            if (min.IsUnionJsFile) {
                listOpt.Add(new HtmlUnionJsEditor());
            }

            if (min.IsUnionCssFile) {
                listOpt.Add(new HtmlUnionCssEditor());
            }

            listOpt.Add(new JsWsSymbolsEditor());
            listOpt.Add(new JsEofTokenRemover());
            listOpt.Add(new HtmlWsSymbolsEditor());

            if (min.IsCommentHtml) {
                listOpt.Add(new HtmlCommentEditor());
            }

            if (min.IsNames) {
                listOpt.Add(new NamesEditor());
            }

            if (min.IsCommonJs) {
                listOpt.Add(new JsMinifyingEditor());
                if (min.IsUnionJsFile) {
                    listOpt.Add(new JsMinifyingEditor(false));
                }
            }

            if (min.IsCommonCss) {
                listOpt.Add(new CssMinifyingEditor());
                if (min.IsUnionCssFile) {
                    listOpt.Add(new CssMinifyingEditor(false));
                }
            }

            if (min.IsImage) {
                listOpt.Add(new ImageEditor());
            }

            listOpt.ForEach(i => i.ToEdit(valueProvider));

            var message = new BaseOutputMessage("ТЕСТИРОВАНИЕ");
            message.AddAction(AnswerType.Ok, () => {
                Console.WriteLine("ТЕСТИРОВАНИЕ ОК!!!!");
            });
            message.AddAction(AnswerType.No, () => {
                Console.WriteLine("ТЕСТИРОВАНИЕ NO!!!!");
            });
            valueProvider.GetOutputMessages().Add(message);

            message = new BaseOutputMessage("ТЕСТИРОВАНИЕ");
            message.AddAction(AnswerType.Ok, () => {
                Console.WriteLine("ТЕСТИРОВАНИЕ ОК!!!!");
            });
            message.AddAction(AnswerType.No, () => {
                Console.WriteLine("ТЕСТИРОВАНИЕ NO!!!!");
            });

            valueProvider.GetOutputMessages().Add(message);

            var list = new List<IOutputMessage>();

            list.AddRange(valueProvider.GetOutputMessages());

            return list;
        }

        public Dictionary<string, Stream> GetFiles(List<Answer<AnswerType>> answers) {
            var list = valueProvider.GetOutputMessages();

            foreach (var answer in answers) {
                if(answer.Id >= list.Count) {
                    continue;
                }
                list[answer.Id].ExecuteAction(answer.Value);
            }

            var resultFiles = valueProvider.GetFiles().Where(f => !f.IsInternal);

            foreach (var file in resultFiles) {
                if (file.Tree != null) {
                    file.Stream = file.Tree.GetStream();
                }
            }

            return resultFiles.ToDictionary(k => k.FileName, v => v.Stream);
        }
    }
}
