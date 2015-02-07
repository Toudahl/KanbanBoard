using System;
using System.Collections.Generic;
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
        private EnumCategories _selectedCategory;
        private ICommand _saveCommand;
        private EmployeeModel _responsiblePerson;

        public ManipulatePostItViewModel()
        {
            _taskName = null;
            _taskDetails = null;

            _saveCommand = new RelayCommand(Save);

            _responsiblePerson = new EmployeeModel("Morten", "Toudahl", EnumEmployeeTitles.LeadDeveloper);
            _deadline = DateTime.Today.ToString("HH:mm - dd MMMM, yyyy");
        }

        #region Methods
        /// <summary>
        /// If all fields has input, create a <see cref="PostItModel"/> with the information
        /// from the fields.
        /// Add the PostItModel to the correct list.
        /// </summary>
        private void Save()
        {
            SolidColorBrush colorBrush;

            switch (SelectedCategory)
            {
                case EnumCategories.ToDo:
                    colorBrush = new SolidColorBrush(Colors.Red);
                    break;
                case EnumCategories.WorkInProgress:
                    colorBrush = new SolidColorBrush(Colors.Yellow);
                    break;
                case EnumCategories.CompletedWork:
                    colorBrush = new SolidColorBrush(Colors.Green);
                    break;
                default:
                    colorBrush = new SolidColorBrush();
                    break;
            }

            if (CheckInput())
            {
                MainViewModel.Board[SelectedCategory].PostItsInCategory.Add(new PostItModel(_taskName, _taskDetails, _deadline, _responsiblePerson, colorBrush));
            }
        }

        /// <summary>
        /// Check if TaskName and TaskDetails has been set.
        /// </summary>
        /// <returns>Returns true if both have been set.</returns>
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
            return true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Provides access to the properties in the <see cref="MainViewModel"/>, which contains the board
        /// </summary>
        public MainViewModel MainViewModel
        {
            get { return _mainViewModel; }
            set { _mainViewModel = value; }
        }

        /// <summary>
        /// Get and set the TaskName
        /// </summary>
        public string TaskName
        {
            get { return _taskName; }
            set { _taskName = value; }
        }

        /// <summary>
        /// Get and set the TaskDetails
        /// </summary>
        public string TaskDetails
        {
            get { return _taskDetails; }
            set { _taskDetails = value; }
        }

        /// <summary>
        /// Get and set the Deadline
        /// </summary>
        public string Deadline
        {
            get { return _deadline; }
            set { _deadline = value; }
        }

        /// <summary>
        /// Get and set the SelectedCategory
        /// </summary>
        public EnumCategories SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; }
        }

        /// <summary>
        /// Get the entries from <see cref="EnumCategories"/>, used to populate the combobox
        /// </summary>
        public string[] AvailableCategories
        {
            get { return Enum.GetNames(typeof(EnumCategories)); }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Will save the content of the window
        /// </summary>
        public ICommand SaveCommand
        {
            get { return _saveCommand; }
            set { _saveCommand = value; }
        }
        #endregion
    }
}
