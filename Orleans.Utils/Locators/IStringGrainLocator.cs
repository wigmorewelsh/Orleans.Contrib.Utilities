namespace Orleans.Utils;

public interface IStringGrainLocator<TGrain> where TGrain : IGrainWithStringKey
{
    TGrain GetGrain(string key);
}