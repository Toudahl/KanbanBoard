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
using KanbanBoard.View;
using Microsoft.Win32;

namespace KanbanBoard.ViewModel
{
    /// <summary>
    /// This class handles the Main window, and holds all the information regarding the board(s)
    /// </summary>
    public class MainViewModel: IDropTarget, INotifyPropertyChanged
    {
        #region Fields
        // The categories
        private Dictionary<EnumCategories, CategoryViewModel> _board;

        // Commands for various actions
        private ICommand _newCommand;
        private ICommand _saveAsDialogCommand;
        private ICommand _loadFromDialogCommand;
        private ICommand _saveCommand;
        private ICommand _addOrEditCommand;

        // Used for saving, loading, adding and editing operations
        private SaveFileDialog _saveAsFileDialog;
        private OpenFileDialog _openFileDialog;
        private string _boardFileNameAndPath;
        private readonly string _compatibleFiles = PersistenceHandler.CompatibleFiles;
        private ManipulatePostItView addOrEditWindow;

        #endregion

        public MainViewModel()
        {
            #region Create the board, and add categories to it.
            // Instantiate the categories in the board.
            Board = new Dictionary<EnumCategories, CategoryViewModel>();
            Board[EnumCategories.ToDo] = new CategoryViewModel(EnumCategories.ToDo);
            Board[EnumCategories.WorkInProgress] = new CategoryViewModel(EnumCategories.WorkInProgress);
            Board[EnumCategories.CompletedWork] = new CategoryViewModel(EnumCategories.CompletedWork);
            #endregion


            #region Instantiate the commands
            _saveAsDialogCommand = new RelayCommand(SaveAsDialog);
            _saveCommand = new RelayCommand(SaveBoard);
            _newCommand = new RelayCommand(NewBoard);
            _loadFromDialogCommand = new RelayCommand(LoadFromDialog);
            _addOrEditCommand = new RelayCommand(AddOrEdit);
            #endregion

            #region Prepare the dialogs
            // Prepare dialog for Save As feature
            _saveAsFileDialog = new SaveFileDialog();
            _saveAsFileDialog.AddExtension = true;
            _saveAsFileDialog.CheckPathExists = true;
            _saveAsFileDialog.Filter = _compatibleFiles;

            // Prepare dialog for Load feature
            _openFileDialog = new OpenFileDialog();
            _openFileDialog.AddExtension = true;
            _openFileDialog.CheckPathExists = true;
            _openFileDialog.Filter = _compatibleFiles;
            #endregion
        }

        #region Properties
        /// <summary>
        /// Use this property to access the categories of the board
        /// </summary>
        public Dictionary<EnumCategories, CategoryViewModel> Board
        {
            get { return _board; }
            set
            {
                _board = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods for commands
        /// <summary>
        /// Clears the boards, and the file name and path to the save file.
        /// </summary>
        private void NewBoard()
        {
            foreach (KeyValuePair<EnumCategories, CategoryViewModel> category in Board)
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
                PersistenceHandler.Save(Board, _boardFileNameAndPath);
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
                PersistenceHandler.Save(Board, _boardFileNameAndPath);
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
                Board = (Dictionary<EnumCategories, CategoryViewModel>) PersistenceHandler.Load(_boardFileNameAndPath);
            }
        }

        private void AddOrEdit()
        {
            addOrEditWindow = new ManipulatePostItView(this);
            addOrEditWindow.Show();
        }

        #endregion

        #region Commands
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

        /// <summary>
        /// Used for opening the add / edit window
        /// </summary>
        public ICommand AddOrEditCommand
        {
            get { return _addOrEditCommand; }
            set { _addOrEditCommand = value; }
        }
        #endregion

        #region Gong drag and drop
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
            PostItModel postIt = (PostItModel)dropInfo.Data;
            keyValuePair.Value.PostItsInCategory.Add(postIt);


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
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}