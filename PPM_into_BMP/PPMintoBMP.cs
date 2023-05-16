

namespace PPM_into_BMP
{
    public class PPMintoBMP : IImageReader {

        public bool CanRead(string filePath)
        {

            return true;
        }

        public void ReadImage(string filePath, int ppmWidth, int ppmHeight, byte[] ppmData, out byte[] bmpData, out byte[] bmpHeader)
        {
            int width = ppmWidth;
            int height = ppmHeight;

            bmpData = new byte[width * height * 3];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int ppmOffset = (y * width + x) * 3;
                    int bmpOffset1 = ((height - y - 1) * width + x) * 3;
                    bmpData[bmpOffset1 + 2] = ppmData[ppmOffset + 1];     // Blue
                    bmpData[bmpOffset1] = ppmData[ppmOffset]; // Green
                    bmpData[bmpOffset1 + 1] = ppmData[ppmOffset + 2];     // Red
                }
            }
            int bmpSize = 14 + 40 + bmpData.Length;
            int bmpOffset = 14 + 40;
            bmpHeader = new byte[] {
            0x42, 0x4D,
        (byte)(bmpSize), (byte)(bmpSize >> 8), (byte)(bmpSize >> 16), (byte)(bmpSize >> 24),
            0, 0, 0, 0,
        (byte)(bmpOffset), (byte)(bmpOffset >> 8), (byte)(bmpOffset >> 16), (byte)(bmpOffset >> 24),
            40, 0, 0, 0,
        (byte)(width), (byte)(width >> 8), (byte)(width >> 16), (byte)(width >> 24), // Width
        (byte)(height), (byte)(height >> 8), (byte)(height >> 16), (byte)(height >> 24), // Height
            1, 0, // Color Planes
            24, 0, // Bits per Pixel
            0, 0, 0, 0, // Compression
        (byte)(bmpData.Length), (byte)(bmpData.Length >> 8), (byte)(bmpData.Length >> 16), (byte)(bmpData.Length >> 24), // Image Size
            0, 0, 0, 0, // X Pixels per Meter
            0, 0, 0, 0, // Y Pixels per Meter
            0, 0, 0, 0, // Colors Used
            0, 0, 0, 0, // Colors Important

            
    };

    }
    }
}