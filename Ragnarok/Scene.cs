using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok
{
    /// <summary>
    /// Manages the currently rendered scene, including the Camera and Map.
    /// </summary>
    class Scene
    {
        private Map map;
        public Scene()
        {
            map = new Map();
        }
        public void Update(double dt)
        {
        }
    }
}
