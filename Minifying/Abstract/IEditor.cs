using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Abstract {
    public interface IEditor {
        void ToEdit(IValueProvider valueProvider);
    }
}
