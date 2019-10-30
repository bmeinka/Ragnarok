using OpenTK;
using Ragnarok.Core.Graphics;

namespace Ragnarok.World
{
    /// <summary>
    /// A Player, Monster or NPC. Drawn as a Sprite in the game world.
    /// </summary>
    abstract class Character : IGameObject
    {
        public Vector3 Position { get; set; }
        public Sprite Sprite { get; set; }

        internal SpriteBatch sprite_batch;

        public Character(SpriteBatch batch) => sprite_batch = batch;
        public void Draw(float delta) => sprite_batch.Add(Sprite, Position);
        public abstract void Update(float delta);
    }
}
