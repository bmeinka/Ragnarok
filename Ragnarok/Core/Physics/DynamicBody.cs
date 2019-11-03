using OpenTK;

namespace Ragnarok.Core.Physics
{
    class DynamicBody : IPhysicsBody
    {
        public float MovementSpeed => 3f;
        private Vector2? destination, direction;
        public float InverseMass => 1f; // all dynamic bodies have the same mass, so it doesn't really matter the number here
        public Vector2 Position { get; set; }
        public ICollisionShape Shape { get; private set; }
        public Vector2 Destination
        {
            get
            {
                // if there is a target, return it
                if (destination != null)
                    return (Vector2)destination;
                // if there is a direction, return it scaled up with the movement speed
                if (direction != null)
                    return (Vector2)direction * MovementSpeed;
                // otherwise, return the current position (no movement)
                return Position;
            }
        }

        public DynamicBody(Vector2 position, ICollisionShape shape)
        {
            Position = position;
            Shape = shape;
            destination = null;
            direction = null;
        }

        public void Move(Vector2 direction)
        {
            destination = null;
            this.direction = direction.Normalized();
        }

        public void MoveTo(Vector2 destination)
        {
            direction = null;
            this.destination = destination;
        }
    }
}
