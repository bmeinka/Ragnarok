using System;
using OpenTK;

namespace Ragnarok.Core
{
    class Camera
    {

        private Vector2 viewport;
        private float AspectRatio { get { return viewport.X / viewport.Y; } }

        private const float field_of_view = (float)Math.PI / 4f;

        private const float angle_min = 0.018f; // ~1 degree
        private const float angle_max = 1.555f; // ~89 degrees

        private float angle = (float)Math.PI / 4f; // 45 degrees
        private float zoom = 0.5f;

        private const float offset_min = 10f;
        private const float offset_max = 20f;
        private float Offset => offset_min + zoom * (offset_max - offset_min);

        public Matrix4 Projection => Matrix4.CreatePerspectiveFieldOfView(field_of_view, AspectRatio, 1f, 100f);
        public Matrix4 View => Matrix4.LookAt(Position, Target, Vector3.UnitZ);
        public Matrix4 ViewProjection => View * Projection;
        public Ray MouseRay { get; private set; }

        /// <summary>
        /// The zoom level of the camera
        /// </summary>
        /// <remarks>Clamped between zero and one. Zero is zoomed in and one is zoomed out.</remarks>
        public float Zoom { get { return zoom; } set { zoom = MathHelper.Clamp(value, 0f, 1f); } }
        public float Rotation { get; set; }
        public float Angle { get { return angle; } set { angle = MathHelper.Clamp(value, angle_min, angle_max); } }
        public Vector3 Target { get; set; }
        public Vector3 Position
        {
            get
            {
                var origin = Vector4.UnitW;
                var translation = Matrix4.CreateTranslation(new Vector3(0f, 0f, Offset));
                var rotation = Matrix4.CreateRotationX(Angle) * Matrix4.CreateRotationZ(Rotation);
                return Target + new Vector3(origin * translation * rotation);
            }
        }

        public Camera()
        {
            viewport = new Vector2(Game.Window.Width, Game.Window.Height);
            Game.Window.Resize += (object sender, EventArgs e) => viewport = new Vector2(Game.Window.Width, Game.Window.Height);
        }
        public void Update(float delta) => MouseRay = GetRay(Game.Mouse.X, Game.Mouse.Y);

        /// <summary>
        /// get a ray through world space from viewport space
        /// </summary>
        /// <param name="x">the x position in viewport space (pixels)</param>
        /// <param name="y">the y position in viewport space (pxiels)</param>
        public Ray GetRay(int x, int y)
        {
            var clip_space = new Vector4(2f * x / viewport.X - 1f, 1f - 2f * y / viewport.Y, -1f, 1f);
            var view_space = clip_space * Projection.Inverted();
            view_space = new Vector4(view_space.X, view_space.Y, -1f, 0f);
            var world_space = view_space * View.Inverted();
            return new Ray(Position, new Vector3(world_space));
        }
    }
}
