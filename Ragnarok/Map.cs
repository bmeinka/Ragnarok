using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Ragnarok
{
    /// <summary>
    /// A map in the world of Ragnarok
    /// </summary>
    class Map : IDrawable
    {
        public static Shader Shader { get; set; }
        public Vector3 SpawnPoint => new Vector3(24f, 24f, 0f);
        /*
        The initial map object is going to be a very basic plane that will be used for testing purposes.
        Eventually, the test map will be phased out in favor of real game maps loaded from the grf.
         */
        private float[] vertices =
        {
            // position   texture coordinates
             0f,  0f, 0f,  0f,  0f, // top-left 
            48f,  0f, 0f, 24f,  0f, // top-right
            48f, 48f, 0f, 24f, 24f, // bottom-right
             0f,  0f, 0f,  0f,  0f, // top-left 
            48f, 48f, 0f, 24f, 24f, // bottom-right
             0f, 48f, 0f,  0f, 24f, // bottom-left
        };
        private byte[] pixels =
        {
            // I don't understand why we need these extra zeros at the end of the row
            102, 102, 153, 170, 170, 204, 0, 0,
            170, 170, 204, 102, 102, 153, 0, 0,
        };
        private readonly int texture, vertex_array, vertex_buffer;

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

        public void Draw(float delta)
        {
            Shader.Use();
            Shader.Uniform("mvp", Game.Scene.Camera.ViewProjection);
            GL.BindVertexArray(vertex_array);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Determine if a ray intersects with the map plane, and set the intersection point.
        /// </summary>
        /// <param name="ray">the ray to trace</param>
        /// <param name="intersection">where on the plane the ray intersects</param>
        /// <returns>true if the ray does intersect the plane</returns>
        public bool Intersect(Ray ray, out Vector3 intersection)
        {
            // Euclidean plane intersection:
            // Variables:
            //   Po: ray origin
            //   Vo: plane origin
            //   u:  ray direction
            //   n:  plane normal (up vector)
            //   w:  Po - Vo
            // using the parametric line function P(s) = Po + su
            // the intersection point s can be determined by: -n * w / n * u
            var normal = Vector3.UnitZ;
            var origin = Vector3.Zero;
            if (Vector3.Dot(normal, ray.Direction) == 0)
            {
                intersection = Vector3.Zero;
                return false;
            }
            var w = ray.Origin - origin;
            var s = Vector3.Dot(-normal, w) / Vector3.Dot(normal, ray.Direction);
            intersection = ray.Parametric(s);
            return true;
        }
    }
}
