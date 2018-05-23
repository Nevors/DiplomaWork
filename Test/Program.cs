using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jint;
using Minifying;

namespace Test {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            string pathRoot;

            OpenFileDialog ofd = new OpenFileDialog();
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = "C:/Users/anduser/Desktop/CourseWork/ДИСК/Release/Example";
            fbd.ShowDialog();
            pathRoot = fbd.SelectedPath;
            var files = GetFiles(pathRoot);
            var result = Manager.ToMinimize(files, 0);

            foreach (var file in result) {
                Console.WriteLine(file.Key);
                Console.WriteLine(new StreamReader(file.Value).ReadToEnd());
                file.Value.Position = 0;
            }
            WriteFiles("Example_Min\\", result);
            Console.ReadKey();
        }

        static Dictionary<string, Stream> GetFiles(string pathRoot) {
            var files = new Dictionary<string, Stream>();

            void BypassFiles(string path, DirectoryInfo rootFolder) {
                foreach (var folder in rootFolder.GetDirectories()) {
                    string nextPath = Path.Combine(path, folder.Name);
                    BypassFiles(nextPath, folder);
                }
                foreach (var file in rootFolder.GetFiles()) {
                    var fs = new FileStream(file.FullName, FileMode.Open);
                    var pathFile = Path.Combine(path, file.Name);

                    files.Add(pathFile, fs);
                }
            }

            BypassFiles(Path.Combine(""), new DirectoryInfo(pathRoot));
            return files;
        }

        static void WriteFiles(string pathRoot, Dictionary<string, Stream> files) {
            Directory.CreateDirectory(pathRoot);
            foreach (var item in files) {
                string fileName = item.Key;
                int index = fileName.LastIndexOf('\\');
                if (index != -1) {
                    string path = fileName.Substring(0, index + 1);
                    Directory.CreateDirectory(Path.Combine(pathRoot, path));
                }
                var fs = new FileStream(Path.Combine(pathRoot, fileName), FileMode.Create);
                item.Value.CopyTo(fs);
                fs.Close();
            }
        }
    }
}
