using OpenTK;
using Ragnarok.Core.Graphics;

namespace Ragnarok.World
{
    class Player : Mob
    {
        public Player() : base(new Sprite(new Vector2(1f, 2f), new Vector3(0f, 0.5f, 0.8f))) { }
    }
}
