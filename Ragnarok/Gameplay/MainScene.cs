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
        private CoreShader shader; // TODO: create a renderer object to handle shaders
        private Camera camera;
        private SpriteBatch sb;
        private Player player;
        private PlayerController pc;
        public void Load()
        {
            shader = new CoreShader();
            camera = new Camera();
            sb = new SpriteBatch(camera);
            map = new Map(48f, 48f);
            player = new Player();
            player.Spawn(map, map.SpawnPoint);
            pc = new PlayerController(camera, map, player);
        }

        public void Unload() { }

        public void Update(float delta)
        {
            camera.Update(delta);
            pc.Update(delta);
            map.Update(delta);
        }

        public void Draw()
        {
            shader.Use();
            shader.MVP = camera.ViewProjection;
            map.Draw(sb);
            player.Draw(sb);
            sb.Draw();
        }
    }
}
