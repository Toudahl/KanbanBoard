using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using KanbanBoard.Model;
using KanbanBoard.Persistence;
using Newtonsoft.Json;

namespace KanbanBoard.ViewModel
{
    /// <summary>
    /// This class contains the list of the employees.
    /// </summary>
    class EmployeeViewModel
    {
        private List<EmployeeModel> _employees;
        private string _fileName;
        private int _selectedEmployee;

        public EmployeeViewModel()
        {
            Employees = new List<EmployeeModel>();

            CreateEmployeeFolderIfNotExistAndSetPath();
            
            LoadDataOrAddDummyData();
        }

        #region Methods
        /// <summary>
        /// Check if the employee file exist, if it doesnt. Add dummy data, and save it.
        /// If it does exist, it will load all the data.
        /// </summary>
        private void LoadDataOrAddDummyData()
        {
            if (!File.Exists(_fileName))
            {
                Employees.Add(new EmployeeModel("Dummy", "Data", EnumEmployeeTitles.LeadDeveloper));
                Employees.Add(new EmployeeModel("Morten", "Toudahl", EnumEmployeeTitles.LeadDeveloper));
                PersistenceHandler.Save(Employees, _fileName);
            }
            else
            {
                Employees = PersistenceHandler.Load<List<EmployeeModel>>(_fileName);
            }
        }

        /// <summary>
        /// Mainly used for the first run of the program. This will make sure that 
        /// the needed folder exist before trying to use it. And it will set the path to the list of employees
        /// </summary>
        private void CreateEmployeeFolderIfNotExistAndSetPath()
        {
            string pathToExecutable = Assembly.GetExecutingAssembly().Location;
            pathToExecutable = pathToExecutable.Remove(pathToExecutable.Count() - 15);
            if (!Directory.Exists(pathToExecutable + "Employees"))
            {
                Directory.CreateDirectory(pathToExecutable + "Employees");
            }
            _fileName = pathToExecutable + @"Employees\employees.kbe";
        }
        #endregion

        /// <summary>
        /// Provides access to the list of Employees
        /// </summary>
        public List<EmployeeModel> Employees
        {
            get { return _employees; }
            set { _employees = value; }
        }
    }
}
