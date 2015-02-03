using System.Collections.Generic;
using KanbanBoard.ViewModel;

namespace KanbanBoard.Persistence
{
    /// <summary>
    /// Handles the different types of persistence
    /// </summary>
    static class PersistenceHandler
    {
        /// <summary>
        /// Uses the JsonPersistence class to save the post its.
        /// </summary>
        /// <param name="informationToSave">The post its that will be saved</param>
        /// <param name="fileName">The path to the file that will be saved to</param>
        static public void Save(List<List<CategoryViewModel>> informationToSave, string fileName)
        {
            JsonPersistence.Save(informationToSave, fileName);
        }

        /// <summary>
        /// Uses the JsonPersistence class to save the post its.
        /// </summary>
        /// <param name="informationToSave">The post its that will be saved</param>
        /// <param name="fileName">The path to the file that will be saved to</param>
        /// <param name="persistenceOptions">Specify what type of persistence that will be used.</param>
        static public void Save(List<List<CategoryViewModel>> informationToSave, string fileName, EnumPersistenceOptions persistenceOptions)
        {
            if (persistenceOptions == EnumPersistenceOptions.JSON)
            {
                JsonPersistence.Save(informationToSave, fileName);
            }
        }

        /// <summary>
        /// Uses the JsonPersistence class to load the post its.
        /// </summary>
        /// <param name="fileName">The path to the file that will be saved to</param>
        /// <returns></returns>
        static public List<List<CategoryViewModel>> Load(string fileName)
        {
            return JsonPersistence.Load(fileName);
        }

        /// <summary>
        /// Uses the JsonPersistence class to load the post its.
        /// </summary>
        /// <param name="fileName">The path to the file that will be saved to</param>
        /// <param name="persistenceOptions">Specify what type of persistence that will be used.</param>
        /// <returns></returns>
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
