using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obj_reader.Interface
{
    public interface IImageWriter
    {
        void WriteImageToFile(Color[,] image, int width, int height, string outputPath);
    }
}

