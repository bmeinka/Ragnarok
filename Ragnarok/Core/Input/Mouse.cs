using System;
using System.Diagnostics;
using OpenTK;
using OpenTK.Input;

namespace Ragnarok.Core.Input
{
    class Mouse : EventArgs
    {
        private const double click_delay = 0.5;
        private readonly Window window;
        private readonly Stopwatch click_timer;

        private Vector2 position;
        private MouseButton click_button;
        private MouseState state;

        /// <summary>
        /// the x position of the cursor in viewport space (pixels)
        /// </summary>
        public int X { get { return (int)position.X; } }
        /// <summary>
        /// the y position of the cursor in viewport space (pixels)
        /// </summary>
        public int Y { get { return (int)position.Y; } }

        public event EventHandler<MouseMoveEventArgs> Move { add { window.MouseMove += value; } remove { window.MouseMove -= value; } }
        public event EventHandler<MouseButtonEventArgs> Down { add { window.MouseDown += value; } remove { window.MouseDown -= value; } }
        public event EventHandler<MouseButtonEventArgs> Up { add { window.MouseUp += value; } remove { window.MouseUp -= value; } }
        public event EventHandler<EventArgs> Enter { add { window.MouseEnter += value; } remove { window.MouseEnter -= value; } }
        public event EventHandler<EventArgs> Leave { add { window.MouseLeave += value; } remove { window.MouseLeave -= value; } }
        public event EventHandler<MouseWheelEventArgs> Scroll { add { window.MouseWheel += value; } remove { window.MouseWheel -= value; } }

        // add a double click event for when a double click is believed to occur
        public event EventHandler<MouseButtonEventArgs> DoubleClick;

        public Mouse(Window window)
        {
            this.window = window;
            click_timer = new Stopwatch();
            click_button = MouseButton.LastButton;
            position = Vector2.Zero;

            window.MouseMove += OnMove;
            window.UpdateFrame += OnUpdate;
            window.MouseDown += OnClick;
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == click_button && click_timer.Elapsed.TotalSeconds < click_delay)
                DoubleClick(this, e);
            click_button = e.Button;
            click_timer.Restart();
        }

        public bool IsButtonDown(MouseButton button) => state.IsButtonDown(button);
        public bool IsButtonUp(MouseButton button) => state.IsButtonUp(button);

        private void OnUpdate(object sender, FrameEventArgs e) => state = OpenTK.Input.Mouse.GetState();
        private void OnMove(object sender, MouseMoveEventArgs e) => position = new Vector2(e.X, e.Y);
    }
}
