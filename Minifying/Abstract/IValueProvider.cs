using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Abstract {
    public interface IValueProvider {
        void AddFile(File file);
        File GetFile(string fileName);
        List<File> GetFiles(FileType type);
        List<File> GetFiles();
    }
}
