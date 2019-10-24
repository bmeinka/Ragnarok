using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Ragnarok
{
    class Window : GameWindow
    {
        public Window() : base(1280, 720, GraphicsMode.Default, "Ragnarok") { }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.8f, 0.2f, 1.0f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            Game.Scene = new Scene(this);

            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Key.Escape))
                Exit();
            Game.Scene.Update((float)e.Time);
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Game.Scene.Draw((float)e.Time);
            SwapBuffers();
            base.OnRenderFrame(e);
        }
    }
}
