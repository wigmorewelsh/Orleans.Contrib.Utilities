using Microsoft.Extensions.DependencyInjection;
using Orleans.Utilities;
using Shouldly;
using Orleans.Utilities.Sample.GrainIdentiferSamples;

namespace Orleans.Utilities.Tests;

public class GrainLocatorTests : IClassFixture<UtilsSilo>
{
    UtilsSilo _factory;
    public GrainLocatorTests(UtilsSilo factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task GivenGrainWithIntegerKey_KeyFromGrainIdentifer_ShouldMatchGrain()
    {
        var grain = _factory.Services.GetRequiredService<IIntergerGrainLocator<IIntergerGrain>>();
        var key = await grain.GetGrain(1).GetKey();
        key.ShouldBe(1);
    }
    
    [Fact]
    public async Task GivenGrainWithStringKey_KeyFromGrainIdentifer_ShouldMatchGrain()
    {
        var grain = _factory.Services.GetRequiredService<IStringGrainLocator<IStringGrain>>();
        var key = await grain.GetGrain("1").GetKey();
        key.ShouldBe("1");
    }
    
    [Fact]
    public async Task GivenGrainWithGuidKey_KeyFromGrainIdentifer_ShouldMatchGrain()
    {
        var grain = _factory.Services.GetRequiredService<IGuidGrainLocator<IGuidGrain>>();
        var key = await grain.GetGrain(Guid.NewGuid()).GetKey();
        key.ShouldBe(key);
    }
}



