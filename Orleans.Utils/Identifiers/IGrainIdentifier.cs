namespace Orleans.Utils;

public interface IGrainIdentifier
{
    public Guid PrimaryKeyGuid { get; }
    public long PrimaryKeyLong { get; }
    public string PrimaryKeyString { get; }
}