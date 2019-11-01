using System.Collections.Generic;
using OpenTK;

namespace Ragnarok.Core.Physics
{
    class PhysicsWorld
    {
        private List<Body> bodies;

        public PhysicsWorld()
        {
            bodies = new List<Body>();
        }

        public Body AddBody(Vector2 position)
        {
            var body = new Body(position);
            bodies.Add(body);
            return body;
        }

        public void Update(float delta)
        {
            foreach (var body in bodies)
                body.Update(delta);
        }
    }
}
