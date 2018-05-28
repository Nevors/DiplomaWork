using Minifying.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using ImageMagick;

namespace Minifying.Concrete.Common.Editors
{
    class ImageEditor : IEditor {
        public void ToEdit(IValueProvider valueProvider) {
            var images = valueProvider.GetFiles(Entities.FileType.Image);
            var optimizer = new ImageOptimizer();
            optimizer.OptimalCompression = true;
            foreach (var item in images) {
                optimizer.LosslessCompress(item.Stream);
            }
        }
    }
}
