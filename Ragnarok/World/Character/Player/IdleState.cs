using Ragnarok.Gameplay.Control;

namespace Ragnarok.World.Player
{
    class IdleState : IControlState
    {
        public IdleState(Player player) => player.MoveTo(player.Position);
        public void Update(Controller parent) { }
    }
}
