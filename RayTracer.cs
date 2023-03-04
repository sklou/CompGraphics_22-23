using System;

class Program
{
    static void Main(string[] args)
    {
        // Константний вектор L, що визначає напрямок падіння світла.
        Vector L = new Vector(0, 0, -1);

        // Загальні параметри сцени.
        Vector camera_pos = new Vector(0, 0, 0);
        Sphere[] objects = {sphere1, sphere2, cube1, ...}; // список об'єктів на сцені

        // Цикл по кожному пікселю екрану.
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 20; x++)
            {
                // Створити промінь з камери до пікселя на екрані.
                Vector pixel_pos = new Vector(x, y, 1);
                Vector ray_dir = (pixel_pos - camera_pos).Normalized();

                // Шукаємо перетин з об'єктами.
                bool hit = false;
                Vector hit_pos = null;
                Vector hit_normal = null;
                double t_min = double.MaxValue;

                foreach (Sphere obj in objects)
                {
                    double t = obj.Intersect(ray_dir, camera_pos);
                    if (t > 0 && t < t_min)
                    {
                        hit = true;
                        t_min = t;
                        hit_pos = camera_pos + ray_dir * t;
                        hit_normal = (hit_pos - obj.Center).Normalized();
                    }
                }

                // Вивід в консоль залежно від результатів перетину.
                if (hit)
                {
                    double dot_product = Vector.Dot(L, hit_normal);
                    if (dot_product < 0)
                        Console.Write(" ");
                    else if (dot_product > 0 && dot_product < 0.2)
                        Console.Write(".");
                    else if (dot_product >= 0.2 && dot_product < 0.5)
                        Console.Write("*");
                    else if (dot_product >= 0.5 && dot_product < 0.8)
                        Console.Write("O");
                    else
                        Console.Write("#");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }
}

class Vector
{
    public double x, y, z;

    public Vector(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public double Magnitude()
    {
        return Math.Sqrt(x * x + y * y + z * z);
    }

    public Vector Normalized()
    {
        double mag = Magnitude();
        return new Vector(x / mag, y / mag, z / mag);
    }

    public static Vector operator +(Vector a, Vector b)
    {
        return new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static Vector operator -(Vector a, Vector b)
    {
        return new Vector(a.x - b.x, a.y - b)
    }