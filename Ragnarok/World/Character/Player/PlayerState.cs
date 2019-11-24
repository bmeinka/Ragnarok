using Ragnarok.Gameplay.Control;

namespace Ragnarok.World.Player
{
    abstract class PlayerState : IControlState
    {
        protected readonly Player player;
        public PlayerState(Player player) => this.player = player;
        public abstract void Update(Controller parent);
    }
}
