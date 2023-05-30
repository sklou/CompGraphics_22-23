
using Convertor.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Convertor.Class
{
    public class Writer : IImageWriter
    {
        public void WriteFile(string outputPath, byte[] Header, byte[] Data)
        {
            try
            {
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    file.Write(Header, 0, Header.Length);
                    file.Write(Data, 0, Data.Length);
                }
                Console.WriteLine($"Conversion complete: {outputPath}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing file: " + e.Message);
            }
        }
    }
}
