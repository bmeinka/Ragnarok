using OpenTK;
using Ragnarok.World;

namespace Ragnarok.Gameplay
{
    class MoveState : IControlState
    {
        private readonly Mob mob;
        public Vector2 Position { get; set; }
        public MoveState(Mob mob, Vector2 position)
        {
            this.mob = mob;
            Position = position;
            mob.MoveTo(position);
        }
        public void Update(Controller parent)
        {
            if (mob.Position == Position)
                parent.Pop();
        }
    }
}
