namespace Orleans.Utilities.Sample.GrainIdentiferSamples;

public interface IStringGrain : IGrainWithStringKey
{
    public Task<string> GetKey();
}