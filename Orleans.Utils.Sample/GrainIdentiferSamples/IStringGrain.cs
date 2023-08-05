namespace Orleans.Utils.Sample.GrainIdentiferSamples;

public interface IStringGrain : IGrainWithStringKey
{
    public Task<string> GetKey();
}