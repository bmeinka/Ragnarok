using OpenTK;

namespace Ragnarok.Core.Physics
{
    class Box
    {
        private readonly Vector2 min, max;

        public Box(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
