using OpenTK;
using Ragnarok.Core;
using Ragnarok.Core.Graphics;
using Ragnarok.Core.Physics;
using Ragnarok.Gameplay;

namespace Ragnarok.World
{
    class Monster : Character
    {
        private readonly DynamicBody body;
        private readonly MonsterController controller;
        protected override PhysicsBody Body => body;
        public Monster(Scene scene, Sprite sprite, Vector2 position) : base(scene.SpriteBatch)
        {
            body = scene.World.AddDynamicBody(position, new Circle(0.5f));
            body.MoveTo(position);
            Sprite = sprite;
            controller = new MonsterController(this);
            controller.Start();
        }
        public void Move(Vector2 direction) => body.Move(direction);
        public void Stop() => body.MoveTo(body.Position);
        public override void Update(float delta)
        {
            // TODO: implement moving around
        }
    }
}
