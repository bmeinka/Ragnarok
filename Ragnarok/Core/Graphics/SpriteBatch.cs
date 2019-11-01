﻿using System.Collections.Generic;
using OpenTK;
using Ragnarok.Core.Graphics.Shaders;

namespace Ragnarok.Core.Graphics
{
    // TODO: integrate into a rendering pipeline
    class SpriteBatch
    {
        private class Item
        {
            public Sprite Sprite { get; private set; }
            public Vector3 Position { get; private set; }
            public Item(Sprite sprite, Vector3 position)
            {
                Sprite = sprite;
                Position = position;
            }
        }

        private readonly List<Item> items;
        private readonly Camera camera;
        private readonly SpriteShader shader;

        public SpriteBatch(Scene scene)
        {
            items = new List<Item>();
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
        public void Add(Sprite sprite, Vector3 position) => items.Add(new Item(sprite, position));

        public void Draw(float delta)
        {
            shader.Use();
            foreach(var item in items)
            {
                var model = Matrix4.CreateRotationX(camera.Angle) * Matrix4.CreateRotationZ(camera.Rotation);
                model *= Matrix4.CreateTranslation(item.Position);
                shader.MVP = model * camera.ViewProjection;
                item.Sprite.Draw(delta);
            }
            items.Clear();
        }
    }
}