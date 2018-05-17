using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Abstract {
    interface IEditor {
        void ToEdit(IValueProvider valueProvider);
    }
}
