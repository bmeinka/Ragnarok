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
        /// used to scale the angle adjustments to make sure they aren't too fast or slow
        /// </summary>
        private const float angle_factor = 0.01f;
        /// <summary>
        /// how far to zoom per mouse-scroll step
        /// </summary>
        private const float zoom_factor = 0.1f;

        /// <summary>
        /// how long to wait after the mouse down event before calling MoveTo again
        /// </summary>
        /// <remarks>use to increase single click movement accuracy</remarks>
        private const float move_delay = 0.5f;

        private readonly Player player;
        private readonly Camera camera;
        private readonly Map map;
        private readonly Stopwatch timer;

        public PlayerController(Scene scene, Player player)
        {
            this.player = player;
            camera = scene.Camera;
            map = scene.Map;
            timer = new Stopwatch();

            Game.Mouse.Down += StartMovement;
            Game.Mouse.Move += Rotate;
            Game.Mouse.Scroll += Zoom;
            Game.Mouse.DoubleClick += Reset;
        }

        private void Move()
        {
            // TODO: figure out null reference from camera.MouseRay
            var ray = camera.GetRay(Game.Mouse.X, Game.Mouse.Y);
            if (map.Intersect(ray, out Vector2 pos))
                player.MoveTo(pos);
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

        private void Reset(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Right)
            {
                camera.Rotation = 0f;
                camera.Angle = MathHelper.DegreesToRadians(45f);
            }
        }

        private void Zoom(object sender, MouseWheelEventArgs e) => camera.Zoom -= e.DeltaPrecise * zoom_factor;

        private void Rotate(object sender, MouseMoveEventArgs e)
        {
            if (e.Mouse.IsButtonDown(MouseButton.Right))
            {
                var keyboard = Keyboard.GetState();
                if (keyboard.IsKeyDown(Key.LShift))
                    camera.Angle -= e.YDelta * angle_factor;
                else
                    camera.Rotation += e.XDelta * angle_factor;
            }
        }

        public void Update(float delta)
        {
            if (Game.Mouse.IsButtonDown(MouseButton.Left) && timer.Elapsed.TotalSeconds >= move_delay)
                Move();
            camera.Target = player.Position;
        }
    }
}
