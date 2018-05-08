using Minifying.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Common {
    class FactoryNames : IFactoryNames {
        private Dictionary<string, string> dict = new Dictionary<string, string>();
        private INameGenerator nameGenerator;
        public FactoryNames(INameGenerator nameGenerator) {
            this.nameGenerator = nameGenerator;
        }
        public string GetShortName(string name) {
            if (dict.ContainsKey(name)) {
                return dict[name];
            }
            string genValue = nameGenerator.GetNext();
            dict.Add(name, genValue);
            return genValue;
        }

        public bool ContainsName(string name) {
            return dict.ContainsKey(name);
        }
    }
}
