using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Minifying.Abstract;

namespace Minifying.Concrete.Common {
    class ValueProvider : IValueProvider{
        Dictionary<FileType, Dictionary<string, File>> files = new Dictionary<FileType, Dictionary<string, File>>();

        public void AddFile(File file) {
            Dictionary<string, File> dic;
            if (!files.ContainsKey(file.Type)) {
                dic = new Dictionary<string, File>();
                files.Add(file.Type, dic);
            } else {
                dic = files[file.Type];
            }
            dic.Add(file.FileName, file);
        }

        public File GetFile(string fileName) {
            foreach (var item in files.Values) {
                if (item.ContainsKey(fileName)) {
                    return item[fileName];
                }
            }
            return null;
        }
        public List<File> GetFiles(FileType type) {
            if (files.ContainsKey(type)) {
                return files[type].Values.ToList();
            }
            return new List<File>();
        }

        public List<File> GetFiles() {
            return files.SelectMany(i => i.Value.Values).ToList();
        }
    }
}
