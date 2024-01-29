using Orleans.Utilities;

namespace Orleans.Utilities.Sample.GrainIdentiferSamples;

public class IntergerGrain : Grain, IIntergerGrain
{
    private readonly IGrainIdentifier _grainIdentifier;

    public IntergerGrain(IGrainIdentifier grainIdentifier)
    {
        _grainIdentifier = grainIdentifier;
    }
    
    public Task<long> GetKey()
    {
        return Task.FromResult(_grainIdentifier.PrimaryKeyLong);  
    }

    public Task<bool> CheckGrainIdMatches()
    {
        return Task.FromResult(this.GetGrainId().Equals(_grainIdentifier.GrainId));
    }
}

// grain with string key

// grain with guid key