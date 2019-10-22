using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Ragnarok
{
    class Sprite
    {

        public static Shader Shader { get; set; }
        public Vector3 Position { get; set; }
        private int vertex_array, vertex_buffer;


        // okay, so we need to make sure that we are dealing with the shader correctly and making sure there is a shader program.
        // not sure how to go about doing that, though. Is it normal to have different shader programs for different "types" of objects?

        /// <summary>
        /// create a new sprite
        /// </summary>
        /// <param name="size">the size of the sprite in world space</param>
        /// <param name="color">the color of the sprite</param>
        public Sprite(Vector2 size, Vector3 color)
        {
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

            vertex_array = GL.GenVertexArray();
            GL.BindVertexArray(vertex_array);

            vertex_buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertex_buffer);
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

        public void Render(double dt)
        {
            var camera = Game.Scene.Camera;
            GL.BindVertexArray(vertex_array);

            Shader.Use();

            var rotate =  Matrix4.CreateRotationX(camera.Angle) * Matrix4.CreateRotationZ(camera.Rotation);
            var model = rotate * Matrix4.CreateTranslation(Position);
            var view_projection = camera.View * camera.Projection;

            Shader.Uniform("mvp", model * view_projection);
            //Shader.Uniform("mvp", model * view_projection);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.BindVertexArray(0);
        }
    }
}
