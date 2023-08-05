using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Utils.Sample.GrainIdentiferSamples;
using Shouldly;

namespace Orleans.Utils.Tests;

public class GrainIdentifierTests : IClassFixture<UtilsSilo>
{
    UtilsSilo _factory;
    public GrainIdentifierTests(UtilsSilo factory)
    {
        _factory = factory;
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