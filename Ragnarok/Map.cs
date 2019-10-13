using OpenTK.Graphics.OpenGL;

namespace Ragnarok
{
    /// <summary>
    /// A map in the world of Ragnarok
    /// </summary>
    class Map
    {
        /*
        The initial map object is going to be a very basic plane that will be used for testing purposes.
        Eventually, the test map will be phased out in favor of real game maps loaded from the grf.
         */
        private float[] vertices =
        {
            // texture coordinates being upside down doesn't matter
            // position  texture coordinates
             0f,  0f, 0f,  0f,  0f, // top-left 
            48f,  0f, 0f, 24f,  0f, // top-right
            48f, 48f, 0f, 24f, 24f, // bottom-right
             0f,  0f, 0f,  0f,  0f, // top-left 
            48f, 48f, 0f, 24f, 24f, // bottom-right
             0f, 48f, 0f,  0f, 24f, // bottom-left
        };
        /* simple RGB array with only four pixels in it */
        private byte[] pixels =
        {
            // I don't understand why we need these extra zeros at the end of the row
            102, 102, 153, 170, 170, 204, 0, 0,
            170, 170, 204, 102, 102, 153, 0, 0,
        };

        private int texture, vertex_array, vertex_buffer;

        public Map()
        {
            vertex_array = GL.GenVertexArray();
            GL.BindVertexArray(vertex_array);

            vertex_buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertex_buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            // position
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            // texture coordinates
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);
            // use nearest filtering and repeat wrapping to create a pixelated checker pattern
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            // copy the pixel data to the GPU
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, 2, 2, 0, PixelFormat.Rgb, PixelType.UnsignedByte, pixels);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            // unbind the vertex array when it is done being manipulated
            GL.BindVertexArray(0);
        }

        public void Render(double dt)
        {
            GL.BindVertexArray(vertex_array);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

            GL.BindVertexArray(0);
        }
    }
}
