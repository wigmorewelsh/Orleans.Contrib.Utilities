namespace Orleans.Utils;

public class IntegerCompoundGrainLocator<TGrain> : IIntegerCompoundGrainLocator<TGrain> where TGrain : IGrainWithIntegerCompoundKey
{
    private readonly IGrainFactory _grainFactory;

    public IntegerCompoundGrainLocator(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    public TGrain GetGrain(int key, string keyExt)
    {
        return _grainFactory.GetGrain<TGrain>(key, keyExt, null);
    }
}