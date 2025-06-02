using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Globalization;




namespace ConsoleApp1;

internal class StudentManager
{
    private readonly string _filePath;
    private readonly List<Student> _students;

  
    public StudentManager()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string? filePathFromConfig = config["FilePath"];
        if (string.IsNullOrWhiteSpace(filePathFromConfig))
        {
            throw new InvalidOperationException("Missing or empty 'FilePath'");
        }

        _filePath = filePathFromConfig;
        _students = InitializeDefaultStudents();
    } 

  // public string FilePath => _filePath;

  private static List<Student> InitializeDefaultStudents()
  {
      return
      [
          new Student("Le The Nam", new DateTime(2004, 8, 6), "Thanh Hoa", 172, 58, "Fpt", 2022, 7.0),
          new Student("Le The Dat", new DateTime(2000, 12, 1), "Thanh Hoa", 172, 76, "PCA", 2019, 7.0)
      ];
  }
    
    /// The user for input and validates it using the provided validator.
    private string GetInput(string prompt, Func<string, Response> validator)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(input))
            {
                  Response response = validator(input);
                  if (response.Success) 
                      return input;
                  Console.WriteLine(response.Message);                             
            } 
        }
    }

    
    /// Creates a new student by collecting input and adds it to the collection.
    public void CreateStudent()
    {
        Student newStudent = InputStudent();
        _students.Add(newStudent);
        StoreDataInFile();
        PrintSuccessMessage("New student created successfully", newStudent.ToString());
    }

    /// Retrieves a student by their ID.
    private Student? GetStudentById(int id) => _students.FirstOrDefault(s => s.Id == id);

    
    /// Searches for a student by ID and displays their information.
    public void SearchStudentById()
    {
        int id = int.Parse(GetInput("Please enter ID: ", Validation.CheckGenerateId));
        Student? student = GetStudentById(id);

        if (student == null)
        {
            Console.WriteLine("Student not found or ID not valid.");
            return;
        }

        Console.WriteLine($"{student}\n");
    }

    
    /// Updates an existing student's information by ID.
    public void UpdateStudentById()
    {
        int id = int.Parse(GetInput("Please enter ID: ", Validation.CheckGenerateId));
        Student? student = GetStudentById(id);

        if (student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        Student updatedStudent = InputStudent();
        Student.StudentId--; 
        UpdateStudentFields(student, updatedStudent);
        StoreDataInFile();
        PrintSuccessMessage("Updated student successfully", student.ToString());
    }
    
    /// Deletes a student by ID.
    public void DeleteStudentById()
    {
        int id = int.Parse(GetInput("Please enter ID: ", Validation.CheckGenerateId));
        Student? student = GetStudentById(id);

        if (student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        Student.StudentId--;
        _students.Remove(student);
        StoreDataInFile();
        PrintSuccessMessage("Deleted student successfully");
        DisplayAllStudents();
    }
    
    /// Displays all students in the collection.
    public void DisplayAllStudents()
    {
        foreach (Student student in _students)
        {
            Console.WriteLine($"{student}\n");
        }
        Console.WriteLine($"List has: {_students.Count} students");
    }

    
    /// Displays the percentage of students by academic level.
    public void DisplayLevelByPercent()
    {
        var levelDistribution = _students
            .GroupBy(s => s.Level)
            .Select(g => new
            {
                Level = g.Key,
                Percentage = (double)g.Count() / _students.Count * 100
            });

        foreach (var item in levelDistribution)
        {
            Console.WriteLine($"Level {item.Level}: {item.Percentage:F2}%");
        }
    }

    
    /// Displays the percentage of students by GPA.
    public void DisplayGpaByPercent()
    {
        var gpaDistribution = _students
            .GroupBy(s => s.Gpa)
            .Select(g => new
            {
                Gpa = g.Key,
                Percentage = (double)g.Count() / _students.Count * 100
            })
            .OrderBy(g => g.Gpa);

        foreach (var item in gpaDistribution)
        {
            Console.WriteLine($"GPA {item.Gpa:F1}: {item.Percentage:F2}%");
        }
    }

    
    /// Displays students matching the specified academic level.
    
    public void DisplayLevelFromKeyboard()
    {
        try
        {
            string input = GetInput(
                "Please enter Level (Kem/Yeu/TrungBinh/Kha/Gioi/XuatSac): ",
                Validation.CheckLevelInput);

            Student.StudentLevel level = Enum.Parse<Student.StudentLevel>(input, true);
            var matchingStudents = _students.Where(s => s.Level == level).ToList();

            if (!matchingStudents.Any())
            {
                Console.WriteLine("No students found for the specified level.\n");
                return;
            }

            foreach (var student in matchingStudents)
            {
                Console.WriteLine($"Name: {student.Name} with GPA: {student.Gpa}\n");
            }
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Invalid level entered.\n");
        }
    }

    
    /// Saves the student list to a JSON file.
    public void StoreDataInFile()
    {
        try
        {
            string directory = Path.GetDirectoryName(_filePath)!;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_students, options);
            File.WriteAllText(_filePath, json);
            Console.WriteLine("Data saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }
    
    /// Loads the student list from a JSON file.
    public List<Student>? ReadJsonFile(string filepath)
    {
        try
        {
            if (!File.Exists(filepath))
            {
                Console.WriteLine("File not found.");
                return null;
            }

            string json = File.ReadAllText(filepath);
            _students.Clear();
            _students.AddRange(JsonSerializer.Deserialize<List<Student>>(json) ?? []);
            PrintSuccessMessage("Data loaded successfully");
            DisplayAllStudents();
            return _students;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
            return null;
        }
    }


    private Student InputStudent()
    {
        string name = GetInput("Please enter name: ", Validation.CheckNameInput);
        string birthDate = GetInput("Please enter birth date: ", Validation.CheckBirthDateInput);
        string address = GetInput("Please enter address: ", Validation.CheckAddressInput);
        string height = GetInput("Please enter height: ", Validation.CheckHeightInput);
        string weight = GetInput("Please enter weight: ", Validation.CheckWeightInput);
        string school = GetInput("Please enter school: ", Validation.CheckSchoolInput);
        string yearStart = GetInput("Please enter year start: ", Validation.CheckStartYearInput);
        string gpa = GetInput("Please enter GPA: ", Validation.CheckGpaInput);

        return new Student(
            name,
            DateTime.ParseExact(birthDate, ConstValue.FormatBirth, CultureInfo.InvariantCulture),
            address,
            double.Parse(height, CultureInfo.InvariantCulture),
            double.Parse(weight, CultureInfo.InvariantCulture),
            school,
            int.Parse(yearStart),
            double.Parse(gpa, CultureInfo.InvariantCulture));
    }

    private static void UpdateStudentFields(Student target, Student source)
    {
        target.Name = source.Name;
        target.BirthDate = source.BirthDate;
        target.Address = source.Address;
        target.Height = source.Height;
        target.Weight = source.Weight;
        target.StudentSchool = source.StudentSchool;
        target.YearStart = source.YearStart;
        target.Gpa = source.Gpa;
    }

    private static void PrintSuccessMessage(string message, string? additionalInfo = null)
    {
        Console.WriteLine("|--------------------------------|");
        Console.WriteLine($"|--{message}--|");
        Console.WriteLine("|--------------------------------|\n");
        if (!string.IsNullOrEmpty(additionalInfo))
        {
            Console.WriteLine($"{additionalInfo}\n");
        }
    }
}