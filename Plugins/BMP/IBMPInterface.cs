using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMP
{
    public interface IBMPInterface
    {
        bool CanRead(string filePath);

        void Conver_INTO_bmp(string filePath, int ppmWidth, int ppmHeight, byte[] ppmData, out byte[] bmpData, out byte[] bmpHeader);

        void WriteBMPFile(string outputPath, byte[] bmpHeader, byte[] bmpData);
    }
}
