using OpenTK;
using System.Diagnostics;
using Ragnarok.Core;

namespace Ragnarok.World.Monster
{
    class MoveState : MonsterState
    {
        private const float duration = 10f;
        private const float min = 3f, max = 7f;
        private readonly Stopwatch watch = new Stopwatch();
        private readonly Vector2 Destination;
        public MoveState(MonsterController parent) 
        {
            var direction = Game.Random.Vector2();
            var distance = Game.Random.Float(min, max);
            Destination = parent.Monster.Position + (direction * distance);
            Destination.X = MathHelper.Clamp(Destination.X, 0.5f, parent.Map.Width - 0.5f);
            Destination.Y = MathHelper.Clamp(Destination.Y, 0.5f, parent.Map.Height - 0.5f);
            parent.Monster.MoveTo(Destination);
            watch.Start();
        }
        public override void Update(MonsterController parent)
        {
            if (parent.Monster.Position == Destination || watch.Elapsed.TotalSeconds >= duration)
                parent.Replace(new IdleState());
        }
    }
}
