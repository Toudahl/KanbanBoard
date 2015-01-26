using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GongSolutions.Wpf.DragDrop;
using KanbanBoard.Model;

namespace KanbanBoard.ViewModel
{
    class MainViewModel: IDropTarget
    {
        private ObservableCollection<ObservableCollection<PostIt>> _categories;

        public MainViewModel()
        {
            Categories = new ObservableCollection<ObservableCollection<PostIt>>
            {
                new ObservableCollection<PostIt>
                {
                    new PostIt
                    {
                        Name = "To Do",
                    }
                },
                new ObservableCollection<PostIt>
                {
                    new PostIt
                    {
                        Name = "Work in progress",
                    }
                },
                new ObservableCollection<PostIt>
                {
                    new PostIt
                    {
                        Name = "Completed",
                    }
                }
            };
        }

        public ObservableCollection<ObservableCollection<PostIt>> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is PostIt && dropInfo.TargetItem is ObservableCollection<ObservableCollection<PostIt>>)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            ObservableCollection<ObservableCollection<PostIt>> postItcCollection = (ObservableCollection<ObservableCollection<PostIt>>)dropInfo.TargetItem;
            int index = dropInfo.InsertIndex;
            PostIt postIt = (PostIt)dropInfo.Data;
            postItcCollection[index].Add(postIt);
            var sourceCollection = (ObservableCollection<ObservableCollection<PostIt>>)dropInfo.DragInfo.SourceCollection;
            sourceCollection[index].Remove(postIt);
            //((ObservableCollection<PostIt>)dropInfo.DragInfo.SourceCollection).Remove(postIt);
        }
    }
}
