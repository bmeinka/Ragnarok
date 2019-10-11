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
    class Window : GameWindow
    {
        public Window() : base(1280, 720, GraphicsMode.Default, "Ragnarok") { }
    }
}
