using OpenTK;
using Ragnarok.Core.Graphics;
using Ragnarok.Core.Physics;

namespace Ragnarok.World
{
    /// <summary>
    /// A Player, Monster or NPC. Drawn as a Sprite in the game world.
    /// </summary>
    abstract class Character
    {
        protected readonly SpriteBatch sprite_batch;
        protected readonly Body body;
        public Vector3 Position => new Vector3(body.Position.X, body.Position.Y, 0f);
        public Sprite Sprite { get; set; }


        public Character(SpriteBatch batch, PhysicsWorld world, Vector2 position)
        {
            sprite_batch = batch;
            body = world.AddBody(position);
        }
        public void Draw(float delta) => sprite_batch.Add(Sprite, Position);
        public abstract void Update(float delta);
    }
}
