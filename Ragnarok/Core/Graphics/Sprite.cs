using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Ragnarok.Core.Graphics
{
    class Sprite : Drawable
    {
        /// <summary>
        /// create a new sprite
        /// </summary>
        /// <param name="size">the size of the sprite in world space</param>
        /// <param name="color">the color of the sprite</param>
        public Sprite(Vector2 size, Vector3 color)
        {
            GL.BindVertexArray(vao);
            // use the bottom center as the origin (0, 0, 0)
            var half_x = size.X / 2;
            float[] vertices = new[]
            {
                -half_x, size.Y, 0f, color.X, color.Y, color.Z, // top-left
                 half_x, size.Y, 0f, color.X, color.Y, color.Z, // top-right
                 half_x,     0f, 0f, color.X, color.Y, color.Z, // bottom-right
                -half_x, size.Y, 0f, color.X, color.Y, color.Z, // top-left
                 half_x,     0f, 0f, color.X, color.Y, color.Z, // bottom-right
                -half_x,     0f, 0f, color.X, color.Y, color.Z, // bottom-left
            };
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            // position
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            // color
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            // unbind the vertex array when it is done being manipulated
            GL.BindVertexArray(0);
        }

        public override void Draw()
        {
            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.BindVertexArray(0);
        }
    }
}
