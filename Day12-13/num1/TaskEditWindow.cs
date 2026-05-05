using System.Windows;

namespace num1
{
    public partial class TaskEditWindow : Window
    {
        private TaskEditModel _editModel;

        public TaskEditWindow(TaskEditModel editModel)
        {
            InitializeComponent();
            _editModel = editModel;

            TitleTextBox.Text = editModel.Title;
            DescriptionTextBox.Text = editModel.Description;

            Title = editModel.IsEditMode ? "Редактирование задачи" : "Создание новой задачи";

            TitleTextBox.TextChanged += (s, e) => ValidateInput();

            ValidateInput();
        }

        private void ValidateInput()
        {
            bool isValid = !string.IsNullOrWhiteSpace(TitleTextBox.Text);

            ValidationMessage.Visibility = isValid ? Visibility.Collapsed : Visibility.Visible;

            SaveButton.IsEnabled = isValid;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                ValidateInput();
                return;
            }

            _editModel.Title = TitleTextBox.Text.Trim();
            _editModel.Description = DescriptionTextBox.Text.Trim();

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}