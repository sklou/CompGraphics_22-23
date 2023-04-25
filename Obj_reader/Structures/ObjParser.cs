using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obj_reader.Structures
{
     class ObjParser
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

}
