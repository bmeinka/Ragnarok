using OpenTK;

namespace Ragnarok.Core.Physics
{
    struct Circle : ICollisionShape
    {
        public float HalfWidth { get; }
        public float HalfHeight => HalfWidth;
        public Vector2 Min(Vector2 position) => new Vector2(position.X - HalfWidth, position.Y - HalfHeight);
        public Vector2 Max(Vector2 position) => new Vector2(position.X + HalfWidth, position.Y + HalfHeight);
        public Circle(float radius) => HalfWidth = radius;
    }
}
