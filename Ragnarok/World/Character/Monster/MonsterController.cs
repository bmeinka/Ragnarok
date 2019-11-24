using Ragnarok.Gameplay.Control;

namespace Ragnarok.World.Monster
{
    class MonsterController : Controller
    {
        private readonly Monster monster;
        private readonly Map map;
        public MonsterController(Monster monster, Map map)
        {
            this.monster = monster;
            this.map = map;
        }
        public override IControlState GetDefaultState() => new IdleState(monster, map);
    }
}
