using Mini_Project_due_21_07;
using Mini_Project_due_21_07.Enums;
using Mini_Project_due_21_07.Models;
using Mini_Project_due_21_07.Utilities;
using Newtonsoft.Json;


string path = @"C:\Users\Fatima\source\repos\Mini Project due 21-07\Mini Project due 21-07.sln.json";

string environment = Environment.GetEnvironmentVariable(path);


Console.WriteLine(environment);

string json = JsonConvert.SerializeObject

using (StreamWriter sw = new StreamWriter(path))
{
    sw.WriteLine(json);
}



bool systemProcess = true;
while (systemProcess)
{

    Console.WriteLine("> > > > MENU < < < <");
    Console.WriteLine("[1] Create a classroom");
    Console.WriteLine("[2] Add a student");
    Console.WriteLine("[3] Show all students");
    Console.WriteLine("[4] Show all students in the chosen classroom");
    Console.WriteLine("[5] Remove a student");
    Console.WriteLine("[0] Exit");

    string command = Console.ReadLine();

    switch (command)
    {
        case "1":

    }


}

void AddClassroom()
{
    Console.Write("Classroom name: ");
    string className = Console.ReadLine();

    Console.Write("Course Name(Frontend/Backend): ");
    string chosenCourseName = Console.ReadLine();

    Course courseName;
    if (chosenCourseName == "Backend")
    {
        courseName = Course.Backend;
        Classroom classroom = new(className, courseName);


    }
    else if (chosenCourseName == "Frontend")
    {
        courseName = Course.Frontend;
        Classroom classroom = new Classroom(className, courseName);

    }
    else
    {
        Color.WriteLine("Course not found, only Frontend and Backend available.", ConsoleColor.Red);
    }


}
void AddStudent()
{
    Console.Write("Student name: ");
    string studentName = Console.ReadLine();

    Console.Write("Student surname: ");
    string studentSurname = Console.ReadLine();

    Console.Write("Course to enroll in: ");
    string courseName = Console.ReadLine();

    Student student = new Student(studentName, studentSurname, courseName);
}
