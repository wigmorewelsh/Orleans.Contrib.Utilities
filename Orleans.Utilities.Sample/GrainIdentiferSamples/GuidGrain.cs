using Orleans.Utilities;

namespace Orleans.Utilities.Sample.GrainIdentiferSamples;

public class GuidGrain : Grain, IGuidGrain
{
    private readonly IGrainIdentifier _grainIdentifier;

    public GuidGrain(IGrainIdentifier grainIdentifier)
    {
        _grainIdentifier = grainIdentifier;
    }
    
    public Task<Guid> GetKey()
    {
        return Task.FromResult(_grainIdentifier.PrimaryKeyGuid);  
    }
}