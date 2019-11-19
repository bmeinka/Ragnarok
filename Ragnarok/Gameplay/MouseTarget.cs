﻿using OpenTK;
using Ragnarok.Core;
using Ragnarok.World;

namespace Ragnarok.Gameplay
{
    /// <summary>
    /// a general idea about what the mouse is actually hovering over
    /// </summary>
    class MouseTarget
    {
        public enum TargetType { None, Terrain, Monster }
        public TargetType Type { get; private set; }
        public Vector2 Position { get; private set; }
        public Monster Monster { get; private set; }
        public MouseTarget(Camera camera, Map map)
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
            foreach (var monster in map.Monsters)
            {
                plane.Normal = -(camera.Target - camera.Position).Normalized();
                plane.Origin = monster.Position;
                var pos = ray.Parametric(ray.Intersect(plane));
                pos -= monster.Position;
                if (pos.X >= -0.5f && pos.X <= 0.5f && pos.Y >= 0f && pos.Y <= 1f) // TODO: figure out how to use actual sprite size information
                {
                    Type = TargetType.Monster;
                    Monster = monster;
                }
            }
        }
    }
}
