class BatteryMonitor
{
    public event EventHandler BatteryLow;

    public void CheckBattery(int level)
    {
        if (level < 20)
        {
            Console.WriteLine($"Монитор: Внимание! Низкий заряд батареи ({level}%)");
            BatteryLow?.Invoke(this, EventArgs.Empty);
        }
    }
}
