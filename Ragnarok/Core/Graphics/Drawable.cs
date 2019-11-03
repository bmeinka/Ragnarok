using System;
using OpenTK.Graphics.OpenGL;

namespace Ragnarok.Core.Graphics
{
    abstract class Drawable : IDisposable
    {
        protected readonly int vao;
        protected readonly int vbo;

        /// <summary>
        /// initialize the vertex array and vertex buffer objects
        /// </summary>
        public Drawable()
        {
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            GL.BindVertexArray(0);
        }

        public virtual void Dispose()
        {
            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);
            GL.BindVertexArray(0);
        }

        public abstract void Draw();
    }
}
