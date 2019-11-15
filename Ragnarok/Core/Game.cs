using Ragnarok.Core.Input;
namespace Ragnarok.Core
{
    static class Game
    {
        public static IScene Scene { get; private set; }
        public static Mouse Mouse { get; private set; }
        public static Random Random { get; private set; }
        public static Window Window { get; private set; }
        public static void Begin(IScene scene)
        {
            Random = new Random();
            Scene = scene;
            using (Window = new Window())
            {
                Mouse = new Mouse(Window);
                Window.Run(60f);
            }
        }
    }
}
