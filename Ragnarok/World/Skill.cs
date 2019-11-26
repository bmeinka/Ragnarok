namespace Ragnarok.World
{
    struct Skill
    {
        public int Damage { get; private set; }
        public float Range { get; private set; }
        public float Delay { get; private set; }
        public Skill(int damage, float range, float delay) =>
            (Damage, Range, Delay) = (damage, range, delay);
    }
}
