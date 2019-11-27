using Ragnarok.World;

namespace Ragnarok.Gameplay.Control.State
{
    class ApplySkill : TimeoutState
    {
        private readonly Mob attacker;
        private readonly Mob target;
        private readonly Skill skill;
        public ApplySkill(Mob attacker, Mob target, Skill skill) : base(skill.Delay) =>
            (this.attacker, this.target, this.skill) = (attacker, target, skill);
        public override void Update(Controller parent)
        {
            if (Elapsed)
                target.TakeHit(attacker, skill);
            base.Update(parent);
        }

        public override string ToString() => "ApplySkill";
    }
}
