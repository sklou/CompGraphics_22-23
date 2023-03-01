using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Structures
{
    public struct Ray
    {
        public Point origin;
        public Vector direction;

    public Ray(Point origin, Vector direction)
        {
            this.origin = origin;
            this.direction = direction.Normalize();
        }

        public Point GetPoint(double distance)
        {
            return origin + direction * distance;
        }
    }

    }
