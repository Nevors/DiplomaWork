using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Abstract;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace WebUI.Concrete {
    public class ArchiveManager : IArchiveManager {
        public Stream Compress(Dictionary<string, Stream> files) {
            var zip = ArchiveFactory.Create(ArchiveType.Zip);
            foreach (var item in files) {
                zip.AddEntry(item.Key, item.Value, false);
            }
            
            var ms = new MemoryStream();
            zip.SaveTo(ms, CompressionType.Deflate);

            ms.Position = 0;
            return ms;
        }

        public Dictionary<string, Stream> Uncompress(Stream stream) {
            var archive = ArchiveFactory.Open(stream);
            Dictionary<string, Stream> files = new Dictionary<string, Stream>();

            foreach (var entry in archive.Entries.Where(e => !e.IsDirectory)) {
                var ms = new MemoryStream();
                entry.WriteTo(ms);
                ms.Position = 0;
                files.Add(entry.Key, ms);
            }

            archive.Dispose();

            return files;
        }
    }
}
