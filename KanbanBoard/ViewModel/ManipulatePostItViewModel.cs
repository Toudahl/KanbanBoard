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
        private PersonModel _responsiblePerson;

        public ManipulatePostItViewModel()
        {
            _taskName = null;
            _taskDetails = null;

            _addCommand = new RelayCommand(Save);

            _responsiblePerson = new PersonModel("Morten", "Toudahl");
            _deadline = DateTime.Today.ToString("HH:mm - dd MMMM, yyyy");

        }

        /// <summary>
        /// If all fields has input, create a <see cref="PostItModel"/> with the information
        /// from the fields.
        /// Add the PostItModel to the correct list.
        /// </summary>
        private void Save()
        {
            SolidColorBrush redSolidColorBrush = new SolidColorBrush(Colors.Red);
            SolidColorBrush yellowSolidColorBrush = new SolidColorBrush(Colors.Yellow);
            SolidColorBrush greenSolidColorBrush = new SolidColorBrush(Colors.Green);

            if (CheckInput())
            {
                if (Category == EnumCategories.ToDo)
                {
                    MainViewModel.ListOfToDo.Add(new PostItModel(_taskName, _taskDetails, _deadline, _responsiblePerson, redSolidColorBrush));
                }
                if (Category == EnumCategories.WorkInProgress)
                {
                    MainViewModel.ListOfWorkInProgress.Add(new PostItModel(_taskName, _taskDetails, _deadline, _responsiblePerson, yellowSolidColorBrush));
                }
                if (Category == EnumCategories.CompletedWork)
                {
                    MainViewModel.ListOfCompletedWork.Add(new PostItModel(_taskName, _taskDetails, _deadline, _responsiblePerson, greenSolidColorBrush));
                }
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
