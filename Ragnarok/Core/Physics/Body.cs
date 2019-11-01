using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Ragnarok.Core.Physics
{
    class Body
    {
        public float Speed => 3f;
        public Vector2? Target { get; private set; }
        public Vector2? Direction { get; private set; }
        public Vector2 Position { get; set; }
        public Body(Vector2 position) => Position = position;
        public Body(float x, float y) : this(new Vector2(x, y))
        {
            Target = null;
            Direction = null;
        }

        public void Move(Vector2 direction)
        {
            Target = null;
            Direction = direction.Normalized();
        }
        public void MoveTo(Vector2 target)
        {
            Direction = null;
            Target = target;
            //if (Target != Position)
            //{
            //    var distance = delta * walk_speed;
            //    var movement = Target - Position;
            //    // if the player can make it to the destination this frame,
            //    // just go ahead and place it where it wants to be.
            //    if (movement.Length < distance)
            //        Position = Target;
            //    else
            //        Position += movement.Normalized() * distance;
            //}
        }

        public void Update(float delta)
        {
            if (Target != null)
            {
                var target = (Vector2)Target;
                if (target != Position)
                {
                    var distance = delta * Speed;
                    var movement = target - Position;
                    if (movement.Length < distance)
                    {
                        Position = target;
                        Target = null;
                    }
                    else
                        Position += movement.Normalized() * distance;
                }
            }
            else if (Direction != null)
            {
                var direction = (Vector2)Direction;
                var distance = delta * Speed;
                Position += direction.Normalized() * distance;
            }
        }
    }
}
