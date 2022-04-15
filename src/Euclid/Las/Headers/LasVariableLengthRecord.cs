using System;
using System.IO;
using System.Text;

using Euclid.Las.Headers.Interfaces;

namespace Euclid.Las.Headers
{
    public class LasVariableLengthRecord : ILasVariableLengthRecord
    {
        public static readonly ushort HeaderLength = 54;

        public const int MaxUserIDLength = 16;
        public const int MaxDescriptionLength = 32;

        public const string ProjectionUserID = "LASF_Projection";
        public const string ProjectionDescription = "Projection";

        public ushort Reserved { get; set; } = 0;
        public ushort RecordID { get; set; } = 2112;
        public ushort TotalRecordLength => (ushort)(Data.Length + HeaderLength);
        public ushort RecordLengthAfterHeader { get; set; } = 0;

        public char[] UserID { get; set; } = new char[16];
        public char[] Description { get; set; } = new char[32];
        public byte[] Data { get; set; }

        public void SetUserID(string userID)
        {
            if (userID.Length > 16) throw new($"UserID must be no more than 16 characters in length");

            this.UserID = userID.PadRight(16, '\0').ToCharArray();
        }

        public void SetDescription(string description)
        {
            if (description.Length > 32) throw new($"Description must be no more than 32 characters in length");

            this.Description = description.PadRight(32, '\0').ToCharArray();
        }

        public static LasVariableLengthRecord ReadFromStream(BinaryReader reader)
        {
            //< Parse inital fields to determine length of record after header
            var vlr = new LasVariableLengthRecord()
            {
                Reserved = reader.ReadUInt16(),
                UserID = reader.ReadChars(16),
                RecordID = reader.ReadUInt16(),
                RecordLengthAfterHeader = reader.ReadUInt16(),
                Description = reader.ReadChars(32)
            };
            //< Grab the remaining binary data and return
            vlr.Data = reader.ReadBytes(vlr.RecordLengthAfterHeader);
            return vlr;
        }

        public static LasVariableLengthRecord GetProjectionVLR(string projString)
        {
            var vlr = new LasVariableLengthRecord
            {
                Reserved = 0,
                RecordID = 2112
            };

            vlr.SetUserID(ProjectionUserID);
            vlr.SetDescription(ProjectionDescription);

            //< Projection needs to be null-terminated and is assumed UTF-8
            var projData = projString.PadRight(projString.Length + 1, '\0');
            vlr.Data = Encoding.UTF8.GetBytes(projData);
            vlr.RecordLengthAfterHeader = (ushort)vlr.Data.Length;

            return vlr;
        }
    }
}
