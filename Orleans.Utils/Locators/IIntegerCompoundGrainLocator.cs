namespace Orleans.Utils;

public interface IIntegerCompoundGrainLocator<TGrain> where TGrain : IGrainWithIntegerCompoundKey
{
    TGrain GetGrain(int key, string keyExt);
}