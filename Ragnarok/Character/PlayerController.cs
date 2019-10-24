using OpenTK;
using OpenTK.Input;
using System;

namespace Ragnarok.Character
{
    class PlayerController : IUpdateable
    {
        /// <summary>
        /// used to scale the angle adjustments to make sure they aren't too fast or slow
        /// </summary>
        private const float angle_factor = 0.01f;
        /// <summary>
        /// how far to zoom per mouse-scroll step
        /// </summary>
        private const float zoom_factor = 0.1f;

        private readonly Player player;
        private readonly Camera camera;
        private readonly Map map;

        public PlayerController(Scene scene, Player player)
        {
            this.player = player;
            camera = scene.Camera;
            map = scene.Map;

            Game.Mouse.Move += Rotate;
            Game.Mouse.Scroll += Zoom;
            Game.Mouse.DoubleClick += Reset;
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
            if (Game.Mouse.IsButtonDown(MouseButton.Left))
            {
                var ray = camera.GetRay(Game.Mouse.X, Game.Mouse.Y);
                if (map.Intersect(ray, out Vector3 pos))
                    player.Target = pos;
            }
            camera.Target = player.Position;
        }
    }
}
