using System.Runtime.Serialization;

namespace Mini_Project_due_21_07.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message)
        {
            
        }
    }
}