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
        private Map map;
        private Shader shader;
        private float speed = 3f;
        private Vector3 target;

        public Scene(Window window)
        {
            map = new Map();
            shader = new Shader("shaders/core.vert", "shaders/core.frag");
            window.MouseDown += MouseDown;
            target = new Vector3(0, 0, 0);
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            var ray = Camera.ScreenToRay(e.X, e.Y);
            Vector3 pos;
            if (map.Intersect(ray, out pos))
                target = pos;
        }

        public void Update(double dt)
        {
            if (target != Camera.Target)
            {
                var distance = (float)dt * speed;
                var movement = target - Camera.Target;
                if (movement.Length < distance)
                    Camera.Target = target;
                else
                    Camera.Target += movement.Normalized() * distance;
            }
        }

        public void Render(double dt)
        {
            shader.Use();
            shader.Uniform("mvp", Camera.Transform);
            map.Render(dt);
        }
    }
}
