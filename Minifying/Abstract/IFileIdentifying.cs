using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Minifying.Abstract
{
    interface IFileIdentifying
    {
        FileType GetType(string name, Stream stream);
    }
}
