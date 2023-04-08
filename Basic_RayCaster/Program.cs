using Basic_RayCaster.Structures;
using Basic_RayCaster.Tests;
using NUnit.Framework;
using System;
using System.Xml.Linq;


class Program
{
    static void Main(string[] args)
    {
        //Plane plane = new Plane(new Vector(0f, 0f, 10f), new Vector(0f, 0f, 1f));
        //Disc disc = new Disc(new Vector(0f, 0f, 10f), 6f, 3f);
        // Disc disc = new Disc(new Vector(0f, 0f, 10f), new Vector(0, 0, 1), 3f);
        Camera camera = new Camera(new Vector(0f, 0f, 0f));

        Sphere sphere = new Sphere(new Vector(0f, 0f, 8f), 4f);
        Triangle triangle = new Triangle(new Vector(3f, 0f,10f), new Vector(-3f, 0f, 5f), new Vector(0f, 5f, 5f)); 
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

                //if (sphere.Intersects(ray).normal.HasValue)
                if (triangle.Intersects(ray).normal.HasValue)
                {
                    //result = Vector.Dot(sphere.Intersects(ray).normal, L);
                    result = Vector.Dot(triangle.Intersects(ray).normal, L);

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

              /*  if (plane.Intersects(ray))*/
                /*if (disc.Intersects(ray))*/
                /*if (sphere.Intersects(ray))*/
                /* if (sphere.Intersects(ray).intersection != null || sphere.Intersects(ray).normal != null)*/
               /* {
                           Console.Write("#");
                       }
                       else
                       {
                           Console.Write(" ");
                       }*/








            }
            Console.WriteLine();
        }


        SphereIntersectionTests intersectionTests = new SphereIntersectionTests();
        intersectionTests.SphereIntersectionTrue();
        intersectionTests.SphereIntersectionFalse();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();


        PlaneIntersectionTests intersectionTestsS = new PlaneIntersectionTests();
        intersectionTestsS.PlaneIntersectionTrue();
        intersectionTestsS.PlaneIntersectionFalse();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();


       /* DiscIntersectionTests intersectionTest = new DiscIntersectionTests();
        intersectionTest.DiscIntersectionTrue();
        intersectionTest.DiscIntersectionFalse();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();*/

    }
}
