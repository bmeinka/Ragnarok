using System;
using OpenTK;
using OpenTK.Input;
using Ragnarok.Character;

namespace Ragnarok
{
    /// <summary>
    /// Manages the currently rendered scene, including the Camera and Map.
    /// </summary>
    class Scene : IGameObject
    {
        public Camera Camera { get; private set; }
        public Map Map { get; private set; }
        public Player Player { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public Scene(Window window)
        {
            Camera = new Camera(window);
            Map = new Map();
            // TODO: fix dependencies
            // maybe it isn't such a good idea to just be copying out the things that we need, because
            // if the player is created before the spritebatch... null reference exception
            SpriteBatch = new SpriteBatch(this);
            Player = new Player(this);

            Map.Shader = new Shader("shaders/core.vert", "shaders/core.frag");
            Sprite.Shader = new Shader("shaders/sprite.vert", "shaders/sprite.frag");
        }

        public void Update(float delta)
        {
            Player.Update(delta);
        }

        public void Draw(float delta)
        {
            Map.Draw(delta);

            // drawing sprite-based objects only queues them up in the spritebatch
            // calling the draw method on the sprite batch actually draws them
            Player.Draw(delta);
            SpriteBatch.Draw(delta);
        }
    }
}
