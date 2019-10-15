using System;
using OpenTK;

namespace Ragnarok
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
    }
}
