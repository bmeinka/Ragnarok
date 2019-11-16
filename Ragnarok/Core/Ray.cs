using OpenTK;

namespace Ragnarok.Core
{
    class Ray
    {
        public Vector3 Origin { get; set; }
        public Vector3 Direction { get; set; }

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction.Normalized();
        }

        /// <summary>
        /// using the parametric function P(t) = Po + tu, get the vector of a given t
        /// </summary>
        /// <param name="t">the t value to get the position for</param>
        /// <returns>the position of the point t on the ray line</returns>
        public Vector3 Parametric(float t) => Origin + (Direction * t);

        /// <summary>
        /// determine the position on the ray where it intersects with a plane
        /// </summary>
        /// <param name="plane">the plane to check for intersection</param>
        /// <returns>the t value for <see cref="Ray.Parametric(float)"/></returns>
        /// <remarks>negative values can be considered a miss (no intersection)</remarks>
        public float Intersect(Plane plane)
        {
            // Euclidean plane intersection:
            // Variables:
            //   Po: ray origin
            //   Vo: plane origin
            //   u:  ray direction
            //   n:  plane normal
            //   w:  Po - Vo
            // using the parametric line function P(t) = Po + tu
            // the intersection point s can be determined by: -n * w / n * u
            if (Vector3.Dot(plane.Normal, Direction) == 0)
                return -1f;
            return Vector3.Dot(-plane.Normal, Origin - plane.Origin) / Vector3.Dot(plane.Normal, Direction);
        }
    }
}
