using System.Runtime.Serialization;

namespace Mini_Project_due_21_07.Exceptions
{
   public class InvalidNamesException : Exception
    {
        public InvalidNamesException(string message) : base(message)
        {

        }
    }
}