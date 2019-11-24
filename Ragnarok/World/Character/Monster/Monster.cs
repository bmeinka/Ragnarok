using Ragnarok.Core.Graphics;

namespace Ragnarok.World.Monster
{
    class Monster : Mob
    {
        private readonly MonsterController controller;

        public Monster(Sprite sprite, Map map) : base(sprite)
        {
            HP = 50;
            ATK = 7;
            controller = new MonsterController(this, map);
        }
        public void Update() => controller.Update();
    }
}
