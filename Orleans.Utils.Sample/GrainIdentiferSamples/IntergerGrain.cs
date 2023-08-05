namespace Orleans.Utils.Sample.GrainIdentiferSamples;

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
}

// grain with string key

// grain with guid key