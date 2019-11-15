using OpenTK;
using Ragnarok.Core;
using Ragnarok.Core.Graphics;
using Ragnarok.Core.Graphics.Shaders;
using Ragnarok.Core.Physics; // TODO: move to World.Map
using Ragnarok.World;

namespace Ragnarok.Gameplay
{
    class MainScene : IScene
    {
        private Map map;
        private Sprite monster_sprite;
        private CoreShader shader; // TODO: create a renderer object to handle shaders
        private Monster[] monsters; // TODO: move to map
        private PhysicsWorld world; // TODO: move to map
        private Camera camera;
        private SpriteBatch sb;
        private Player player;
        public void Load()
        {
            shader = new CoreShader();
            camera = new Camera();
            sb = new SpriteBatch(camera);
            map = new Map(48f, 48f);
            world = new PhysicsWorld(map.Width, map.Height); // TODO: move to map
            player = new Player(sb, map, world, camera);
            monster_sprite = new Sprite(new Vector2(1f, 1f), new Vector3(1f, 0.5f, 0.4f));
            monsters = new Monster[5];
            for (var i = 0; i < 5; i++)
            {
                var x = Game.Random.Float(0f, map.Width);
                var y = Game.Random.Float(0f, map.Height);
                monsters[i] = new Monster(sb, monster_sprite, world, new Vector2(x, y));
            }
        }

        public void Unload() { }

        public void Update(float delta)
        {
            camera.Update(delta);
            player.Update(delta);
            foreach (var monster in monsters)
                monster.Update(delta);
            world.Update(delta);
        }

        public void Draw()
        {
            shader.Use();
            shader.MVP = camera.ViewProjection;
            map.Draw();

            player.Draw();
            foreach (var monster in monsters)
                monster.Draw();
            sb.Draw();
        }
    }
}
