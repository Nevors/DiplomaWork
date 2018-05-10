using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minifying;

namespace Test
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string fileName;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            fileName = ofd.FileName;
            var files = new Dictionary<string, Stream> { { fileName, new FileStream(fileName, FileMode.Open) } };
            var result = Manager.ToMinimize(files, 0);

            foreach (var file in result) {
                Console.WriteLine(file.Key);
                file.Value.Position = 0;
                Console.WriteLine(new StreamReader(file.Value).ReadToEnd());
            }
            Console.ReadKey();
        }
    }
}
