using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace Ragnarok.Core.Physics
{
    class PhysicsWorld
    {
        private readonly Vector2 size;
        private readonly List<PhysicsBody> bodies;
        private HashSet<PhysicsBody> dirty;

        public PhysicsWorld(float width, float height)
        {
            bodies = new List<PhysicsBody>();
            dirty = new HashSet<PhysicsBody>();
            size = new Vector2(width, height);
        }

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
            foreach (var body in from body in bodies where body.Destination != body.Position && body.MovementSpeed > 0 select body)
            {
                var distance = delta * body.MovementSpeed;
                var goal = body.Destination - body.Position;
                if (goal.Length < distance)
                    body.Position = body.Destination;
                else
                    body.Position += goal.Normalized() * distance;
                dirty.Add(body);
            }
        }

        private void UpdateCollisions()
        {
            var moved = new HashSet<PhysicsBody>();
            while (dirty.Count > 0)
            {
                moved.Clear();
                foreach (var body in dirty)
                {
                    var position = body.Position;
                    // handle collision with world bounds
                    if (body.Min.X < 0)
                        position.X = body.Shape.HalfWidth;
                    if (body.Min.Y < 0)
                        position.Y = body.Shape.HalfHeight;
                    if (body.Max.X > size.X)
                        position.X = size.X - body.Shape.HalfWidth;
                    if (body.Max.Y > size.Y)
                        position.Y = size.Y - body.Shape.HalfHeight;
                    if (position != body.Position)
                    {
                        body.Position = position;
                        moved.Add(body);
                    }
                }

                foreach (var a in dirty)
                {
                    // every other body
                    foreach (var b in from body in bodies where body != a select body)
                    {
                        var collision = new Collision(a, b);
                        if (collision.Collides)
                        {
                            var mass = a.InverseMass + b.InverseMass;
                            a.Position -= collision.Normal * collision.Penetration * a.InverseMass / mass;
                            b.Position += collision.Normal * collision.Penetration * b.InverseMass / mass;
                            moved.Add(a);
                            moved.Add(b);
                        }
                    }
                }
                dirty.Clear();
                dirty.UnionWith(moved);
            }
        }

        public void Update(float delta)
        {
            UpdateMovement(delta);
            UpdateCollisions();
        }
    }
}
