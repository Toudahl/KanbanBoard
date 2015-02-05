using System.Windows;
using KanbanBoard.Model;
using KanbanBoard.View;

namespace KanbanBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // TODO: Move the "AddWindow" methods to MainViewModel, and use commands to access them.
        private void Menu_ToDo_OnClick(object sender, RoutedEventArgs e)
        {
            var addWindow = new ManipulatePostItView(EnumCategories.ToDo, viewModel_MainView);
            addWindow.Show();
        }
        
        private void Menu_WorkInProgress_OnClick(object sender, RoutedEventArgs e)
        {
            var addWindow = new ManipulatePostItView(EnumCategories.WorkInProgress, viewModel_MainView);
            addWindow.Show();
        }

        private void Menu_CompletedWork_OnClick(object sender, RoutedEventArgs e)
        {
            var addWindow = new ManipulatePostItView(EnumCategories.CompletedWork, viewModel_MainView);
            addWindow.Show();
        }
    }
}
