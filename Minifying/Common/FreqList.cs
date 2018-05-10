using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minifying.Common {
    class FreqList<T> {
        private readonly Dictionary<T, int> freqList;

        public FreqList() {
            this.freqList = new Dictionary<T, int>();
        }

        public void Increment(T key) {
            if (freqList.ContainsKey(key)) {
                freqList[key]++;
            } else {
                freqList.Add(key, 1);
            }
        }

        public IEnumerable<T> GetOrderedList() {
            return freqList.OrderBy(i => i.Key).Select(p => p.Key);
        }
    }
}
