using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.External.Models {
    class DispalyNameAttribute : Attribute {
        private readonly string name;

        public DispalyNameAttribute(string name) {
            this.name = name;
        }
        public override string ToString() {
            return name;
        }
    }
}
