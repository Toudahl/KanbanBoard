using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using KanbanBoard.Model;
using KanbanBoard.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KanbanBoard.Persistence
{

    /// <summary>
    /// Handles the different types of persistence
    /// </summary>
    static class PersistenceHandler
    {
        private static IPersistence _persistence = new JsonPersistence();
        private static EnumPersistenceOptions _selectedPersistenceOptions = EnumPersistenceOptions.JsonPersistence;
        private static string _compatibleFiles = "KanBan Board file (*.kbb)|*.kbb";

        /// <summary>
        /// Uses the persistence type set by <see cref="SetPersistenceType()"/> to save the post its.
        /// Default is <see cref="JsonPersistence"/>
        /// </summary>
        /// <param name="informationToSave">The post its that will be saved</param>
        /// <param name="fileName">The path to the file that will be saved to</param>
        static public void Save(object informationToSave, string fileName)
        {
            _persistence.Save(informationToSave, fileName);
        }

        // TODO: Make a check on the file beeing loaded to detect what type of persistence was used, and automatically select the correct class to deserialize.

        /// <summary>
        /// Uses the persistence type set by <see cref="SetPersistenceType()"/> to load the post its.
        /// Default is <see cref="JsonPersistence"/>
        /// </summary>
        /// <param name="fileName">The path to the file that will be saved to</param>
        /// <returns>The entire board as a list of a list of CategoryViewModel</returns>
        static public object Load(string fileName)
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
                case EnumPersistenceOptions.XmlPersistence:
                    _selectedPersistenceOptions = EnumPersistenceOptions.XmlPersistence;
                    _persistence = new XmlPersistence();
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

        /// <summary>
        /// A string suitable for use in dialogs Filter property.
        /// </summary>
        public static string CompatibleFiles
        {
            get { return _compatibleFiles; }
            private set { _compatibleFiles = value; }
        }

        // TODO: Add other types of persistence.

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
            public void Save(object informationToSave, string fileName)
            {
                string jsonViewModel = JsonConvert.SerializeObject(informationToSave);

                File.WriteAllText(fileName, jsonViewModel);
            }

            /// <summary>
            /// Loads the post its from a file
            /// </summary>
            /// <param name="fileName">The file to load from</param>
            /// <returns>The post its that was loaded</returns>
            public object Load(string fileName)
            {
                // TODO: find out why casting only works when using build in DeserializeObject<>.
                string readAllText = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject(readAllText);
                //return JsonConvert.DeserializeObject<Dictionary<EnumCategories, CategoryViewModel>>(readAllText);
                //return JsonConvert.DeserializeObject<object>(readAllText);
            }
        }
        #endregion

        #region XmlPersistence - not implemented
        private class XmlPersistence : IPersistence
        {
            public void Save(object informationToSave, string fileName)
            {
                throw new System.NotImplementedException();
            }

            public object Load(string fileName)
            {
                throw new System.NotImplementedException();
            }
        }
        #endregion
    }

}
