using System.Text;

namespace PPM
{
    public class PPMfile : IPPMInterface

    {
        public bool CanRead(string filepath)
        {
            using (FileStream ppmFile = File.OpenRead(filepath))
            {
                using (BinaryReader ppmReader = new BinaryReader(ppmFile))
                {
                    string ppmMagic = Encoding.ASCII.GetString(ppmReader.ReadBytes(2));
                    if (!ppmMagic.Equals("P6"))
                    {
                        Console.Error.WriteLine("Invalid PPM magic number");
                        return false;
                    }
                    return true;
                }
            }
        }
        public void ReadPPM(string sourcePath, out byte[] ppmData, out int ppmWidth, out int ppmHeight)
        {
            ppmData = null;
            ppmWidth = 0;
            ppmHeight = 0;
            try
            {
                using (FileStream ppmFile = File.OpenRead(sourcePath))
                {
                    using (BinaryReader ppmReader = new BinaryReader(ppmFile))
                    {
                        string ppmMagic = Encoding.ASCII.GetString(ppmReader.ReadBytes(2));
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
            }
        }
    }
}