using Mini_Project_due_21_07.Enums;
using Mini_Project_due_21_07.Utilities;
using Mini_Project_due_21_07.Exceptions;


namespace Mini_Project_due_21_07.Models
{
    public class Classroom
    {
        private static int _id;
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public Course CourseName { get; set; }

        public Classroom(string name, Course courseName)
        {
            Id = ++_id;
            Students = new List<Student>();
            CourseName = courseName;
            Name = name;
        }

        public void AddStudent(Student student)
        {
            int limit=0;
            if (CourseName == Course.Backend)
            {
                limit = 15;
                Students.Add(student);
                Color.WriteLine("Student successfully added.", ConsoleColor.Green);
            }
            else if (CourseName == Course.Frontend)
            {
                limit = 20;
                Students.Add(student);
                Color.WriteLine("Student successfully added.", ConsoleColor.Green);
            }
            else if (Students.Count < limit)
            {
                throw new LimitExceededException("Limit was exceeded. Backend classes can have 15 students, Frontend classes can have 20 students maximum.");
            }
            else
            {
                throw new ClassroomNotFoundException("Classroom not found.");
            }
            Students.Add(student);

        }

        public Student FindStudentById(int id)
        {
            foreach (var student in Students)
            {
                if (student.Id == id)
                {
                    return student;
                }
            }
            throw new StudentNotFoundException("Student not found.");
        }

        public Student RemoveStudent(Student student, int studentId)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (student.Id == studentId)
                {
                    Student removedStudent = Students[i];

                    for (int j = i; j < Students.Count - 1; j++)
                    {
                        Students[j] = Students[j + 1];
                    }

                }
            }
            throw new StudentNotFoundException("Student not found.");
        }
    }
}
