using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Minifying.External.Models {
    public class MinimizationType {
        [DispalyName("Оптимизация CSS")]
        public bool IsCommonCss { get; set; }
        [DispalyName("Оптимизация Js")]
        public bool IsCommonJs { get; set; }
        [DispalyName("Удаление комментариев HTML")]
        public bool IsCommentHtml { get; set; }
        [DispalyName("Оптимизация Изображений")]
        public bool IsImage { get; set; }
        [DispalyName("Объединение CSS-файлов (Alfa)")]
        public bool IsUnionCssFile { get; set; }
        [DispalyName("Объединение Js-файлов (Alfa)")]
        public bool IsUnionJsFile { get; set; }
        [DispalyName("Загрузка внешних CSS-файлов")]
        public bool IsLoadExtJsFile { get; set; }
        [DispalyName("Загрузка внешних Js-файлов")] 
        public bool IsLoadExtCssFile { get; set; }
        [DispalyName("Замена имен идентификаторов,классов (Alfa)")]
        public bool IsNames { get; set; }

    }
}
