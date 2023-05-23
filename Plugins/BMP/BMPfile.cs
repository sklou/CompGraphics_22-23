﻿using System.Text;

namespace BMP
{
    public class BMPfile : IBMPInterface
    {
        public bool CanRead(string filePath)
        {
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    byte[] header = new byte[2];
                    fileStream.Read(header, 0, 2);
                    if(header[0] == 0x42 && header[1] == 0x4D)
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


        public void ReadBMP(string sourcePath, out byte[] bmpData, out int bmpWidth, out int bmpHeight)
        {
            bmpData = null;
            bmpWidth = 0;
            bmpHeight = 0;

            try
            {
                using (FileStream fs = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] header = new byte[54];
                    fs.Read(header, 0, 54);

                    bmpWidth = BitConverter.ToInt32(header, 18);
                    bmpHeight = BitConverter.ToInt32(header, 22);

                    int dataSize = bmpWidth * bmpHeight * 3; 

                    bmpData = new byte[dataSize];
                    fs.Read(bmpData, 0, dataSize);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading BMP file: " + ex.Message);
            }
        }


        public void Conver_INTO_bmp(string filePath, int ppmWidth, int ppmHeight, byte[] ppmData, out byte[] bmpData, out byte[] bmpHeader)
        {
            int width = ppmWidth;
            int height = ppmHeight;
            bmpData = new byte[width * height * 3];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int ppmOffset = (y * width + x) * 3;
                    int bmpOffset1 = ((height - y - 1) * width + x) * 3;
                    //  int bmpOffset1 = (y * width + x) * 3; 
                    bmpData[bmpOffset1 + 2] = ppmData[ppmOffset + 1];     // Blue 
                    bmpData[bmpOffset1] = ppmData[ppmOffset]; // Green 
                    bmpData[bmpOffset1 + 1] = ppmData[ppmOffset + 2];     // Red 
                }
            }
            int bmpSize = 14 + 40 + bmpData.Length;//BMP size static  
            int bmpOffset = 14 + 40;
            bmpHeader = new byte[] {
            0x42, 0x4D,
        (byte)(bmpSize), (byte)(bmpSize >> 8), (byte)(bmpSize >> 16), (byte)(bmpSize >> 24), 0, 0, 0, 0,
        (byte)(bmpOffset), (byte)(bmpOffset >> 8), (byte)(bmpOffset >> 16), (byte)(bmpOffset >> 24), 40, 0, 0, 0,
        (byte)(width), (byte)(width >> 8), (byte)(width >> 16), (byte)(width >> 24), // Width 
        (byte)(height), (byte)(height >> 8), (byte)(height >> 16), (byte)(height >> 24), // Height 
            1, 0, // Color Planes 
            24, 0, // Bits per Pixel 
            0, 0, 0, 0, // Compression 
        (byte)(bmpData.Length), (byte)(bmpData.Length >> 8), (byte)(bmpData.Length >> 16), (byte)(bmpData.Length >> 24),
            0, 0, 0, 0, // X Pixels per Meter 
            0, 0, 0, 0, // Y Pixels per Meter 
            0, 0, 0, 0,
            0, 0, 0, 0,
    };
        }
        public void WriteBMPFile(string outputPath, byte[] bmpHeader, byte[] bmpData)
        {
            try
            {
                using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
                {
                    bmpStream.Write(bmpHeader, 0, bmpHeader.Length);
                    bmpStream.Write(bmpData, 0, bmpData.Length);
                }
                Console.WriteLine($"Conversion complete: {outputPath}");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error writing BMP file: {e.Message}");
            }
        }


    }
}

