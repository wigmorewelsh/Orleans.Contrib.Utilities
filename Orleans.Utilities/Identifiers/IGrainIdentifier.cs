using Orleans.Runtime;

namespace Orleans.Utilities;

public interface IGrainIdentifier
{
    public Guid PrimaryKeyGuid { get; }
    public long PrimaryKeyLong { get; }
    public string PrimaryKeyString { get; }
    public GrainId GrainId { get; }
}