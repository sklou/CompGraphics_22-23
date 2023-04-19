using Obj_reader.Structures;
using Obj_reader.Tests;
using System;
using System.Xml.Linq;
using System.Globalization;
using System.IO;
using NUnit.Framework;
using System.Security.Cryptography;


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

        //----------------------------------Matrix_Transfor_for_obj---------------------//
        float scaleX = 1f;
        float scaleY = 1f;
        float scaleZ = 1f;
        Matrix scaleMatrix = Matrix.Scaling(scaleX, scaleY, scaleZ);

        float transX = 0f;
        float transY = 0f;
        float transZ = 0f;
        Matrix translateMatrix = Matrix.Translation(transX, transY, transZ);

        float angleX = (float)(0f * (Math.PI / 180));
        float angleY = (float)(0f * (Math.PI / 180));     //-90
        float angleZ = (float)(0f * (Math.PI/180));         // 90
        Matrix rotateMatrix = Matrix.RotationX(angleX) * Matrix.RotationY(angleY) * Matrix.RotationZ(angleZ);


        Matrix transformMatrix = translateMatrix * rotateMatrix * scaleMatrix;
       // Matrix transformMatrix = rotateMatrix;

        for (int i = 0; i < triangles.Length; i++)
        {
            triangles[i].a = Vector.Transform(triangles[i].a, transformMatrix);
            triangles[i].b = Vector.Transform(triangles[i].b, transformMatrix);
            triangles[i].c = Vector.Transform(triangles[i].c, transformMatrix);
        }

        //----------------------------------Matrix_Transfor_for_camera---------------------//
        float scaleX_C = 1f;
        float scaleY_C = 1f;
        float scaleZ_C = 1f;
        Matrix scaleCam = Matrix.Scaling(scaleX_C, scaleY_C, scaleZ_C);

        float transX_C = 0f;
        float transY_C = 0f;
        float transZ_C = 0f;       //-1
        Matrix translateCam = Matrix.Translation(transX_C, transY_C, transZ_C);

        float angleX_C = (float)(0f * (Math.PI / 180));
        float angleY_C = (float)(0f * (Math.PI / 180));
        float angleZ_C = (float)(0f * (Math.PI / 180));
        Matrix rotateCam = Matrix.RotationX(angleX_C) * Matrix.RotationY(angleY_C) * Matrix.RotationZ(angleZ_C);


        Matrix transformCam = translateCam * rotateCam * scaleCam;
        Camera camera = new Camera(new Vector(0f, 0f, 0f));

        camera.position = Vector.Transform(camera.position, transformCam);


       // Vector L = new Vector(0f, 0, 1);
        Vector L = new Vector(0.5f, 0, 1);
        L.Normalize();

        //int width = 400;
        //int height = 120;

        int width = 180;
        int height = 80;
        Color[,] image = new Color[width, height];
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

        string outputPath = "D:\\GitLab\\CompGraphics_22-23\\Obj_reader\\forRender\\cow.ppm";
        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine("P3");
            writer.WriteLine($"{width} {height}");
            writer.WriteLine("255");

            for (int y2 = 0; y2 < height; y2++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color color = image[x, y2];
                    writer.Write($"{color.R} {color.G} {color.B} ");
                }
                writer.WriteLine();
            }
        }

        Console.WriteLine($"Зображення збережено у файлі: {outputPath}");

        //Tests usage
       
        //Camera tests usage
        CameraTests CameraTesting = new CameraTests();
    //    CameraTesting.GetRayThroughPixel_CorrectDirection();
  //      CameraTesting.GetRayThroughPixel_CorrectOrigin();
   //     CameraTesting.GetRayThroughPixel_NormalizedDirection();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();

        //Intersection tests usage
        IntersectionTests IntersectionTesting = new IntersectionTests();
        IntersectionTesting.TestIntersectionInitialization();
        IntersectionTesting.TestDefaultNormalValue();
        IntersectionTesting.TestIntersectionFieldAssignment();
        IntersectionTesting.TestDefaultHitValue();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();

        //Normal tests usage
        NormalTests NormalTesting = new NormalTests();
        NormalTesting.TestNormalCreation();
        NormalTesting.TestNormalAddition();
        NormalTesting.TestNormalSubtraction();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();

        //Point tests usage
        PointTests PointTesting = new PointTests();
        PointTesting.TestPointConstructor();
        PointTesting.TestSetX();
        PointTesting.TestSetY();
        PointTesting.TestSetZ();
        PointTesting.TestPointEquality();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();

        //Ray tests usage
        RayTests RayTesting = new RayTests();
        RayTesting.TestRayInitialization();
        RayTesting.TestRayPointAt();
        RayTesting.TestRayEquality();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();

        //Vector tests usage
        VectorTests VectorTesting = new VectorTests();
        VectorTesting.VectorConstructorTest();
        VectorTesting.VectorNormalizeTest();
        VectorTesting.VectorMagnitudeTest();
        VectorTesting.VectorToVectorTest();
        VectorTesting.VectorDotTest();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();

        //Parse file tests usage
        ParseFileTests ParseFileTesting = new ParseFileTests();
        ParseFileTesting.VerticesList_IsNotNull();
        ParseFileTesting.TriangleIndicesList_IsNotNull();
        ParseFileTesting.ParseFile_ThrowsException_WhenFileDoesNotExist();
        ParseFileTesting.ParseFile_PopulatesVerticesList_WithCorrectValues();
        ParseFileTesting.ParseFile_PopulatesTriangleIndicesList_WithCorrectValues();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
        
    }
}
