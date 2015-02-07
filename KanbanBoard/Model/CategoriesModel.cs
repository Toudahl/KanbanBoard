using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoard.Model
{
    class CategoriesModel
    {
        private Dictionary<string, string> _availableCategories;

        public CategoriesModel(Dictionary<string, string> availableCategories)
        {
            _availableCategories = availableCategories;
        }

        /// <summary>
        /// key = category name, without spaces.
        /// value = category name, with spaces.
        /// </summary>
        public Dictionary<string, string> AvailableCategories
        {
            get { return _availableCategories; }
            set { _availableCategories = value; }
        }
    }
}
