using Ragnarok.World;

namespace Ragnarok.Gameplay.Control.State
{
    class ApplySkill : TimeoutState
    {
        private readonly Mob target;
        private readonly Skill skill;
        public ApplySkill(Mob target, Skill skill) : base(skill.Delay) =>
            (this.target, this.skill) = (target, skill);
        public override void Update(Controller parent)
        {
            if (Elapsed)
                target.TakeHit(skill);
            base.Update(parent);
        }

        public override string ToString() => "ApplySkill";
    }
}
