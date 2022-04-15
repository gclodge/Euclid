namespace Euclid.Las.Headers.Interfaces
{
    public interface ILasVariableLengthRecord
    {
        /// <summary>
        /// Two leading reserved bytes of the VariableLengthRecord (VLR)
        /// </summary>
        ushort Reserved { get; }
        /// <summary>
        /// ID field indicating the type of VariableLengthRecord (VLR)
        /// </summary>
        ushort RecordID { get; }
        /// <summary>
        /// Total length (in bytes) of the VariableLengthRecord (VLR)
        /// </summary>
        ushort TotalRecordLength { get; }
        /// <summary>
        /// Total length (in bytes) of data following the header of the VariableLengthRecord (VLR)
        /// </summary>
        ushort RecordLengthAfterHeader { get; }

        /// <summary>
        /// 16-byte UserID field of the VariableLengthRecord (VLR)
        /// </summary>
        char[] UserID { get; }
        /// <summary>
        /// 32-byte Description field of the VariableLengthRecord (VLR)
        /// </summary>
        char[] Description { get; }
        /// <summary>
        /// The actual binary data stored within the VariableLengthRecord (VLR)
        /// </summary>
        byte[] Data { get; }

        /// <summary>
        /// Set the underlying 16-byte UserID field
        /// </summary>
        /// <param name="userID"></param>
        void SetUserID(string userID);
        /// <summary>
        /// Set the underlying 32-byte Description field
        /// </summary>
        /// <param name="description"></param>
        void SetDescription(string description);
    }
}
