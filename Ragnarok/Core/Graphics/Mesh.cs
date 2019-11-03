using OpenTK.Graphics.OpenGL;

namespace Ragnarok.Core.Graphics
{
    class Mesh : Drawable
    {
        private readonly int tex;

        public Mesh(float w, float h)
        {
            GL.BindVertexArray(vao);

            // the vbo is already bound. it gets bound in the base constructor
            var vertices = new float[]
            {
                0f, 0f, 0f,  0f,  0f, 
                 w, 0f, 0f, w/2,  0f,
                 w,  h, 0f, w/2, h/2,
                0f, 0f, 0f,  0f,  0f, 
                 w,  h, 0f, w/2, h/2,
                0f,  h, 0f,  0f, h/2,
            };
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // position attribute
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            // texture coordinate attribute
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            tex = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, tex);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            var pixels = new byte[]
            {
                102, 102, 153, 255, 170, 170, 204, 255,
                170, 170, 204, 255, 102, 102, 153, 255,
            };
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, 2, 2, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.BindVertexArray(0);
        }

        public override void Dispose()
        {
            GL.DeleteTexture(tex);
            base.Dispose();
        }

        public override void Draw()
        {
            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.BindVertexArray(0);
        }
    }
}
