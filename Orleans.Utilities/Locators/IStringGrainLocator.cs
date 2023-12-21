namespace Orleans.Utilities;

public interface IStringGrainLocator<TGrain> where TGrain : IGrainWithStringKey
{
    TGrain GetGrain(string key);
}