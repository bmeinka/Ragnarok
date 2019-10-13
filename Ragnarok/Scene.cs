using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;

namespace Ragnarok
{
    /// <summary>
    /// Manages the currently rendered scene, including the Camera and Map.
    /// </summary>
    class Scene
    {
        private Map map;
        private Shader shader;
        private float speed = 3f;
        public Scene()
        {
            map = new Map();
            shader = new Shader("shaders/core.vert", "shaders/core.frag");
        }
        public void Update(double dt)
        {
            var input = Keyboard.GetState();
            var movement = Vector3.Zero;

            if (input.IsKeyDown(Key.Left)) movement.X -= 1;
            if (input.IsKeyDown(Key.Right)) movement.X += 1;
            if (input.IsKeyDown(Key.Up)) movement.Y += 1;
            if (input.IsKeyDown(Key.Down)) movement.Y -= 1;

            Camera.Target += movement * (float)dt * speed;
        }

        public void Render(double dt)
        {
            shader.Use();
            shader.Uniform("mvp", Camera.Transform);
            map.Render(dt);
        }
    }
}
