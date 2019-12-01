using System.IO;
using GFT.Internal.GRF;

namespace GFT
{
    public class GRF
    {
        private readonly Header header;
        private readonly BinaryReader stream;

        public uint FileCount => header.FileCount;

        public GRF(string path)
        {
            stream = new BinaryReader(File.OpenRead(path));
            header = new Header(stream);
        }
    }
}
