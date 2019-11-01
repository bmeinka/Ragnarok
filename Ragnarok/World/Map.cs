using OpenTK;
using Ragnarok.Core;
using Ragnarok.Core.Graphics;

namespace Ragnarok.World
{
    /// <summary>
    /// A map in the world of Ragnarok
    /// </summary>
    class Map
    {
        private readonly Mesh mesh;
        private readonly Vector2 size;
        public Vector2 SpawnPoint => new Vector2(24f, 24f);

        public float Width => size.X;
        public float Height => size.Y;

        public Map(float width, float height)
        {
            size = new Vector2(width, height);
            mesh = new Mesh(width, height);
        }

        public void Draw(float delta) => mesh.Draw(delta);

        /// <summary>
        /// Determine if a ray intersects with the map plane, and set the intersection point.
        /// </summary>
        /// <param name="ray">the ray to trace</param>
        /// <param name="intersection">where on the plane the ray intersects</param>
        /// <returns>true if the ray does intersect the plane</returns>
        public bool Intersect(Ray ray, out Vector2 intersection)
        {
            // Euclidean plane intersection:
            // Variables:
            //   Po: ray origin
            //   Vo: plane origin
            //   u:  ray direction
            //   n:  plane normal (up vector)
            //   w:  Po - Vo
            // using the parametric line function P(s) = Po + su
            // the intersection point s can be determined by: -n * w / n * u
            var normal = Vector3.UnitZ;
            var origin = Vector3.Zero;
            if (Vector3.Dot(normal, ray.Direction) == 0)
            {
                intersection = Vector2.Zero;
                return false;
            }
            var w = ray.Origin - origin;
            var s = Vector3.Dot(-normal, w) / Vector3.Dot(normal, ray.Direction);
            intersection = new Vector2(ray.Parametric(s).X, ray.Parametric(s).Y);
            return true;
        }
    }
}
