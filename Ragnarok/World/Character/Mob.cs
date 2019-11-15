using OpenTK;
using Ragnarok.Core.Graphics;
using Ragnarok.Core.Physics;

namespace Ragnarok.World
{
    /// <summary>
    /// A Player or a Monster. Drawn as a Sprite in the game world.
    /// </summary>
    abstract class Mob
    {
        protected DynamicBody body;
        protected readonly Sprite sprite;
        public Vector3 Position => new Vector3(body.Position.X, body.Position.Y, 0f);
        public Mob(Sprite sprite) => this.sprite = sprite;
        public void Spawn(Map map, Vector2 position) => body = map.SpawnMob(position);
        public void Draw(SpriteBatch sb) => sb.Add(sprite, Position);
        public void Move(Vector2 direction) => body.Move(direction);
        public void MoveTo(Vector2 position) => body.MoveTo(position);
        public void Stop() => body.MoveTo(body.Position);
    }
}
