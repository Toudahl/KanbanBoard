using System.Windows.Media;

namespace KanbanBoard.Model
{
    public class PostItModel
    {
        private string _deadline;
        private EmployeeModel _responsiblePerson;
        private SolidColorBrush _solidColorBrush;
        private string _taskName;
        private string _taskDetails;
        private readonly string DateTimeFormat = "HH:mm - dd MMMM, yyyy";

        public PostItModel(string taskName, string taskDetails, string deadline, EmployeeModel responsiblePersonModel, SolidColorBrush solidColorBrush)
        {
            _responsiblePerson = responsiblePersonModel;
            _taskName = taskName;
            _taskDetails = taskDetails;
            _deadline = deadline;
            _solidColorBrush = solidColorBrush;
        }

        public string TaskName
        {
            get { return _taskName; }
            set { _taskName = value; }
        }

        public string TaskDetails
        {
            get { return _taskDetails; }
            set { _taskDetails = value; }
        }

        public string Deadline
        {
            get { return _deadline; }
            set { _deadline = value; }
        }

        public EmployeeModel ResponsiblePerson
        {
            get { return _responsiblePerson; }
            set { _responsiblePerson = value; }
        }

        public SolidColorBrush SolidColorBrush
        {
            get { return _solidColorBrush; }
            set { _solidColorBrush = value; }
        }
    }
}
