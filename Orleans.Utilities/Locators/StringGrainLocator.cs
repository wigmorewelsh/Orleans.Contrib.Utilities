namespace Orleans.Utilities;

internal class StringGrainLocator<TGrain> : IStringGrainLocator<TGrain> where TGrain : IGrainWithStringKey
{
    private readonly IGrainFactory _grainFactory;

    public StringGrainLocator(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    public TGrain GetGrain(string key)
    {
        return _grainFactory.GetGrain<TGrain>(key);
    }
}