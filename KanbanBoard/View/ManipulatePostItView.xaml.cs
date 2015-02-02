using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
