using System;
using OpenTK;

namespace Ragnarok
{
    class Camera
    {

        private Vector2 viewport;
        private float aspect_ratio { get { return viewport.X / viewport.Y; } }

        private const float fov_min = 0.3f;
        private const float fov_max = 1.3f;
        private float field_of_view { get { return (fov_max - fov_min) * Zoom + fov_min; } }
        private float zoom = 0.5f;

        private const float angle_min = 0.018f; // ~1 degree
        private const float angle_max = 1.555f; // ~89 degrees
        private float angle = MathHelper.DegreesToRadians(45f);

        private const float offset = 10f;

        private Vector4 relative_position
        {
            get
            {
                var origin = new Vector4(0f, 0f, 0f, 1f);
                var translation = Matrix4.CreateTranslation(new Vector3(0f, 0f, offset));
                var rotation = Matrix4.CreateRotationX(Angle) * Matrix4.CreateRotationZ(Rotation);
                return origin * translation * rotation;
            }
        }
        private Matrix4 projection => Matrix4.CreatePerspectiveFieldOfView(field_of_view, aspect_ratio, 1f, 100f);
        private Matrix4 view => Matrix4.LookAt(Position, Target, Vector3.UnitZ);

        /// <summary>
        /// The zoom level of the camera
        /// </summary>
        /// <remarks>Clamped between zero and one. Zero is zoomed in and one is zoomed out.</remarks>
        public float Zoom { get { return zoom; } set { zoom = MathHelper.Clamp(value, 0f, 1f); } }
        public float Rotation { get; set; }
        public float Angle { get { return angle; } set { angle = MathHelper.Clamp(value, angle_min, angle_max); } }
        public Vector3 Target { get; set; }
        public Vector3 Position => Target + new Vector3(relative_position);
        public Matrix4 ViewProjection => view * projection;

        public Camera(Window window)
        {
            viewport = new Vector2(window.Width, window.Height);
            window.Resize += Resize;
        }

        public Camera(int width, int height) => viewport = new Vector2(width, height);
        public Camera(Vector2 viewport) => this.viewport = viewport; // Vector2 is a struct, so not a reference, but a value

        /// <summary>
        /// get a ray through world space from viewport space
        /// </summary>
        /// <param name="x">the x position in viewport space (pixels)</param>
        /// <param name="y">the y position in viewport space (pxiels)</param>
        public Ray GetRay(int x, int y)
        {
            var clip_space = new Vector4(2f * x / viewport.X - 1f, 1f - 2f * y / viewport.Y, -1f, 1f);
            var view_space = clip_space * projection.Inverted();
            view_space = new Vector4(view_space.X, view_space.Y, -1f, 0f);
            var world_space = view_space * view.Inverted();
            return new Ray(Position, new Vector3(world_space));
        }

        private void Resize(object sender, EventArgs e)
        {
            var window = (Window)sender;
            viewport = new Vector2(window.Width, window.Height);
        }
    }
}
