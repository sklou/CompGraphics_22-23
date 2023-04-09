using Obj_reader.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obj_reader.Interface
{
    internal interface IShape
    {
        (Vector? intersection, Vector? normal) Intersects(Ray ray);
    }
}

