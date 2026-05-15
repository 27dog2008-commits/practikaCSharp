using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using num1.Models;

namespace num1.Services
{
    public class AuthService
    {
        private readonly string _connectionString;
        private List<UserModel> _users;
        private UserModel _currentUser;

        public UserModel CurrentUser => _currentUser;

        public AuthService()
        {
            var dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TaskManager",
                "tasks.db");

            var directory = Path.GetDirectoryName(dbPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            _connectionString = $"Data Source={dbPath};Version=3;";
            _users = new List<UserModel>();
        }

        //создание табл если нету 
        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string createUsersTable = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT NOT NULL UNIQUE,
                        PasswordHash TEXT NOT NULL,
                        CreatedDate TEXT NOT NUL
                    )";

                using (var cmd = new SQLiteCommand(createUsersTable, connection))
                    cmd.ExecuteNonQuery();
            }
        }

        public async Task InitializeAsync()
        {
            await Task.Run(() => InitializeDatabase());
            await LoadUsersAsync();
        }


        //чтение всех пользователей из бд
        private async Task LoadUsersAsync()
        {
            _users.Clear();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();

                //Запрос для получения всех пользователей
                string query = "SELECT Id, Username, PasswordHash, CreatedDate FROM Users";


                using (var cmd = new SQLiteCommand(query, connection))
                //выполнение запрос
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var user = new UserModel(); //пустой чмошник
                        user.Id = reader.GetInt32(0);
                        user.Username = reader.GetString(1);
                        user.PasswordHash = reader.GetString(2);
                        user.CreatedDate = DateTime.Parse(reader.GetString(3));
                        _users.Add(user);  //кидаем в список в память 
                    }
                }
            }
        }

        //хэширование пароля
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }


        //регистрация
        public async Task<bool> RegisterAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            await LoadUsersAsync();

            if (_users.Any(u => u.Username == username))
                return false;

            var hashedPassword = HashPassword(password);

            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                    INSERT INTO Users (Username, PasswordHash, CreatedDate)
                    VALUES (@Username, @PasswordHash, @CreatedDate);
                    SELECT last_insert_rowid();";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    var newId = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    var newUser = new UserModel
                    {
                        Id = newId,
                        Username = username,
                        PasswordHash = hashedPassword,
                        CreatedDate = DateTime.Now
                    };

                    _users.Add(newUser);
                }
            }

            return true;
        }


        //вход
        public async Task<bool> LoginAsync(string username, string password)
        {
            // 1. Загружаем пользователей из БД В ****** СПИСОК 
            await LoadUsersAsync();

            // 2. Ищем пользователя с таким логином
            var user = _users.FirstOrDefault(u => u.Username == username);

            if (user == null)
                return false;  

            // 3. Хэшируем введённый пароль
            var hash = HashPassword(password);

            // 4. Сравниваем хэши
            if (user.PasswordHash == hash)
            {
                _currentUser = user;  // Запоминаем текущего пользователя
                return true;
            }

            return false;  // Неправильный пароль
        }

        public void Logout()
        {
            _currentUser = null;
        }

        public bool IsAuthenticated => _currentUser != null;
    }
}