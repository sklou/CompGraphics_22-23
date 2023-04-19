using System;
using NUnit.Framework;
using Obj_reader.Structures;
using static Program;

namespace Obj_reader.Tests
{
    public class ParseFileTests
    {
        [Test]
        public void VerticesList_IsNotNull()
        {
            ObjParser parser = new ObjParser();
            Assert.IsNotNull(parser.Vertices);
        }

        [Test]
        public void TriangleIndicesList_IsNotNull()
        {
            ObjParser parser = new ObjParser();
            Assert.IsNotNull(parser.TriangleIndices);
        }

        [Test]
        public void ParseFile_ThrowsException_WhenFileDoesNotExist()
        {
            ObjParser parser = new ObjParser();
            Assert.Throws<FileNotFoundException>(() => parser.ParseFile("non-existent-file.obj"));
        }

        [Test]
        public void ParseFile_PopulatesVerticesList_WithCorrectValues()
        {
            ObjParser parser = new ObjParser();
            parser.ParseFile("test.obj");
            Assert.AreEqual(new Vector(0.0f, 0.0f, 0.0f), parser.Vertices[0]);
            Assert.AreEqual(new Vector(1.0f, 0.0f, 0.0f), parser.Vertices[1]);
            Assert.AreEqual(new Vector(0.0f, 1.0f, 0.0f), parser.Vertices[2]);
        }

        [Test]
        public void ParseFile_PopulatesTriangleIndicesList_WithCorrectValues()
        {
            ObjParser parser = new ObjParser();
            parser.ParseFile("test.obj");
            Assert.AreEqual(new Tuple<int, int, int>(0, 1, 2), parser.TriangleIndices[0]);
        }
    }
}