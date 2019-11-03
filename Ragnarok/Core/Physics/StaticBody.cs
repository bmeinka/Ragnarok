using OpenTK;

namespace Ragnarok.Core.Physics
{
    class StaticBody : IPhysicsBody
    {
        public float InverseMass => 0f; // infinite mass, so it doesn't move due to collisions
        public float MovementSpeed => 0f; // a movement speed of zero will result in no movement
        public Vector2 Position { get; set; }
        public Vector2 Destination => Position; // the body only wants to be where it already is... because it's already there
        public ICollisionShape Shape { get; private set;}

        public StaticBody(Vector2 position, ICollisionShape shape)
        {
            Position = position;
            Shape = shape;
        }
    }
}
