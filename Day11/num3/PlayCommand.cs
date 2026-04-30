public class PlayCommand : ICommand
{
    private MusicPlayer _player;
    public PlayCommand(MusicPlayer player) => _player = player;
    public void Execute() => _player.Play();
}
