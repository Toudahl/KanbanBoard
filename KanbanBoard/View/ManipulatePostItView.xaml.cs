using System;
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

        // TODO: Create functionality to select an employee
        
        public ManipulatePostItView(MainViewModel viewModel_MainView)
        {
            InitializeComponent();
            ViewModel_Manipulate.MainViewModel = viewModel_MainView;
        }

        private void Button_CloseWindow(object sender, RoutedEventArgs e)
        {
            Window_ManipulatePostItView.Close();
        }
    }
}
