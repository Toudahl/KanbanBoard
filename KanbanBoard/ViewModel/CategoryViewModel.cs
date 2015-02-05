using System.Collections.ObjectModel;
using KanbanBoard.Model;
using KanbanBoard.Persistence;

namespace KanbanBoard.ViewModel
{
    public class CategoryViewModel
    {
        private EnumCategories _categoryName;
        private ObservableCollection<PostItModel> _postItsInCategory;

        public CategoryViewModel(EnumCategories categoryName)
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

        public EnumCategories CategoryName
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
