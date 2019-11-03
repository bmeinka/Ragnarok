using OpenTK;

namespace Ragnarok.Core.Physics
{
    interface IPhysicsBody
    {
        /// <summary>
        /// every object has mass. this is the inverse of that mass (more useful for calculations)
        /// </summary>
        float InverseMass { get; }
        /// <summary>
        /// how fast the body is moving
        /// </summary>
        float MovementSpeed { get; }
        /// <summary>
        /// the position in physics space of the body
        /// </summary>
        Vector2 Position { get; set; }
        /// <summary>
        /// where the body wants to be located in physics space
        /// </summary>
        Vector2 Destination { get; }
        /// <summary>
        /// the shape of the body used for collision calculations
        /// </summary>
        ICollisionShape Shape { get; }
    }
}
