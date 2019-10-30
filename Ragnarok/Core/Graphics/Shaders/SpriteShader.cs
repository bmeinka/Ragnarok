using OpenTK;

namespace Ragnarok.Core.Graphics.Shaders
{
    class SpriteShader : Program
    {
        private const string vertex_path = @"shaders\sprite.vert";
        private const string fragment_path = @"shaders\sprite.frag";

        private readonly Uniform mvp;
        public Matrix4 MVP { set { mvp.Set(value); } }

        public SpriteShader() : base(vertex_path, fragment_path)
        {
            mvp = new Uniform(Location("mvp"));
        }
    }
}
