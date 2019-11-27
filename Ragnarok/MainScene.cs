using OpenTK;
using Ragnarok.Core;
using Ragnarok.Core.Graphics;
using Ragnarok.Core.Graphics.Shaders;
using Ragnarok.Gameplay;
using Ragnarok.World;

namespace Ragnarok
{
    class MainScene : IScene
    {
        private Map map;
        private CoreShader shader; // TODO: create a renderer object to handle shaders
        private TopDownCamera camera;
        private SpriteBatch sb;
        private Player player;
        private PlayerController pc;
        public void Load()
        {
            shader = new CoreShader();
            camera = new TopDownCamera();
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
            pc.Update();
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
