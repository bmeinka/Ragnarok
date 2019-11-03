using OpenTK;
using Ragnarok.Core.Physics;
using Ragnarok.Core.Graphics;
using Ragnarok.World;
using Ragnarok.Core.Graphics.Shaders;

namespace Ragnarok.Core
{
    /// <summary>
    /// Manages the currently rendered scene, including the Camera and Map.
    /// </summary>
    class Scene
    {
        public Camera Camera { get; private set; }
        public Map Map { get; private set; }
        public Player Player { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public PhysicsWorld World { get; private set; }
        private readonly CoreShader shader;
        private readonly Monster[] monsters;
        private readonly Sprite monster_sprite;

        public Scene(Window window)
        {
            Camera = new Camera(window);
            Map = new Map(48f, 48f);
            // TODO: fix dependencies
            // maybe it isn't such a good idea to just be copying out the things that we need, because
            // if the player is created before the spritebatch... null reference exception
            SpriteBatch = new SpriteBatch(this);
            World = new PhysicsWorld();
            Player = new Player(this);
            monster_sprite = new Sprite(new Vector2(1f, 1f), new Vector3(1f, 0.5f, 0.4f));
            monsters = new Monster[5];
            for (var i = 0; i < 5; i++)
            {
                var x = (float)Game.Random.Next(1, (int)Map.Width-1);
                var y = (float)Game.Random.Next(1, (int)Map.Height-1);
                monsters[i] = new Monster(this, monster_sprite, new Vector2(x, y));
            }
            shader = new CoreShader();
        }

        public void Update(float delta)
        {
            Player.Update(delta);
            foreach (var monster in monsters)
                monster.Update(delta);
            World.Update(delta);
        }

        public void Draw()
        {
            shader.Use();
            shader.MVP = Camera.ViewProjection;
            Map.Draw();

            // drawing sprite-based objects only queues them up in the spritebatch
            // calling the draw method on the sprite batch actually draws them
            Player.Draw();
            foreach (var monster in monsters)
                monster.Draw();
            SpriteBatch.Draw();
        }
    }
}
