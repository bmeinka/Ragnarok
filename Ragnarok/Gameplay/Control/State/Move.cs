using OpenTK;
using Ragnarok.World;

namespace Ragnarok.Gameplay.Control.State
{
    /// <summary>
    /// used to handle moving to a specific location
    /// </summary>
    class Move : TimeoutState
    {
        private readonly Mob mob;
        private readonly Vector2 position;
        public Move(Mob mob, Vector2 position, float timeout = 0f) : base(timeout)
        {
            this.mob = mob;
            this.position = position;
        }

        public override void Update(Controller parent)
        {
            mob.MoveTo(position);
            if (mob.Position == position)
                parent.Pop();
            base.Update(parent);
        }

        public override string ToString() => "Move";
    }
}
