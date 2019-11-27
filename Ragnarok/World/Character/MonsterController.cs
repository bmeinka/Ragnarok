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

        public MonsterController(Monster monster, Map map)
        {
            (this.monster, this.map) = (monster, map);
            monster.Hit += OnHit;
            monster.Died += OnDeath;
        }

        private void OnDeath(object sender, HitEvent e) =>
            Enabled = false;

        private void OnHit(object sender, HitEvent e) =>
            Collapse(new Attack(monster, e.Attacker));

        private float WaitTime() => Game.Random.Float(idle_range);
        private float MoveRange() => Game.Random.Float(move_range);

        public override IControlState GetDefaultState() =>
            new Wander(monster, map, WaitTime, MoveRange);
    }
}
