using System;
using System.Collections.Generic;
using System.Text;
using Minifying.External.Models;
namespace Minifying.External.Abstract {
    public interface IValueProvider {
        File GetFile(string path);
        List<File> GetFiles();
    }
}
