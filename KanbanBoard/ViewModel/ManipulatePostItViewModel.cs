using System;
using System.Windows.Input;
using System.Windows.Media;
using HelperClasses;
using KanbanBoard.Model;

namespace KanbanBoard.ViewModel
{
    class ManipulatePostItViewModel
    {
        private MainViewModel _mainViewModel;
        private string _taskName;
        private string _taskDetails;
        private string _deadline;
        private EnumCategories _category;
        private ICommand _addCommand;
        private EmployeeModel _responsiblePerson;

        public ManipulatePostItViewModel()
        {
            _taskName = null;
            _taskDetails = null;

            _addCommand = new RelayCommand(Save);

            _responsiblePerson = new EmployeeModel("Morten", "Toudahl", EnumEmployeeTitles.LeadDeveloper);
            _deadline = DateTime.Today.ToString("HH:mm - dd MMMM, yyyy");

        }

        /// <summary>
        /// If all fields has input, create a <see cref="PostItModel"/> with the information
        /// from the fields.
        /// Add the PostItModel to the correct list.
        /// </summary>
        private void Save()
        {
            SolidColorBrush ColorBrush;

            switch (Category)
            {
                case EnumCategories.ToDo:
                    ColorBrush = new SolidColorBrush(Colors.Red);
                    break;
                case EnumCategories.WorkInProgress:
                    ColorBrush = new SolidColorBrush(Colors.Yellow);
                    break;
                case EnumCategories.CompletedWork:
                    ColorBrush = new SolidColorBrush(Colors.Green);
                    break;
                default:
                    ColorBrush = new SolidColorBrush();
                    break;
            }

            if (CheckInput())
            {
                MainViewModel.Categories[Category].PostItsInCategory.Add(new PostItModel(_taskName, _taskDetails, _deadline, _responsiblePerson, ColorBrush));
            }
        }


        private bool CheckInput()
        {
            if (TaskName == null)
            {
                return false;
            }
            if (TaskDetails == null)
            {
                return false;
            }
            if (Deadline == DateTime.MinValue.ToString())
            {
                return false;
            }
            return true;
        }


        public MainViewModel MainViewModel
        {
            get { return _mainViewModel; }
            set { _mainViewModel = value; }
        }

        public string TaskName
        {
            get { return _taskName; }
            set { _taskName = value; }
        }

        public string TaskDetails
        {
            get { return _taskDetails; }
            set { _taskDetails = value; }
        }

        public string Deadline
        {
            get { return _deadline; }
            set { _deadline = value; }
        }

        public EnumCategories Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }
    }
}
