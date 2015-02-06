using System.Collections.Generic;
using System.Windows;
using KanbanBoard.Model;
using KanbanBoard.ViewModel;

namespace KanbanBoard.View
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class ManipulatePostItView : Window
    {
        private EnumCategories _selectedCategory;

        // TODO: Create functionality to select an employee
        
        public ManipulatePostItView(EnumCategories selectedCategory, MainViewModel viewModel_MainView)
        {
            _selectedCategory = selectedCategory;
            InitializeComponent();
            ViewModel_Manipulate.MainViewModel = viewModel_MainView;
        }

        // TODO: Move the methods to the ManipulatePostItViewModel, and use commands to access them instead
        private void ComboBox_SelectedCategory_OnLoaded(object sender, RoutedEventArgs e)
        {
            List<string> categories = new List<string>();

            foreach (KeyValuePair<EnumCategories, CategoryViewModel> keyValuePair in ViewModel_Manipulate.MainViewModel.Categories)
            {
                categories.Add(keyValuePair.Key.ToString());
            }

            ComboBox_SelectedCategory.ItemsSource = categories;
            ComboBox_SelectedCategory.SelectedIndex = (int)_selectedCategory;
        }

        private void Button_CloseWindow(object sender, RoutedEventArgs e)
        {
            Window_ManipulatePostItView.Close();
        }
    }
}
