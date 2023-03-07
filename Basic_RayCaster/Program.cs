﻿using Basic_RayCaster.Structures;
using Basic_RayCaster.Tests;
using NUnit.Framework;
using System;
using Test;

class Program
{
    static void Main(string[] args)
    {
        //Plane plane = new Plane(new Vector(0f, 0f, 10f), new Vector(0f, 0f, 1f));
        Disc disc= new Disc(new Vector(0f, 0f, 10f), 6f, 3f);
        Sphere sphere = new Sphere(new Vector(0f, 0f, 10f), 4f);
        Camera camera = new Camera(new Vector(0f, 0f, 0f));

        int width = 120;
        int height = 80;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Ray ray = camera.GetRayThroughPixel(x, y, width, height);
                //if (plane.Intersects(ray))
                //if (disc.Intersects(ray))
                if (sphere.Intersects(ray))               
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }

        SphereIntersectionTests intersectionTests = new SphereIntersectionTests();
        intersectionTests.SphereIntersectionTrue();
        intersectionTests.SphereIntersectionFalse();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();

        /*
        PlaneIntersectionTests intersectionTests = new PlaneIntersectionTests();
        intersectionTests.PlaneIntersectionTrue();
        intersectionTests.PlaneIntersectionFalse();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
        */
        /*
        DiscIntersectionTests intersectionTests = new DiscIntersectionTests();
        intersectionTests.DiscIntersectionTrue();
        intersectionTests.DiscIntersectionFalse();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
        */
    }
}
