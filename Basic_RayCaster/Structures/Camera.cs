using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_RayCaster.Structures
{
    public struct Camera
{
    public Point position;
    public Vector forward;
    public Vector up;
    public Vector right;
    public double distance;
    public double width;
    public double height;

    public Camera(Point position, Vector lookAt, Vector up, double distance, double width, double height)
    {
        this.position = position;
        this.forward = (lookAt - position).Normalize();
        this.right = Vector.Cross(this.forward, up.Normalize()).Normalize();
        this.up = Vector.Cross(this.right, this.forward).Normalize();
        this.distance = distance;
        this.width = width;
        this.height = height;
    }

    public Ray GetRay(double x, double y)
    {
        Vector rayDirection = forward + ((x - 0.5) * width * right) + ((y - 0.5) * height * up);
        return new Ray(position, rayDirection.Normalize());
    }
}


}
