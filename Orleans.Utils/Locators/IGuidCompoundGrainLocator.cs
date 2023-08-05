namespace Orleans.Utils;

public interface IGuidCompoundGrainLocator<TGrain> where TGrain : IGrainWithGuidCompoundKey
{
    TGrain GetGrain(Guid key, string keyExt);
}