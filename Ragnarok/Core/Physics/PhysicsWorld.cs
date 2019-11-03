using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace Ragnarok.Core.Physics
{
    class PhysicsWorld
    {
        private List<IPhysicsBody> bodies;

        public PhysicsWorld() => bodies = new List<IPhysicsBody>();

        public DynamicBody AddDynamicBody(Vector2 position, ICollisionShape shape)
        {
            var body = new DynamicBody(position, shape);
            bodies.Add(body);
            return body;
        }
        public StaticBody AddStaticBody(Vector2 position, ICollisionShape shape)
        {
            var body = new StaticBody(position, shape);
            bodies.Add(body);
            return body;
        }

        private void UpdateMovement(float delta)
        {
            // query for bodies that both want to move and are actually able to move
            foreach(var body in from body in bodies where body.Destination != body.Position && body.MovementSpeed > 0 select body)
            {
                var distance = delta * body.MovementSpeed;
                var goal = body.Destination - body.Position;
                if (goal.Length < distance)
                    body.Position = body.Destination;
                else
                    body.Position += goal.Normalized() * distance;
                // TODO: body.Dirty = true;
            }
        }
        public void Update(float delta)
        {
            UpdateMovement(delta);
        }
    }
}
