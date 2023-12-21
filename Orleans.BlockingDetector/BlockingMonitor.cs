using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Orleans.BlockingDetector;

internal class BlockingMonitor
{
    [ThreadStatic]
    private static int t_recursionCount;

    private readonly ILogger _logger;

    public BlockingMonitor(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<BlockingMonitor>();
    }


    public void BlockingStart(DectectionSource dectectionSource)
    {
        if (!Thread.CurrentThread.IsThreadPoolThread)
        {
            return;
        }

        t_recursionCount++;

        try
        {
            if (t_recursionCount == 1)
            {
                _logger.BlockingMethodCalled(
                    new StackTrace(dectectionSource == DectectionSource.SynchronizationContext ? 3 : 6));
            }
        }
        catch
        {
        }
    }

    public void BlockingEnd()
    {
        if (!Thread.CurrentThread.IsThreadPoolThread)
        {
            return;
        }

        t_recursionCount--;
    }
}