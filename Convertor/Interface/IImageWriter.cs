using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convertor.Interface
{
    public interface IImageWriter
    {
        public void WriteFile(string outputPath, byte[] Header, byte[] Data);

    }
}
