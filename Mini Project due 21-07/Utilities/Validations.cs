using Mini_Project_due_21_07.Exceptions;

namespace Mini_Project_due_21_07.Utilities
{
    public static class Validations
    {
        public static bool ValidFirstName(string firstName)
        {
            foreach (char c in firstName)
            {
                if (!char.IsLetter(c) && !char.IsUpper(firstName[0]))
                {
                    throw new InvalidNamesException("Name should consist of letters only (first letter uppercase included).");
                }
            }
            return true;
        }

        public static bool ValidLastName(string lastName)
        {
            foreach (char c in lastName)
            {
                if (!char.IsLetter(c) && !char.IsUpper(lastName[0]))
                {
                    throw new InvalidNamesException("Name should consist of letters only (first letter uppercase included).");
                }
            }
            return true;
        }

        public static bool ValidEmail(string email)
        {

            if (email.Contains('@') && email.Contains('.'))
            {
                return true;
            }
            throw new InvalidEmailException("Email should consist of '@' and '.'");
        }
        public static bool IsEmailDuplicate(string email)
        {
            List<Student> students = new List<Student>();
            foreach (var student in students)
            {
                if (email == student.Email)
                {
                    throw new DuplicateEmailException("This email already exists.");
                }
            }
            return false;
        }
    }
}
