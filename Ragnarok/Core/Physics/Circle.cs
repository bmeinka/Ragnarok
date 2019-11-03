using OpenTK;

namespace Ragnarok.Core.Physics
{
    struct Circle : ICollisionShape
    {
        private readonly float radius;
        public Vector2 Max(Vector2 position) => new Vector2(position.X + radius, position.Y + radius);
        public Vector2 Min(Vector2 position) => new Vector2(position.X - radius, position.Y - radius);
        public Circle(float radius) => this.radius = radius;
    }
}
