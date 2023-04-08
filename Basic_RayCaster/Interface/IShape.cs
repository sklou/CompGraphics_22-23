using Basic_RayCaster.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Interface
{
    internal interface IShape
    {
        (Vector? intersection, Vector? normal) Intersects(Ray ray);
    }
}

