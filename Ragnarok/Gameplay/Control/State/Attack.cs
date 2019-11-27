using Ragnarok.World;

namespace Ragnarok.Gameplay.Control.State
{
    class Attack : IControlState
    {
        public delegate bool Callback();

        private readonly Mob attacker, target;
        private readonly Callback should_attack;
        public Attack(Mob attacker, Mob target, Callback callback = null) =>
            (this.attacker, this.target, should_attack) = (attacker, target, callback);

        public void Update(Controller parent)
        {
            if (should_attack != null && !should_attack())
                parent.Pop();
            else if (!target.IsAlive())
                parent.Pop();
            else
                parent.Push(new UseSkill(attacker, target, attacker.BasicAttack));
        }

        public override string ToString() => "Attack";
    }
}
