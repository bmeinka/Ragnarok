using OpenTK;
using System.Diagnostics;
using Ragnarok.Core;
using Ragnarok.Gameplay.Control;

namespace Ragnarok.World.Monster
{
    class MoveState : MonsterState
    {
        private const float duration = 10f;
        private const float min = 3f, max = 7f;
        private readonly Stopwatch watch = new Stopwatch();
        private readonly Vector2 Destination;
        public MoveState(Monster monster, Map map) : base(monster, map)
        {
            var direction = Game.Random.Vector2();
            var distance = Game.Random.Float(min, max);
            Destination = monster.Position + (direction * distance);
            Destination.X = MathHelper.Clamp(Destination.X, 0.5f, map.Width - 0.5f);
            Destination.Y = MathHelper.Clamp(Destination.Y, 0.5f, map.Height - 0.5f);
            monster.MoveTo(Destination);
            watch.Start();
        }
        public override void Update(Controller parent)
        {
            if (monster.Position == Destination || watch.Elapsed.TotalSeconds >= duration)
                parent.Replace(new IdleState(monster, map));
        }
    }
}
