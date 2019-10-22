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
        public Camera Camera { get; set; }

        private Map map;
        private Shader map_shader;
        private Sprite sprite;
        private float speed = 3f;
        private Vector3 target;

        public Scene(Window window)
        {
            Camera = new Camera(window);
            map = new Map();
            map_shader = new Shader("shaders/core.vert", "shaders/core.frag");
            sprite = new Sprite(new Vector2(1f, 2f), new Vector3(0f, 0.2f, 0.8f));
            Sprite.Shader = new Shader("shaders/sprite.vert", "shaders/sprite.frag");
            Game.Mouse.Move += MouseMove;
            Game.Mouse.Scroll += Scroll;
            Game.Mouse.DoubleClick += DoubleClick;
            window.UpdateFrame += Update;
            target = new Vector3(0, 0, 0);
        }

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Right)
            {
                Camera.Rotation = 0f;
                Camera.Angle = MathHelper.DegreesToRadians(45f);
            }
        }

        private void Scroll(object sender, MouseWheelEventArgs e)
        {
            Camera.Zoom -= e.DeltaPrecise * 0.1f;
        }

        private void MouseMove(object sender, MouseMoveEventArgs e)
        {
            if (e.Mouse.IsButtonDown(MouseButton.Right))
            {
                var keyboard = Keyboard.GetState();
                if (keyboard.IsKeyDown(Key.LShift))
                    Camera.Angle -= e.YDelta * 0.01f;
                else
                    Camera.Rotation += e.XDelta * 0.01f;
            }
        }

        private void Update(object sender, FrameEventArgs e)
        {
            // update the target to the mouse click position
            if (Game.Mouse.IsButtonDown(MouseButton.Left))
            {
                var ray = Camera.GetRay(Game.Mouse.X, Game.Mouse.Y);
                if (map.Intersect(ray, out Vector3 pos))
                    target = pos;
            }

            // move toward the target
            if (target != Camera.Target)
            {
                var distance = (float)e.Time * speed;
                var movement = target - Camera.Target;
                if (movement.Length < distance)
                    Camera.Target = target;
                else
                    Camera.Target += movement.Normalized() * distance;
            }
        }

        public void Render(double dt)
        {
            map_shader.Use();
            map_shader.Uniform("mvp", Camera.ViewProjection);
            map.Render(dt);

            sprite.Position = new Vector3(1f, 1f, 0f);
            Sprite.Shader.Use();
            Sprite.Shader.Uniform("mvp", Camera.ViewProjection);
            sprite.Render(dt);
        }
    }
}
