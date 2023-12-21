using Microsoft.Extensions.Logging;

namespace Orleans.BlockingDetector;

/// <summary>
/// Based on https://github.com/benaadams/Ben.BlockingDetector altered for the taskschedulers that orleans uses
/// </summary>
internal class BlockingDetector
{
    private readonly ILoggerFactory _loggerFactory;

    private readonly BlockingMonitor _monitor;
    private readonly DetectBlockingSynchronizationContext _detectBlockingSyncCtx;
    private readonly TaskBlockingListener _listener;

    public BlockingDetector(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        // Detect blocking
        _monitor = new BlockingMonitor(loggerFactory);
        _detectBlockingSyncCtx = new DetectBlockingSynchronizationContext(_monitor);
        _listener = new TaskBlockingListener(_monitor);
    }

    public async Task<T> Invoke<T>(Func<Task<T>> next)
    {
        var syncCtx = SynchronizationContext.Current;
        if (TaskScheduler.Current != TaskScheduler.Default)
        {
            SynchronizationContext.SetSynchronizationContext(new DetectBlockingSynchronizationContextTaskScheduler(_monitor, TaskScheduler.Current));
        }
        else
        {
            SynchronizationContext.SetSynchronizationContext(syncCtx == null ? _detectBlockingSyncCtx : new DetectBlockingSynchronizationContext(_monitor, syncCtx));
        }

        try
        {
            return await next();
        }

        finally
        {
            SynchronizationContext.SetSynchronizationContext(syncCtx);
        }
    }
        
    public async Task<IDisposable> Invoke()
    {
        var syncCtx = SynchronizationContext.Current;
        if (TaskScheduler.Current != TaskScheduler.Default)
        {
            SynchronizationContext.SetSynchronizationContext(new DetectBlockingSynchronizationContextTaskScheduler(_monitor, TaskScheduler.Current));
        }
        else
        {
            SynchronizationContext.SetSynchronizationContext(syncCtx == null ? _detectBlockingSyncCtx : new DetectBlockingSynchronizationContext(_monitor, syncCtx));
        }

        var scope = new BlockingDetectorScope(syncCtx);
        return scope;
    }

    private class BlockingDetectorScope : IDisposable
    {
        private readonly SynchronizationContext? _syncCtx;

        public BlockingDetectorScope(SynchronizationContext? syncCtx)
        {
            _syncCtx = syncCtx;
        }

        public void Dispose()
        {
            SynchronizationContext.SetSynchronizationContext(_syncCtx);
        }
    }
}