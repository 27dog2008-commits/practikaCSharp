using System.Windows;
using num1.ViewModels;
using num1.Services;

namespace num1.Views
{
    public partial class MainWindow : Window
    {
        private TaskManagerViewModel _viewModel;

        public MainWindow(AuthService authService)
        {
            InitializeComponent();

            _viewModel = new TaskManagerViewModel(authService);
            this.DataContext = _viewModel; 

            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _viewModel?.Dispose();
        }
    }
}