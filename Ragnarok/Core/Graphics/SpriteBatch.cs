using System.Collections.Generic;
using OpenTK;
using Ragnarok.Core.Graphics.Shaders;

namespace Ragnarok.Core.Graphics
{
    // TODO: integrate into a rendering pipeline
    class SpriteBatch
    {
        private readonly List<(Sprite sprite, Vector3 position)> items;
        private readonly Camera camera;
        private readonly SpriteShader shader;

        public SpriteBatch(Scene scene)
        {
            items = new List<(Sprite sprite, Vector3 position)>();
            camera = scene.Camera;
            shader = new SpriteShader();
        }

        /// <summary>
        /// Add a sprite render to the batch queue
        /// </summary>
        /// <param name="sprite">the sprite to draw</param>
        /// <param name="position">the position to draw the sprite in</param>
        /// <remarks>
        /// Everything in the queue is drawn when <see cref="SpriteBatch.Draw(float)"/> is called.
        /// After drawing everything, the queue gets cleared.
        /// </remarks>
        public void Add(Sprite sprite, Vector3 position) => items.Add((sprite, position));

        public void Draw()
        {
            shader.Use();
            foreach(var (sprite, position) in items)
            {
                var model = Matrix4.CreateRotationX(camera.Angle) * Matrix4.CreateRotationZ(camera.Rotation);
                model *= Matrix4.CreateTranslation(position);
                shader.MVP = model * camera.ViewProjection;
                sprite.Draw();
            }
            items.Clear();
        }
    }
}
