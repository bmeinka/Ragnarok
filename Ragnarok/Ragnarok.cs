using Ragnarok.Core;
using GFT;

namespace Ragnarok
{
    static class Ragnarok
    {
        const string GRF_PATH = @"D:\programs\ragnarok\data.grf";
        static void Main(string[] args)
        {
            var grf = new GRF(GRF_PATH);
            System.Console.WriteLine(grf.FileCount);
            Game.Begin(new MainScene());
        }
    }
}
