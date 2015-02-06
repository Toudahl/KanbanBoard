using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanBoard.Model;
using KanbanBoard.ViewModel;

namespace KanbanBoard.Persistence
{
    interface IPersistence
    {
        void Save(Dictionary<EnumCategories, CategoryViewModel> informationToSave, string fileName);
        Dictionary<EnumCategories, CategoryViewModel> Load(string fileName);
    }
}
