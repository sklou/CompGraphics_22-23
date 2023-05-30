using Convertor.Class;
using System;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

using PPM;
using BMP;
using Convertor.Interface;


//Convertor.exe --source=C:\Users\sssok\OneDrive\Documents\GitHub\CompGraphics_22-23\Convertor\Test_pic\test.ppm --goal-format=bmp

//Convertor.exe --source=D:\GitLab\CompGraphics_22-23\Convertor\Test_pic\test.ppm --goal-format=bmp

//Convertor.exe --source=D:\GitLab\CompGraphics_22-23\Convertor\Test_pic\test4.bmp --goal-format=ppm
class Program
{
    static void Main(string[] args)
    {
        //string sourcePath = "D:\\GitLab\\CompGraphics_22-23\\Convertor\\Test_pic\\test.ppm";
        //string goalFormat = "bmp";
        //string outputPath = null;

        string sourcePath = "D:\\GitLab\\CompGraphics_22-23\\Convertor\\Test_pic\\test.bmp";
        string goalFormat = "ppm";
        string outputPath = null;

       // string sourcePath = null;
       // string goalFormat = null;
       // string outputPath = null;


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
        if (outputPath == null)
        {
            outputPath = Path.ChangeExtension(sourcePath, goalFormat);
        }



        PPMfile ppm = new PPMfile();
        BMPfile bmp = new BMPfile();
        IImageWriter writer = new Writer();


        if (ppm.CanRead(sourcePath) == true)
            {
            ppm.Read(sourcePath, out byte[] Data, out int Width, out int Height);
            bmp.Convert(sourcePath, Height, Width, Data, out byte[] outData, out byte[] Header);
           // ppm.Convert(sourcePath, Height, Width, Data, out byte[] outData, out byte[] Header);

            writer.WriteFile(outputPath, Header, outData);

        }else if (bmp.CanRead(sourcePath) == true)
        {
            bmp.Read(sourcePath, out byte[] Data, out int Width, out int Height);
            ppm.Convert(sourcePath, Height, Width, Data, out byte[] outData, out byte[] Header);
            //bmp.Convert(sourcePath, Height, Width, Data, out byte[] outData, out byte[] Header);
            writer.WriteFile(outputPath, Header, outData);
        }
        else
        {
            Console.WriteLine("Invalid format");
        }
      


    }
}



