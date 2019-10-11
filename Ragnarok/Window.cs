using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
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
            scene = new Scene();
            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            scene.Update(e.Time);
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
        }
    }
}
