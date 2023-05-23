using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPM
{
    public interface IPPMInterface
    {
        bool CanRead(string filepath);

        public void ReadPPM(string sourcePath, out byte[] ppmData, out int ppmWidth, out int ppmHeight);
        void Convert_INTO_PPM(string sourcePath, int bmpHeight, int bmpWidth, byte[] bmpData, out byte[] ppmData, out byte[] ppmHeader);

        void WritePPMFile(string outputPath, byte[] ppmHeader, byte[] ppmData);
    }
}

