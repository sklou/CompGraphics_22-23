using System;
using System.IO;
using System.Reflection;
using PPM_into_BMP;

namespace laba2;

public interface IImageReader
{
    bool CanRead(string filePath);
    void ReadImage(string filePath);
}

class Reader
{
    static void Main(string[] args)
    {
        string pluginPath = "plugins/Reader.PPM.dll"; // Путь к .dll файлу

        // Загрузка сборки из .dll файла
        Assembly assembly = Assembly.LoadFrom(pluginPath);

        // Поиск типа, реализующего интерфейс IImageReader
        Type imageReaderType = typeof(IImageReader);
        Type readerImplementationType = assembly.GetTypes()
            .FirstOrDefault(t => imageReaderType.IsAssignableFrom(t));

        if (readerImplementationType != null)
        {
            // Создание экземпляра класса, реализующего IImageReader
            object readerInstance = Activator.CreateInstance(readerImplementationType);

            // Вызов метода CanRead
            MethodInfo canReadMethod = readerImplementationType.GetMethod("CanRead");
            bool canRead = (bool)canReadMethod.Invoke(readerInstance, new object[] { "path/to/image.ppm" });

            if (canRead)
            {
                // Вызов метода ReadImage
                MethodInfo readImageMethod = readerImplementationType.GetMethod("ReadImage");
                readImageMethod.Invoke(readerInstance, new object[] { "path/to/image.ppm" });
            }
            else
            {
                Console.WriteLine("Image format not supported by the reader.");
            }
        }
        else
        {
            Console.WriteLine("No suitable image reader found in the plugin.");
        }
    }
}
