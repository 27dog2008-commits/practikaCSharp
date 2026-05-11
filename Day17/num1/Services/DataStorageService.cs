using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using num1.Models;
using System.Text.Json;

namespace num1.Services
{
    public class DataStorageService
    {
        private readonly string _tasksFilePath;
        private readonly string _usersFilePath;
        private readonly string _appDataFolder;

        public DataStorageService()
        {
            _appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TaskManager");

            if (!Directory.Exists(_appDataFolder))
                Directory.CreateDirectory(_appDataFolder);

            _tasksFilePath = Path.Combine(_appDataFolder, "tasks.json");
            _usersFilePath = Path.Combine(_appDataFolder, "users.json");
        }

        public void SaveUsers(List<UserModel> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_usersFilePath, json);
        }

        public List<UserModel> LoadUsers()
        {
            if (!File.Exists(_usersFilePath))
                return new List<UserModel>();

            var json = File.ReadAllText(_usersFilePath);
            return JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();
        }

        public async Task SaveUsersAsync(List<UserModel> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            await Task.Run(() => File.WriteAllText(_usersFilePath, json));
        }

        public async Task<List<UserModel>> LoadUsersAsync()
        {
            if (!File.Exists(_usersFilePath))
                return new List<UserModel>();

            var json = await Task.Run(() => File.ReadAllText(_usersFilePath));
            return JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();
        }

        public void SaveTasks(int userId, List<TaskModel> tasks)
        {
            var userTasksFile = Path.Combine(_appDataFolder, $"tasks_{userId}.json");
            var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(userTasksFile, json);
        }

        public List<TaskModel> LoadTasks(int userId)
        {
            var userTasksFile = Path.Combine(_appDataFolder, $"tasks_{userId}.json");

            if (!File.Exists(userTasksFile))
                return new List<TaskModel>();

            var json = File.ReadAllText(userTasksFile);
            return JsonSerializer.Deserialize<List<TaskModel>>(json) ?? new List<TaskModel>();
        }

        public async Task SaveTasksAsync(int userId, List<TaskModel> tasks)
        {
            var userTasksFile = Path.Combine(_appDataFolder, $"tasks_{userId}.json");
            var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            await Task.Run(() => File.WriteAllText(userTasksFile, json));
        }

        public async Task<List<TaskModel>> LoadTasksAsync(int userId)
        {
            var userTasksFile = Path.Combine(_appDataFolder, $"tasks_{userId}.json");

            if (!File.Exists(userTasksFile))
                return new List<TaskModel>();

            var json = await Task.Run(() => File.ReadAllText(userTasksFile));
            return JsonSerializer.Deserialize<List<TaskModel>>(json) ?? new List<TaskModel>();
        }
    }
}