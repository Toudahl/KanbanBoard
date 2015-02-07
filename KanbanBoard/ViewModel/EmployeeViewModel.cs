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
    class EmployeeViewModel
    {
        private List<EmployeeModel> _employees;
        private string _fileName;

        public EmployeeViewModel()
        {
            Employees = new List<EmployeeModel>();

            CreateEmployeeFolderIfNotExistAndSetPath();
            if (!File.Exists(_fileName))
            {
                Employees.Add(new EmployeeModel("Dummy", "Data", EnumEmployeeTitles.LeadDeveloper));
                PersistenceHandler.Save(Employees, _fileName);
            }
            else
            {
                
                Employees = (List<EmployeeModel>)PersistenceHandler.Load(_fileName);
            }
        }

        private void CreateEmployeeFolderIfNotExistAndSetPath()
        {
            string pathToExecutable = Assembly.GetExecutingAssembly().Location;
            pathToExecutable = pathToExecutable.Remove(pathToExecutable.Count() - 15);
            if (!Directory.Exists(pathToExecutable + "Employees"))
            {
                Directory.CreateDirectory(pathToExecutable + "Employees");
            }
            _fileName = pathToExecutable + @"Employees\employees.json";
        }

        public List<EmployeeModel> Employees
        {
            get { return _employees; }
            set { _employees = value; }
        }
    }
}
