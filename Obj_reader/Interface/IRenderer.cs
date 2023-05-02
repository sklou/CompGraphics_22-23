using Obj_reader.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obj_reader.Interface
{
    interface IRenderer
    {
        Color[,] Render(Camera camera, Triangle[] triangles, Vector L, int width, int height);
    }
}
