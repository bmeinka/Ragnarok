using System.Diagnostics;
using Ragnarok.Core;
using Ragnarok.Gameplay.Control;

namespace Ragnarok.World.Monster
{
    class IdleState : MonsterState
    {
        private const float min = 2f, max = 4f;
        private readonly float duration;
        private readonly Stopwatch watch = new Stopwatch();

        public IdleState(Monster monster, Map map) : base(monster, map)
        {
            duration = Game.Random.Float(min, max);
            watch.Start();
        }
        public override void Update(Controller parent)
        {
            if (watch.Elapsed.TotalSeconds >= duration)
                parent.Replace(new MoveState(monster, map));
        }
    }
}
