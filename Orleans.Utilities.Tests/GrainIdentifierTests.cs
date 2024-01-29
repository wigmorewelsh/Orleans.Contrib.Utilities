using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shouldly;
using Orleans.Utilities.Sample.GrainIdentiferSamples;

namespace Orleans.Utilities.Tests;

public class GrainIdentifierTests : IClassFixture<UtilsSilo>
{
    UtilsSilo _factory;
    public GrainIdentifierTests(UtilsSilo factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenGrainWithIntegerKey_GrainId_ShouldMatchGrain() 
    {
        var grainFactory = _factory.Services.GetRequiredService<IGrainFactory>();
        var grain = grainFactory.GetGrain<IIntergerGrain>(1);
        var check = await grain.CheckGrainIdMatches();
        check.ShouldBeTrue();
    }
    
    [Fact]
    public async Task GivenGrainWithIntegerKey_KeyFromGrainIdentifer_ShouldMatchGrain()
    {
        var grainFactory = _factory.Services.GetRequiredService<IGrainFactory>();
        var grain = grainFactory.GetGrain<IIntergerGrain>(1);
        var key = await grain.GetKey();
        key.ShouldBe(1);
    }
    
    [Fact]
    public async Task GivenGrainWithStringKey_KeyFromGrainIdentifer_ShouldMatchGrain()
    {
        var grainFactory = _factory.Services.GetRequiredService<IGrainFactory>();
        var grain = grainFactory.GetGrain<IStringGrain>("1");
        var key = await grain.GetKey();
        key.ShouldBe("1");
    }
    
    [Fact]
    public async Task GivenGrainWithGuidKey_KeyFromGrainIdentifer_ShouldMatchGrain()
    {
        var grainFactory = _factory.Services.GetRequiredService<IGrainFactory>();
        var grain = grainFactory.GetGrain<IGuidGrain>(Guid.NewGuid());
        var key = await grain.GetKey();
        key.ShouldBe(key);
    }
}