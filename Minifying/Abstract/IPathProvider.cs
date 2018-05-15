using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Abstract {
    interface IPathProvider {
        string GetPathFile(string path);
    }
}
