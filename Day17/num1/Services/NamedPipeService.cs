using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

namespace num1.Services
{
    public class NamedPipeService
    {
        private const string PipeName = "TaskManagerChat";
        private NamedPipeServerStream _serverStream;
        private NamedPipeClientStream _clientStream;

        public event Action<string> MessageReceived;

        public async Task StartServerAsync()
        {
            _serverStream = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 10);

            await Task.Run(() => _serverStream.WaitForConnection());

            var reader = new StreamReader(_serverStream);

            while (true)
            {
                try
                {
                    var message = await reader.ReadLineAsync();
                    if (message != null)
                    {
                        MessageReceived?.Invoke(message);
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        public async Task SendMessageAsync(string message)
        {
            try
            {
                _clientStream = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut);
                await _clientStream.ConnectAsync(1000);

                var writer = new StreamWriter(_clientStream);
                await writer.WriteLineAsync(message);
                await writer.FlushAsync();

                _clientStream.Close();
                _clientStream.Dispose();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Pipe Send Error: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _serverStream?.Dispose();
            _clientStream?.Dispose();
        }
    }
}