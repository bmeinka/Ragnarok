using System.IO;
using System.Linq;
using System.Text;

namespace GFT.Internal.GRF
{
    class Header
    {
        /*
         * GRF archives start with a 46-byte header with the following layout:
         * 
         * 0-15:  "Master of Magic" watermark
         * 15-30: 15-byte encryption flag
         * 30-34: position of the file index
         * 34-42: the number of files in the archive
         * 42-46: version information
         * 
         * ## Encryption Flag
         * 
         * The encryption flag can have one of two values:
         * 
         * Deny:  00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
         * Allow: 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E
         * 
         * The easiest way to verify the flag is to take a sum of the bytes and
         * compare to the expected values of either 0 for deny or 105 for allow.
         * 
         * ## Index Offset
         * 
         * The index offset stored in the file does not include the 46 byte header.
         * This parser will add 46 to the offset to get the actual index position.
         * 
         * ## File Count
         * 
         * The file count is stored in two sequential unsigned integers.
         * The formula for calculating the actual file count is b - a - 7.
         * 
         * ## Version
         * 
         * The GRF version is stored in an unsigned integer. The first two bytes are
         * the major version and the second two are the minor version. For version 2.0,
         * which is the version supported, it should be 0x0200.
         */
        private readonly BinaryReader stream;

        private enum EncryptionFlag { Deny = 0, Allow = 105 }
        private enum Lengths { Watermark = 15, Encryption = 15, Index = 4, Count = 8, Version = 4 }
        private enum Offsets
        {
            Watermark = 0,
            Encryption = Watermark + Lengths.Watermark,
            Index = Encryption + Lengths.Encryption,
            Count = Index + Lengths.Index,
            Version = Count + Lengths.Count,
        }

        /// <summary>
        /// the length of the header in bytes
        /// </summary>
        public uint Length => (uint)Offsets.Version + (uint)Lengths.Version;

        /// <summary>
        /// the beginning watermark of the GRF file. Should be a value of "Master of Magic"
        /// </summary>
        public string Watermark
        {
            get
            {
                stream.BaseStream.Position = (int)Offsets.Watermark;
                return Encoding.ASCII.GetString(stream.ReadBytes((int)Lengths.Watermark));
            }
        }

        /// <summary>
        /// whether or not encrypted files are allowed in the archive
        /// </summary>
        public bool AllowEncryption
        {
            get
            {
                stream.BaseStream.Position = (int)Offsets.Encryption;
                var flag = (EncryptionFlag)stream.ReadBytes((int)Lengths.Encryption).Sum((byte b) => (decimal)b);
                if (flag == EncryptionFlag.Allow)
                    return true;
                if (flag == EncryptionFlag.Deny)
                    return false;
                throw new InvalidDataException("Invalid GRF encryption flag");
            }
        }

        /// <summary>
        /// the position in the base stream where the file index is located
        /// </summary>
        public uint IndexOffset
        {
            get
            {
                stream.BaseStream.Position = (int)Offsets.Index;
                return stream.ReadUInt32() + Length;
            }
        }

        /// <summary>
        /// the number of files contained in the GRF archive
        /// </summary>
        public uint FileCount
        {
            get
            {
                stream.BaseStream.Position = (int)Offsets.Count;
                var a = stream.ReadUInt32();
                var b = stream.ReadUInt32();
                return b - a - 7;
            }
        }

        /// <summary>
        /// the version of the GRF archive
        /// </summary>
        public uint Version
        {
            get
            {
                stream.BaseStream.Position = (int)Offsets.Version;
                return stream.ReadUInt32();
            }
        }

        public Header(BinaryReader stream)
        {
            this.stream = stream;
            // ensure the watermark is correct
            if (Watermark != "Master of Magic")
                throw new InvalidDataException("Invalid GRF header");
            // ensure the major version is 2
            if ((Version & 0xFF00) != 0x0200)
                throw new InvalidDataException("Unsupported GRF version");
        }
    }
}
