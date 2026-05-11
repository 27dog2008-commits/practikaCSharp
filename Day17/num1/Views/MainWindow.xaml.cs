using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using num1.ViewModels;
using num1.Services;
using num1.Models;

namespace num1.Views
{
    public partial class MainWindow : Window
    {
        private TaskManagerViewModel _viewModel;
        private Point _dragStartPoint;
        private TaskModel _draggedTask;
        private ListBoxItem _draggedContainer;
        private bool _isDragging;

        public MainWindow(AuthService authService)
        {
            InitializeComponent();

            _viewModel = new TaskManagerViewModel(authService);
            this.DataContext = _viewModel;

            this.Closing += MainWindow_Closing;

            // Подписываемся на событие добавления задачи
            _viewModel.TaskAdded += OnTaskAdded;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _viewModel.TaskAdded -= OnTaskAdded;
            _viewModel?.Dispose();
        }

        // Анимация при добавлении задачи
        private void OnTaskAdded(TaskModel newTask)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                TaskListBox.UpdateLayout();

                var container = TaskListBox.ItemContainerGenerator.ContainerFromItem(newTask) as ListBoxItem;
                if (container != null)
                {
                    var border = FindVisualChild<Border>(container);
                    if (border != null)
                    {
                        border.RenderTransformOrigin = new Point(0.5, 0.5);
                        var scaleTransform = new ScaleTransform { ScaleX = 0.5, ScaleY = 0.5 };
                        border.RenderTransform = scaleTransform;
                        border.Opacity = 0;

                        var scaleAnimation = new DoubleAnimation
                        {
                            From = 0.5,
                            To = 1,
                            Duration = TimeSpan.FromSeconds(0.4),
                            EasingFunction = new ElasticEase { Oscillations = 1, Springiness = 3 }
                        };

                        var opacityAnimation = new DoubleAnimation
                        {
                            From = 0,
                            To = 1,
                            Duration = TimeSpan.FromSeconds(0.3)
                        };

                        scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
                        scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
                        border.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
                    }
                }
            }));
        }

        // Нажатие ЛКМ - увеличиваем задачу
        private void TaskListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
            _draggedTask = (e.OriginalSource as FrameworkElement)?.DataContext as TaskModel;

            if (_draggedTask != null)
            {
                _draggedContainer = TaskListBox.ItemContainerGenerator.ContainerFromItem(_draggedTask) as ListBoxItem;
                if (_draggedContainer != null)
                {
                    // Увеличиваем задачу при нажатии
                    var scaleTransform = new ScaleTransform();
                    _draggedContainer.RenderTransform = scaleTransform;
                    _draggedContainer.RenderTransformOrigin = new Point(0.5, 0.5);

                    var animation = new DoubleAnimation
                    {
                        From = 1,
                        To = 1.1,
                        Duration = TimeSpan.FromSeconds(0.1)
                    };
                    scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
                    scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
                }
            }
        }

        // Отпускание ЛКМ - возвращаем размер
        private void TaskListBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ResetDragSize();
            _isDragging = false;
        }

        // Движение мыши - начало перетаскивания
        private void TaskListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _draggedTask != null)
            {
                Point currentPos = e.GetPosition(null);
                Vector diff = _dragStartPoint - currentPos;

                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    _isDragging = true;
                    var data = new DataObject("TaskModel", _draggedTask);
                    DragDrop.DoDragDrop(sender as ListBox, data, DragDropEffects.Move);
                    _draggedTask = null;
                    ResetDragSize();
                }
            }
        }

        // Завершение перетаскивания
        private void TaskListBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("TaskModel"))
            {
                var draggedTask = e.Data.GetData("TaskModel") as TaskModel;
                var targetItem = (e.OriginalSource as FrameworkElement)?.DataContext as TaskModel;

                if (draggedTask != null && targetItem != null && draggedTask != targetItem)
                {
                    _viewModel.SwapTasks(draggedTask, targetItem);
                }
            }

            ResetDragSize();
            _draggedTask = null;
            _isDragging = false;
        }

        // Сброс размера задачи
        private void ResetDragSize()
        {
            if (_draggedContainer != null)
            {
                var scaleTransform = _draggedContainer.RenderTransform as ScaleTransform;
                if (scaleTransform != null)
                {
                    var animation = new DoubleAnimation
                    {
                        To = 1,
                        Duration = TimeSpan.FromSeconds(0.1)
                    };
                    scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
                    scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
                }
            }
            _draggedContainer = null;
        }

        // Поиск визуального элемента
        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T result)
                    return result;

                var descendant = FindVisualChild<T>(child);
                if (descendant != null)
                    return descendant;
            }
            return null;
        }
    }
}