using Obj_reader.Structures;
using Obj_reader.Tests;
using System;
using System.Xml.Linq;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using Obj_reader.Interface;
using Obj_reader;
using NUnit.Framework;

class Program
{


    static void Main(string[] args)
    {
       
       // string objFilePath = "D:\\GitLab\\CompGraphics_22-23\\Obj_reader\\forRender\\cow.obj";
       //  string outputPath = "D:\\GitLab\\CompGraphics_22-23\\Obj_reader\\forRender\\cow.ppm";
        string objFilePath = null;
        string outputPath = null;
        foreach (string arg in args)
        {
            if (arg.StartsWith("--source="))
            {
                objFilePath = arg.Substring("--source=".Length);
            }
            else if (arg.StartsWith("--output="))
            {
                outputPath = arg.Substring("--output=".Length);
            }
        }
        if (objFilePath == null)
        {
            Console.WriteLine("Error: no input file provided (--source=<path to obj file>)");
            return;
        }
        if (!File.Exists(objFilePath))
        {
            Console.WriteLine($"Error: input file '{objFilePath}' does not exist");
            return;
        }


        ObjParser parser = new();
        parser.ParseFile(objFilePath);

        List<Vector> vertices = parser.Vertices;
        List<Tuple<int, int, int>> triangleIndices = parser.TriangleIndices;

        Triangle[] triangles = Triangle.CreateTriangles(triangleIndices, vertices);

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
        float angleZ = (float)(0f * (Math.PI / 180));         // 90
        Matrix rotateMatrix = Matrix.RotationX(angleX) * Matrix.RotationY(angleY) * Matrix.RotationZ(angleZ);


        Matrix transformMatrix = translateMatrix * rotateMatrix * scaleMatrix;

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
        float transZ_C = -1f;       //-1
        Matrix translateCam = Matrix.Translation(transX_C, transY_C, transZ_C);

        float angleX_C = (float)(0f * (Math.PI / 180));
        float angleY_C = (float)(0f * (Math.PI / 180));
        float angleZ_C = (float)(0f * (Math.PI / 180));
        Matrix rotateCam = Matrix.RotationX(angleX_C) * Matrix.RotationY(angleY_C) * Matrix.RotationZ(angleZ_C);


        Matrix transformCam = translateCam * rotateCam * scaleCam;
        Camera camera = new Camera(new Vector(0f, 0f, 0f));

        camera.position = Vector.Transform(camera.position, transformCam);


        Vector L = new Vector(0.5f, 0, 1);
        L.Normalize();
        int width = 320;
        int height = 240;

        Color[,] image = new Color[width, height * 3];



        if (outputPath != null)
        {
            IRenderer render = new Render();
            image = render.Render(camera, triangles, L, width, height);

            IImageWriter writer = new PpmImageWriter();
            writer.WriteImageToFile(image, width, height, outputPath);
        }
        else     //if outputPath was not mentioned == null == output into console
        {

            Console.WriteLine("Render");
            IRenderer render = new Render();
            image = render.Render(camera, triangles, L, width, height);

        }


        bool a = false;
        while (a == true)
        {
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
            break;
            Console.ReadKey();

        }
    }
}
