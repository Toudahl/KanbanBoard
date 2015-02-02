using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanBoard.Enum;
using KanbanBoard.Model;

namespace KanbanBoard.ViewModel
{
    public class CategoryViewModel
    {
        private Categories _categoryName;
        private ObservableCollection<PostItModel> _postItsInCategory;

        public CategoryViewModel(Categories categoryName)
        {
            this.CategoryName = categoryName;
            PostItsInCategory = new ObservableCollection<PostItModel>();
            
        }

        public ObservableCollection<PostItModel> PostItsInCategory
        {
            get
            {
                return _postItsInCategory;
            }
            set
            {
                _postItsInCategory = value;
            }
        }

        public Categories CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        public override string ToString()
        {
            return CategoryName.ToString();
        }
    }
}
