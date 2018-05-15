using Minifying.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Minifying.Concrete.Common
{
    class PathProvider : IPathProvider {
        private string siteName;
        private string relativePath;
        public PathProvider(string mainPath) {
            relativePath =  Path.GetDirectoryName(mainPath);
            siteName = Path.GetPathRoot(mainPath);
        }
        public string GetPathFile(string path) {
            if(path[0] == '\\' || path[0] == '/') {
                return siteName + path;
            }
            return Path.Combine(relativePath, path);
        }
    }
}
