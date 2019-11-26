using Ragnarok.World;

namespace Ragnarok.Gameplay.Control.State
{
    class Attack : IControlState
    {
        private readonly Mob attacker, target;
        public Attack(Mob attacker, Mob target) =>
            (this.attacker, this.target) = (attacker, target);

        public void Update(Controller parent)
        {
            // TODO: add an external conditional to determine if the attacks should continue
            if (!target.IsAlive())
                parent.Pop();
            else
                parent.Push(new UseSkill(attacker, target, attacker.BasicAttack));
        }

        public override string ToString() => "Attack";
    }
}
