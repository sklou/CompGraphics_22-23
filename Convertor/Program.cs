using Convertor.Class;
using System;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

using PPM;
using BMP;
using Convertor.Interface;
using System.Reflection.PortableExecutable;


//Convertor.exe --source=C:\Users\sssok\OneDrive\Documents\GitHub\CompGraphics_22-23\Convertor\Test_pic\test.ppm --goal-format=bmp



//Convertor.exe --source=D:\GitLab\CompGraphics_22-23\Convertor\Test_pic\test.ppm --goal-format=bmp --output=D:\GitLab\CompGraphics_22-23\Convertor\Test_pic\out\test.bmp
//Convertor.exe --source=D:\GitLab\CompGraphics_22-23\Convertor\Test_pic\test.ppm --goal-format=ppm --output=D:\GitLab\CompGraphics_22-23\Convertor\Test_pic\out\test.ppm       //incorect


//Convertor.exe --source=D:\GitLab\CompGraphics_22-23\Convertor\Test_pic\test4.bmp --goal-format=ppm --output=D:\GitLab\CompGraphics_22-23\Convertor\Test_pic\out\test4.ppm
//Convertor.exe --source=D:\GitLab\CompGraphics_22-23\Convertor\Test_pic\test4.bmp --goal-format=bmp --output=D:\GitLab\CompGraphics_22-23\Convertor\Test_pic\out\test4.bmp     //incorect

class Program
{
    static void Main(string[] args)
    {
       // string sourcePath = "D:\\GitLab\\CompGraphics_22-23\\Convertor\\Test_pic\\test.ppm";
       // string goalFormat = "bmp";
       // string outputPath = "D:\\GitLab\\CompGraphics_22-23\\Convertor\\Test_pic\\out\\test.bmp";

        // string sourcePath = "D:\\GitLab\\CompGraphics_22-23\\Convertor\\Test_pic\\test4.bmp";
        // string goalFormat = "ppm";
        // string outputPath = "D:\\GitLab\\CompGraphics_22-23\\Convertor\\Test_pic\\out\\test4.ppm";

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
        if (outputPath == null)
        {
            outputPath = Path.ChangeExtension(sourcePath, goalFormat);
        }



        PPMfile ppm = new PPMfile();
        BMPfile bmp = new BMPfile();
        IImageWriter writer = new Writer();

        bool ppmRead = ppm.CanRead(sourcePath);
        bool bmpRead = bmp.CanRead(sourcePath);

        if (ppmRead == true)
            {
            ppm.Read(sourcePath, out byte[] Data, out int Width, out int Height);
            if(goalFormat == "bmp")
            {
                bmp.Convert(sourcePath, Height, Width, Data, out byte[] outData, out byte[] Header);
                writer.WriteFile(outputPath, Header, outData);

            }
            if (goalFormat == "ppm")
            {
                ppm.Convert(sourcePath, Height, Width, Data, out byte[] outData, out byte[] Header);
                writer.WriteFile(outputPath, Header, outData);

            }  

        }
        else if (bmpRead == true)
        {
            bmp.Read(sourcePath, out byte[] Data, out int Width, out int Height);
            if (goalFormat == "ppm")
            {
                ppm.Convert(sourcePath, Height, Width, Data, out byte[] outData, out byte[] Header);
                writer.WriteFile(outputPath, Header, outData);

            }
            if (goalFormat == "bmp")
            {
                bmp.Convert(sourcePath, Height, Width, Data, out byte[] outData, out byte[] Header);
                writer.WriteFile(outputPath, Header, outData);

            }         
        }
        else
        {
            Console.WriteLine("Invalid format");
        }
        

    }
}



