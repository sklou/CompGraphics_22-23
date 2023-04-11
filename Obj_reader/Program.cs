using Obj_reader.Structures;
using System;
using System.Xml.Linq;
using System.Globalization;


class Program
{
    public class ObjParser
    {
        public List<Vector> Vertices { get; private set; }
        public List<Tuple<int, int, int>> TriangleIndices { get; private set; }

        public ObjParser()
        {
            Vertices = new List<Vector>();
            TriangleIndices = new List<Tuple<int, int, int>>();
        }

        public void ParseFile(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length > 0)
                    {
                        switch (parts[0])
                        {
                            case "v":
                                ParseVertex(parts);
                                break;
                            case "f":
                                ParseFace(parts);
                                break;
                        }
                    }
                }
            }
        }

        private void ParseVertex(string[] parts)
        {
            float x = float.Parse(parts[1], CultureInfo.InvariantCulture);
            float y = float.Parse(parts[2], CultureInfo.InvariantCulture);
            float z = float.Parse(parts[3], CultureInfo.InvariantCulture);
            Vertices.Add(new Vector(x, y, z));
        }

        private void ParseFace(string[] parts)
        {
            int v1 = int.Parse(parts[1].Split('/')[0]) - 1;
            int v2 = int.Parse(parts[2].Split('/')[0]) - 1;
            int v3 = int.Parse(parts[3].Split('/')[0]) - 1;
            TriangleIndices.Add(new Tuple<int, int, int>(v1, v2, v3));
        }
    }


    static void Main(string[] args)
    {
         string objFilePath = "D:\\GitLab\\CompGraphics_22-23\\Obj_reader\\forRender\\cow.obj";

         ObjParser parser = new ObjParser();
         parser.ParseFile(objFilePath);

         List<Vector> vertices = parser.Vertices;
         List<Tuple<int, int, int>> triangleIndices = parser.TriangleIndices;

        Triangle[] triangles = new Triangle[triangleIndices.Count];

        for (int i = 0; i < triangleIndices.Count; i++)
        {
            int index1 = triangleIndices[i].Item1;
            int index2 = triangleIndices[i].Item2;
            int index3 = triangleIndices[i].Item3;

            Vector vertex1 = vertices[index1];
            Vector vertex2 = vertices[index2];
            Vector vertex3 = vertices[index3];

            triangles[i] = new Triangle(vertex1, vertex2, vertex3);
        }


        Camera camera = new Camera(new Vector(0f, 0f, 0f));

       // Triangle[] triangles = new Triangle[3];
        //triangles[0] = new Triangle(new Vector(3f, 0f, 10f), new Vector(-3f, 0f, 5f), new Vector(0f, 5f, 5f));
      //  triangles[1] = new Triangle(new Vector(-3f, -5f, 10f), new Vector(3f, -5f, 5f), new Vector(0f, 0f, 5f));
       // triangles[2] = new Triangle(new Vector(0f, 5f, 15f), new Vector(-3f, -5f, 15f), new Vector(3f, -5f, 15f));

        Vector L = new Vector(2, 0, 1);
        L.Normalize();

        int width = 120;
        int height = 40;

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
