using OpenTK;
using Ragnarok.World;

namespace Ragnarok.Gameplay.Control.State
{
    class Chase : IControlState
    {
        private readonly Mob chaser, chased;
        private readonly float range;

        /// <summary>
        /// chase a mob to within a certain distance to that mob
        /// </summary>
        /// <param name="chaser">the mob doing the chasing</param>
        /// <param name="chased">the mob being chased</param>
        /// <param name="range">the minimum distance to consider the chased mob caught</param>
        /// <param name="sight">the maximum distance before considering the chased mob as gotten away</param>
        public Chase(Mob chaser, Mob chased, float range) =>
            (this.chaser, this.chased, this.range) = (chaser, chased, range);

        public void Update(Controller parent)
        {
            chaser.MoveTo(chased.Position);
            var distance = (chased.Position - chaser.Position).Length;
            if (distance <= range)
                parent.Pop();
        }

        public override string ToString() => "Chase";
    }
}
