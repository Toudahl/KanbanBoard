using System.Collections.Generic;
using System.Windows;
using KanbanBoard.Enum;
using KanbanBoard.ViewModel;

namespace KanbanBoard.View
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class ManipulatePostItView : Window
    {
        private Categories _selectedCategory;

        public ManipulatePostItView(Categories selectedCategory, MainViewModel viewModel_MainView)
        {
            _selectedCategory = selectedCategory;
            InitializeComponent();
            ViewModel_Manipulate.MainViewModel = viewModel_MainView;
        }

        // TODO: Move the methods to the ManipulatePostItViewModel, and use commands to access them instead
        private void ComboBox_SelectedCategory_OnLoaded(object sender, RoutedEventArgs e)
        {
            List<string> categories = new List<string>();


            categories.Add(Categories.ToDo.ToString());
            categories.Add(Categories.WorkInProgress.ToString());
            categories.Add(Categories.CompletedWork.ToString());

            ComboBox_SelectedCategory.ItemsSource = categories;
            ComboBox_SelectedCategory.SelectedIndex = (int)_selectedCategory;
        }

        private void Button_CloseWindow(object sender, RoutedEventArgs e)
        {
            Window_ManipulatePostItView.Close();
        }
    }
}
