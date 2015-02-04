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
        private List<CategoryViewModel> _toDoCategoryContainer;
        private List<CategoryViewModel> _workInProgressCategoryContainer;
        private List<CategoryViewModel> _completedWorkCategoryContainer;

        // Contains the actual postits in the Specific category
        private CategoryViewModel _toDoCategory;
        private CategoryViewModel _workInProgressCategory;
        private CategoryViewModel _completedWorkCategory;

        // Commands for various actions
        private ICommand _newCommand;
        private ICommand _saveAsDialogCommand;
        private ICommand _loadFromDialogCommand;
        private ICommand _saveCommand;

        // Used for saving and loading operations
        private SaveFileDialog _saveAsFileDialog;
        private OpenFileDialog _openFileDialog;
        private string _boardFileNameAndPath;
        private const string COMPATIBLE_FILES = "KanBan Board file (*.kbb)|*.kbb";
        private List<List<CategoryViewModel>> _boardContainer;

        public MainViewModel()
        {
            // Instantiate the categories in the board.
            _toDoCategory = new CategoryViewModel(EnumCategories.ToDo);
            _workInProgressCategory = new CategoryViewModel(EnumCategories.WorkInProgress);
            _completedWorkCategory = new CategoryViewModel(EnumCategories.CompletedWork);
            // And the containers for them.
            _toDoCategoryContainer = new List<CategoryViewModel>();
            _workInProgressCategoryContainer = new List<CategoryViewModel>();
            _completedWorkCategoryContainer = new List<CategoryViewModel>();


            // Put the categories in the containers.
            _toDoCategoryContainer.Add(_toDoCategory);
            _workInProgressCategoryContainer.Add(_workInProgressCategory);
            _completedWorkCategoryContainer.Add(_completedWorkCategory);

            // Put the containers in the board container, used for persistence purposes.
            _boardContainer = new List<List<CategoryViewModel>>();
            _boardContainer.Add(_toDoCategoryContainer);
            _boardContainer.Add(_workInProgressCategoryContainer);
            _boardContainer.Add(_completedWorkCategoryContainer);

            // Treat the following properties as the same type as the argument
            ToDoCategory = CollectionViewSource.GetDefaultView(_toDoCategoryContainer);
            WorkInProgressCategory = CollectionViewSource.GetDefaultView(_workInProgressCategoryContainer);
            CompletedCategory = CollectionViewSource.GetDefaultView(_completedWorkCategoryContainer);

            // Reset board, and create "new" board.
            _newCommand = new RelayCommand(NewBoard);

            // Prepare the Save feature
            _saveCommand = new RelayCommand(SaveBoard);
            
            // Prepare command for Save As feature
            _saveAsDialogCommand = new RelayCommand(SaveAsDialog);
            _saveAsFileDialog = new SaveFileDialog();
            _saveAsFileDialog.AddExtension = true;
            _saveAsFileDialog.CheckPathExists = true;
            _saveAsFileDialog.Filter = COMPATIBLE_FILES;

            // Prepare command for Load feature
            _loadFromDialogCommand = new RelayCommand(LoadFromDialog);
            _openFileDialog = new OpenFileDialog();
            _openFileDialog.AddExtension = true;
            _openFileDialog.CheckPathExists = true;
            _openFileDialog.Filter = COMPATIBLE_FILES;

            _boardFileNameAndPath = null;

        }

        /// <summary>
        /// Clears the boards, and the file name and path to the save file.
        /// </summary>
        private void NewBoard()
        {
            ListOfToDo.Clear();
            ListOfWorkInProgress.Clear();
            ListOfCompletedWork.Clear();
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
                PersistenceHandler.Save(_boardContainer, _boardFileNameAndPath);
            }
            else
            {
                SaveAsDialog();
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
                _boardContainer = PersistenceHandler.Load(_boardFileNameAndPath);
                ListOfToDo = _boardContainer[0][0].PostItsInCategory;
                ListOfWorkInProgress = _boardContainer[1][0].PostItsInCategory;
                ListOfCompletedWork = _boardContainer[2][0].PostItsInCategory;
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
                PersistenceHandler.Save(_boardContainer, _boardFileNameAndPath);
            }
        }

        /// <summary>
        /// Provides access to the List Of To do
        /// </summary>
        public ObservableCollection<PostItModel> ListOfToDo
        {
            get { return _toDoCategory.PostItsInCategory; }
            set
            {
                _toDoCategory.PostItsInCategory = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Provides access to the Lisf Of Work In Progress.
        /// </summary>
        public ObservableCollection<PostItModel> ListOfWorkInProgress
        {
            get { return _workInProgressCategory.PostItsInCategory; }
            set
            {
                _workInProgressCategory.PostItsInCategory = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Provides access to the list of Completed Work.
        /// </summary>
        public ObservableCollection<PostItModel> ListOfCompletedWork
        {
            get { return _completedWorkCategory.PostItsInCategory; }
            set
            {
                _completedWorkCategory.PostItsInCategory = value;
                OnPropertyChanged();
            }
        }

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

        public ICommand SaveCommand
        {
            get { return _saveCommand; }
            set { _saveCommand = value; }
        }

        /// <summary>
        /// Provides access to the category container - used as drop target for the postits
        /// </summary>
        public ICollectionView ToDoCategory { get; set; }

        /// <summary>
        /// Provides access to the category container - used as drop target for the postits
        /// </summary>
        public ICollectionView WorkInProgressCategory { get; set; }

        /// <summary>
        /// Provides access to the category container - used as drop target for the postits
        /// </summary>
        public ICollectionView CompletedCategory { get; set; }
        
        /// <summary>
        //  Updates the current drag state.
        // Remarks:
        //     To allow a drop at the current drag position, the GongSolutions.Wpf.DragDrop.DropInfo.Effects
        //     property on dropInfo should be set to a value other than System.Windows.DragDropEffects.None
        //     and GongSolutions.Wpf.DragDrop.DropInfo.Data should be set to a non-null
        //     value.
        /// </summary>
        /// <param name="dropInfo">Information about the drag.</param>
        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is PostItModel && dropInfo.TargetItem is CategoryViewModel)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        /// <summary>
        /// Performs a drop. And changes the colour of the posit it, according to the target.
        /// </summary>
        /// <param name="dropInfo">Information about the drop.</param>
        public void Drop(IDropInfo dropInfo)
        {
            CategoryViewModel postItcCollection = (CategoryViewModel)dropInfo.TargetItem;
            PostItModel postIt = (PostItModel)dropInfo.Data;
            postItcCollection.PostItsInCategory.Add(postIt);

            if (postItcCollection.CategoryName == EnumCategories.ToDo)
            {
                postIt.SolidColorBrush = new SolidColorBrush(Colors.Red);
            }

            if (postItcCollection.CategoryName == EnumCategories.WorkInProgress)
            {
                postIt.SolidColorBrush = new SolidColorBrush(Colors.Yellow);
            }

            if (postItcCollection.CategoryName == EnumCategories.CompletedWork)
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
