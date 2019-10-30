using OpenTK;

namespace Ragnarok.Core.Graphics.Shaders
{
    class CoreShader : Program
    {
        private const string vertex_path = @"shaders\core.vert";
        private const string fragment_path = @"shaders\core.frag";

        private readonly Uniform mvp;
        public Matrix4 MVP { set { mvp.Set(value); } }

        public CoreShader() : base(vertex_path, fragment_path)
        {
            mvp = new Uniform(Location("mvp"));
        }
    }
}
