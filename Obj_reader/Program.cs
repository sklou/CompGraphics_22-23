using Obj_reader.Structures;
using System;
using System.Xml.Linq;


class Program
{
    static void Main(string[] args)
    {

        Camera camera = new Camera(new Vector(0f, 0f, 0f));

        Triangle[] triangles = new Triangle[3];
        triangles[0] = new Triangle(new Vector(3f, 0f, 10f), new Vector(-3f, 0f, 5f), new Vector(0f, 5f, 5f));
        triangles[1] = new Triangle(new Vector(-3f, -5f, 10f), new Vector(3f, -5f, 5f), new Vector(0f, 0f, 5f));
        triangles[2] = new Triangle(new Vector(0f, 5f, 15f), new Vector(-3f, -5f, 15f), new Vector(3f, -5f, 15f));

        Vector L = new Vector(-3, 0, 0);
        L.Normalize();

        int width = 120;
        int height = 60;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Ray ray = camera.GetRayThroughPixel(x, y, width, height);
                float result;


                // Перевіряємо перетин з кожним трикутником
                bool hit = false;
                float minDistance = float.MaxValue; 
                Vector? normal = null; // Нормаль до перетину
                foreach (Triangle triangle in triangles)
                {
                    
                    var intersection = triangle.Intersects(ray);

                    // Якщо є точка перетину
                    if (intersection.intersection.HasValue)
                    {
                        // Обчислюємо відстань до перетину
                        Vector distance = (intersection.intersection.Value - ray.origin);
                       float dist = distance.Magnitude();

                        
                        if (dist < minDistance)
                        {
                          
                            minDistance = dist;
                            normal = intersection.normal;
                            hit = true;
                        }
                    }
                }

                // Якщо було перетин з найближчим трикутником
                if (hit)
                {
                    result = Vector.Dot(normal, L);

                    if (result < 0)
                    {
                        Console.Write(" ");
                    }
                    else if (result < 0.2)
                    {
                        Console.Write(".");
                    }
                    else if (result < 0.5)
                    {
                        Console.Write("*");
                    }
                    else if (result < 0.8)
                    {
                        Console.Write("O");
                    }
                    else
                    {
                        Console.Write("#");
                    }
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
