using Mini_Project_due_21_07.Exceptions;

namespace Mini_Project_due_21_07.Utilities
{
    public static class Validations
    {
        public static bool ValidClassName(this string className)
        {
            if (className.Length != 5)
            {
                return false;
            }
            if (char.IsUpper(className[0]) && char.IsUpper(className[1]) && char.IsDigit(className[2]) && char.IsDigit(className[3]) && char.IsDigit(className[4]))
            {
                return true;
            }
            return false;
        }
        public static bool ValidFirstName(this string firstName)
        {
            //if (string.IsNullOrEmpty(firstName) || 
            //{
                
            //}
            foreach (char c in firstName)
            {
                if (!char.IsLetter(c) || !char.IsUpper(firstName[0]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ValidLastName(this string lastName)
        {
            foreach (char c in lastName)
            {
                if (!char.IsLetter(c) || !char.IsUpper(lastName[0]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ValidEmail(this string email)
        {

            if (email.Contains('@') && email.Contains('.'))
            {
                return true;
            }
            return false;
        }
        public static bool IsEmailDuplicate(this string studentEmail, List<Student> students)
        {

            foreach (var student in students)
            {
                if (studentEmail == student.Email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
