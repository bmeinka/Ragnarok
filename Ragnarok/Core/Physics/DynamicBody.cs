using OpenTK;

namespace Ragnarok.Core.Physics
{
    class DynamicBody : PhysicsBody
    {
        private Vector2? destination, direction;
        public override Vector2 Destination
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

        public DynamicBody(Vector2 position, ICollisionShape shape) : base(position, shape)
        {
            destination = null;
            direction = null;
            MovementSpeed = 3f;
            InverseMass = 1f;
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
