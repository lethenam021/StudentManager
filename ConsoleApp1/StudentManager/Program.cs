

namespace ConsoleApp1
{
    internal static class Program
    {
      static void Main()
{
    var studentManager = new StudentManager();
    bool exit = false;
    
    while (!exit)
    {
        Menu();
        
        Console.WriteLine("\nEnter your choice (0-10):");
        var input = Console.ReadLine();

        if (!int.TryParse(input, out int choice) || choice < 0 || choice > 10)
        {
            Console.WriteLine("Invalid choice! Please enter a number between 0 and 10.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
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
                studentManager.DisplayLevelByPercent();
                break;
            case 6:
                studentManager.DisplayGpaByPercent();
                break;
            case 7:
                studentManager.DisplayLevelFromKeyboard();
                break;
            case 8:
                studentManager.StoreDataInFile();
                break;
            case 9:
                studentManager.DisplayAllStudents();
                break;
            case 10:
                Console.Write("Enter the path to the JSON file (e.g., C:\\data\\students.json): ");
                string? inputPath= Console.ReadLine();
                if (inputPath == null)
                {
                    Console.WriteLine("Input cannot be null.");
                    return;
                }
                string jsonPath = inputPath;
                if (string.IsNullOrWhiteSpace(jsonPath))
                {
                    Console.WriteLine("File path cannot be empty!");
                }
                else
                {
                    try
                    { 
                        var result = studentManager.ReadJsonFile(jsonPath);
                        if (result == null)
                        {
                            Console.WriteLine("Failed to read students from file.");
                            return;
                        }
                        List<Student> studentsFromFile = result;
                        Console.WriteLine($"Successfully loaded {studentsFromFile.Count} students from file.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading file: {ex.Message}");
                    }
                }
                break;
            case 0:
                Console.WriteLine("Are you sure you want to exit? (Y/N)");
                var confirm = Console.ReadLine();
                if (confirm?.Trim().ToUpper() == "Y")
                {
                    exit = true;
                    Console.WriteLine("Exiting program...");
                }
                break;
          
        }

        if (!exit)
        {
            Console.WriteLine("\nOperation completed. Press any key to return to menu...");
            Console.ReadKey();
        }
    }
}

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
            Console.WriteLine("|-- 7. Display student with enter GPA from keyboard |");
            Console.WriteLine("|-- 8. Save list student ---------------------------|");
            Console.WriteLine("|-- 9. Display all students ------------------------|");
            Console.WriteLine("|-- 10. Read json file -----------------------------|");
            Console.WriteLine("|-- 0. Exit ----------------------------------------|");
            Console.WriteLine("|---------------------------------------------------|");

            
        }
    }
}