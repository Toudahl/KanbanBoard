using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using GongSolutions.Wpf.DragDrop;
using HelperClasses;
using KanbanBoard.Annotations;
using KanbanBoard.Enum;
using KanbanBoard.Model;
using KanbanBoard.Persistence;
using Microsoft.Win32;

namespace KanbanBoard.ViewModel
{
    public class MainViewModel: IDropTarget, INotifyPropertyChanged
    {
        private List<CategoryViewModel> toDoCategory;
        private List<CategoryViewModel> workInProgressCategory;
        private List<CategoryViewModel> completedWorkCategory;
        private CategoryViewModel _toDoList;
        private CategoryViewModel _workInProgressList;
        private CategoryViewModel _completedWorkList;
        private ICommand _saveAsDialogCommand;
        private SaveFileDialog saveAsFileDialog;
        private OpenFileDialog openFileDialog;
        private string FileName;
        private string FilePath;
        private ICommand _loadFromDialogCommand;
        private readonly string compatibleFiles = "KanBan Board file (*.kbb)|*.kbb|JSON file (*.json)|*.json|All type (*.*)|*.*";
        private List<List<CategoryViewModel>> EntireBoard;

        public MainViewModel()
        {
            _toDoList = new CategoryViewModel(Categories.ToDo);
            _workInProgressList = new CategoryViewModel(Categories.WorkInProgress);
            _completedWorkList = new CategoryViewModel(Categories.CompletedWork);
            toDoCategory = new List<CategoryViewModel>();
            workInProgressCategory = new List<CategoryViewModel>();
            completedWorkCategory = new List<CategoryViewModel>();



            toDoCategory.Add(_toDoList);
            workInProgressCategory.Add(_workInProgressList);
            completedWorkCategory.Add(_completedWorkList);

            EntireBoard = new List<List<CategoryViewModel>>();
            EntireBoard.Add(toDoCategory);
            EntireBoard.Add(workInProgressCategory);
            EntireBoard.Add(completedWorkCategory);


            //ToDoCategory = CollectionViewSource.GetDefaultView(toDoCategory);
            WorkInProgressCategory = CollectionViewSource.GetDefaultView(workInProgressCategory);
            CompletedCategory = CollectionViewSource.GetDefaultView(completedWorkCategory);

            _saveAsDialogCommand = new RelayCommand(SaveAsDialog);

            saveAsFileDialog = new SaveFileDialog();
            saveAsFileDialog.AddExtension = true;
            saveAsFileDialog.CheckPathExists = true;
            saveAsFileDialog.Filter = compatibleFiles;
            this.FileName = null;


            _loadFromDialogCommand = new RelayCommand(LoadFromDialog);
            openFileDialog = new OpenFileDialog();
            openFileDialog.AddExtension = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter = compatibleFiles;
        }

        private void LoadFromDialog()
        {
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                FileName = openFileDialog.FileName;
                EntireBoard = PersistenceHandler.Load(FileName);
                toDoCategory = EntireBoard[0];
                workInProgressCategory = EntireBoard[1];
                completedWorkCategory = EntireBoard[2];
            }
        }

        private void SaveAsDialog()
        {
            saveAsFileDialog.ShowDialog();
            if (saveAsFileDialog.FileName != "")
            {
                FileName = saveAsFileDialog.FileName;
                PersistenceHandler.Save(EntireBoard, FileName);
            }
        }

        public ObservableCollection<PostItModel> ListOfToDo
        {
            get { return _toDoList.PostItsInCategory; }
            set { _toDoList.PostItsInCategory = value; }
        }

        public ObservableCollection<PostItModel> ListOfWorkInProgress
        {
            get { return _workInProgressList.PostItsInCategory; }
            set { _workInProgressList.PostItsInCategory = value; }
        }

        public ObservableCollection<PostItModel> ListOfCompletedWork
        {
            get { return _completedWorkList.PostItsInCategory; }
            set { _completedWorkList.PostItsInCategory = value; }
        }

        public ICommand SaveAsDialogCommand
        {
            get { return _saveAsDialogCommand; }
            set { _saveAsDialogCommand = value; }
        }

        public ICommand LoadFromDialogCommand
        {
            get { return _loadFromDialogCommand; }
            set { _loadFromDialogCommand = value; }
        }

        public ICollectionView ToDoCategory { get; set; }

        public ICollectionView WorkInProgressCategory { get; set; }
        public ICollectionView CompletedCategory { get; set; }


        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is PostItModel && dropInfo.TargetItem is CategoryViewModel)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            CategoryViewModel postItcCollection = (CategoryViewModel)dropInfo.TargetItem;
            PostItModel postIt = (PostItModel)dropInfo.Data;
            postItcCollection.PostItsInCategory.Add(postIt);

            if (postItcCollection.CategoryName == Categories.ToDo)
            {
                postIt.SolidColorBrush = new SolidColorBrush(Colors.Red);
            }

            if (postItcCollection.CategoryName == Categories.WorkInProgress)
            {
                postIt.SolidColorBrush = new SolidColorBrush(Colors.Yellow);
            }

            if (postItcCollection.CategoryName == Categories.CompletedWork)
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
