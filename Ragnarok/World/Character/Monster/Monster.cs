using Ragnarok.Core.Graphics;

namespace Ragnarok.World.Monster
{
    class Monster : Mob
    {
        private readonly MonsterController controller;
        public Monster(Sprite sprite, Map map) : base(sprite) => controller = new MonsterController(this, map);
        public void Update() => controller.Update();
    }
}
