using OpenTK;

namespace Ragnarok.Core.Physics
{
    interface ICollisionShape
    {
        Vector2 Min(Vector2 position);
        Vector2 Max(Vector2 position);
    }
}
