using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Euclid.Las.Headers
{
    public class LasVariableLengthRecord
    {
        public static readonly ushort HeaderLength = 54;
        public ushort TotalRecordLength => (ushort)(Data.Length + HeaderLength);

        public ushort Reserved = 0;
        public char[] UserID = new char[16];
        public ushort RecordID = 2112;
        public ushort RecordLengthAfterHeader = 0;
        public char[] Description = new char[32];
        public byte[] Data;

        public LasVariableLengthRecord()
        { }

        public LasVariableLengthRecord(BinaryReader reader)
        {
            this.Reserved = reader.ReadUInt16();
            this.UserID = reader.ReadChars(16);
            this.RecordID = reader.ReadUInt16();
            this.RecordLengthAfterHeader = reader.ReadUInt16();
            this.Description = reader.ReadChars(32);
            this.Data = reader.ReadBytes(this.RecordLengthAfterHeader);
        }

        public void WriteToFile(BinaryWriter writer)
        {
            writer.Write(this.Reserved);
            writer.Write(this.UserID);
            writer.Write(this.RecordID);
            writer.Write(this.RecordLengthAfterHeader);
            writer.Write(this.Description);
            writer.Write(this.Data);
        }

        public static LasVariableLengthRecord GetProjectionVLR(string projString)
        {
            var vlr = new LasVariableLengthRecord();

            vlr.Reserved = 0;
            var userID = "LASF_Projection".PadRight(16, '\0');
            vlr.UserID = userID.ToArray();
            var desc = "Projection".PadRight(32, '\0');
            vlr.Description = desc.ToArray();
            vlr.RecordID = 2112;
            //< Projection needs to be null-terminated and is assumed UTF-8
            var projData = projString.PadRight(projString.Length + 1, '\0');
            vlr.Data = Encoding.UTF8.GetBytes(projData);
            vlr.RecordLengthAfterHeader = (ushort)vlr.Data.Length;

            return vlr;
        }
    }
}
