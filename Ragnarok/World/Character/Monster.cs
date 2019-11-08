using OpenTK;
using Ragnarok.Core;
using Ragnarok.Core.Graphics;
using Ragnarok.Core.Physics;

namespace Ragnarok.World
{
    class Monster : Character
    {
        private readonly DynamicBody body;
        protected override PhysicsBody Body => body;
        public Monster(Scene scene, Sprite sprite, Vector2 position) : base(scene.SpriteBatch)
        {
            body = scene.World.AddDynamicBody(position, new Circle(0.5f));
            body.MoveTo(position);
            Sprite = sprite;
        }
        public override void Update(float delta)
        {
            // TODO: implement moving around
        }
    }
}
