using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoard.Model
{
    public class EmployeeModel : PersonModel
    {
        private EnumEmployeeTitles _title;

        public EmployeeModel(string firstName, string lastName, EnumEmployeeTitles title) : base(firstName, lastName)
        {
            _title = title;
        }

        public EnumEmployeeTitles Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public override string ToString()
        {
            return base.ToString() + "," + Title;
        }
    }
}
