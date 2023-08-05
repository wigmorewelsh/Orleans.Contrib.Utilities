using Orleans.Runtime;

namespace Orleans.Utils;

public class GrainIdentifier : IGrainIdentifier
{
    private readonly IGrainContext _grainRuntime;

    public GrainIdentifier(IGrainContext grainRuntime)
    {
        _grainRuntime = grainRuntime;
    }

    public Guid PrimaryKeyGuid => _grainRuntime.GrainId.GetGuidKey();
    public long PrimaryKeyLong => _grainRuntime.GrainId.GetIntegerKey();
    public string PrimaryKeyString => _grainRuntime.GrainId.Key.ToString();
}