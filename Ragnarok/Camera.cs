using System;
using OpenTK;

namespace Ragnarok
{
    static class Camera
    {
        private static readonly float fov = MathHelper.DegreesToRadians(45f);
        private static readonly float height = 15f;
        private static Vector3 motion = Vector3.Zero;

        public static int Width { get; set; }
        public static int Height { get; set; }

        /// <summary>
        /// the position of the camera in world space
        /// </summary>
        public static Vector3 Position { get { return Target + new Vector3(0f, height, height); } }

        /// <summary>
        /// where in world space the camera is looking
        /// </summary>
        public static Vector3 Target { get; set; }

        /// <summary>
        /// the camera's projection matrix
        /// </summary>
        public static Matrix4 Projection { get { return Matrix4.CreatePerspectiveFieldOfView(fov, Width / Height, 1f, 100f); } }

        /// <summary>
        /// the camera's view matrix
        /// </summary>
        public static Matrix4 View { get { return Matrix4.LookAt(Position, Target, Vector3.UnitZ); } }

        /// <summary>
        /// the camera's transform matrix (view * projection)
        /// </summary>
        public static Matrix4 Transform { get { return View * Projection; } }

        /// <summary>
        /// the inverse of the camera's transfrom. used for getting from screen space to world space
        /// </summary>
        public static Matrix4 Inverse { get { return Transform.Inverted(); } }

        public static void Initialize(int w, int h, Vector3 target)
        {
            Width = w;
            Height = h;
            Target = target;
        }

        /// <summary>
        /// create a ray from screen position
        /// </summary>
        /// <param name="x">x position to create a ray from in client space</param>
        /// <param name="y">y position to create a ray from in client space</param>
        /// <returns></returns>
        public static Ray ScreenToRay(int x, int y)
        {
            var clip = new Vector4(2f * x / Width - 1f, 1f - 2f * y / Height, 1f, 1f);
            var view = Projection.Inverted() * clip;
            view = new Vector4(view.X, view.Y, -1f, 0f);
            var world = new Vector3(View.Inverted() * view);
            return new Ray(Camera.Position, world);
        }
    }
}
