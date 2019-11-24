using OpenTK;
using Ragnarok.Gameplay.Control;

namespace Ragnarok.World.Player
{
    class MoveState : PlayerState
    {
        private readonly Vector2 position;
        public MoveState(Player player, Vector2 position) : base(player)
        {
            this.position = position;
            player.MoveTo(position);
        }
        public override void Update(Controller parent)
        {
            if (player.Position == position)
                parent.Pop();
        }
    }
}
