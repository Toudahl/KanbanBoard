using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanBoard.ViewModel;

namespace KanbanBoard.Persistence
{
    interface IPersistence
    {
        void Save(List<List<CategoryViewModel>> informationToSave, string fileName);
        List<List<CategoryViewModel>> Load(string fileName);
    }
}
