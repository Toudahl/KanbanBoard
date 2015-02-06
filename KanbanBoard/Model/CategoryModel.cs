using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoard.Model
{
    class CategoryModel
    {
        // TODO: Move category details here, and refactor the CategoryViewModel
        

        private List<PostItModel> _postItsInCategory;
        private EnumCategories _category;
        private string _categoryTitle;
        private string _categoryDetails;

        public CategoryModel(EnumCategories category, string categoryTitle, string categoryDetails)
        {
            _category = category;
            _categoryTitle = categoryTitle;
            _categoryDetails = categoryDetails;
            _postItsInCategory = new List<PostItModel>();
        }


        public List<PostItModel> PostItsInCategory
        {
            get { return _postItsInCategory; }
            set { _postItsInCategory = value; }
        }

        public EnumCategories Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public string CategoryTitle
        {
            get { return _categoryTitle; }
            set { _categoryTitle = value; }
        }

        public string CategoryDetails
        {
            get { return _categoryDetails; }
            set { _categoryDetails = value; }
        }
    }
}
