using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using num1.Models;

namespace num1.Services
{
    public class TaskService
    {
        private ObservableCollection<TaskModel> _tasks;
        private ObservableCollection<TaskCategoryModel> _categories;

        public TaskService()
        {
            _tasks = new ObservableCollection<TaskModel>();
            _categories = new ObservableCollection<TaskCategoryModel>();
            InitializeData();
        }

        private void InitializeData()
        {
            _categories.Add(new TaskCategoryModel { Id = 1, Name = "Работа", Color = "#FF2196F3" });
            _categories.Add(new TaskCategoryModel { Id = 2, Name = "Личное", Color = "#FF4CAF50" });
            _categories.Add(new TaskCategoryModel { Id = 3, Name = "Учеба", Color = "#FFFF9800" });

            _tasks.Add(new TaskModel
            {
                Id = 1,
                Title = "Изучить WPF MVVM",
                Description = "Прочитать документацию и сделать пример",
                Deadline = DateTime.Now.AddDays(2),
                CreatedDate = DateTime.Now,
                Status = MyTaskStatus.ВРаботе,
                CategoryId = 3,
                Category = _categories[2]
            });

            _tasks.Add(new TaskModel
            {
                Id = 2,
                Title = "Сделать проект",
                Description = "Написать код с асинхронностью",
                Deadline = DateTime.Now.AddDays(5),
                CreatedDate = DateTime.Now.AddDays(-1),
                Status = MyTaskStatus.Ожидание,
                CategoryId = 1,
                Category = _categories[0]
            });

            _tasks.Add(new TaskModel
            {
                Id = 3,
                Title = "Сдать работу",
                Description = "Отправить преподавателю",
                Deadline = DateTime.Now.AddDays(3),
                CreatedDate = DateTime.Now.AddDays(-2),
                Status = MyTaskStatus.Выполнено,
                CategoryId = 2,
                Category = _categories[1]
            });
        }

        public async Task<ObservableCollection<TaskModel>> GetTasksAsync()
        {
            await Task.Delay(2000);
            return new ObservableCollection<TaskModel>(_tasks);
        }

        public async Task<ObservableCollection<TaskCategoryModel>> GetCategoriesAsync()
        {
            await Task.Delay(500);
            return new ObservableCollection<TaskCategoryModel>(_categories);
        }

        public async Task AddTaskAsync(TaskModel task)
        {
            await Task.Delay(300);
            task.Id = _tasks.Count + 1;
            task.CreatedDate = DateTime.Now;
            task.Category = _categories.FirstOrDefault(c => c.Id == task.CategoryId);
            _tasks.Add(task);
        }

        public async Task DeleteTaskAsync(TaskModel task)
        {
            await Task.Delay(300);
            _tasks.Remove(task);
        }

        public async Task UpdateTaskAsync(TaskModel task)
        {
            await Task.Delay(300);
            var index = _tasks.IndexOf(task);
            if (index >= 0)
            {
                _tasks[index] = task;
            }
        }
    }
}