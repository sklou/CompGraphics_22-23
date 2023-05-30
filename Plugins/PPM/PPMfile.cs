
using System.Text;
using Interfaces.Inter;


namespace PPM

{

	public class PPMfile : IImageReader
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

						// Console.Error.WriteLine("Invalid PPM magic number"); 

						return false;

					}

					return true;

				}

			}

		}



		public void Convert(string sourcePath, int Height, int Width, byte[] from_Data, out byte[] outData, out byte[] Header)

		{



			outData = null;

			Header = null;





			try

			{

				string header = $"P6\n{Width} {Height}\n255\n";

				Header = Encoding.ASCII.GetBytes(header);



				int bmpDataSize = Width * Height * 3;

				outData = new byte[bmpDataSize];

				for (int y = 0; y < Height; y++)

				{

					for (int x = 0; x < Width; x++)

					{

						int bmpIndex = ((Height - y - 1) * Width + x) * 3;

						int ppmIndex = (y * Width + x) * 3;



						outData[ppmIndex + 2] = from_Data[bmpIndex];      //B 

						outData[ppmIndex + 1] = from_Data[bmpIndex + 1];              //G 

						outData[ppmIndex] = from_Data[bmpIndex + 2];      //R 

					}

				}



			}

			catch (Exception ex)

			{

				Console.WriteLine("Error converting BMP to PPM: " + ex.Message);

			}

		}



		public void Read(string sourcePath, out byte[] Data, out int Width, out int Height)

		{

			Data = null;

			Width = 0;

			Height = 0;

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

						Width = 0;

						Height = 0;

						while (true)

						{

							char c = (char)ppmReader.ReadByte();

							if (c == '#')

							{

								while (ppmReader.ReadByte() != '\n') ;

							}

							else if (char.IsDigit(c))

							{

								Width = Width * 10 + (c - '0');

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

								Height = Height * 10 + (c - '0');

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

						int ppmDataSize = Width * Height * 3;

						Data = new byte[ppmDataSize];

						int bytesRead = ppmReader.Read(Data, 0, ppmDataSize);

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

