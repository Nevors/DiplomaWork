using Minifying.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Minifying.Concrete.Common {
    class PathProvider : IPathProvider {
        private string siteName;
        private string relativePath;
        public PathProvider(string mainPath) {
            int index = mainPath.LastIndexOf('\\');
            if (index != -1) {
                relativePath = mainPath.Substring(0, index + 1);
            } else {
                relativePath = "";
            }
            siteName = Path.GetPathRoot(mainPath);
        }
        public string GetPathFile(string path) {
            path = path.Replace('/', '\\');
            if (path[0] == '/') {
                return siteName + path;
            }
            var relativePath = this.relativePath;
            if (relativePath.Length != 0) {
                relativePath = relativePath.Substring(0, this.relativePath.Length - 1);
                while (path.StartsWith("..")) {
                    path = path.Substring(3);
                    var indexInPath = relativePath.LastIndexOf('\\');
                    relativePath = relativePath.Substring(0, indexInPath);
                }
            }
            return Path.Combine(relativePath, path);
        }
    }
}
