using Obj_reader.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obj_reader.Structures
{
    struct Intersection
    {
        public bool hit; // Чи був перетин
        public Vector? normal; // Нормаль до перетину
        public float distance; // Відстань до перетину

        // Конструктор структури
        public Intersection(bool hit, Vector? normal, float distance)
        {
            this.hit = hit;
            this.normal = normal;
            this.distance = distance;
        }
    }
}
