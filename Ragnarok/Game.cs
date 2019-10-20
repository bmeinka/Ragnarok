using System;
using OpenTK;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok
{
    /// <summary>
    /// The global state for the game. Also contains the main entry point.
    /// </summary>
    static class Game
    {
        public static Window CurrentWindow { get; set; }
        public static Scene CurrentScene { get; set; }
 
        static void Main(string[] args)
        {
            using (Window win = new Window())
            {
                CurrentWindow = win;
                win.Run(60f);
            }
        }
    }
}
