using System;
using OpenTK;

/*
How converting from world space to camera space works:

The trick to understanding this process is knowing that it is all about defining the XYZ axes for camera space.

The Z axis is the easiest. It is a vector pointing from where the camera is in world space to where the camera is pointing in world space. This is just a simple vector subtraction normalize(position - target). This vector is the Z axis of camera space.
Then, the X axis can be figured out. The cross product of two vectors is a vector perpendicular to the two vectors. The x axis should be perpendicular to the Z axis that was just calculated. It should also be perpendicular to the "up" vector in world space. So, the X axis is cross(Z, world_up)
Lastly, the Y axis is the axis that is perpendicular to the X and Z axes, so a simple cross(X, Z) will yield the Y.

The LookAt method does all of this work to create a matrix to translate between coordinate systems. It needs to know
 */

namespace Ragnarok
{
    static class Camera
    {
        private static readonly float fov = MathHelper.DegreesToRadians(45f);
        private static readonly float height = 15f;
        private static Vector3 target;
        private static Matrix4 transform;
        public static float AspectRatio { get; set; }
        public static Vector3 Target { get { return target; } set { target = value; update(); } }
        public static Matrix4 Transform { get { return transform; } }
        private static void update()
        {
            // if the aspect ratio isn't set, this will throw an argument out of bounds exception
            // so make sure to set the aspect ratio before using the camera!
            var postion = target + new Vector3(0f, -height, height);
            var view = Matrix4.LookAt(postion, target, Vector3.UnitZ);
            var projection = Matrix4.CreatePerspectiveFieldOfView(fov, AspectRatio, 0.01f, 100f);
            transform = view * projection;
        }
    }
}
