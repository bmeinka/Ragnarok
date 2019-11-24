using Ragnarok.Gameplay.Control;

namespace Ragnarok.World.Monster
{
    class MonsterController : Controller
    {
        public Monster Monster { get; private set; }
        public Map Map { get; private set; }
        public MonsterController(Monster monster, Map map)
        {
            Monster = monster;
            Map = map;
        }
        public override IControlState GetDefaultState() => new IdleState();
    }
}
