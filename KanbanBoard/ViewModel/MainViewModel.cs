using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using GongSolutions.Wpf.DragDrop;
using HelperClasses;
using KanbanBoard.Annotations;
using KanbanBoard.Model;
using KanbanBoard.Persistence;
using Microsoft.Win32;

namespace KanbanBoard.ViewModel
{
    
    public class MainViewModel: IDropTarget, INotifyPropertyChanged
    {
        // Contains the categories.
        //private List<CategoryViewModel> _toDoCategoryContainer;
        //private List<CategoryViewModel> _workInProgressCategoryContainer;
        //private List<CategoryViewModel> _completedWorkCategoryContainer;

        // Contains the actual postits in the Specific category
        private Dictionary<EnumCategories, CategoryViewModel> _categories;

        // Commands for various actions
        private ICommand _newCommand;
        private ICommand _saveAsDialogCommand;
        private ICommand _loadFromDialogCommand;
        private ICommand _saveCommand;

        // Used for saving and loading operations
        private SaveFileDialog _saveAsFileDialog;
        private OpenFileDialog _openFileDialog;
        private string _boardFileNameAndPath;
        private readonly string _compatibleFiles = PersistenceHandler.CompatibleFiles;
        private List<List<CategoryViewModel>> _boardContainer;

        public MainViewModel()
        {
            // Instantiate the categories in the board.
            _categories = new Dictionary<EnumCategories, CategoryViewModel>();
            _categories[EnumCategories.ToDo] = new CategoryViewModel(EnumCategories.ToDo);
            _categories[EnumCategories.WorkInProgress] = new CategoryViewModel(EnumCategories.WorkInProgress);
            _categories[EnumCategories.CompletedWork] = new CategoryViewModel(EnumCategories.CompletedWork);

            // Put the containers in the board container, used for persistence purposes.
            //_boardContainer = new List<List<CategoryViewModel>>();
            //_boardContainer.Add(_toDoCategoryContainer);
            //_boardContainer.Add(_workInProgressCategoryContainer);
            //_boardContainer.Add(_completedWorkCategoryContainer);

            // Reset board, and create "new" board.
            _newCommand = new RelayCommand(NewBoard);

            // Prepare the Save feature
            _saveCommand = new RelayCommand(SaveBoard);
            
            // Prepare command for Save As feature
            _saveAsDialogCommand = new RelayCommand(SaveAsDialog);
            _saveAsFileDialog = new SaveFileDialog();
            _saveAsFileDialog.AddExtension = true;
            _saveAsFileDialog.CheckPathExists = true;
            _saveAsFileDialog.Filter = _compatibleFiles;

            // Prepare command for Load feature
            _loadFromDialogCommand = new RelayCommand(LoadFromDialog);
            _openFileDialog = new OpenFileDialog();
            _openFileDialog.AddExtension = true;
            _openFileDialog.CheckPathExists = true;
            _openFileDialog.Filter = _compatibleFiles;

            _boardFileNameAndPath = null;

        }

        /// <summary>
        /// Clears the boards, and the file name and path to the save file.
        /// </summary>
        private void NewBoard()
        {
            foreach (KeyValuePair<EnumCategories, CategoryViewModel> category in Categories)
            {
                category.Value.PostItsInCategory.Clear();
            }
            _boardFileNameAndPath = null;
        }


        /// <summary>
        /// Saves the board to a file, using the allready set file name and path.
        /// If it has not been set, it will open the <see cref="SaveAsDialog()"/>
        /// </summary>
        private void SaveBoard()
        {
            if (_boardFileNameAndPath != null)
            {
                PersistenceHandler.Save(_categories, _boardFileNameAndPath);
            }
            else
            {
                SaveAsDialog();
            }
        }

        /// <summary>
        /// Opens the Save As dialog, and allow the user to pick the location and name for storing the Kanban Board file
        /// </summary>
        private void SaveAsDialog()
        {
            _saveAsFileDialog.ShowDialog();
            if (_saveAsFileDialog.FileName != "")
            {
                _boardFileNameAndPath = _saveAsFileDialog.FileName;
                PersistenceHandler.Save(_categories, _boardFileNameAndPath);
            }
        }


        /// <summary>
        /// Opens the Load dialog, and allows the user to pick the desired Kanban Board file
        /// </summary>
        private void LoadFromDialog()
        {
            _openFileDialog.ShowDialog();
            if (_openFileDialog.FileName != "")
            {
                _boardFileNameAndPath = _openFileDialog.FileName;
                Categories = PersistenceHandler.Load(_boardFileNameAndPath);
                //ListOfToDo = _boardContainer[0][0].PostItsInCategory;
                //ListOfWorkInProgress = _boardContainer[1][0].PostItsInCategory;
                //ListOfCompletedWork = _boardContainer[2][0].PostItsInCategory;
            }
        }

        ///// <summary>
        ///// Provides access to the List Of To do
        ///// </summary>
        //public ObservableCollection<PostItModel> ListOfToDo
        //{
        //    get { return _categories[EnumCategories.ToDo].PostItsInCategory; }
        //    set
        //    {
        //        _categories[EnumCategories.ToDo].PostItsInCategory = value;
        //        OnPropertyChanged();
        //    }
        //}

        ///// <summary>
        ///// Provides access to the Lisf Of Work In Progress.
        ///// </summary>
        //public ObservableCollection<PostItModel> ListOfWorkInProgress
        //{
        //    get { return _categories[EnumCategories.WorkInProgress].PostItsInCategory; }
        //    set
        //    {
        //        _categories[EnumCategories.WorkInProgress].PostItsInCategory = value;
        //        OnPropertyChanged();
        //    }
        //}

        ///// <summary>
        ///// Provides access to the list of Completed Work.
        ///// </summary>
        //public ObservableCollection<PostItModel> ListOfCompletedWork
        //{
        //    get { return _categories[EnumCategories.CompletedWork].PostItsInCategory; }
        //    set
        //    {
        //        _categories[EnumCategories.CompletedWork].PostItsInCategory = value;
        //        OnPropertyChanged();
        //    }
        //}

        /// <summary>
        /// The command used to initiate the board reset.
        /// </summary>
        public ICommand NewCommand
        {
            get { return _newCommand; }
            set { _newCommand = value; }
        }

        /// <summary>
        /// The command used to initiate the Save As dialog
        /// </summary>
        public ICommand SaveAsDialogCommand
        {
            get { return _saveAsDialogCommand; }
            set { _saveAsDialogCommand = value; }
        }

        /// <summary>
        /// The command used to initiate the Load from dialog
        /// </summary>
        public ICommand LoadFromDialogCommand
        {
            get { return _loadFromDialogCommand; }
            set { _loadFromDialogCommand = value; }
        }

        /// <summary>
        /// The command used to initaite saving
        /// </summary>
        public ICommand SaveCommand
        {
            get { return _saveCommand; }
            set { _saveCommand = value; }
        }

        public Dictionary<EnumCategories, CategoryViewModel> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value; 
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        ///  Updates the current drag state.
        ///  Remarks:
        ///  To allow a drop at the current drag position, the GongSolutions.Wpf.DragDrop.DropInfo.Effects
        ///  property on dropInfo should be set to a value other than System.Windows.DragDropEffects.None
        ///  and GongSolutions.Wpf.DragDrop.DropInfo.Data should be set to a non-null value.
        /// </summary>
        /// <param name="dragInfo">Information about the drag.</param>
        public void DragOver(IDropInfo dragInfo)
        {
            if (dragInfo.Data is PostItModel && dragInfo.TargetItem is KeyValuePair<EnumCategories, CategoryViewModel>)
            {
                dragInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dragInfo.Effects = DragDropEffects.Move;
            }
        }

        /// <summary>
        /// Performs a drop. And changes the colour of the posit it, according to the target.
        /// </summary>
        /// <param name="dropInfo">Information about the drop.</param>
        public void Drop(IDropInfo dropInfo)
        {
            KeyValuePair<EnumCategories, CategoryViewModel> keyValuePair = (KeyValuePair<EnumCategories, CategoryViewModel>)dropInfo.TargetItem;
            //CategoryViewModel postItcCollection = (CategoryViewModel)dropInfo.TargetItem;
            PostItModel postIt = (PostItModel)dropInfo.Data;
            keyValuePair.Value.PostItsInCategory.Add(postIt);

            //postItcCollection.PostItsInCategory.Add(postIt);

            if (keyValuePair.Value.CategoryName == EnumCategories.ToDo)
            {
                postIt.SolidColorBrush = new SolidColorBrush(Colors.Red);
            }

            if (keyValuePair.Value.CategoryName == EnumCategories.WorkInProgress)
            {
                postIt.SolidColorBrush = new SolidColorBrush(Colors.Yellow);
            }

            if (keyValuePair.Value.CategoryName == EnumCategories.CompletedWork)
            {
                postIt.SolidColorBrush = new SolidColorBrush(Colors.Green);
            }
            ((IList)dropInfo.DragInfo.SourceCollection).Remove(postIt);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}