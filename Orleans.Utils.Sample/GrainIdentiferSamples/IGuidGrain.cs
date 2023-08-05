namespace Orleans.Utils.Sample.GrainIdentiferSamples;

public interface IGuidGrain : IGrainWithGuidKey
{
    public Task<Guid> GetKey();
}