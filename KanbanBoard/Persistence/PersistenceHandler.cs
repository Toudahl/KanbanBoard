using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanBoard.ViewModel;

namespace KanbanBoard.Persistence
{
    static class PersistenceHandler
    {
        static public void Save(List<List<CategoryViewModel>> viewModel_Main, string fileName)
        {
            JsonPersistence.Save(viewModel_Main, fileName);
        }
        static public void Save(List<List<CategoryViewModel>> viewModel_Main, string fileName, EnumPersistenceOptions persistenceOptions)
        {
            if (persistenceOptions == EnumPersistenceOptions.JSON)
            {
                JsonPersistence.Save(viewModel_Main, fileName);
            }
        }

        static public List<List<CategoryViewModel>> Load(string fileName)
        {
            return JsonPersistence.Load(fileName);
        }
        static public List<List<CategoryViewModel>> Load(string fileName, EnumPersistenceOptions persistenceOptions)
        {
            if (persistenceOptions == EnumPersistenceOptions.JSON)
            {
               return JsonPersistence.Load(fileName);
            }
            return null;
        }
    }
}
