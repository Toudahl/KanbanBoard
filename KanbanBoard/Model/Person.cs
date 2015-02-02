namespace KanbanBoard.Model
{
    public class PersonModel
    {
        private string _firstName;
        private string _lastName;

        public PersonModel(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}