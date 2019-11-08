using OpenTK;

namespace Ragnarok.Core.Physics
{
    class StaticBody : PhysicsBody
    {
        public override Vector2 Destination => Position; // the body only wants to be where it already is... because it's already there
        public StaticBody(Vector2 position, ICollisionShape shape) : base(position, shape)
        {
            InverseMass = 0f;
            MovementSpeed = 0f;
        }
    }
}
