using System.Runtime.Serialization;

namespace Mini_Project_due_21_07.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException(string message) : base(message)
        {

        }
    }
}