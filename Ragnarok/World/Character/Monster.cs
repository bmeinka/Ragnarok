using OpenTK;
using Ragnarok.Core;
using Ragnarok.Core.Graphics;

namespace Ragnarok.World
{
    class Monster : Character
    {
        public Monster(Scene scene, Sprite sprite, Vector2 position) : base(scene.SpriteBatch, scene.World, position) => Sprite = sprite;
        public override void Update(float delta)
        {
            // TODO: implement moving around
        }
    }
}
