using Obj_reader.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Obj_reader.Structures
{
    struct Matrix
    {
     
        public float[,] Values { get; set; }

        public Matrix(float[,] values)
        {
            Values = values;
        }
        public static Matrix Identity()
        {
            Matrix matrix = new Matrix();

            matrix.Values[0, 0] = 1f;
            matrix.Values[1, 1] = 1f;
            matrix.Values[2, 2] = 1f;
            matrix.Values[3, 3] = 1f;

            return matrix;
        }


        public static Matrix Translation(float transX, float transY, float transZ)
        {
            float[,] values = new float[,]
            {
            { 1, 0, 0, 0 },
            { 0, 1, 0, 0 },
            { 0, 0, 1, 0 },
            { transX, transY, transZ, 1 }
            };    
            return new Matrix(values);
        }


        public static Matrix Scaling(float scaleX, float scaleY, float scaleZ)
        {
            float[,] values = new float[,]
            {
            { scaleX, 0, 0, 0 },
            { 0, scaleY, 0, 0 },
            { 0, 0, scaleZ, 0 },
            { 0, 0, 0, 1 }
            };

            return new Matrix(values);
        }

        public static Matrix RotationX(float angleX)
        {
            float sinX = (float)Math.Sin(angleX);
            float cosX = (float)Math.Cos(angleX);

            float[,] values = new float[4, 4]
            {
                {1, 0, 0, 0},
                {0, cosX, sinX, 0},
                {0, -sinX, cosX, 0},
                {0, 0, 0, 1}
            };

            return new Matrix(values);
        }

        public static Matrix RotationY(float angleY)
        {
            float sinY = (float)Math.Sin(angleY);
            float cosY = (float)Math.Cos(angleY);

            float[,] values = new float[4, 4]
            {
                {cosY, 0, -sinY, 0},
                {0, 1, 0, 0},
                {sinY, 0, cosY, 0},
                {0, 0, 0, 1}
            };

            return new Matrix(values);
        }

        public static Matrix RotationZ(float angleZ)
        {
            float sinZ = (float)Math.Sin(angleZ);
            float cosZ = (float)Math.Cos(angleZ);

            float[,] values = new float[4, 4]
            {
                {cosZ, sinZ, 0, 0},
                {-sinZ, cosZ, 0, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 1}
            };

            return new Matrix(values);
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {

            int rowsA = a.Values.GetLength(0);
            int colsA = a.Values.GetLength(1);
            int colsB = b.Values.GetLength(1);

            float[,] result = new float[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    float sum = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        sum += a.Values[i, k] * b.Values[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return new Matrix(result);
        }


        public static Matrix operator *(float f, Matrix matrix)
        {
            int rows = matrix.Values.GetLength(0);
            int cols = matrix.Values.GetLength(1);
            float[,] resultValues = new float[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    resultValues[i, j] = f * matrix.Values[i, j];
                }
            }
            return new Matrix(resultValues);
        }



    }


}
