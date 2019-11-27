using OpenTK;
using Ragnarok.Core;
using Ragnarok.World;

namespace Ragnarok.Gameplay.Control.State
{
    delegate float RangeCallback();
    class Wander : IControlState
    {
        private readonly Mob mob;
        private readonly Map map;
        private readonly RangeCallback wait_time, move_range;
        public Wander(Mob mob, Map map, RangeCallback wait, RangeCallback move) =>
            (this.mob, this.map, wait_time, move_range) = (mob, map, wait, move);

        /// <summary>
        /// pick a random, but valid, destination
        /// </summary>
        /// <returns>a valid position on the map</returns>
        private Vector2 GetDestination()
        {
            var distance = move_range();
            var direction = Game.Random.Vector2();
            var destination = (direction * distance) + mob.Position;
            if (map.ValidPosition(destination))
                return destination;
            return GetDestination();
        }
        public void Update(Controller parent)
        {
            // push both a move and idle state to the stack
            // when they both complete, two more will get pushed, creating a loop
            parent.Push(new Move(mob, GetDestination(), 10f));
            parent.Push(new Idle(mob, wait_time()));
        }
    }
}
