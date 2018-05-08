using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Abstract {
    public interface IFactoryNames {
        string GetShortName(string name);
        bool ContainsName(string name);
    }
}
