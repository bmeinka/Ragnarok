using System;
using System.Diagnostics;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Ragnarok
{
    class Window : GameWindow
    {
        private const double click_delay = 0.5;
        private Stopwatch click_timer;
        private MouseButton click_button;
        public bool IsKeyDown(Key key) => Keyboard.GetState().IsKeyDown(key);
        public bool IsButtonDown(MouseButton button) => Mouse.GetState().IsButtonDown(button);
        public Vector2 MousePosition { get; private set; }

        public event EventHandler<MouseButtonEventArgs> DoubleClick;
        public Window() : base(1280, 720, GraphicsMode.Default, "Ragnarok")
        {
            click_timer = new Stopwatch();
            click_button = MouseButton.LastButton;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.8f, 0.2f, 1.0f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            Game.CurrentScene = new Scene(this);
            //Input.Mouse.Initialize(this);

            base.OnLoad(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            MousePosition = new Vector2(e.X, e.Y);
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.Button == click_button && click_timer.Elapsed.TotalSeconds < click_delay)
                DoubleClick(this, e);
            click_button = e.Button;
            click_timer.Restart();
            base.OnMouseDown(e);
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
