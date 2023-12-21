namespace Orleans.Utilities;

public interface IGuidGrainLocator<TGrain> where TGrain : IGrainWithGuidKey
{
    TGrain GetGrain(Guid key);
}