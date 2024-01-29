namespace Orleans.Utilities.Sample.GrainIdentiferSamples;

public interface IIntergerGrain : IGrainWithIntegerKey
{
    public Task<long> GetKey();
    public Task<bool> CheckGrainIdMatches();
}