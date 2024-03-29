﻿using OpenTK;

namespace Ragnarok.Core.Physics
{
    public delegate bool EnabledCallback();
    abstract class PhysicsBody
    {
        private bool enabled = true;

        /// <summary>
        /// a callback to determine if the body shouldbe enabled or disabled
        /// </summary>
        /// <remarks>overrides Enable() and Disable(). set to null to disable again and return to normal</remarks>
        public EnabledCallback EnabledCallback { get; set; }
        public bool Enabled { get { if (EnabledCallback != null) return EnabledCallback(); else return enabled; } }
        public bool Disabled => !Enabled;
        /// <summary>
        /// every object has mass. this is the inverse of that mass (more useful for calculations)
        /// </summary>
        public float InverseMass { get; protected set; }
        /// <summary>
        /// how fast the body is moving
        /// </summary>
        public float MovementSpeed { get; protected set; }
        /// <summary>
        /// the position in physics space of the body
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// where the body wants to be located in physics space
        /// </summary>
        public virtual Vector2 Destination { get; }
        /// <summary>
        /// the shape of the body used for collision calculations
        /// </summary>
        public ICollisionShape Shape { get; protected set; }
        public Vector2 Min => Shape.Min(Position);
        public Vector2 Max => Shape.Max(Position);
        public PhysicsBody(Vector2 position, ICollisionShape shape)
        {
            Position = position;
            Shape = shape;
        }
        public void Enable() => enabled = true;
        public void Disable() => enabled = false;
    }
}
