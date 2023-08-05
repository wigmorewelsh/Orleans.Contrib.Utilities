namespace Orleans.Utils;

public interface IGuidGrainLocator<TGrain> where TGrain : IGrainWithGuidKey
{
    TGrain GetGrain(Guid key);
}