class PowerManager
{
    public void Setup(BatteryMonitor monitor, PowerSaver saver, UserNotifier notifier)
    {
        monitor.BatteryLow += saver.Activate;
        monitor.BatteryLow += notifier.Notify;
    }
}
