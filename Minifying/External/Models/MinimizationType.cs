using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Minifying.External.Models {
    public class MinimizationType {
        public bool IsCommonCss { get; set; }
        public bool IsCommonJs { get; set; }
        public bool IsCommentHtml { get; set; }
        public bool IsImage { get; set; }
        public bool IsUnionCssFile { get; set; }
        public bool IsUnionJsFile { get; set; }
        public bool IsLoadExtJsFile { get; set; }
        public bool IsLoadExtCssFile { get; set; }
        public bool IsNames { get; set; }

    }
}
