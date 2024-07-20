using Mini_Project_due_21_07;
using Mini_Project_due_21_07.Enums;
using Mini_Project_due_21_07.Exceptions;
using Mini_Project_due_21_07.Models;
using Mini_Project_due_21_07.Utilities;
using Newtonsoft.Json;


//string classroomsPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Jsons", "classrooms.json");


//List<Classroom> classrooms = new List<Classroom>();
//Student student = new Student("fatima", "valiyeva", "fatima@gmail.com");


//string result;
//var json = JsonConvert.SerializeObject(classrooms);

//using (StreamReader streamreader = new StreamReader(classroomsPath))
//{
//    result = streamreader.ReadToEnd();
//}

//classrooms = JsonConvert.DeserializeObject<List<Classroom>>(result);
//if (classrooms is null)
//    classrooms = new();

//Student student1 = new("Gunel", "Qehremani", "gunel.com");
//Student student2 = new("Aysun", "Eminli", "aysun.com");

//Classroom classroom = new("PB303", Course.Backend);
//classroom.AddStudent(student1);
//classroom.AddStudent(student2);

//classrooms.Add(classroom);


//var newJson = JsonConvert.SerializeObject(classrooms);


//using (StreamWriter streamWriter = new StreamWriter(classroomsPath))
//{
//    streamWriter.WriteLine(newJson);
//    Color.WriteLine("User successfully added.", ConsoleColor.Green);
//}



bool systemProcess = true;

string classroomsPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Jsons", "classrooms.json");
List<Classroom> classrooms = Classroom.LoadClassrooms();

while (systemProcess)
{
    restartMenu:

    Console.WriteLine("> > > > MENU < < < <");
    Console.WriteLine("[1] Create a classroom");
    Console.WriteLine("[2] Add a student");
    Console.WriteLine("[3] Show all students");
    Console.WriteLine("[4] Show all students in the chosen classroom");
    Console.WriteLine("[5] Remove a student");
    Console.WriteLine("[0] Exit");

    List<Student> students = new List<Student>();

    string command = Console.ReadLine();

    switch (command)
    {
        case "1":
            AddClassroom();
            break;
        case "2":
            AddStudent();
            break;
        case "3":
            ShowAllStudents();
            break;
        case "4":
            ShowAllStudentsInClassroom();
            break;
        case "5":
            RemoveStudent();
            break;

    }


}



void AddClassroom()
{
    Console.Write("Classroom name: ");
    string className = Console.ReadLine().ToLower();

    Console.Write("Course Name(Frontend/Backend): ");
    string chosenCourseName = Console.ReadLine().ToLower();

    Course courseName;
    if (chosenCourseName == "backend")
    {
        courseName = Course.Backend;
    }
    else if (chosenCourseName == "frontend")
    {
        courseName = Course.Frontend;
    }
    else
    {
        Color.WriteLine("Course not found, only Frontend and Backend available.", ConsoleColor.White, ConsoleColor.DarkRed);
        return;
    }

    Classroom classroom = new Classroom(className, courseName);
    classrooms.Add(classroom);
    Classroom.SaveClassrooms(classrooms);
    Color.WriteLine("Classroom sucessfully added.", ConsoleColor.White, ConsoleColor.Green);
}

void AddStudent()
{
    Console.Write("Student name: ");
    string studentName = Console.ReadLine();

    Console.Write("Student surname: ");
    string studentSurname = Console.ReadLine();

    Console.Write("Student email: ");
    string studentEmail = Console.ReadLine();

    Console.Write("Course to enroll in (Frontend/Backend): ");
    string courseName = Console.ReadLine().ToLower();

    var classroom = classrooms.FirstOrDefault(x => x.CourseName.ToString().ToLower() == courseName);
    if (classroom != null)
    {
        Student student = new Student(studentName, studentSurname, studentEmail);
        classroom.AddStudent(student);
        Classroom.SaveClassrooms(classrooms);
    }
    else
    {
        Color.WriteLine("Classroom not found.", ConsoleColor.White, ConsoleColor.DarkRed);
    }
}

void ShowAllStudents()
{
    foreach (var classroom in classrooms)
    {
        Color.WriteLine($"Classroom: {classroom.Name} ({classroom.CourseName})", ConsoleColor.DarkYellow, ConsoleColor.Black);
        foreach (var student in classroom.Students)
        {
            Console.WriteLine($" => [{student.Id}] {student.Name} {student.Surname} \t{student.Email}");
        }
    }
}

void ShowAllStudentsInClassroom()
{
    Console.Write("Classroom name: ");
    string classroomName = Console.ReadLine().ToLower();

    var classroom = classrooms.FirstOrDefault(x => x.Name.ToLower() == classroomName);
    if (classroom != null)
    {
        if (classroom.Students.Count != 0)
        {
            Color.WriteLine($"Classroom: {classroom.Name} ({classroom.CourseName})", ConsoleColor.DarkYellow, ConsoleColor.Black);
            foreach (var student in classroom.Students)
            {
                Console.WriteLine($" => [{student.Id}] {student.Name} {student.Surname} \t{student.Email}");
            }
        }
        else
        {
            Color.WriteLine("No students in this class yet.", ConsoleColor.DarkYellow, ConsoleColor.Black);
        }
    }
    else
    {
        Color.WriteLine("Classroom not found.", ConsoleColor.White, ConsoleColor.DarkRed);
    }
}

void RemoveStudent()
{
    restartRemoveStudent:
    Console.Write("Student ID: ");
    if (int.TryParse(Console.ReadLine(), out int studentId))
    {
        foreach (var classroom in classrooms)
        {
            var student = classroom.Students.FirstOrDefault(x => x.Id == studentId);
            if (student != null)
            {
                Color.WriteLine($"Are you sure you want to remove {student.Name} {student.Surname} from {classroom.Name} ({student.CourseName})?", ConsoleColor.Red, ConsoleColor.Black);
                Color.WriteLine("yes/no", ConsoleColor.Red, ConsoleColor.Black);
                string yesNo = Console.ReadLine().ToLower();
                if (yesNo == "yes")
                {
                    classroom.Students.Remove(student);
                    Classroom.SaveClassrooms(classrooms);
                    Color.WriteLine("Student successfully removed.", ConsoleColor.Yellow, ConsoleColor.Black);
                    return;
                }
                else if (yesNo == "no")
                {
                    Color.WriteLine("Do you want to go back to the MENU or restart student removal?\n(menu / remove)", ConsoleColor.DarkYellow, ConsoleColor.Black);
                    string menuOrRemove = Console.ReadLine().ToLower();
                    if(menuOrRemove == "menu")
                    {
                        //goto restartMenu;     //=> SORT THIS OUT
                    }
                    else if(menuOrRemove == "remove")
                    {
                        goto restartRemoveStudent;
                    }
                    else
                    {
                        Color.WriteLine("Wrong command used.", ConsoleColor.White, ConsoleColor.DarkRed);
                    }
                }
            }
            else
            {
                Color.WriteLine("Student not found.", ConsoleColor.White, ConsoleColor.DarkRed);
            }
        }
    }
}