using Microsoft.Extensions.Logging;

namespace Orleans.Utilities.BlockingDetector;

public class BlockingDetectionCallFilter : IIncomingGrainCallFilter
{
    private readonly ILogger logger;
    private BlockingDetector monitor;

    public BlockingDetectionCallFilter(ILogger<BlockingDetectionCallFilter> logger, ILoggerFactory loggerFactory)
    {
        this.logger = logger;
        this.monitor = new BlockingDetector(loggerFactory);
    }

    public async Task Invoke(IIncomingGrainCallContext context)
    {
        if (context == null) return;

        using var _ = monitor.Invoke();
        await context.Invoke();
    }
}