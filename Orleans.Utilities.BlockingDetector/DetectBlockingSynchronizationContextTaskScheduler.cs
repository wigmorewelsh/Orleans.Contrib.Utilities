namespace Orleans.Utilities.BlockingDetector;

internal sealed class DetectBlockingSynchronizationContextTaskScheduler : SynchronizationContext
{
    private readonly BlockingMonitor _monitor;
    private readonly TaskScheduler _syncCtx;

    public DetectBlockingSynchronizationContextTaskScheduler(BlockingMonitor monitor, TaskScheduler taskScheduler)
    {
        _monitor = monitor;

        SetWaitNotificationRequired();
        _syncCtx = taskScheduler;
    }

    public override void Post(SendOrPostCallback d, object? state)
    {
        Task.Factory.StartNew(() => d.Invoke(state), CancellationToken.None, TaskCreationOptions.None, _syncCtx);
    }

    public override int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
    {
        if (millisecondsTimeout == 0)
        {
            return WaitInternal(waitHandles, waitAll, millisecondsTimeout);
        }
        else
        {
            _monitor.BlockingStart(DectectionSource.SynchronizationContext);

            try
            {
                return WaitInternal(waitHandles, waitAll, millisecondsTimeout);
            }
            finally
            {
                _monitor.BlockingEnd();
            }
        }
    }

    private int WaitInternal(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
    {
        return base.Wait(waitHandles, waitAll, millisecondsTimeout);
    }
}