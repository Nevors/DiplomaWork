using Jint;
using System;
using System.IO;
using System.Net;
using System.Web;

namespace JsOptimizer
{
    public class Jso
    {
        /*Engine engine;
        public Jso() {
            engine = new Engine();
            var text = Properties.Resources.ResourceManager.GetString("jso");
            engine.Execute(text);
        }

        public string ToOptimize(string text) {
            return engine
                .SetValue("text", text)
                .Execute("jso({compilationLevel:'SIMPLE',jsCode:text});")
                .GetCompletionValue()
                .AsString();
        }*/
        public Stream ToOptimize(Stream stream) {
            HttpWebRequest req = WebRequest.Create("https://closure-compiler.appspot.com/compile") as HttpWebRequest;

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            var text = new StreamReader(stream).ReadToEnd();

            using (var s = req.GetRequestStream()) {
                var sw = new StreamWriter(s);
                sw.Write("compilation_level=ADVANCED_OPTIMIZATIONS&output_format=text&output_info=compiled_code&js_code=");
                sw.Write(text);
                sw.Flush();
                s.Close();
            }

            var res = req.GetResponse();
            return res.GetResponseStream();
        }
    }
}

/*
 var ms = new MemoryStream();
            res.GetResponseStream().CopyTo(ms);
            ms.Position = 0;

            return ms;
     */
