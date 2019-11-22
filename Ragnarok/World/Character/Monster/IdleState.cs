using System.Diagnostics;
using Ragnarok.Core;

namespace Ragnarok.World.Monster
{
    class IdleState : MonsterState
    {
        private const float min = 2f, max = 4f;
        private readonly float duration;
        private readonly Stopwatch watch = new Stopwatch();

        public IdleState()
        {
            duration = Game.Random.Float(min, max);
            watch.Start();
        }
        public override void Update(MonsterController parent)
        {
            if (watch.Elapsed.TotalSeconds >= duration)
                parent.Replace(new MoveState(parent));
        }
    }
}
