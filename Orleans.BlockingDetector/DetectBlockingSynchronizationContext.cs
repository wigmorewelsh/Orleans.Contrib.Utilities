namespace Orleans.BlockingDetector;

// Tips of the Toub
internal sealed class DetectBlockingSynchronizationContext : SynchronizationContext
{
    private readonly BlockingMonitor _monitor;
    private readonly SynchronizationContext? _syncCtx;

    public DetectBlockingSynchronizationContext(BlockingMonitor monitor)
    {
        _monitor = monitor;

        SetWaitNotificationRequired();
    }

    public DetectBlockingSynchronizationContext(BlockingMonitor monitor, SynchronizationContext syncCtx) : this(monitor)
    {
        _syncCtx = syncCtx;
    }

    public override void Post(SendOrPostCallback d, object? state)
    {
            
        base.Post(d, state);
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
        if (_syncCtx != null)
        {
            return _syncCtx.Wait(waitHandles, waitAll, millisecondsTimeout);
        }
        else
        {
            return base.Wait(waitHandles, waitAll, millisecondsTimeout);
        }
    }
}