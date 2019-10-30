using OpenTK;

namespace Ragnarok.Core.Physics
{
    class Circle
    {
        public Vector2 Center { get; set; }
        public float Radius { get; set; }

        public Circle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// Determine if two circles are overlapping
        /// </summary>
        /// <param name="other">the other circle</param>
        /// <returns>true if the cirlces are overlapping</returns>
        public bool CollidesWith(Circle other) => (Radius + other.Radius) > (Center - other.Center).Length;
    }
}
