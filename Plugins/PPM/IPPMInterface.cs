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

    }
}

