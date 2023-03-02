using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace Basic_RayCaster.Structures
{
    public struct Point
{
    public double x, y, z;

    public Point(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static Vector operator -(Point p1, Point p2)
    {
        return new Vector(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z); // Вектор, який сполучає дві точки
    }

    public static Point operator +(Point p, Vector v)
    {
        return new Point(p.x + v.x, p.y + v.y, p.z + v.z); // Додавання вектора до точки
    }
}
*/
}
