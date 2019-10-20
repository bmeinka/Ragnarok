using System;
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
        public bool IsKeyDown(Key key) => Keyboard.GetState().IsKeyDown(key);
        public bool IsButtonDown(MouseButton button) => Mouse.GetState().IsButtonDown(button);
        public Vector2 MousePosition { get; private set; }
        public Window() : base(1280, 720, GraphicsMode.Default, "Ragnarok") { }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.8f, 0.2f, 1.0f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            Game.CurrentScene = new Scene(this);

            base.OnLoad(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            MousePosition = new Vector2(e.X, e.Y);
            base.OnMouseMove(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (IsKeyDown(Key.Escape))
                Exit();
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Game.CurrentScene.Render(e.Time);

            SwapBuffers();
            base.OnRenderFrame(e);
        }
    }
}
