namespace Orleans.Utils;

internal class GuidGrainLocator<TGrain> : IGuidGrainLocator<TGrain> where TGrain : IGrainWithGuidKey
{
    private readonly IGrainFactory _grainFactory;

    public GuidGrainLocator(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    public TGrain GetGrain(Guid key)
    {
        return _grainFactory.GetGrain<TGrain>(key);
    }
}