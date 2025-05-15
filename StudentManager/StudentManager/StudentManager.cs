using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Text.Json.Serialization;

namespace ConsoleApp1;

internal class StudentManager
{
    private readonly string filePath;

    private List<Student> students = new List<Student>()
    {
        new Student("Le The Nam", new DateTime(2004, 8, 6), "Thanh Hoa", 172, 58, "Fpt", 2022, 7.0),
        new Student("Le The Dat", new DateTime(2000, 12, 1), "Thanh Hoa", 172, 76, "PCCC", 2019, 7.0)
    };

    public StudentManager()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        filePath = config["FilePath"] ?? throw new ArgumentNullException("FilePath not found in appsettings.json");
    }
    public string FilePath => filePath;

    Student InputStudent()
    {
        string name = getInput("Please enter name:", Validation.CheckNameInput);
        string birthdate = getInput("Please enter birth date:", Validation.CheckBirthDateInput);
        string address = getInput("Please enter address:", Validation.CheckAddressInput);
        string height = getInput("Please enter height:", Validation.CheckHeightInput);
        string weight = getInput("Please enter weight:", Validation.CheckWeightInput);
        string schoolStudent = getInput("Please enter school:", Validation.CheckSchoolInput);
        string yearStart = getInput("Please enter yearStart:", Validation.CheckStartYearInput);
        string gpa = getInput("Please enter GPA:", Validation.CheckGPAInput);

        return new Student(name, // Nếu ConstValue.MinYearBirth thực sự là định dạng ngày tháng (ví dụ: "yyyy")
            DateTime.ParseExact(birthdate, ConstValue.FormatBirth.ToString(), CultureInfo.InvariantCulture),
            address, double.Parse(height), double.Parse(weight), schoolStudent,
            int.Parse(yearStart), double.Parse(gpa));

    }

//addNewStudent
    public void CreateStudent()
    {
        Student newStudent = InputStudent();
        Console.WriteLine("|------------------------------------|");
        Console.WriteLine("|--New student created successfully--|");
        Console.WriteLine("|------------------------------------|");
        Console.WriteLine(newStudent.ToString() + "\n");
        students.Add(newStudent);
        StoreDataInFile();
    }

//getStudentById
    public Student getStudentById(int id)
    {
        return students.FirstOrDefault(x => x.Id == id);
    }

//searchStudentId
    public void SearchStudentById()
    {
        int idExist = int.Parse(getInput("Please enter ID:", Validation.CheckGenerateId));
        Student studentFound = getStudentById(idExist);
        if (studentFound == null)
        {
            Console.WriteLine("Student not found");
        }

        Console.WriteLine(studentFound.ToString() + "\n");

    }

//updateStudent
    public void UpdateStudentById()
    {
        int idExist = int.Parse(getInput("Please enter ID:", Validation.CheckGenerateId));
        Student studentUpdated = getStudentById(idExist);
        if (studentUpdated == null)
        {
            Console.WriteLine("Student not found");
        }

        Student updateStudent = InputStudent();
        Student.StudentID--;
        studentUpdated.Name = updateStudent.Name;
        studentUpdated.BirthDate = updateStudent.BirthDate;
        studentUpdated.Address = updateStudent.Address;
        studentUpdated.Height = updateStudent.Height;
        studentUpdated.Weight = updateStudent.Weight;
        studentUpdated.StudentCode = updateStudent.StudentCode;
        studentUpdated.GPA = updateStudent.GPA;
        studentUpdated.YearStart = updateStudent.YearStart;
        studentUpdated.StudentSchool = updateStudent.StudentSchool;
        Console.WriteLine("|--------------------------------|");
        Console.WriteLine("|--Updated student successfully--|");
        Console.WriteLine("|--------------------------------|" + "\n");
        Console.WriteLine("\n" + studentUpdated.ToString() + "\n");
        StoreDataInFile();
    }

//deleteStudent
    public void DeleteStudentById()
    {
        while (true)
        {
            int idExist = int.Parse(getInput("Please enter ID:", Validation.CheckGenerateId));
            Student studentDeleted = getStudentById(idExist);
            if (studentDeleted == null)
            {
                Console.WriteLine("Student not found");
                return;
            }

            Student.StudentID--;
            students.Remove(studentDeleted);
            Console.WriteLine("|---------------------------------|");
            Console.WriteLine("|--Deleted student successfully--|");
            Console.WriteLine("|---------------------------------|");
            Console.WriteLine("\n--Student remaining--\n");
            DisplayAllStudents();
            StoreDataInFile();
            return;
        }
    }

//DisplayAllStudent
    public void DisplayAllStudents()
    {
        foreach (Student student in students)
        {
            Console.WriteLine(student.ToString() + "\n");
        }
    }

//DisplayLevelbyLevel
    public void DisplayLevelbyPercent()
    {
        var studentLevel = students.GroupBy(x => x.Level)
            .Select(gr => new
                {
                    level = gr.Key,
                    percent = (double)gr.Count() / students.Count() * 100
                }
            );
        foreach (var item in studentLevel)
        {
            Console.WriteLine($"Level {item.level}: {item.percent}%");
        }
    }

//DisplayGPAbyPercent
    public void DisplayGPAbyPercent()
    {
        SortedDictionary<double, double> scorePercent = new SortedDictionary<double, double>();
        foreach (var item in students)
        {
            if (scorePercent.ContainsKey(item.GPA))
            {
                scorePercent[item.GPA]++;
            }
            else
            {
                scorePercent.Add(item.GPA, 1);
            }
        }

        foreach (var item in scorePercent)
        {
            Console.WriteLine($"GPA: {item.Key}: {item.Value / students.Count * 100}%");
        }
    }

//displayLevelFromkeyboard
    public void DisplayLevelFromKeyBoard()
    {
        while (true)
        {
            try
            {
                string input = getInput("Please enter Level (Kem/Yeu/TrungBinh/Kha/Gioi/XuatSac):",
                    Validation.CheckLevelInput);

                // Parse the enum with explicit type specification
                Student.StudentLevel studyLevelInput =
                    (Student.StudentLevel)Enum.Parse(typeof(Student.StudentLevel), input, true);
                var matchingStudents = students.Where(x => x.Level == studyLevelInput);
                if (!matchingStudents.Any())
                {
                    Console.WriteLine("Student not found.\n");
                }
                else
                {
                    foreach (var item in matchingStudents)
                    {
                        Console.WriteLine($"Name: {item.Name} with GPA: {item.GPA}\n");
                    }
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid input: Please enter a valid input.\n");
            }
        }
    }

//storeData    

    public void StoreDataInFile()
    {
        try
        {
            string directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string json = JsonSerializer.Serialize(students, new JsonSerializerOptions
            {
                WriteIndented = true // để format đẹp
            });

            File.WriteAllText(FilePath, json);

            Console.WriteLine("Data saved successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: Cannot save file - {e.Message}");
        }
    }

    

    public List<Student> ReadJsonFile(string path)
    {
        try
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                students = JsonSerializer.Deserialize<List<Student>>(json);
                 Console.WriteLine("|-----------------------------|");
                 Console.WriteLine("|--Data loaded successfully.--|");
                 Console.WriteLine("|-----------------------------|");
                 foreach (Student student in students)
                 {
                     Console.WriteLine(student.ToString() + "\n");
                 } 
                
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: Cannot load file - {e.Message}");
        }
        return  students;
    }

public string getInput(string promt, Func<string, Response> validator)
    {
        string input=null;
        while (true)
        {
            Console.Write(promt);
            input = Console.ReadLine()?.Trim();
            Response resultresponse = validator(input);
            if(resultresponse.Success)
                return input;
                Console.WriteLine(resultresponse.ToString());
            
        }
    }
}