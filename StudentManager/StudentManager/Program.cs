using System;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    internal static class Program
    {
        static void Main()
        {
            //string filename = "StudentList.txt";
            //string content =File.ReadAllText(filename);
            //Console.WriteLine(content);
            var studentManager = new StudentManager();
            var exist = false;
            //while (!exist)
           // {
                Menu();
                while (true)
                {
                    Console.WriteLine("Enter your choice:");
                    var input = Console.ReadLine();

                    if (!int.TryParse(input, out int choice))
                    {
                        Console.WriteLine("Invalid choice");
                        continue;
                    }
                    switch (choice)
                    {
                        case 1:
                            studentManager.CreateStudent();
                            break;
                        case 2:
                            studentManager.SearchStudentById();
                            break;
                        case 3:
                            studentManager.UpdateStudentById();
                            break;
                        case 4:
                            studentManager.DeleteStudentById();
                            break;
                        case 5:
                            studentManager.DisplayLevelbyPercent();
                            break;
                        case 6:
                            studentManager.DisplayGPAbyPercent();
                            break;
                        case 7:
                            studentManager.DisplayLevelFromKeyBoard();
                            break;
                        case 8:
                            studentManager.StoreDataInFile();
                            break;
                        case 9:
                            studentManager.DisplayAllStudents();
                            break;
                        case 10:
                            Console.Write("Enter the path to the JSON file: ");
                            string jsonPath = Console.ReadLine();
                            List<Student> studentsFromFile = studentManager.ReadJsonFile(jsonPath);
                            Console.WriteLine($"Loaded {studentsFromFile.Count} students from file.");
                            break;
                        case 0:
                            Console.WriteLine("Thoát chương trình...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;

                    }
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                }
            }
       // }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("|-----------------------Menu------------------------|");
            Console.WriteLine("|------------------Student Manager------------------|");
            Console.WriteLine("|---------------------------------------------------|");
            Console.WriteLine("|-- 1. Create a new student ------------------------|");
            Console.WriteLine("|-- 2. Search student by Id ------------------------|");
            Console.WriteLine("|-- 3. Update student by Id ------------------------|");
            Console.WriteLine("|-- 4. Delete student by Id ------------------------|");
            Console.WriteLine("|-- 5. Display student list by Level ---------------|");
            Console.WriteLine("|-- 6. Display student list by GPA -----------------|");
            Console.WriteLine("|-- 7. Display percent of all grade'student --------|");
            Console.WriteLine("|-- 8. Save list student ---------------------------|");
            Console.WriteLine("|-- 9. Display all students ------------------------|");
            Console.WriteLine("|-- 10. Read json file -----------------------------|");
            Console.WriteLine("|-- 0. Exit ----------------------------------------|");
            Console.WriteLine("|---------------------------------------------------|");

            
        }
    }
}