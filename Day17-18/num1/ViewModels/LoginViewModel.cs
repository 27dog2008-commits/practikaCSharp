using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using num1.Services;
using num1.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace num1.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;
        private string _username;
        private bool _isLoading;
        private string _loadingMessage;
        private PasswordBox _passwordBox;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
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

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public event Action LoginSuccess;

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
            InitializeAsync();

            LoginCommand = new RelayCommand(o => LoginAsync(), o => !IsLoading);
            RegisterCommand = new RelayCommand(o => RegisterAsync(), o => !IsLoading);
        }

        public void SetPasswordBox(PasswordBox passwordBox)
        {
            _passwordBox = passwordBox;
        }

        private async void InitializeAsync()
        {
            await _authService.InitializeAsync();
        }

        private async void LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                MessageBox.Show("Введите имя пользователя!", "Ошибка");
                return;
            }

            if (_passwordBox == null || string.IsNullOrWhiteSpace(_passwordBox.Password))
            {
                MessageBox.Show("Введите пароль!", "Ошибка");
                return;
            }

            IsLoading = true;
            LoadingMessage = "Вход...";

            var success = await _authService.LoginAsync(Username, _passwordBox.Password);

            IsLoading = false;
            LoadingMessage = "";

            if (success)
            {
                LoginSuccess?.Invoke();
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль!", "Ошибка");
            }
        }

        private async void RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                MessageBox.Show("Введите имя пользователя!", "Ошибка");
                return;
            }

            if (_passwordBox == null || string.IsNullOrWhiteSpace(_passwordBox.Password))
            {
                MessageBox.Show("Введите пароль!", "Ошибка");
                return;
            }

            if (_passwordBox.Password.Length < 4)
            {
                MessageBox.Show("Пароль должен быть не менее 4 символов!", "Ошибка");
                return;
            }

            IsLoading = true;
            LoadingMessage = "Регистрация...";

            var success = await _authService.RegisterAsync(Username, _passwordBox.Password);

            IsLoading = false;
            LoadingMessage = "";

            if (success)
            {
                MessageBox.Show("Регистрация успешна! Теперь войдите.", "Успех");
            }
            else
            {
                MessageBox.Show("Пользователь с таким именем уже существует!", "Ошибка");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}