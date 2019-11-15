using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Ragnarok.Core
{
    class Window : GameWindow
    {
        public Window() : base(1280, 720, GraphicsMode.Default, "Ragnarok") { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.8f, 0.2f, 1.0f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            Game.Scene.Load();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            Game.Scene.Unload();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Key.Escape))
                Exit();
            Game.Scene.Update((float)e.Time);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Game.Scene.Draw();
            SwapBuffers();
        }
    }
}
