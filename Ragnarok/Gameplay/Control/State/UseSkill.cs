using Ragnarok.World;

namespace Ragnarok.Gameplay.Control.State
{
    class UseSkill : IControlState
    {
        private readonly Mob user, target;
        private readonly Skill skill;
        public UseSkill(Mob user, Mob target, Skill skill) =>
            (this.user, this.target, this.skill) = (user, target, skill);

        public void Update(Controller parent)
        {
            var distance = (target.Position - user.Position).Length;
            if (distance > skill.Range)
                parent.Push(new Chase(user, target, skill.Range));
            else
                parent.Replace(new ApplySkill(user, target, skill));
        }

        public override string ToString() => "UseSkill";
    }
}
