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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KanbanBoard.Enum;
using KanbanBoard.View;
using KanbanBoard.ViewModel;

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

        private void Menu_ToDo_OnClick(object sender, RoutedEventArgs e)
        {
            Window AddWindow = new ManipulatePostItView(Categories.ToDo, viewModel_MainView);
            AddWindow.Show();
        }

        private void Menu_WorkInProgress_OnClick(object sender, RoutedEventArgs e)
        {
            Window AddWindow = new ManipulatePostItView(Categories.WorkInProgress, viewModel_MainView);
            AddWindow.Show();
        }

        private void Menu_CompletedWork_OnClick(object sender, RoutedEventArgs e)
        {
            Window AddWindow = new ManipulatePostItView(Categories.CompletedWork, viewModel_MainView);
            AddWindow.Show();
        }
    }
}
