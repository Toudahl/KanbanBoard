using System.Collections.Generic;
using System.IO;
using KanbanBoard.ViewModel;
using Newtonsoft.Json;

namespace KanbanBoard.Persistence
{
    /// <summary>
    /// This class will use the newtonsoft json package to serialize and deserialize
    /// </summary>
    static class JsonPersistence
    {
        /// <summary>
        /// Saves the post its in the categories.
        /// </summary>
        /// <param name="informationToSave">The post its that will be saved</param>
        /// <param name="fileName">The path to the file that will be saved to</param>
        static public void Save(List<List<CategoryViewModel>> informationToSave, string fileName)
        {
            string jsonViewModel = JsonConvert.SerializeObject(informationToSave);

            File.WriteAllText(fileName, jsonViewModel);
        }

        /// <summary>
        /// Loads the post its from a file
        /// </summary>
        /// <param name="fileName">The file to load from</param>
        /// <returns>The post its that was loaded</returns>

        static public List<List<CategoryViewModel>> Load(string fileName)
        {
            string readAllText = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<List<List<CategoryViewModel>>>(readAllText);
        }
    }
}
