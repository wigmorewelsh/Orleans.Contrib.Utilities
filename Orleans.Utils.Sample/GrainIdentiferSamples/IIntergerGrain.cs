namespace Orleans.Utils.Sample.GrainIdentiferSamples;

public interface IIntergerGrain : IGrainWithIntegerKey
{
    public Task<long> GetKey();
}