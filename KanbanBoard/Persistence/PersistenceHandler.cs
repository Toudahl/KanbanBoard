using System.Collections.Generic;
using System.IO;
using KanbanBoard.ViewModel;
using Newtonsoft.Json;

namespace KanbanBoard.Persistence
{

    /// <summary>
    /// Handles the different types of persistence
    /// </summary>
    static class PersistenceHandler
    {
        static private IPersistence _persistence = new JsonPersistence();
        private static EnumPersistenceOptions _selectedPersistenceOptions = EnumPersistenceOptions.JsonPersistence;

        /// <summary>
        /// Uses the persistence type set by <see cref="SetPersistenceType()"/> to save the post its.
        /// Default is <see cref="JsonPersistence"/>
        /// </summary>
        /// <param name="informationToSave">The post its that will be saved</param>
        /// <param name="fileName">The path to the file that will be saved to</param>
        static public void Save(List<List<CategoryViewModel>> informationToSave, string fileName)
        {
            _persistence.Save(informationToSave, fileName);
        }

        //TODO: Make a check on the file beeing loaded to detect what type of persistence was used, and automatically select the correct class to deserialize.

        /// <summary>
        /// Uses the persistence type set by <see cref="SetPersistenceType()"/> to load the post its.
        /// Default is <see cref="JsonPersistence"/>
        /// </summary>
        /// <param name="fileName">The path to the file that will be saved to</param>
        /// <returns>The entire board as a list of a list of CategoryViewModel</returns>
        static public List<List<CategoryViewModel>> Load(string fileName)
        {
            return _persistence.Load(fileName);
        }

        /// <summary>
        /// Set the type of persistence used.
        /// </summary>
        /// <param name="persistenceType">Input the type of persistence you want to use.</param>
        public static void SetPersistenceType(EnumPersistenceOptions persistenceType)
        {
            switch (persistenceType)
            {
                case EnumPersistenceOptions.JsonPersistence:
                    _selectedPersistenceOptions = EnumPersistenceOptions.JsonPersistence;
                    _persistence = new JsonPersistence();
                    break;
            }
        }

        /// <summary>
        /// Returns currently selected persistence type
        /// </summary>
        /// <returns><see cref="EnumPersistenceOptions"/></returns>
        public static EnumPersistenceOptions GetPersistenceOptions()
        {
            return _selectedPersistenceOptions;
        }

        //TODO: Add other types of persistence.

        #region JsonPersistence
        /// <summary>
        /// This class will use the newtonsoft json package to serialize and deserialize
        /// </summary>
        private class JsonPersistence : IPersistence
        {
            /// <summary>
            /// Saves the post its in the categories.
            /// </summary>
            /// <param name="informationToSave">The post its that will be saved</param>
            /// <param name="fileName">The path to the file that will be saved to</param>
            public void Save(List<List<CategoryViewModel>> informationToSave, string fileName)
            {
                string jsonViewModel = JsonConvert.SerializeObject(informationToSave);

                File.WriteAllText(fileName, jsonViewModel);
            }

            /// <summary>
            /// Loads the post its from a file
            /// </summary>
            /// <param name="fileName">The file to load from</param>
            /// <returns>The post its that was loaded</returns>

            public List<List<CategoryViewModel>> Load(string fileName)
            {
                string readAllText = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<List<List<CategoryViewModel>>>(readAllText);
            }
        }
        #endregion

    }
}
