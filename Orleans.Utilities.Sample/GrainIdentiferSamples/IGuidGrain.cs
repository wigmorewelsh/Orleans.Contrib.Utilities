namespace Orleans.Utilities.Sample.GrainIdentiferSamples;

public interface IGuidGrain : IGrainWithGuidKey
{
    public Task<Guid> GetKey();
}