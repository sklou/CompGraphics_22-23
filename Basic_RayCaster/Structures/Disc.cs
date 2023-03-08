using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Structures
{
    struct Disc
     {
         public Vector position;
         public float radius_big;
         public float radius_small;

         public Disc(Vector position, float radius_big, float radius_small)
         {
             this.position = position;
             this.radius_big = radius_big;
             this.radius_small = radius_small;

         }

         public bool Intersects(Ray ray)
         {
             Vector rayOriginToSphere = position - ray.origin;
             float closest_point = Vector.Dot(rayOriginToSphere, ray.direction);
             if (closest_point < 0)
             {
                 return false;
             }
             float d2 = Vector.Dot(rayOriginToSphere, rayOriginToSphere) - closest_point * closest_point;
             if (d2 > radius_big * radius_big || d2 < radius_small*radius_small)
             {
                 return false;
             }
             return true;
         }
     }
    
 }
