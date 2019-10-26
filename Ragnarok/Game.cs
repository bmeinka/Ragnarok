using System;
using Ragnarok.Input;

namespace Ragnarok
{
    /// <summary>
    /// The global state for the game. Also contains the main entry point.
    /// </summary>
    static class Game
    {
        public static Window Window { get; private set; }
        public static Scene Scene { get; set; }
        public static Mouse Mouse { get; private set; }
        public static Random Random { get; private set; }
 
        static void Main(string[] args)
        {
            Random = new Random();
            using (Window window = new Window())
            {
                Window = window;
                Mouse = new Mouse(window);
                Scene = new Scene(window);
                window.Run(60f);
            }
        }
    }
}
