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
            string text = "";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            fileName = ofd.FileName;
            Manager.ToMinimize(new Dictionary<string, Stream> { { fileName, new FileStream(fileName, FileMode.Open) } },0);

            Console.ReadKey();
        }
    }
}
