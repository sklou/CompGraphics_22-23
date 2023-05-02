using Obj_reader.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obj_reader.Structures
{
    public class PpmImageWriter : IImageWriter
    {
        public void WriteImageToFile(Color[,] image, int width, int height, string outputPath)
        {
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.Write("P3\n");
                writer.Write(width);
                writer.Write(" ");
                writer.Write(height);
                writer.Write("\n255\n");

                for (int y2 = 0; y2 < height; y2++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color color = image[x, y2];
                        writer.Write($"{color.R} {color.G} {color.B} ");
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}
