using System;
using System.Collections.Generic;
using System.Text;
using IO = System.IO;
namespace Minifying.External.Models {
    public class File {
        public String FileName { get; set; }
        public IO.Stream Stream { get; set; }
    }
}
