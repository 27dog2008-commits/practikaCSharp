class Num4
{
    static void Main()
    {
        BatteryMonitor monitor = new BatteryMonitor();
        PowerSaver saver = new PowerSaver();
        UserNotifier notifier = new UserNotifier();
        PowerManager manager = new PowerManager();

        manager.Setup(monitor, saver, notifier);

        monitor.CheckBattery(15);
    }
}