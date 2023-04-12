using System;
using NUnit.Framework;
using Obj_reader.Structures;

namespace Obj_reader.Tests
{
	public class ParseFileTests
    {
        public void ParseVertex_AddsVertexToList()
        {
            var objParser = new ObjParser();
            var expectedVertex = new Vector(1.0f, 2.0f, 3.0f);

            objParser.ParseVertex(new[] { "v", "1.0", "2.0", "3.0" });

            Assert.AreEqual(1, objParser.Vertices.Count);
            Assert.AreEqual(expectedVertex, objParser.Vertices[0]);
        }

        public void ParseFile_AddsTriangleToList()
        {
            var objParser = new ObjParser();
            var expectedIndices = new Tuple<int, int, int>(0, 1, 2);

            objParser.Vertices.AddRange(new[] {
                new Vector(1.0f, 0.0f, 0.0f),
                new Vector(0.0f, 1.0f, 0.0f),
                new Vector(0.0f, 0.0f, 1.0f)
            });
            objParser.ParseFile(new[] { "f", "1/1/1", "2/2/2", "3/3/3" });

            Assert.AreEqual(1, objParser.TriangleIndices.Count);
            Assert.AreEqual(expectedIndices, objParser.TriangleIndices[0]);
        }

        public void ParseFile_DoesNotAddAnything()
        {
            var objParser = new ObjParser();
            var emptyFilePath = "empty.obj";

            objParser.ParseFile(emptyFilePath);

            Assert.IsEmpty(objParser.Vertices);
            Assert.IsEmpty(objParser.TriangleIndices);
        }
    }
}