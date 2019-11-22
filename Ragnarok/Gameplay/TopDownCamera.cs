using OpenTK;
using OpenTK.Input;
using Ragnarok.Core;

namespace Ragnarok.Gameplay
{
    class TopDownCamera : Camera
    {
        /// <summary>
        /// used to scale the angle adjustments to make sure they aren't too fast or slow
        /// </summary>
        private const float angle_factor = 0.01f;
        /// <summary>
        /// how far to zoom per mouse-scroll step
        /// </summary>
        private const float zoom_factor = 0.1f;

        public TopDownCamera()
        {
            Game.Mouse.Move += Rotate;
            Game.Mouse.Scroll += ChangeZoom;
            Game.Mouse.DoubleClick += Reset;
        }
        private void Reset(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Right)
            {
                Rotation = 0f;
                Angle = MathHelper.DegreesToRadians(45f);
            }
        }
        private void Rotate(object sender, MouseMoveEventArgs e)
        {
            if (e.Mouse.IsButtonDown(MouseButton.Right))
            {
                var keyboard = Keyboard.GetState();
                if (keyboard.IsKeyDown(Key.LShift))
                    Angle -= e.YDelta * angle_factor;
                else
                    Rotation += e.XDelta * angle_factor;
            }
        }
        private void ChangeZoom(object sender, MouseWheelEventArgs e) => Zoom -= e.DeltaPrecise * zoom_factor;
    }
}
