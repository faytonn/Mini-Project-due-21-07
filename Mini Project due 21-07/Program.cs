using Mini_Project_due_21_07;
using Mini_Project_due_21_07.Enums;
using Mini_Project_due_21_07.Exceptions;
using Mini_Project_due_21_07.Models;
using Mini_Project_due_21_07.Utilities;
using System.Linq.Expressions;


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
    Console.WriteLine("[6] Remove a classroom");
    Console.WriteLine("[0] Exit");

    List<Student> students = new List<Student>();

    string command = Console.ReadLine();

    switch (command)
    {
        case "1":
            AddClassroom();
            goto restartMenu;
        case "2":
            AddStudent();
            goto restartMenu;
        case "3":
            ShowAllStudents();
            goto restartMenu;
        case "4":
            ShowAllStudentsInClassroom();
            goto restartMenu;
        case "5":
            RemoveStudent();
            goto restartMenu;
        case "6":
            RemoveClassroomById();
            goto restartMenu;
        case "0":
            systemProcess = false;
            break;
        default:
            Color.WriteLine("wrong command used", ConsoleColor.DarkRed);
            break;

    }


}



void AddClassroom()
{
    try
    {
    restartClassName:
        Console.Write("Classroom name: ");
        string className = Console.ReadLine().Trim();
        if (!Validations.ValidClassName(className))
        {
            Color.WriteLine("Classroom name must consist of 2 uppercase letters and 3 digits.", ConsoleColor.DarkRed);
            goto restartClassName;
        }

        foreach (var temporaryClassroom in Classroom.LoadClassrooms())
        {
            if (temporaryClassroom.Name == className)
            {
                Color.WriteLine("Classroom with the given name already exists.", ConsoleColor.Red);
                goto restartClassName;
            }
        }

    restartCourseName:
        Console.Write("Course Name(Frontend/Backend): ");
        string chosenCourseName = Console.ReadLine().ToLower().Trim();

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
            Color.WriteLine("Course not found, only Frontend and Backend available.", ConsoleColor.DarkRed);
            goto restartCourseName;
        }

        Classroom classroom = new Classroom(className, courseName);
        classrooms.Add(classroom);
        Classroom.SaveClassrooms(classrooms);
        Color.WriteLine("Classroom sucessfully added.", ConsoleColor.Green);
    }
    catch (Exception ex)
    {
        Color.WriteLine(ex.Message, ConsoleColor.DarkRed);
    }
}

void AddStudent()
{
    try
    {
        Classroom.LoadClassrooms();

        Console.Write("Course to enroll in (Frontend/Backend): ");
        string courseName = Console.ReadLine().ToLower().Trim();
        if (courseName != "backend" && courseName != "frontend")
        {
            Color.WriteLine("Course not found, only frontend and backend available.", ConsoleColor.DarkRed);
            return;
        }
        var classroom = classrooms.FirstOrDefault(x => x.CourseName.ToString().ToLower() == courseName);
        if (classroom == null)
        {
            Color.WriteLine("Classroom not found.", ConsoleColor.DarkRed);

        }



        Console.Write("Student name: ");
        string studentName = Console.ReadLine().Trim();
        if (!Validations.ValidFirstName(studentName))
        {
            Color.WriteLine("Name must start with a capital letter", ConsoleColor.DarkRed);
            return;
        }


        Console.Write("Student surname: ");
        string studentSurname = Console.ReadLine().Trim();
        if (!Validations.ValidLastName(studentSurname))
        {
            Color.WriteLine("Surn ame must start with a capital letter", ConsoleColor.DarkRed);
            return;
        }


        Console.Write("Student email: ");
        string studentEmail = Console.ReadLine();
        Validations.ValidEmail(studentEmail);
        Classroom.SaveClassrooms(classrooms);


        if (classroom != null)
        {
            if (Validations.IsEmailDuplicate(studentEmail, classroom.Students))
            {
                Color.WriteLine("This email already exists in the system.", ConsoleColor.DarkRed);
                return;
            }

            Student student = new Student(studentName, studentSurname, studentEmail);
            try
            {
                classroom.AddStudent(student, classrooms);
                Classroom.SaveClassrooms(classrooms);

            }
            catch (LimitExceededException ex)
            {
                Color.WriteLine(ex.Message, ConsoleColor.DarkRed);
            }
        }
        else
        {
            Color.WriteLine("Classroom not found.", ConsoleColor.DarkRed);
        }
    }
    catch (DuplicateEmailException ex)
    {
        Color.WriteLine(ex.Message, ConsoleColor.DarkRed);
    }
    catch (InvalidEmailException ex)
    {
        Color.WriteLine(ex.Message, ConsoleColor.DarkRed);
    }
    catch (Exception ex)
    {
        Color.WriteLine(ex.Message, ConsoleColor.DarkRed);
    }
}

void ShowAllStudents()
{
    try
    {
        foreach (var classroom in classrooms)
        {
            Color.WriteLine($"Classroom: {classroom.Name} ({classroom.CourseName})", ConsoleColor.DarkYellow);
            foreach (var student in classroom.Students)
            {
                Console.WriteLine($" => [{student.Id}] {student.Name} {student.Surname} \t{student.Email}");
            }
        }
    }
    catch (Exception ex)
    {
        Color.WriteLine(ex.Message, ConsoleColor.DarkRed);
    }
}

void ShowAllStudentsInClassroom()
{
    try
    {
        Color.WriteLine("Here are all of the existing classrooms:", ConsoleColor.DarkYellow);
        foreach (var clssrm in classrooms)
        {
            Color.WriteLine($"[{clssrm.Id}] {clssrm.Name} ({clssrm.CourseName})", ConsoleColor.DarkYellow);
        }

        Console.Write("Classroom name: ");
        string classroomName = Console.ReadLine().ToLower();

        var classroom = classrooms.FirstOrDefault(x => x.Name.ToLower() == classroomName);
        if (classroom != null)
        {
            if (classroom.Students.Count != 0)
            {
                Color.WriteLine($"Classroom: {classroom.Name} ({classroom.CourseName})", ConsoleColor.DarkYellow);
                foreach (var student in classroom.Students)
                {
                    Console.WriteLine($" => [{student.Id}] {student.Name} {student.Surname} \t{student.Email}");
                }
            }
            else
            {
                Color.WriteLine("No students in this class yet.", ConsoleColor.DarkYellow);
            }
        }
        else
        {
            Color.WriteLine("Classroom not found.", ConsoleColor.DarkRed);
        }
    }
    catch (Exception ex)
    {
        Color.WriteLine(ex.Message, ConsoleColor.DarkRed);
    }
}

void RemoveStudent()
{
restartRemoveStudent:
    try
    {
        Color.WriteLine("Here are all of the existing students:", ConsoleColor.DarkYellow);
        foreach (var classroom in classrooms)
        {
            Color.WriteLine($"Classroom: {classroom.Name} ({classroom.CourseName})", ConsoleColor.DarkYellow);
            foreach (var student in classroom.Students)
            {
                Console.WriteLine($" => [{student.Id}] {student.Name} {student.Surname} \t{student.Email}");
            }
        }
        Console.Write("Student ID: ");
        if (int.TryParse(Console.ReadLine(), out int studentId))
        {
            foreach (var classroom in classrooms)
            {
                var student = classroom.Students.FirstOrDefault(x => x.Id == studentId);
                if (student != null)
                {
                    Color.WriteLine($"Are you sure you want to remove {student.Name} {student.Surname} from {classroom.Name} ({student.CourseName})?", ConsoleColor.Red);
                    Color.WriteLine("yes/no", ConsoleColor.Red);
                    string yesNo = Console.ReadLine().ToLower();
                    if (yesNo == "yes")
                    {
                        classroom.Students.Remove(student);
                        Classroom.SaveClassrooms(classrooms);
                        Color.WriteLine("Student successfully removed.", ConsoleColor.Yellow);
                        return;
                    }
                    else if (yesNo == "no")
                    {
                        Color.WriteLine("Do you want to go back to the MENU or restart student removal?\n(menu / remove)", ConsoleColor.DarkYellow);
                        string menuOrRemove = Console.ReadLine().ToLower();
                        if (menuOrRemove == "menu")
                        {
                            //goto restartMenu;     //=> SORT THIS OUT
                        }
                        else if (menuOrRemove == "remove")
                        {
                            goto restartRemoveStudent;
                        }
                        else
                        {
                            Color.WriteLine("Wrong command used.", ConsoleColor.DarkRed);
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Color.WriteLine("Student not found.", ConsoleColor.DarkRed);
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Color.WriteLine("Student not found.", ConsoleColor.DarkRed);
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
        }
    }
    catch (Exception ex)
    {
        Color.WriteLine(ex.Message, ConsoleColor.DarkRed);
    }
}


void RemoveClassroomById()
{
restartRemoveClassroom:
    try
    {
        Classroom.LoadClassrooms();
        Color.WriteLine("Here are all of the existing classrooms:", ConsoleColor.DarkYellow);
        foreach (var classroom in classrooms)
        {
            Color.WriteLine($"[{classroom.Id}] {classroom.Name} ({classroom.CourseName})", ConsoleColor.DarkYellow);
        }
        Console.Write("Classroom ID:");
        if (int.TryParse(Console.ReadLine(), out int classroomId))
        {
            var classroomToRemove = classrooms.FirstOrDefault(x => x.Id == classroomId);
            if (classroomToRemove != null)
            {
                Color.WriteLine($"Are you sure you want to remove [{classroomToRemove.Id}] {classroomToRemove.Name} ({classroomToRemove.CourseName})?\nNOTE: STUDENTS IN THE REMOVED CLASSROOM WILL BE REASSIGNED TO A DIFFERENT CLASS OF {classroomToRemove.CourseName} COURSE BY DEFAULT.", ConsoleColor.Red);
                Color.WriteLine("yes/no", ConsoleColor.Red);
                string yesNo = Console.ReadLine().ToLower();
                if (yesNo == "yes")
                {
                    var otherClassroom = classrooms.FirstOrDefault(x => x.CourseName == classroomToRemove.CourseName && x.Id != classroomId);

                    if (otherClassroom != null)
                    {
                        foreach (var student in classroomToRemove.Students)
                        {
                            otherClassroom.AddStudent(student, classrooms);
                        }
                    }

                    classrooms.Remove(classroomToRemove);
                    Classroom.SaveClassrooms(classrooms);
                    Color.WriteLine("Classroom successfully removed.", ConsoleColor.Yellow);
                    return;
                }
                else if (yesNo == "no")
                {
                    Color.WriteLine("Do you want to go back to the MENU or restart classroom removal?\n(menu / remove)", ConsoleColor.DarkYellow);
                    string menuOrRemove = Console.ReadLine().ToLower();
                    if (menuOrRemove == "menu")
                    {
                        return;     //not sure about this, test it
                    }
                    else if (menuOrRemove == "remove")
                    {
                        goto restartRemoveClassroom;
                    }
                    else
                    {
                        Color.WriteLine("Wrong command used.", ConsoleColor.DarkRed);
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Color.WriteLine("Classroom not removed.", ConsoleColor.DarkRed);
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
            else
            {
                Color.WriteLine("Classroom not found.", ConsoleColor.DarkRed);
                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }
    catch (Exception ex)
    {
        Color.WriteLine(ex.Message, ConsoleColor.DarkRed);
    }




}


