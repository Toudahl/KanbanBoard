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
        void Save(object informationToSave, string fileName);
        object Load(string fileName);
    }
}
