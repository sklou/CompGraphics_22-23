using System;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Text;
using PPM_into_BMP;
using static System.Net.Mime.MediaTypeNames;


//Convertor.exe --source=C:\Users\sssok\OneDrive\Documents\GitHub\CompGraphics_22-23\Convertor\Test_pic\test.ppm --goal-format=bmp

////Convertor.exe --source=D:\GitLab\CompGraphics_22-23\Convertor\Test_pic\test.ppm --goal-format=bmp
class Program
{
    static void Main(string[] args)
    {
        //string sourcePath = "D:\\GitLab\\CompGraphics_22-23\\Convertor\\Test_pic\\test.ppm";
       // string goalFormat = "bmp";
       // string outputPath = null;

        string sourcePath = null;
        string goalFormat = null;
        string outputPath = null;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].StartsWith("--source="))
            {
                sourcePath = args[i].Substring("--source=".Length);
            }
            else if (args[i].StartsWith("--goal-format="))
            {
                goalFormat = args[i].Substring("--goal-format=".Length);
            }
            else if (args[i].StartsWith("--output="))
            {
                outputPath = args[i].Substring("--output=".Length);
            }
            else
            {
                Console.Error.WriteLine($"Invalid argument: {args[i]}");
                return;
            }
        }
        if (sourcePath == null)
        {
            Console.Error.WriteLine("Missing --source argument");
            return;
        }
        if (goalFormat == null)
        {
            Console.Error.WriteLine("Missing --goal-format argument");
            return;
        }
        if (!goalFormat.Equals("bmp", StringComparison.OrdinalIgnoreCase))
        {
            Console.Error.WriteLine($"Invalid goal format: {goalFormat}");
            return;
        }
        if (outputPath == null)
        {
            outputPath = Path.ChangeExtension(sourcePath, ".bmp");
        }

       
        byte[] ppmData;
        int ppmWidth;
        int ppmHeight;
        try
        {
            using (FileStream ppmFile = File.OpenRead(sourcePath))
            {
                using (BinaryReader ppmReader = new BinaryReader(ppmFile))
                {
                    string ppmMagic = Encoding.ASCII.GetString(ppmReader.ReadBytes(2));
                    if (!ppmMagic.Equals("P6"))
                    {
                        Console.Error.WriteLine("Invalid PPM magic number");
                        return;
                    }
                    int ppmMaxValue = 0;
                    while (true)
                    {
                        char c = (char)ppmReader.ReadByte();
                        if (c == '#')
                        {
                            while (ppmReader.ReadByte() != '\n') ;
                        }
                        else if (char.IsDigit(c))
                        {
                            ppmMaxValue = ppmMaxValue * 10 + (c - '0');
                        }
                        else if (c == '\n')
                        {
                            break;
                        }
                        else
                        {
                            Console.Error.WriteLine("Invalid PPM header");
                            return;
                        }
                    }
                    ppmWidth = 0;
                    ppmHeight = 0;
                    while (true)
                    {
                        char c = (char)ppmReader.ReadByte();
                        if (c == '#')
                        {
                            while (ppmReader.ReadByte() != '\n') ;
                        }
                        else if (char.IsDigit(c))
                        {
                            ppmWidth = ppmWidth * 10 + (c - '0');
                        }
                        else if (c == ' ')
                        {
                            break;
                        }
                        else
                        {
                            Console.Error.WriteLine("Invalid PPM header");
                            return;
                        }
                    }
                    while (true)
                    {
                        char c = (char)ppmReader.ReadByte();
                        if (c == '#')
                        {
                            while (ppmReader.ReadByte() != '\n') ;
                        }
                        else if (char.IsDigit(c))
                        {
                            ppmHeight = ppmHeight * 10 + (c - '0');
                        }
                        else if (c == '\n')
                        {
                            break;
                        }
                        else
                        {
                            Console.Error.WriteLine("Invalid PPM header");
                            return;
                        }
                    }
                    int ppmDataSize = ppmWidth * ppmHeight * 3;
                    ppmData = new byte[ppmDataSize];
                    int bytesRead = ppmReader.Read(ppmData, 0, ppmDataSize);
                    if (bytesRead != ppmDataSize)
                    {
                        Console.Error.WriteLine("Invalid PPM data size");
                        return;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error reading PPM file: {e.Message}");
            return;
        }  

        byte[] data = new byte[ppmWidth * ppmHeight * 3];
        IImageReader reader = new PPMintoBMP();
        reader.ReadImage(sourcePath, ppmHeight, ppmWidth, ppmData, out byte[] bmpData, out byte[] bmpHeader);


        using (FileStream bmpStream = new FileStream(outputPath, FileMode.Create))
        {
            bmpStream.Write(bmpHeader, 0, bmpHeader.Length);
            bmpStream.Write(bmpData, 0, bmpData.Length);
        }
        Console.WriteLine($"Conversion complete: {outputPath}");

        
    }
}



