using System;
using OpenTK;

namespace Ragnarok.Character
{
    class Monster : Character
    {
        public Monster(Scene scene, Sprite sprite) : base(scene.SpriteBatch)
        {
            Sprite = sprite;
            var x = (float)Game.Random.NextDouble() * scene.Map.Width;
            var y = (float)Game.Random.NextDouble() * scene.Map.Height;
            Position = new Vector3(x, y, 0f);
        }

        public override void Update(float delta)
        {
            // TODO: implement moving around
        }
    }
}
