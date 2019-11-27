using System.Linq;
using OpenTK;
using Ragnarok.Core;
using Ragnarok.World;

namespace Ragnarok.Gameplay
{
    enum TargetType { None, Terrain, Monster }
    /// <summary>
    /// a general idea about what the mouse is actually hovering over
    /// </summary>
    class MouseTarget
    {
        public TargetType Type { get; private set; }
        public Vector2 Position { get; private set; }
        public Monster Monster { get; private set; }
        public MouseTarget(TopDownCamera camera, Map map)
        {
            Type = TargetType.None;
            // use the default assumption that the thing is hitting the terrain.
            if (map.MouseIntersection(camera, out Vector2 position))
            {
                Type = TargetType.Terrain;
                Position = position;
            }

            var ray = camera.GetRay(Game.Mouse.X, Game.Mouse.Y);
            var plane = new Plane();
            foreach (var monster in from m in map.Monsters where m.IsAlive() select m)
            {
                plane.Normal = -(camera.Target - camera.Position).Normalized();
                plane.Origin = new Vector3(monster.Position.X, monster.Position.Y, 0f);
                var pos = ray.Parametric(ray.Intersect(plane));
                pos -= plane.Origin;
                if (pos.X >= -0.5f && pos.X <= 0.5f && pos.Y >= 0f && pos.Y <= 1f) // TODO: figure out how to use actual sprite size information
                {
                    Type = TargetType.Monster;
                    Monster = monster;
                }
            }
        }
    }
}
