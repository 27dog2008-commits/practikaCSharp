using num1.Commands;
using num1.Data;
using num1.Models;
using num1.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace num1.ViewModels
{
    public class TaskManagerViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly TaskService _taskService;
        private readonly DatabaseHelper _db;
        private readonly AuthService _authService;
        private readonly MemoryMappedFileService _notificationService;
        private readonly NamedPipeService _pipeService;

        private ObservableCollection<TaskModel> _tasks;
        private ObservableCollection<TaskCategoryModel> _categories;
        private TaskModel _selectedTask;
        private string _statusFilter;
        private bool _isLoading;
        private string _loadingMessage;

        public ObservableCollection<TaskModel> Tasks
        {
            get => _tasks;
            set { _tasks = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TaskCategoryModel> Categories
        {
            get => _categories;
            set { _categories = value; OnPropertyChanged(); }
        }

        public TaskModel SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(); }
        }

        public string StatusFilter
        {
            get => _statusFilter;
            set
            {
                _statusFilter = value;
                OnPropertyChanged();
                FilterTasks();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public string LoadingMessage
        {
            get => _loadingMessage;
            set { _loadingMessage = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> FilterOptions { get; set; }

        public ICommand LoadTasksCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand ChangeStatusCommand { get; }

        public event Action<TaskModel> TaskAdded;

        private ObservableCollection<TaskModel> _allTasks;

        public TaskManagerViewModel(AuthService authService)
        {
            _authService = authService;
            _taskService = new TaskService();
            _db = new DatabaseHelper();
            _notificationService = new MemoryMappedFileService();
            _pipeService = new NamedPipeService();

            _allTasks = new ObservableCollection<TaskModel>();
            Tasks = new ObservableCollection<TaskModel>();
            _categories = new ObservableCollection<TaskCategoryModel>();

            FilterOptions = new ObservableCollection<string> { "Все", "Ожидание", "В работе", "Выполнено" };
            StatusFilter = "Все";

            _notificationService.NotificationReceived += OnNotificationReceived;

            Task.Run(() => _pipeService.StartServerAsync());
            _pipeService.MessageReceived += OnChatMessageReceived;

            // вот тут LoadTasksCommand   и иные такс команды

            LoadTasksCommand = new RelayCommand(async o => await LoadTasksAsync());
            AddTaskCommand = new RelayCommand(async o => await AddTaskAsync());
            EditTaskCommand = new RelayCommand(async o => await EditTaskAsync(), o => SelectedTask != null);
            DeleteTaskCommand = new RelayCommand(async o => await DeleteTaskAsync(), o => SelectedTask != null);
            ChangeStatusCommand = new RelayCommand(async o => await ChangeStatusAsync(), o => SelectedTask != null);

            LoadTasksCommand.Execute(null);
        }

        private async Task LoadTasksAsync()
        {
            IsLoading = true;
            LoadingMessage = "Загрузка задач из базы данных...";

            try
            {
                // 1-7: запрос бд
                var tasks = await _db.GetTasksByUserIdAsync(_authService.CurrentUser.Id);
                // 8. возвр задачи обратно (уже в переменной tasks)
                // 9. ОБНОВЛЕНИЕ СПИСКА
                _allTasks.Clear();// Очищаем старый список
                foreach (var task in tasks)
                {
                    _allTasks.Add(task); // Добавляем новые задачи
                }

                if (_allTasks.Count == 0)
                {
                    AddSampleTasks();
                    await SaveAllTasksToDb();
                }

                await LoadCategoriesAsync();
                FilterTasks();

                //10.ОБНОВЛЕНИЕ ЭКРАНА(автоматически через Binding)
                LoadingMessage = "Загрузка завершена!";
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                LoadingMessage = "Ошибка загрузки!";
            }
            finally
            {
                IsLoading = false;
            }
        }


        private async Task LoadCategoriesAsync()
        {
            _categories.Clear();

            var categoriesList = new ObservableCollection<TaskCategoryModel>();
            categoriesList.Add(new TaskCategoryModel { Id = 1, Name = "Работа", Color = "#FF2196F3" });
            categoriesList.Add(new TaskCategoryModel { Id = 2, Name = "Личное", Color = "#FF4CAF50" });
            categoriesList.Add(new TaskCategoryModel { Id = 3, Name = "Учеба", Color = "#FFFF9800" });

            foreach (var cat in categoriesList)
            {
                _categories.Add(cat);
            }
            await Task.CompletedTask;
        }

        private async Task SaveAllTasksToDb()
        {
            foreach (var task in _allTasks)
            {
                await _db.AddTaskAsync(task);
            }
        }

        private void FilterTasks()
        {
            if (_allTasks == null) return;

            var selectedId = SelectedTask?.Id;

            Tasks.Clear();

            if (StatusFilter == "Все")
            {
                foreach (var task in _allTasks)
                    Tasks.Add(task);
            }
            else
            {
                MyTaskStatus status;
                switch (StatusFilter)
                {
                    case "Ожидание": status = MyTaskStatus.Ожидание; break;
                    case "В работе": status = MyTaskStatus.ВРаботе; break;
                    default: status = MyTaskStatus.Выполнено; break;
                }

                foreach (var task in _allTasks)
                {
                    if (task.Status == status)
                        Tasks.Add(task);
                }
            }

            if (selectedId.HasValue)
            {
                SelectedTask = Tasks.FirstOrDefault(t => t.Id == selectedId);
            }
        }

        private async Task AddTaskAsync()
        {
            var window = new Views.TaskEditWindow();
            window.Owner = Application.Current.MainWindow;
            window.Title = "Создание задачи";

            if (window.ShowDialog() == true)
            {
                var newTask = new TaskModel
                {
                    Title = window.TaskTitle,
                    Description = window.TaskDescription,
                    Deadline = window.TaskDeadline,
                    Status = window.TaskStatus,
                    CreatedDate = DateTime.Now,
                    CategoryId = _categories.Count > 0 ? _categories[0].Id : 1,
                    UserId = _authService.CurrentUser.Id
                };

                await _db.AddTaskAsync(newTask);
                await LoadTasksAsync();

                SelectedTask = _allTasks.LastOrDefault();

                TaskAdded?.Invoke(newTask);

                LoadingMessage = $"✅ Добавлена задача: {newTask.Title}";
                await ClearLoadingMessageAfterDelay();

                _notificationService.SendNotification($"Новая задача: {newTask.Title}");
                await _pipeService.SendMessageAsync($"{_authService.CurrentUser.Username} добавил задачу: {newTask.Title}");
            }
        }

        private async Task EditTaskAsync()
        {
            if (SelectedTask == null) return;

            var window = new Views.TaskEditWindow(
                SelectedTask.Title,
                SelectedTask.Description,
                SelectedTask.Deadline,
                SelectedTask.Status
            );
            window.Owner = Application.Current.MainWindow;
            window.Title = "Редактирование задачи";

            if (window.ShowDialog() == true)
            {
                var oldTitle = SelectedTask.Title;
                SelectedTask.Title = window.TaskTitle;
                SelectedTask.Description = window.TaskDescription;
                SelectedTask.Deadline = window.TaskDeadline;
                SelectedTask.Status = window.TaskStatus;

                await _db.UpdateTaskAsync(SelectedTask);
                FilterTasks();

                LoadingMessage = $"✏️ Изменена задача: {oldTitle} -> {SelectedTask.Title}";
                await ClearLoadingMessageAfterDelay();

                _notificationService.SendNotification($"Задача изменена: {oldTitle} -> {SelectedTask.Title}");
                await _pipeService.SendMessageAsync($"{_authService.CurrentUser.Username} изменил задачу: {SelectedTask.Title}");
            }
        }

        private async Task DeleteTaskAsync()
        {
            if (SelectedTask == null) return;

            var result = MessageBox.Show($"Удалить задачу \"{SelectedTask.Title}\"?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var deletedTitle = SelectedTask.Title;
                await _db.DeleteTaskAsync(SelectedTask.Id);
                await LoadTasksAsync();

                LoadingMessage = $"❌ Удалена задача: {deletedTitle}";
                await ClearLoadingMessageAfterDelay();

                _notificationService.SendNotification($"Задача удалена: {deletedTitle}");
                await _pipeService.SendMessageAsync($"{_authService.CurrentUser.Username} удалил задачу: {deletedTitle}");
            }
        }

        private async Task ChangeStatusAsync()
        {
            if (SelectedTask == null) return;

            var task = SelectedTask;
            var taskTitle = task.Title;
            var oldStatus = task.StatusText;

            switch (task.Status)
            {
                case MyTaskStatus.Ожидание:
                    task.Status = MyTaskStatus.ВРаботе;
                    break;
                case MyTaskStatus.ВРаботе:
                    task.Status = MyTaskStatus.Выполнено;
                    break;
                case MyTaskStatus.Выполнено:
                    task.Status = MyTaskStatus.Ожидание;
                    break;
            }

            await _db.UpdateTaskAsync(task);

            var index = Tasks.IndexOf(task);
            if (index >= 0)
            {
                Tasks[index] = null;
                Tasks[index] = task;
            }

            LoadingMessage = $"🔄 Статус задачи \"{taskTitle}\" изменён: {oldStatus} -> {task.StatusText}";
            await ClearLoadingMessageAfterDelay();

            _notificationService?.SendNotification($"Статус задачи \"{taskTitle}\" изменён на {task.StatusText}");
        }

        public void SwapTasks(TaskModel task1, TaskModel task2)
        {
            if (task1 == null || task2 == null) return;

            int index1 = _allTasks.IndexOf(task1);
            int index2 = _allTasks.IndexOf(task2);

            if (index1 >= 0 && index2 >= 0)
            {
                _allTasks.Move(index1, index2);
                FilterTasks();

                LoadingMessage = $"🔄 Задача \"{task1.Title}\" перемещена";
                _ = ClearLoadingMessageAfterDelayAsync();
            }
        }

        private async Task ClearLoadingMessageAfterDelay()
        {
            await Task.Delay(3000);
            if (LoadingMessage != "Загрузка задач из базы данных..." && LoadingMessage != "Ошибка загрузки!")
            {
                LoadingMessage = "";
            }
        }

        private async Task ClearLoadingMessageAfterDelayAsync()
        {
            await Task.Delay(3000);
            if (LoadingMessage != "Загрузка задач из базы данных..." && LoadingMessage != "Ошибка загрузки!")
            {
                LoadingMessage = "";
            }
        }

        private void OnNotificationReceived(string message)
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                LoadingMessage = $"📢 {message}";
                await ClearLoadingMessageAfterDelay();
            });
        }

        private void OnChatMessageReceived(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                System.Diagnostics.Debug.WriteLine($"Чат: {message}");
            });
        }

        private void AddSampleTasks()
        {
            var sampleTask = new TaskModel
            {
                Title = "Изучить WPF MVVM",
                Description = "Прочитать документацию и сделать пример",
                Deadline = DateTime.Now.AddDays(2),
                CreatedDate = DateTime.Now,
                Status = MyTaskStatus.ВРаботе,
                CategoryId = 3,
                UserId = _authService.CurrentUser.Id
            };
            _allTasks.Add(sampleTask);

            var sampleTask2 = new TaskModel
            {
                Title = "Сделать проект",
                Description = "Написать код с асинхронностью",
                Deadline = DateTime.Now.AddDays(5),
                CreatedDate = DateTime.Now,
                Status = MyTaskStatus.Ожидание,
                CategoryId = 1,
                UserId = _authService.CurrentUser.Id
            };
            _allTasks.Add(sampleTask2);

            var sampleTask3 = new TaskModel
            {
                Title = "Сдать работу",
                Description = "Отправить преподавателю",
                Deadline = DateTime.Now.AddDays(3),
                CreatedDate = DateTime.Now,
                Status = MyTaskStatus.Выполнено,
                CategoryId = 2,
                UserId = _authService.CurrentUser.Id
            };
            _allTasks.Add(sampleTask3);
        }

        public void Dispose()
        {
            _notificationService?.Dispose();
            _pipeService?.Dispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}