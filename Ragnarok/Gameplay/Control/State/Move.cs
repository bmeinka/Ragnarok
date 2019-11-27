using OpenTK;
using Ragnarok.World;

namespace Ragnarok.Gameplay.Control.State
{
    /// <summary>
    /// used to handle moving to a specific location
    /// </summary>
    class Move : TimeoutState
    {
        public delegate Vector2 PositionCallback();

        private readonly Mob mob;
        private readonly PositionCallback get_position;

        public Move(Mob mob, Vector2 position, float timeout = 0f) : this(mob, () => position, timeout) { }
        public Move(Mob mob, PositionCallback callback, float timeout = 0f) : base(timeout) =>
            (this.mob, this.get_position) = (mob, callback);

        public override void Update(Controller parent)
        {
            var position = get_position();
            mob.MoveTo(position);
            if (mob.Position == position)
                parent.Pop();
            base.Update(parent);
        }

        public override string ToString() => "Move";
    }
}
