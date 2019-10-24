using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Ragnarok
{
    class Shader
    {
        private readonly int id;

        /// <summary>
        /// Compile a shader from file
        /// </summary>
        /// <param name="path">Path to the file containing the shader source</param>
        /// <param name="type">The type of shader to compile</param>
        /// <returns></returns>
        private static int CreateShader(string path, ShaderType type)
        {
            int shader;
            string source;

            using (var reader = new StreamReader(path))
                source = reader.ReadToEnd();

            shader = GL.CreateShader(type);
            GL.ShaderSource(shader, source);
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out int status);
            if (status == 0)
            {
                string info = GL.GetShaderInfoLog(shader);
                GL.DeleteShader(shader);
                Console.WriteLine(info);
                throw new Exception(info);
            }
            return shader;
        }

        private int Location(string name) => GL.GetUniformLocation(id, name);

        /// <summary>
        /// Create a shader program from vertex and fragment shader source files
        /// </summary>
        /// <param name="vertex_path">the path to the vertex shader source file</param>
        /// <param name="fragment_path">the path to the fragment shader source file</param>
        public Shader(string vertex_path, string fragment_path)
        {
            int vert_id, frag_id;
            vert_id = CreateShader(vertex_path, ShaderType.VertexShader);
            // if the fragment shader fails, the vertex shader needs cleaned up
            try { frag_id = CreateShader(fragment_path, ShaderType.FragmentShader); }
            catch
            {
                GL.DeleteShader(vert_id);
                throw;
            }

            id = GL.CreateProgram();

            GL.AttachShader(id, vert_id);
            GL.AttachShader(id, frag_id);

            GL.LinkProgram(id);

            GL.DetachShader(id, vert_id);
            GL.DetachShader(id, frag_id);
            GL.DeleteShader(vert_id);
            GL.DeleteShader(frag_id);

            GL.GetProgram(id, GetProgramParameterName.LinkStatus, out int status);
            if (status == 0)
            {
                string info = GL.GetProgramInfoLog(id);
                GL.DeleteProgram(id);
                Console.WriteLine(info);
                throw new Exception(info);
            }
        }

        public void Use() => GL.UseProgram(id);

        public void Uniform(string name, float value) => GL.Uniform1(Location(name), value);
        public void Uniform(string name, int value) => GL.Uniform1(Location(name), value);
        public void Uniform(string name, Matrix4 value) => GL.UniformMatrix4(Location(name), false, ref value);
        public void Uniform(string name, Vector3 value) => GL.Uniform3(Location(name), value);
    }
}
