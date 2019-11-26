using Ragnarok.World;

namespace Ragnarok.Gameplay.Control.State
{
    class Idle : TimeoutState
    {
        public Idle(Mob mob, float timeout = 0f) : base(timeout) =>
            mob.MoveTo(mob.Position);

        public override string ToString() => "Idle";
    }
}
