using BMP;
using PPM;
using System;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Text;
using static System.Net.Mime.MediaTypeNames;


//Convertor.exe --source=C:\Users\sssok\OneDrive\Documents\GitHub\CompGraphics_22-23\Convertor\Test_pic\test.ppm --goal-format=bmp

//Convertor.exe --source=D:\GitLab\CompGraphics_22-23\Convertor\Test_pic\test.ppm --goal-format=bmp
class Program
{
    static void Main(string[] args)
    {
        string sourcePath = "D:\\GitLab\\CompGraphics_22-23\\Convertor\\Test_pic\\test3.ppm";
        string goalFormat = "bmp";
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
            outputPath = Path.ChangeExtension(sourcePath, ".bmp");
        }


    
        IPPMInterface ppm = new PPMfile();
        IBMPInterface bmp = new BMPfile();


        
        if(ppm.CanRead(sourcePath) == true)
        {
            ppm.ReadPPM(sourcePath, out byte[] ppmData, out int ppmWidth, out int ppmHeight);
            bmp.Conver_INTO_bmp(sourcePath, ppmHeight, ppmWidth, ppmData, out byte[] bmpData, out byte[] bmpHeader);
            bmp.WriteBMPFile(outputPath, bmpHeader, bmpData);
        }
        

       
        
    }
}



