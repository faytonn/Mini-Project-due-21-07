using Mini_Project_due_21_07.Enums;
using Mini_Project_due_21_07.Utilities;
using Mini_Project_due_21_07.Exceptions;
using Newtonsoft.Json;


namespace Mini_Project_due_21_07.Models
{
    public class Classroom
    {
        private static int _id;
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public List<Classroom> Classrooms { get; set; }
        public Course CourseName { get; set; }

        public Classroom(string name, Course courseName)
        {
            Id = ++_id;
            Students = new List<Student>();
            CourseName = courseName;
            Name = name;
        }



        #region Json part
        private static string classroomsPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Jsons", "classrooms.json");
        public static List<Classroom> LoadClassrooms()
        {
            if (File.Exists(classroomsPath))
            {
                using (StreamReader streamReader = new StreamReader(classroomsPath))
                {
                    string result = streamReader.ReadToEnd();
                    List<Classroom> loadedClassrooms = JsonConvert.DeserializeObject<List<Classroom>>(result);
                    if (loadedClassrooms != null)
                    {
                        return new List<Classroom>();
                    }
                    else
                    {
                        return loadedClassrooms;
                    }
                }
            }
            else
            {
                return new List<Classroom>();
            }
        }

        public static void SaveClassrooms(List<Classroom> classrooms)
        {
            string newJson = JsonConvert.SerializeObject(classrooms);
            using (StreamWriter streamWriter = new StreamWriter(classroomsPath))
            {
                streamWriter.WriteLine(newJson);
            }
        }
        #endregion



        public void AddStudent(Student student)
        {
            int limit = 0;
            if (CourseName == Course.Backend)
            {
                limit = 15;
                Students.Add(student);
                Color.WriteLine("Student successfully added.", ConsoleColor.Green, ConsoleColor.Black);
            }
            else if (CourseName == Course.Frontend)
            {
                limit = 20;
                Students.Add(student);
                Color.WriteLine("Student successfully added.", ConsoleColor.Green, ConsoleColor.Black);
            }
            else if (Students.Count < limit)
            {
                throw new LimitExceededException("Limit was exceeded. Backend classes can have 15 students, Frontend classes can have 20 students maximum.");
            }
            else
            {
                throw new ClassroomNotFoundException("Classroom not found.");
            }


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

        public  Student RemoveStudent(Student student, int studentId)
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

        public override string ToString()
        {
            return $"Id:{Id} \tName: {Name} \tCourse: {CourseName}";
        }
    }
}
