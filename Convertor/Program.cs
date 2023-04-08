using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PpmToBmpConverter
{
    public interface IPpmInput
    {
        byte[] GetPpmData();
    }

    public class FilePpmInput : IPpmInput
    {
        private readonly string _fileName;

        public FilePpmInput(string fileName)
        {
            _fileName = fileName;
        }

        public byte[] GetPpmData()
        {
            return File.ReadAllBytes(_fileName);
        }
    }

    public class PpmImage
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] Pixels { get; set; }

        public PpmImage(byte[] data)
        {
            var header = Encoding.ASCII.GetString(data.TakeWhile(b => b != 10).ToArray());
            var dimensions = header.Split(' ').Skip(1).Select(int.Parse).ToArray();
            Width = 640;
            Height = 426;
            var pixelData = data.Skip(header.Length + 1).ToArray();
            Pixels = pixelData;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ppmInput = new FilePpmInput("D:\\GitLab\\CompGraphics_22-23\\Convertor\\Test_pic\\test_ppm.ppm");
            var ppmData = ppmInput.GetPpmData();
            var bmpData = ConvertPpmToBmp(ppmData);
            File.WriteAllBytes("D:\\GitLab\\CompGraphics_22-23\\Convertor\\Test_pic\\output.bmp", bmpData);
        }

        public static byte[] ConvertPpmToBmp(byte[] ppmData)
        {
            var image = new PpmImage(ppmData);

            var bmpData = new List<byte>();
            bmpData.AddRange(Encoding.ASCII.GetBytes("BM"));
            bmpData.AddRange(BitConverter.GetBytes(54 + image.Pixels.Length));
            bmpData.AddRange(new byte[] { 0, 0, 0, 0 });
            bmpData.AddRange(BitConverter.GetBytes(54));
            bmpData.AddRange(BitConverter.GetBytes(40));
            bmpData.AddRange(BitConverter.GetBytes(image.Width));
            bmpData.AddRange(BitConverter.GetBytes(image.Height));
            bmpData.AddRange(new byte[] { 1, 0 });
            bmpData.AddRange(new byte[] { 24, 0 });
            bmpData.AddRange(new byte[] { 0, 0, 0, 0 });
            bmpData.AddRange(BitConverter.GetBytes(image.Pixels.Length));
            bmpData.AddRange(new byte[] { 0, 0, 0, 0 });

            for (int i = 0; i < image.Pixels.Length; i += 3)
            {
                bmpData.Add(image.Pixels[i + 2]); // Blue
                bmpData.Add(image.Pixels[i + 1]); // Green
                bmpData.Add(image.Pixels[i]);     // Red
            }

            return bmpData.ToArray();
        }
    }
}
