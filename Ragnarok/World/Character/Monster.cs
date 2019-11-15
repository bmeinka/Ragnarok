using OpenTK;
using Ragnarok.Core;
using Ragnarok.Core.Graphics;
using Ragnarok.Core.Physics;
using Ragnarok.Gameplay;

namespace Ragnarok.World
{
    class Monster : Mob
    {
        private readonly MonsterController controller;
        public Monster(Sprite sprite) : base(sprite)
        {
            controller = new MonsterController(this);
            controller.Start();
        }
    }
}
