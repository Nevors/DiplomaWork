using Minifying;
using Minifying.External.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models {
    public class Folder {
        public string Id { get; set; }
        public Dictionary<string, Stream> Files { get; set; }
        public bool IsProcessing { get; set; }
        public List<IOutputMessage> Messages { get; set; }
        public Manager Manager { get; set; }
    }
}
