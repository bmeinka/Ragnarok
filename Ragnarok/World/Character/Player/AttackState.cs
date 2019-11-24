using Ragnarok.Gameplay.Control;

namespace Ragnarok.World.Player
{
    class AttackState : PlayerState
    {
        private readonly Mob target;
        public AttackState(Player player, Mob target) : base(player)
        {
            this.target = target;
            player.MoveTo(target.Position);
        }
        public override void Update(Controller parent)
        {
            if (!target.IsAlive())
                parent.Pop();
            var distance = (target.Position - player.Position).Length;
            if (distance <= 1.1f)
                target.TakeHit(player.ATK);
            else
                player.MoveTo(target.Position);
        }
    }
}
