using System.Diagnostics;
using Ragnarok.Core.Input;

namespace Ragnarok.Core
{
    static class Game
    {
        private static Stopwatch watch;
        public static float Time => (float)watch.Elapsed.TotalSeconds;
        public static IScene Scene { get; private set; }
        public static Mouse Mouse { get; private set; }
        public static Random Random { get; private set; }
        public static Window Window { get; private set; }
        public static void Begin(IScene scene)
        {
            watch = new Stopwatch();
            Random = new Random();
            Scene = scene;
            using (Window = new Window())
            {
                Mouse = new Mouse(Window);
                watch.Start();
                Window.Run(60f);
                watch.Stop();
            }
        }
    }
}
