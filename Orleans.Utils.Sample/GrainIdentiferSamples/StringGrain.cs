namespace Orleans.Utils.Sample.GrainIdentiferSamples;

public class StringGrain : Grain, IStringGrain
{
    private readonly IGrainIdentifier _grainIdentifier;

    public StringGrain(IGrainIdentifier grainIdentifier)
    {
        _grainIdentifier = grainIdentifier;
    }
    
    public Task<string> GetKey()
    {
        return Task.FromResult(_grainIdentifier.PrimaryKeyString);  
    }
}