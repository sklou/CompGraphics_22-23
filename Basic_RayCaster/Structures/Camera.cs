﻿using Basic_RayCaster.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    struct Camera
    {
        public Vector position;

        public Camera(Vector position)
        {
            this.position = position;
        }

        public Ray GetRayThroughPixel(int x, int y, int width, int height)
        {
            float aspectRatio = width / height;
            float fov = 90f * (float)Math.PI / 180f; //Поле зору(fov) камери встановлюється на 60 градусів і перетворюється в радіани
            float halfHeight = (float)Math.Tan(fov / 2f);
            float halfWidth = aspectRatio * halfHeight;
            float pixelWidth = halfWidth * 2f / width;
            float pixelHeight = halfHeight * 2f / height;

            Vector direction = new Vector(x * pixelWidth - halfWidth, y * pixelHeight - halfHeight, 1f);
            direction.Normalize();
            return new Ray(position, direction);
        }
    }
}
