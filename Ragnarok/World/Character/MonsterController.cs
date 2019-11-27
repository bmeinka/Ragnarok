using Ragnarok.Core;
using Ragnarok.Gameplay.Control;
using Ragnarok.Gameplay.Control.State;

namespace Ragnarok.World
{
    class MonsterController : Controller
    {
        private readonly (float Min, float Max) idle_range = (2f, 4f);
        private readonly (float Min, float Max) move_range = (3f, 7f);
        private readonly Monster monster;
        private readonly Map map;

        public MonsterController(Monster monster, Map map) =>
            (this.monster, this.map) = (monster, map);

        private float WaitTime() => Game.Random.Float(idle_range);
        private float MoveRange() => Game.Random.Float(move_range);

        public override IControlState GetDefaultState() =>
            new Wander(monster, map, WaitTime, MoveRange);
    }
}
