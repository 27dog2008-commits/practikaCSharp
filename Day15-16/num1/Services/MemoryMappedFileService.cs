using System;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace num1.Services
{
    public class MemoryMappedFileService : IDisposable
    {
        private const string MapName = "TaskManagerNotifications";
        private const int MapSize = 1024;
        private MemoryMappedFile _mmf;
        private bool _disposed;
        private Timer _readTimer;
        private string _lastMessage;
        private DateTime _lastMessageTime;

        public event Action<string> NotificationReceived;

        public MemoryMappedFileService()
        {
            try
            {
                _mmf = MemoryMappedFile.CreateOrOpen(MapName, MapSize);
                StartReading();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"MMF Error: {ex.Message}");
            }
        }

        public void SendNotification(string message)
        {
            try
            {
                using (var accessor = _mmf.CreateViewAccessor(0, MapSize))
                {
                    var bytes = Encoding.UTF8.GetBytes(message + "\0");
                    accessor.WriteArray(0, bytes, 0, bytes.Length);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Send Error: {ex.Message}");
            }
        }

        private void StartReading()
        {
            _readTimer = new Timer(ReadNotification, null, 0, 500);
        }

        private void ReadNotification(object state)
        {
            try
            {
                using (var accessor = _mmf.CreateViewAccessor(0, MapSize))
                {
                    var bytes = new byte[MapSize];
                    accessor.ReadArray(0, bytes, 0, MapSize);

                    var message = Encoding.UTF8.GetString(bytes).TrimEnd('\0');

                    if (!string.IsNullOrEmpty(message))
                    {
                        var now = DateTime.Now;
                        if (_lastMessage != message || (now - _lastMessageTime).TotalSeconds > 2)
                        {
                            _lastMessage = message;
                            _lastMessageTime = now;
                            NotificationReceived?.Invoke(message);
                        }

                        var emptyBytes = new byte[MapSize];
                        accessor.WriteArray(0, emptyBytes, 0, emptyBytes.Length);
                    }
                }
            }
            catch { }
        }

        public void Dispose()
        {
            if (_disposed) return;

            _readTimer?.Dispose();
            _mmf?.Dispose();
            _disposed = true;
        }
    }
}