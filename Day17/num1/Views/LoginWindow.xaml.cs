using System.Windows;
using num1.ViewModels;
using num1.Services;

namespace num1.Views
{
    public partial class LoginWindow : Window
    {
        private LoginViewModel _viewModel;
        private AuthService _authService;

        public LoginWindow()
        {
            InitializeComponent();

            _authService = new AuthService();
            _viewModel = new LoginViewModel(_authService);
            this.DataContext = _viewModel;  

            _viewModel.LoginSuccess += OnLoginSuccess;
            _viewModel.SetPasswordBox(PasswordBox);
        }

        private void OnLoginSuccess()
        {
            var mainWindow = new MainWindow(_authService);
            mainWindow.Show();
            this.Close();
        }
    }
}