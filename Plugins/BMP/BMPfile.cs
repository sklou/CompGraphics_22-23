
using System.Text;
using Interfaces.Inter;

namespace BMP
{
    public class BMPfile : IImageReader
    {
        public bool CanRead(string filePath)
        {
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    byte[] header = new byte[2];
                    fileStream.Read(header, 0, 2);
                    if (header[0] == 0x42 && header[1] == 0x4D)
                    {
                        return true;
                    }
                    else
                    {
                        // Console.WriteLine("The file is not a BMP file."); 
                        return false;
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("The file is not a BMP file.");
                return false;
            }
        }
        public void Read(string sourcePath, out byte[] Data, out int Width, out int Height)
        {
            Data = null;
            Width = 0;
            Height = 0;
            try
            {
                using (FileStream fs = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] header = new byte[54];
                    fs.Read(header, 0, 54);
                    Width = BitConverter.ToInt32(header, 18);
                    Height = BitConverter.ToInt32(header, 22);
                    int dataSize = Width * Height * 3;
                    Data = new byte[dataSize];
                    fs.Read(Data, 0, dataSize);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading BMP file: " + ex.Message);

            }

        }





        public void Convert(string filePath, int Width, int Height, byte[] from_Data, out byte[] outData, out byte[] Header)
        {
            int width = Width;
            int height = Height;
            outData = new byte[width * height * 3];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int ppmOffset = (y * width + x) * 3;
                    int bmpOffset1 = ((height - y - 1) * width + x) * 3;
                    //  int bmpOffset1 = (y * width + x) * 3;  
                    outData[bmpOffset1 + 2] = from_Data[ppmOffset + 1];     // Blue  
                    outData[bmpOffset1] = from_Data[ppmOffset]; // Green  
                    outData[bmpOffset1 + 1] = from_Data[ppmOffset + 2];     // Red  
                }
            }
            int bmpSize = 14 + 40 + outData.Length;//BMP size static   
            int bmpOffset = 14 + 40;
            Header = new byte[] {
            0x42, 0x4D,
        (byte)(bmpSize), (byte)(bmpSize >> 8), (byte)(bmpSize >> 16), (byte)(bmpSize >> 24), 0, 0, 0, 0,
        (byte)(bmpOffset), (byte)(bmpOffset >> 8), (byte)(bmpOffset >> 16), (byte)(bmpOffset >> 24), 40, 0, 0, 0,
        (byte)(width), (byte)(width >> 8), (byte)(width >> 16), (byte)(width >> 24), // Width  
        (byte)(height), (byte)(height >> 8), (byte)(height >> 16), (byte)(height >> 24), // Height  
            1, 0, // Color Planes  
            24, 0, // Bits per Pixel  
            0, 0, 0, 0, // Compression 
        (byte)(outData.Length), (byte)(outData.Length >> 8), (byte)(outData.Length >> 16), (byte)(outData.Length >> 24),
            0, 0, 0, 0, // X Pixels per Meter  
            0, 0, 0, 0, // Y Pixels per Meter  
            0, 0, 0, 0,
            0, 0, 0, 0,
    };
        }
    }
}