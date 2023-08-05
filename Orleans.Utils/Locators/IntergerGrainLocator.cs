namespace Orleans.Utils;

internal class IntergerGrainLocator<TGrain> : IIntergerGrainLocator<TGrain> where TGrain : IGrainWithIntegerKey
{
    private readonly IGrainFactory _grainFactory;

    public IntergerGrainLocator(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    public TGrain GetGrain(int key)
    {
        return _grainFactory.GetGrain<TGrain>(key);
     }
}