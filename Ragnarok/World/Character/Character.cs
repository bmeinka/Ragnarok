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
        protected abstract IPhysicsBody Body { get; }
        public Vector3 Position => new Vector3(Body.Position.X, Body.Position.Y, 0f);
        public Sprite Sprite { get; set; }


        public Character(SpriteBatch batch) => sprite_batch = batch;
        public void Draw() => sprite_batch.Add(Sprite, Position);
        public abstract void Update(float delta);
    }
}
