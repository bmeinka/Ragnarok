using OpenTK;

namespace Ragnarok.Core.Physics
{
    interface ICollisionShape
    {
        float HalfWidth { get; }
        float HalfHeight { get; }
        Vector2 Min(Vector2 position);
        Vector2 Max(Vector2 position);
    }
}
