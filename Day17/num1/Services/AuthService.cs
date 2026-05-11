using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using num1.Models;

namespace num1.Services
{
    public class AuthService
    {
        private readonly DataStorageService _storage;
        private List<UserModel> _users;
        private UserModel _currentUser;

        public UserModel CurrentUser => _currentUser;

        public AuthService()
        {
            _storage = new DataStorageService();
            _users = new List<UserModel>();
        }

        public async Task InitializeAsync()
        {
            _users = await _storage.LoadUsersAsync();
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            if (_users.Any(u => u.Username == username))
                return false;

            var newUser = new UserModel
            {
                Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1,
                Username = username,
                PasswordHash = HashPassword(password),
                CreatedDate = DateTime.Now
            };

            _users.Add(newUser);
            await _storage.SaveUsersAsync(_users);
            return true;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);

            if (user == null)
                return false;

            var hash = HashPassword(password);

            if (user.PasswordHash == hash)
            {
                _currentUser = user;
                return true;
            }

            return false;
        }

        public void Logout()
        {
            _currentUser = null;
        }

        public bool IsAuthenticated => _currentUser != null;
    }
}