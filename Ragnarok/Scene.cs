using System;
using OpenTK;
using OpenTK.Input;

namespace Ragnarok
{
    /// <summary>
    /// Manages the currently rendered scene, including the Camera and Map.
    /// </summary>
    class Scene
    {
        private Camera camera;
        private Map map;
        private Shader shader;
        private float speed = 3f;
        private Vector3 target;

        public Scene(Window window)
        {
            camera = new Camera(window);
            map = new Map();
            shader = new Shader("shaders/core.vert", "shaders/core.frag");
            window.MouseMove += MouseMove;
            window.MouseWheel += Scroll;
            window.UpdateFrame += Update;
            window.DoubleClick += DoubleClick;
            target = new Vector3(0, 0, 0);
        }

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Right)
                camera.Rotation = 0f;
        }

        private void Scroll(object sender, MouseWheelEventArgs e)
        {
            camera.Zoom -= e.DeltaPrecise * 0.1f;
        }

        private void MouseMove(object sender, MouseMoveEventArgs e)
        {
            if (e.Mouse.IsButtonDown(MouseButton.Right))
            {
                var window = (Window)sender;
                if (window.IsKeyDown(Key.LShift))
                    camera.Angle -= e.YDelta * 0.01f;
                else
                    camera.Rotation += e.XDelta * 0.01f;
            }
        }

        private void Update(object sender, FrameEventArgs e)
        {
            var window = (Window)sender;

            // update the target to the mouse click position
            if (window.IsButtonDown(MouseButton.Left))
            {
                var ray = camera.GetRay((int)window.MousePosition.X, (int)window.MousePosition.Y);
                if (map.Intersect(ray, out Vector3 pos))
                    target = pos;
            }

            // move toward the target
            if (target != camera.Target)
            {
                var distance = (float)e.Time * speed;
                var movement = target - camera.Target;
                if (movement.Length < distance)
                    camera.Target = target;
                else
                    camera.Target += movement.Normalized() * distance;
            }
        }

        public void Render(double dt)
        {
            shader.Use();
            shader.Uniform("mvp", camera.ViewProjection);
            map.Render(dt);
        }
    }
}
