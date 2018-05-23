using Jint;
using System;
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
        public string ToOptimize(string text) {
            WebClient web = new WebClient();
            web.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            string q = "compilation_level=WHITESPACE_ONLY&output_format=text&output_info=compiled_code&js_code=";
            q += HttpUtility.UrlEncode(text);
            var result = web.UploadString("https://closure-compiler.appspot.com/compile", "POST", q);
            return result;
        }
    }
}
