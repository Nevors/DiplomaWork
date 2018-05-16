using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Abstract.Managers {
    abstract class StringManager {
        char symbol = '\0';

        string value;
        private readonly Func<string> getValue;
        private readonly Action<string> setValue;

        public string Value {
            get {
                return value;
            }
            set {
                this.value = value;
                if (symbol != '\0') {
                    value = symbol + value + symbol;
                }
                setValue(value);
            }
        }
        public StringManager(Func<string> getValue,Action<string> setValue) {
            string text = getValue();
            int index = 0;
            int length = text.Length - 1;
            if (text[index] == '"') {
                index++;
                length--;
                symbol = '"';
            } else if (text[index] == '\'') {
                index++;
                length--;
                symbol = '\'';
            }
            value = text.Substring(index, length);
            this.getValue = getValue;
            this.setValue = setValue;
        }
    }
}
