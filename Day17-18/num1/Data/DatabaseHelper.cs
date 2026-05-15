using num1.Models;
using num1.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace num1.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            var dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TaskManager",
                "tasks.db");

            var directory = Path.GetDirectoryName(dbPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            _connectionString = $"Data Source={dbPath};Version=3;";

            InitializeDatabase();
        }

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
                        CreatedDate TEXT NOT NULL
                    )";

                string createCategoriesTable = @"
                    CREATE TABLE IF NOT EXISTS Categories (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Color TEXT NOT NULL
                    )";

                string createTasksTable = @"
                    CREATE TABLE IF NOT EXISTS Tasks (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Description TEXT,
                        Deadline TEXT NOT NULL,
                        CreatedDate TEXT NOT NULL,
                        Status INTEGER NOT NULL,
                        CategoryId INTEGER NOT NULL,
                        UserId INTEGER NOT NULL
                    )";

                using (var cmd = new SQLiteCommand(createUsersTable, connection))
                    cmd.ExecuteNonQuery();

                using (var cmd = new SQLiteCommand(createCategoriesTable, connection))
                    cmd.ExecuteNonQuery();

                using (var cmd = new SQLiteCommand(createTasksTable, connection))
                    cmd.ExecuteNonQuery();

                // Категории по умолчанию
                using (var cmd = new SQLiteCommand("INSERT OR IGNORE INTO Categories (Id, Name, Color) VALUES (1, 'Работа', '#FF2196F3')", connection))
                    cmd.ExecuteNonQuery();

                using (var cmd = new SQLiteCommand("INSERT OR IGNORE INTO Categories (Id, Name, Color) VALUES (2, 'Личное', '#FF4CAF50')", connection))
                    cmd.ExecuteNonQuery();

                using (var cmd = new SQLiteCommand("INSERT OR IGNORE INTO Categories (Id, Name, Color) VALUES (3, 'Учеба', '#FFFF9800')", connection))
                    cmd.ExecuteNonQuery();
            }
        }

        public async Task<List<TaskModel>> GetTasksByUserIdAsync(int userId)
        {
            var tasks = new List<TaskModel>();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT t.*, c.Name as CategoryName, c.Color as CategoryColor
                    FROM Tasks t
                    LEFT JOIN Categories c ON t.CategoryId = c.Id
                    WHERE t.UserId = @UserId";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var task = new TaskModel();
                            task.Id = reader.GetInt32(0);
                            task.Title = reader.GetString(1);
                            task.Description = reader.GetString(2);
                            task.Deadline = DateTime.Parse(reader.GetString(3));
                            task.CreatedDate = DateTime.Parse(reader.GetString(4));
                            task.Status = (MyTaskStatus)reader.GetInt32(5);
                            task.CategoryId = reader.GetInt32(6);
                            task.UserId = reader.GetInt32(7);
                            task.Category = new TaskCategoryModel();
                            task.Category.Id = task.CategoryId;
                            task.Category.Name = reader.GetString(8);
                            task.Category.Color = reader.GetString(9);

                            tasks.Add(task);
                        }
                    }
                }
            }

            return tasks;
        }

        public async Task AddTaskAsync(TaskModel task)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                    INSERT INTO Tasks (Title, Description, Deadline, CreatedDate, Status, CategoryId, UserId)
                    VALUES (@Title, @Description, @Deadline, @CreatedDate, @Status, @CategoryId, @UserId);
                    SELECT last_insert_rowid();";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", task.Title);
                    cmd.Parameters.AddWithValue("@Description", task.Description);
                    cmd.Parameters.AddWithValue("@Deadline", task.Deadline.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@CreatedDate", task.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Status", (int)task.Status);
                    cmd.Parameters.AddWithValue("@CategoryId", task.CategoryId);
                    cmd.Parameters.AddWithValue("@UserId", task.UserId);

                    task.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }
            }
        }
            
        public async Task UpdateTaskAsync(TaskModel task)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
            UPDATE Tasks 
            SET Title = @Title, Description = @Description, Deadline = @Deadline, 
                Status = @Status, CategoryId = @CategoryId
            WHERE Id = @Id";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", task.Id);
                    cmd.Parameters.AddWithValue("@Title", task.Title);
                    cmd.Parameters.AddWithValue("@Description", task.Description);
                    cmd.Parameters.AddWithValue("@Deadline", task.Deadline.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Status", (int)task.Status);
                    cmd.Parameters.AddWithValue("@CategoryId", task.CategoryId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Tasks WHERE Id = @Id";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", taskId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

//mvvm ✔️

//===============Четние

//1.ПОЛЬЗОВАТЕЛЬ → Открывает программу
//         ↓
//2. КНОПКА "Загрузить" (или автоматически при запуске)
//         ↓
//3. LoadTasksCommand        TaskManagerViewModel ⬅️ тут вот находится
//         ↓
//4. LoadTasksAsync() метод     
//         ↓
//5. DatabaseHelper.GetTasksByUserIdAsync()           ⬅️ этот файл
//         ↓
//6. SQL SELECT запрос  
//         ↓
//7. SQLite БД (файл tasks.db)
//         ↓
//8. Возвращаются задачи обратно 
//         ↓
//9. Tasks.Clear() и Tasks.Add() - обновление списка
//         ↓
//10. ЭКРАН → Показывает задачи


//==========================================================
//<div>
//TaskManagerViewModel  |  ViewModel |  Управляет задачами
//MainWindow	        |  View	     |  Главное окно
//LoginWindow	        |  View	     |  Окно входа
//TaskEditWindow	    |  View	     |  Окно добавления/редактирования
//DatabaseHelper        |  Data      |  Сервис	Работа с SQLite БД
//AuthService	        |  Service   |  Сервис	Регистрация и вход
//TaskModel	            |  Models    |  Модель	Данные задачи
//RelayCommand          |  Commands	 |  Команда	Привязка действий к кнопкам
//LoginViewModel	    |  ViewModel |  Логика входа
//</div>
//==========================================================






/**

                
      ________  
     /        \
   /| ()    () |\
 []  \________/ [] 
     ///    \\\  


 _                _/\__ 
| |  ____________/     \___
\ \_/               ______|  
 \   _________    /  
  |  /         | |
   \/          |_|
                 


               _   _
              / \_/ \
             | -   - |
             |       |
            /         \
           /  | | | |  \
          |   | | | |   |
           \__|_|_|_|__/


**/













//===============Добавить

//1.ПОЛЬЗОВАТЕЛЬ → Нажимает кнопку "Добавить"
//         ↓
//2. ОТКРЫВАЕТСЯ TaskEditWindow (окно ввода)
//         ↓
//3. ПОЛЬЗОВАТЕЛЬ → Вводит название, описание, срок → "Сохранить"
//         ↓
//4. ТИПА ПРИОБРЕТАЕТ ЗНАНИЯ В КОДЕ
//         ↓
//5. AddTaskCommand.Execute()
//         ↓
//6. TaskManagerViewModel.AddTaskAsync()
//         ↓
//7. СОЗДАЁТСЯ новый объект TaskModel
//         ↓
//8. DatabaseHelper.AddTaskAsync()
//         ↓
//9. SQL INSERT запрос
//         ↓
//10. SQLite БД (файл tasks.db) - ДОБАВЛЯЕТСЯ задача
//         ↓
//11. LoadTasksAsync() - ОБНОВЛЕНИЕ списка
//         ↓
//12. ЭКРАН → Показывает обновлённый список

//===============статус

//1.ПОЛЬЗОВАТЕЛЬ → Выбирает задачу в списке
//         ↓
//2. ПОЛЬЗОВАТЕЛЬ → Нажимает "Сменить статус"
//         ↓
//3. ChangeStatusCommand.Execute()
//         ↓
//4. TaskManagerViewModel.ChangeStatus()
//         ↓
//5. МЕНЯЕТСЯ статус у задачи (в памяти)
//         ↓
//6. DatabaseHelper.UpdateTaskAsync()
//         ↓
//7. SQL UPDATE запрос
//         ↓
//8. SQLite БД (файл tasks.db) - ОБНОВЛЯЕТ статус
//         ↓
//9. ЭКРАН → Обновляется статус у задачи

//==================удаление

//1.ПОЛЬЗОВАТЕЛЬ → Выбирает задачу в списке
//         ↓
//2. ПОЛЬЗОВАТЕЛЬ → Нажимает "Удалить"
//         ↓
//3. ВСПЛЫВАЕТ ОКНО "Вы уверены?"
//         ↓
//4. ПОЛЬЗОВАТЕЛЬ → Нажимает "Да"
//         ↓
//5. DeleteTaskCommand.Execute()
//         ↓
//6. TaskManagerViewModel.DeleteTaskAsync()
//         ↓
//7. DatabaseHelper.DeleteTaskAsync()
//         ↓
//8. SQL DELETE запрос
//         ↓
//9. SQLite БД (файл tasks.db) - УДАЛЯЕТ задачу
//         ↓
//10. LoadTasksAsync() - ОБНОВЛЕНИЕ списка
//         ↓
//11. ЭКРАН → Задача исчезает