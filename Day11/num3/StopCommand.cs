public class StopCommand : ICommand
{
    private MusicPlayer _player;
    public StopCommand(MusicPlayer player) => _player = player;
    public void Execute() => _player.Stop();
}
