using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Abstract {
    public interface IArchiveManager {
        Dictionary<string, Stream> Uncompress(Stream stream);
        Stream Compress(Dictionary<string, Stream> files);
    }
}
