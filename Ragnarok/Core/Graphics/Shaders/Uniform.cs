using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Ragnarok.Core.Graphics.Shaders
{
    class Uniform
    {
        private readonly int location;
        public Uniform(int location) => this.location = location;
        // as new uniforms types get used, append a Set() method for that type
        public void Set(Matrix4 value) => GL.UniformMatrix4(location, false, ref value);
    }
}
