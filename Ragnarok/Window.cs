﻿using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Ragnarok
{
    /// <summary>
    /// Handles window creation, events, and the rendering loop.
    /// </summary>
    class Window : GameWindow
    {
        private Scene scene;
        public Window() : base(1280, 720, GraphicsMode.Default, "Ragnarok") { }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.8f, 0.2f, 1.0f, 1.0f);
            GL.Color4(1f, 1f, 1f, 1f);
            GL.Enable(EnableCap.DepthTest);

            Camera.AspectRatio = Width / Height;
            Camera.Target = new Vector3(0f, 0f, 0f);

            scene = new Scene();
            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var input = Keyboard.GetState();
            if (input.IsKeyDown(Key.Escape)) Exit();
            scene.Update(e.Time);
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            scene.Render(e.Time);
            SwapBuffers();
            base.OnRenderFrame(e);
        }
    }
}
