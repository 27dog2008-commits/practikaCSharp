using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using num1.Models;
using num1.Services;
using num1.Commands;

namespace num1.ViewModels
{
    public class TaskManagerViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly TaskService _taskService;
        private readonly DataStorageService _dataStorage;
        private readonly AuthService _authService;
        private readonly MemoryMappedFileService _notificationService;
        private readonly NamedPipeService _pipeService;

        private ObservableCollection<TaskModel> _tasks;
        private ObservableCollection<TaskCategoryModel> _categories;
        private TaskModel _selectedTask;
        private string _statusFilter;
        private bool _isLoading;
        private string _loadingMessage;

        public event Action<TaskModel> TaskAdded;
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

        private ObservableCollection<TaskModel> _allTasks;

        public TaskManagerViewModel(AuthService authService)
        {
            _authService = authService;
            _taskService = new TaskService();
            _dataStorage = new DataStorageService();
            _notificationService = new MemoryMappedFileService();
            _pipeService = new NamedPipeService();

            _allTasks = new ObservableCollection<TaskModel>();
            Tasks = new ObservableCollection<TaskModel>();
            Categories = new ObservableCollection<TaskCategoryModel>();

            FilterOptions = new ObservableCollection<string> { "Все", "Ожидание", "В работе", "Выполнено" };
            StatusFilter = "Все";

            _notificationService.NotificationReceived += OnNotificationReceived;

            Task.Run(() => _pipeService.StartServerAsync());
            _pipeService.MessageReceived += OnChatMessageReceived;

            LoadTasksCommand = new RelayCommand(async o => await LoadTasksAsync());
            AddTaskCommand = new RelayCommand(async o => await AddTaskAsync());
            EditTaskCommand = new RelayCommand(async o => await EditTaskAsync(), o => SelectedTask != null);
            DeleteTaskCommand = new RelayCommand(async o => await DeleteTaskAsync(), o => SelectedTask != null);
            ChangeStatusCommand = new RelayCommand(o => ChangeStatus(), o => SelectedTask != null);

            LoadTasksCommand.Execute(null);
        }

        private async Task LoadTasksAsync()
        {
            IsLoading = true;
            LoadingMessage = "Загрузка задач...";

            try
            {
                var tasks = await _dataStorage.LoadTasksAsync(_authService.CurrentUser.Id);

                _allTasks.Clear();
                foreach (var task in tasks)
                {
                    _allTasks.Add(task);
                }

                if (_allTasks.Count == 0)
                {
                    AddSampleTasks();
                    await SaveTasksToStorage();
                }

                Categories = await _taskService.GetCategoriesAsync();
                FilterTasks();

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
                    Id = _allTasks.Count > 0 ? _allTasks.Max(t => t.Id) + 1 : 1,
                    Title = window.TaskTitle,
                    Description = window.TaskDescription,
                    Deadline = window.TaskDeadline,
                    Status = window.TaskStatus,
                    CreatedDate = DateTime.Now,
                    CategoryId = Categories.Count > 0 ? Categories[0].Id : 1
                };

                _allTasks.Add(newTask);
                await SaveTasksToStorage();
                FilterTasks();

                SelectedTask = newTask;

                // ВЫЗЫВАЕМ АНИМАЦИЮ
                TaskAdded?.Invoke(newTask);

                LoadingMessage = $"✅ Добавлена задача: {newTask.Title}";
                _ = ClearLoadingMessageAfterDelay();

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
                await SaveTasksToStorage();
                FilterTasks();

                LoadingMessage = $"✏️ Изменена задача: {oldTitle} -> {SelectedTask.Title}";
                _ = ClearLoadingMessageAfterDelay();

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
                _allTasks.Remove(SelectedTask);
                await SaveTasksToStorage();
                FilterTasks();

                LoadingMessage = $"❌ Удалена задача: {deletedTitle}";
                _ = ClearLoadingMessageAfterDelay();

                _notificationService.SendNotification($"Задача удалена: {deletedTitle}");
                await _pipeService.SendMessageAsync($"{_authService.CurrentUser.Username} удалил задачу: {deletedTitle}");
            }
        }

        private void ChangeStatus()
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

            _ = SaveTasksToStorage();

            var index = Tasks.IndexOf(task);
            if (index >= 0)
            {
                Tasks[index] = null;
                Tasks[index] = task;
            }

            LoadingMessage = $"🔄 Статус задачи \"{taskTitle}\" изменён: {oldStatus} -> {task.StatusText}";
            _ = ClearLoadingMessageAfterDelay();

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
                _ = SaveTasksToStorage();
                FilterTasks();

                LoadingMessage = $"🔄 Задача \"{task1.Title}\" перемещена";
                _ = ClearLoadingMessageAfterDelay();
            }
        }

        private async Task SaveTasksToStorage()
        {
            await _dataStorage.SaveTasksAsync(_authService.CurrentUser.Id, _allTasks.ToList());
        }

        private async Task ClearLoadingMessageAfterDelay()
        {
            await Task.Delay(3000);
            if (LoadingMessage != "Загрузка задач..." && LoadingMessage != "Ошибка загрузки!")
            {
                LoadingMessage = "";
            }
        }

        private void OnNotificationReceived(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                LoadingMessage = $"📢 {message}";
                _ = ClearLoadingMessageAfterDelay();
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
            _allTasks.Add(new TaskModel
            {
                Id = 1,
                Title = "Изучить WPF MVVM",
                Description = "Прочитать документацию и сделать пример",
                Deadline = DateTime.Now.AddDays(2),
                CreatedDate = DateTime.Now,
                Status = MyTaskStatus.ВРаботе,
                CategoryId = 3
            });

            _allTasks.Add(new TaskModel
            {
                Id = 2,
                Title = "Сделать проект",
                Description = "Написать код с асинхронностью",
                Deadline = DateTime.Now.AddDays(5),
                CreatedDate = DateTime.Now,
                Status = MyTaskStatus.Ожидание,
                CategoryId = 1
            });

            _allTasks.Add(new TaskModel
            {
                Id = 3,
                Title = "Сдать работу",
                Description = "Отправить преподавателю",
                Deadline = DateTime.Now.AddDays(3),
                CreatedDate = DateTime.Now,
                Status = MyTaskStatus.Выполнено,
                CategoryId = 2
            });
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