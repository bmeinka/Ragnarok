using OpenTK;
using Ragnarok.Core;
using Ragnarok.Core.Graphics;
using Ragnarok.Core.Physics;

namespace Ragnarok.World
{
    /// <summary>
    /// A map in the world of Ragnarok
    /// </summary>
    class Map
    {
        private readonly Mesh mesh;
        private readonly Vector2 size;
        private readonly PhysicsWorld world;
        private readonly Monster[] monsters;
        private readonly Sprite monster_sprite = new Sprite(new Vector2(1f, 1f), new Vector3(1f, 0.5f, 0.4f));

        public Vector2 SpawnPoint => new Vector2(24f, 24f);

        public float Width => size.X;
        public float Height => size.Y;

        public Map(float width, float height)
        {
            size = new Vector2(width, height);
            world = new PhysicsWorld(width, height);
            mesh = new Mesh(width, height);

            monsters = new Monster[5];
            for (var i = 0; i < monsters.Length; i++)
            {
                var x = Game.Random.Float(0f, Width);
                var y = Game.Random.Float(0f, Height);
                monsters[i] = new Monster(monster_sprite);
                monsters[i].Spawn(this, new Vector2(x, y));
            }
        }

        public DynamicBody SpawnMob(Vector2 position)
        {
            var body = world.AddDynamicBody(position, new Circle(0.5f));
            body.MoveTo(position);
            return body;
        }

        public void Draw(SpriteBatch sb)
        {
            mesh.Draw();
            foreach (var monster in monsters)
                monster.Draw(sb);
        }
        public void Update(float delta) => world.Update(delta);

        /// <summary>
        /// Determine if a ray intersects with the map plane, and set the intersection point.
        /// </summary>
        /// <param name="ray">the ray to trace</param>
        /// <param name="intersection">where on the plane the ray intersects</param>
        /// <returns>true if the ray does intersect the plane</returns>
        public bool Intersect(Ray ray, out Vector2 intersection) // TODO: move to core (method on Ray class)
        {
            // Euclidean plane intersection:
            // Variables:
            //   Po: ray origin
            //   Vo: plane origin
            //   u:  ray direction
            //   n:  plane normal (up vector)
            //   w:  Po - Vo
            // using the parametric line function P(s) = Po + su
            // the intersection point s can be determined by: -n * w / n * u
            var normal = Vector3.UnitZ;
            var origin = Vector3.Zero;
            if (Vector3.Dot(normal, ray.Direction) == 0)
            {
                intersection = Vector2.Zero;
                return false;
            }
            var w = ray.Origin - origin;
            var s = Vector3.Dot(-normal, w) / Vector3.Dot(normal, ray.Direction);
            intersection = new Vector2(ray.Parametric(s).X, ray.Parametric(s).Y);
            return true;
        }
    }
}
