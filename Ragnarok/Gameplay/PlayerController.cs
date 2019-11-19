using System;
using System.Diagnostics;
using OpenTK;
using OpenTK.Input;
using Ragnarok.Core;
using Ragnarok.World;

namespace Ragnarok.Gameplay
{
    class PlayerController
    {
        /// <summary>
        /// how long to wait after the mouse down event before calling MoveTo again
        /// </summary>
        /// <remarks>use to increase single click movement accuracy</remarks>
        private const float move_delay = 0.5f;

        private readonly Player player;
        private readonly Camera camera;
        private readonly Map map;
        private readonly Stopwatch timer;

        public PlayerController(Camera camera, Map map, Player player)
        {
            // TODO: single responsibility; split out camera controls to its own controller
            this.player = player;
            this.camera = camera;
            this.map = map;
            timer = new Stopwatch();

            Game.Mouse.Down += StartMovement;
        }

        private void Move()
        {
            if (map.MouseIntersection(camera, out Vector2 position))
                player.MoveTo(position);
        }

        private void StartMovement(object sender, MouseButtonEventArgs e)
        {
            // move to where the player clicked and start the timer for the click delay
            if (e.Button == MouseButton.Left)
            {
                Move();
                timer.Restart();
            }
        }
        public void Update(float delta)
        {
            var target = new MouseTarget(camera, map);
            switch (target.Type)
            {
                case MouseTarget.TargetType.Terrain:
                    Game.Window.CursorVisible = true;
                    break;
                case MouseTarget.TargetType.Monster:
                    Game.Window.CursorVisible = false;
                    break;
                case MouseTarget.TargetType.None:
                    Game.Window.CursorVisible = true;
                    break;
            }
            if (Game.Mouse.IsButtonDown(MouseButton.Left) && timer.Elapsed.TotalSeconds >= move_delay)
                Move();
            camera.Target = player.Position;
        }
    }
}
