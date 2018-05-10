using Minifying.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minifying.Concrete.Common {
    class NameGenerator : INameGenerator {
        private char[] posibleValueFirst;
        private char[] posibleValue;

        private IList<int> curentCode;

        public NameGenerator() {
            curentCode = new List<int>();

            var tempVal = new List<char>();

            for (char i = 'a'; i <= 'z'; i++) {
                tempVal.Add(i);
            }
            for (char i = 'A'; i <= 'Z'; i++) {
                tempVal.Add(i);
            }
            posibleValueFirst = tempVal.ToArray();

            for (char i = '0'; i <= '9'; i++) {
                tempVal.Add(i);
            }

            posibleValue = tempVal.ToArray();

            curentCode.Add(0);
        }


        public string GetNext() {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            sb.Append(posibleValueFirst[curentCode[i]]);
            i++;

            while (i < curentCode.Count) {
                sb.Append(posibleValue[curentCode[i]]);
                i++;
            }

            int cur = 0;
            if (curentCode[cur] + 1 == posibleValueFirst.Length) {
                curentCode[cur] = 0;
                cur++;
            }
            while (cur < curentCode.Count && curentCode[cur] + 1 == posibleValue.Length) {
                curentCode[cur] = 0;
                cur++;
            }
            if (cur == curentCode.Count) {
                curentCode.Add(-1);
            }
            curentCode[cur]++;

            return sb.ToString();
        }
    }
}
