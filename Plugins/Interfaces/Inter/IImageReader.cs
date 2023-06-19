using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Inter
{
    public interface IImageReader
    {
        bool CanRead(string filePath);

       // void Convert(string filePath, int Width, int Height, byte[] from_Data, out byte[] outData, out byte[] Header);

        ImagePixels Read(string sourcePath);
    }

    public class ImagePixels
    {
        public int Width;   
        public int Height;
        public Pixel[] pixels;
    }
       
        
}
