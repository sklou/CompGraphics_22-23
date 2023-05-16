using System;
using System.IO;
using System.Reflection;
using PPM_into_BMP;

namespace PPM_into_BMP;

public interface IImageReader
{
    bool CanRead(string filePath);
    void ReadImage(string filePath, int ppmWidth, int ppmHeight, byte[] ppmData, out byte[] bmpData, out byte[] bmpHeader);
}
