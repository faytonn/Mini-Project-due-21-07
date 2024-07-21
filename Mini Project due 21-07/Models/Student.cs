using Mini_Project_due_21_07.Enums;

namespace Mini_Project_due_21_07
{
    public class Student
    {
        private static int _id;
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Course CourseName { get; set; }
        public int ClassroomId { get; set; }

        public Student(string name, string surname, string email)
        {
            Id = ++_id;
            Name = name;
            Surname = surname;
            Email = email;
        }
        public Student()
        {
            
        }
    }
}
