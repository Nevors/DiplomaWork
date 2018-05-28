using Minifying.Abstract;
using Minifying.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Minifying.Concrete.Common
{
    class FileIdentifying : IFileIdentifying {
        public FileType GetType(string name, Stream stream) {
            int indexPoint = name.LastIndexOf('.');
            if (indexPoint == -1) {
                return FileType.None;
            }

            string typeS = name.Substring(indexPoint + 1);
            switch (typeS.ToUpper()) {
                case "CSS":
                    return FileType.Css;
                case "HTML":
                    return FileType.Html;
                case "JS":
                    return FileType.Js;
                case "JPEG":
                    return FileType.Image;
                case "JPG":
                    return FileType.Image;
                case "PNG":
                    return FileType.Image;
                case "BMP":
                    return FileType.Image;
                case "GIF":
                    return FileType.Image;
                default:
                    return FileType.None;
            }
        }
    }
}
