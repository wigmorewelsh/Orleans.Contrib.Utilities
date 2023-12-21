namespace Orleans.Utilities;

public class GuidCompoundGrainLocator<TGrain> : IGuidCompoundGrainLocator<TGrain> where TGrain : IGrainWithGuidCompoundKey
{
    private readonly IGrainFactory _grainFactory;

    public GuidCompoundGrainLocator(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    public TGrain GetGrain(Guid key, string keyExt)
    {
        return _grainFactory.GetGrain<TGrain>(key, keyExt, null);
    }
}