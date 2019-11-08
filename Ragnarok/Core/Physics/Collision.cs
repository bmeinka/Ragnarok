using OpenTK;

namespace Ragnarok.Core.Physics
{
    struct Collision
    {
        const float epsilon = 0.001f;
        public bool Collides { get; private set; }
        public Vector2 Normal { get; private set; }
        public float Penetration { get; private set; }
        public Collision(PhysicsBody a, PhysicsBody b)
        {
            // TODO: refactor shape-specific code into its own method
            Collides = false;
            Normal = Vector2.Zero;
            Penetration = 0f;
            if (!(a.Min.X >= b.Max.X || a.Max.X <= b.Min.X || a.Min.Y >= b.Max.Y || a.Max.Y <= b.Min.Y))
            {
                if (a.Shape.GetType() == typeof(Circle) && b.Shape.GetType() == typeof(Circle))
                {
                    var n = b.Position - a.Position;
                    if (n.Length + epsilon < a.Shape.HalfWidth + b.Shape.HalfWidth)
                    {
                        Collides = true;
                        Normal = n.Normalized();
                        Penetration = (a.Shape.HalfWidth + b.Shape.HalfWidth) - n.Length;
                    }
                }
            }
        }
    }
}
