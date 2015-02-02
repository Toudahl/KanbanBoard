using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanBoard.ViewModel;
using Newtonsoft.Json;

namespace KanbanBoard.Persistence
{
    static class JsonPersistence
    {
        static public void Save(List<List<CategoryViewModel>> viewModelMain, string filename)
        {
            string jsonViewModel = JsonConvert.SerializeObject(viewModelMain);

            File.WriteAllText(filename, jsonViewModel);
        }

        static public List<List<CategoryViewModel>> Load(string fileName)
        {
            string readAllText = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<List<List<CategoryViewModel>>>(readAllText);
        }
    }
}
