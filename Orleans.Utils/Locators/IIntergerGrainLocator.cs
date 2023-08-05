namespace Orleans.Utils;

public interface IIntergerGrainLocator<TGrain> where TGrain : IGrainWithIntegerKey
{
    TGrain GetGrain(int key);
}