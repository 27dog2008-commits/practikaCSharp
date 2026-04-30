public class PauseCommand : ICommand
{
    private MusicPlayer _player;
    public PauseCommand(MusicPlayer player) => _player = player;
    public void Execute() => _player.Pause();
}
