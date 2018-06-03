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
                string query = "";
                query += "output_format=text";
                query += "&compilation_level=SIMPLE_OPTIMIZATIONS";
                query += "&output_info=compiled_code";
                query += "&js_code=";
                sw.Write(query);
                sw.Write(text);
                sw.Flush();
                //s.Close();
            }

            var res = (HttpWebResponse)req.GetResponse();

            if (res.ContentLength == 0) {
                stream.Position = 0;
                return stream;
            }

            var streamRes = new MemoryStream();
            res.GetResponseStream().CopyTo(streamRes);

            streamRes.Position = 0;

            string str = new StreamReader(streamRes).ReadToEnd();

            char[] error = new char[5];
            
            var swRes = new StreamReader(streamRes);
            swRes.Read(error, 0, 5);
            streamRes.Position = 0;

            if (new string(error).ToUpper() == "ERROR") {
                throw new Exception(swRes.ReadToEnd());
            }
           
            return streamRes;
        }
    }
}

/*
 var ms = new MemoryStream();
            res.GetResponseStream().CopyTo(ms);
            ms.Position = 0;

            return ms;
     */
