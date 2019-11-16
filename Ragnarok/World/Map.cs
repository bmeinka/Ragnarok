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
        /// determine if the mouse is interescting the map, and where
        /// </summary>
        /// <param name="camera">the camera the map is viewed from</param>
        /// <param name="position">the map position the mouse is at (if any)</param>
        /// <returns>true if the mouse is intersectiong, and false otherwise</returns>
        public bool MouseIntersection(Camera camera, out Vector2 position)
        {
            var ray = camera.GetRay(Game.Mouse.X, Game.Mouse.Y);
            var t = ray.Intersect(mesh.Plane);
            if (t < 0)
            {
                position = Vector2.Zero;
                return false;
            }
            position = ray.Parametric(t).Xy;
            // only consider intersections that fall within map bounds
            return (position.X >= 0 && position.X <= Width && position.Y >= 0 && position.Y <= Height);
        }
    }
}
