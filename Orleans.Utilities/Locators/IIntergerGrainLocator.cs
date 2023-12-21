namespace Orleans.Utilities;

public interface IIntergerGrainLocator<TGrain> where TGrain : IGrainWithIntegerKey
{
    TGrain GetGrain(int key);
}