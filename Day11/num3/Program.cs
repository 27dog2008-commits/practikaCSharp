class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Паттерн Команда");

        MusicPlayer player = new MusicPlayer();
        PlayerRemote remote = new PlayerRemote();

        remote.SetCommand(new PlayCommand(player));
        remote.PressButton();

        remote.SetCommand(new PauseCommand(player));
        remote.PressButton();

        remote.SetCommand(new StopCommand(player));
        remote.PressButton();
    }
}
