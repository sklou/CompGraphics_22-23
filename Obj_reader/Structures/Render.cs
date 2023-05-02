using Obj_reader.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obj_reader.Structures
{
    public class Render:IRenderer
    {
        Color[,] IRenderer.Render(Camera camera, Triangle[] triangles, Vector L, int width, int height)
        {
            Color[,] image = new Color[width, height * 3];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Ray ray = camera.GetRayThroughPixel(x, y, width, height);
                    float result;


                    bool hit = false;
                    float minDistance = float.MaxValue;
                    Vector? intersectionPoint = null; // Точка перетину
                    Vector? normal = null; // Нормаль до перетину
                    foreach (Triangle triangle in triangles)
                    {
                        var intersection = triangle.Intersects(ray);


                        if (intersection.intersection.HasValue)
                        {

                            Vector distance = (intersection.intersection.Value - ray.origin);
                            float dist = distance.Magnitude();

                            if (dist < minDistance)
                            {
                                minDistance = dist;
                                intersectionPoint = intersection.intersection.Value;
                                normal = intersection.normal;
                                hit = true;
                            }
                        }
                    }

                    bool inShadow = false;

                    // Якщо було перетин з найближчим трикутником
                    if (hit)
                    {
                        //новий промінь
                        Vector lightDirection = (L - intersectionPoint.Value);
                        lightDirection.Normalize();

                        Ray shadowRay = new Ray(intersectionPoint.Value + 0.001f * lightDirection, lightDirection);

                        // Перевіряємо перетин з іншими трикутниками на сцені
                        foreach (Triangle triangle in triangles)
                        {
                            if (triangle.Intersects(shadowRay).intersection.HasValue)
                            {
                                inShadow = true;
                                break;
                            }
                        }

                        if (inShadow)
                        {
                            result = Vector.Dot(normal, lightDirection);

                            if (result < 0)
                            {
                                Console.Write("=");
                                image[x, y] = new Color(32, 32, 32);
                            }
                            else if (result < 0.2)
                            {
                                Console.Write("*");
                                image[x, y] = new Color(64, 64, 64);
                            }
                            else if (result < 0.5)
                            {
                                Console.Write("#");
                                image[x, y] = new Color(128, 128, 128);
                            }
                            else if (result < 0.8)
                            {
                                Console.Write("%");
                                image[x, y] = new Color(192, 192, 192);
                            }
                            else
                            {
                                //█
                                Console.Write("@");
                                image[x, y] = new Color(255, 255, 255);
                            }
                        }
                        else
                        {
                            Console.Write(" ");
                            image[x, y] = new Color(0, 0, 0);

                        }
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                }
                Console.WriteLine();
            }

            return image;
        }


    }
}
