using Ragnarok.Core.Graphics;

namespace Ragnarok.World.Monster
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
