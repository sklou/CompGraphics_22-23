using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ImageConverter
{
    public interface IImageReader
    {
        bool CanRead(string filePath);
        void ReadImage(string filePath);
    }

    public interface IImageWriter
    {
        string FileExtension { get; }
        void WriteImage(string filePath);
    }

    public class ImagePluginFactory
    {
        private const string PluginsDirectory = "plugins";

        public static IEnumerable<IImageReader> GetImageReaders()
        {
            List<IImageReader> imageReaders = new List<IImageReader>();
            foreach (string pluginPath in GetPluginPaths())
            {
                Assembly assembly = Assembly.LoadFrom(pluginPath);
                foreach (Type type in assembly.GetExportedTypes())
                {
                    if (typeof(IImageReader).IsAssignableFrom(type) && !type.IsInterface)
                    {
                        IImageReader reader = Activator.CreateInstance(type) as IImageReader;
                        if (reader != null)
                            imageReaders.Add(reader);
                    }
                }
            }
            return imageReaders;
        }

        public static IEnumerable<IImageWriter> GetImageWriters()
        {
            List<IImageWriter> imageWriters = new List<IImageWriter>();
            foreach (string pluginPath in GetPluginPaths())
            {
                Assembly assembly = Assembly.LoadFrom(pluginPath);
                foreach (Type type in assembly.GetExportedTypes())
                {
                    if (typeof(IImageWriter).IsAssignableFrom(type) && !type.IsInterface)
                    {
                        IImageWriter writer = Activator.CreateInstance(type) as IImageWriter;
                        if (writer != null)
                            imageWriters.Add(writer);
                    }
                }
            }
            return imageWriters;
        }

        private static IEnumerable<string> GetPluginPaths()
        {
            string pluginsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PluginsDirectory);
            return Directory.GetFiles(pluginsDirectory, "*.dll", SearchOption.TopDirectoryOnly);
        }
    }
}
