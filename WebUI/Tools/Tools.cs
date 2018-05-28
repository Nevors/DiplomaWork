using System;
using System.Text;

namespace WebUI.Tools {
    static class Tools {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string GetRandomString(int length) {
            var stringBuilder = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < length; i++) {
                stringBuilder.Append(Chars[random.Next(Chars.Length - 1)]);
            }

            return stringBuilder.ToString();
        }
    }
}