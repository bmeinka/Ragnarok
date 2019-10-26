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
        private readonly Monster[] monsters;
        private readonly Sprite monster_sprite;

        public Scene(Window window)
        {
            Camera = new Camera(window);
            Map = new Map();
            // TODO: fix dependencies
            // maybe it isn't such a good idea to just be copying out the things that we need, because
            // if the player is created before the spritebatch... null reference exception
            SpriteBatch = new SpriteBatch(this);
            Player = new Player(this);
            monster_sprite = new Sprite(new Vector2(1f, 1f), new Vector3(1f, 0.5f, 0.4f));
            monsters = new Monster[5];
            for (var i = 0; i < 5; i++)
                monsters[i] = new Monster(this, monster_sprite);

            Map.Shader = new Shader("shaders/core.vert", "shaders/core.frag");
            Sprite.Shader = new Shader("shaders/sprite.vert", "shaders/sprite.frag");
        }

        public void Update(float delta)
        {
            Player.Update(delta);
            foreach (var monster in monsters)
                monster.Update(delta);
        }

        public void Draw(float delta)
        {
            Map.Draw(delta);

            // drawing sprite-based objects only queues them up in the spritebatch
            // calling the draw method on the sprite batch actually draws them
            Player.Draw(delta);
            foreach (var monster in monsters)
                monster.Draw(delta);
            SpriteBatch.Draw(delta);
        }
    }
}
