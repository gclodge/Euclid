namespace Euclid.Las.Headers.Interfaces
{
    public interface ILasHeader14 : ILasHeader
    {
        ulong StartOfWaveformDataPacketRecord { get; }
        ulong StartOfFirstExtendedVLR { get; }
        uint NumExtendedVLRs { get; }
        ulong NumPointRecords { get; }
        ulong[] NumPointRecordsByReturn { get; }
    }
}