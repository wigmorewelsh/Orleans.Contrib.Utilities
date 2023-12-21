namespace Orleans.Utilities;

public interface IIntegerCompoundGrainLocator<TGrain> where TGrain : IGrainWithIntegerCompoundKey
{
    TGrain GetGrain(int key, string keyExt);
}