namespace ConsoleApp1;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using System.Globalization;
 internal class Student : Person
 {
     public static int StudentID = 0;
     public string StudentCode { get; set; }
    public string StudentSchool { get; set; }
    public int YearStart { get; set; }
    public double GPA { get; set; }
    
    
    [JsonConstructor]
    public Student(string Name, DateTime BirthDate, string Address, double Height, double Weight, 
        string StudentSchool, int YearStart, double GPA)
        : base(Name, BirthDate, Address, Height, Weight)
    {
        Id = ++StudentID;
        this.StudentSchool = StudentSchool;
        this.YearStart = YearStart;
        this.GPA = GPA;
        StudentCode = GetStudentCode();
       // Level = GetLevel();
    }

    
    [JsonConverter(typeof(JsonStringEnumConverter))] 
    [JsonIgnore]
    public StudentLevel Level => GetLevel();


    public enum StudentLevel
    {
        Kem,
        Yeu,
        TrungBinh,
        Kha,
        Gioi,
        XuatSac
    }

    public StudentLevel GetLevel()
    {
        if (GPA < 3)
            return StudentLevel.Kem;
        else if (GPA < 5)
            return StudentLevel.Yeu;
        else if (GPA < 6.5)
            return StudentLevel.TrungBinh;
        else if (GPA < 7.5)
            return StudentLevel.Kha;
        else if (GPA < 9)
            return StudentLevel.Gioi;
        else
            return StudentLevel.XuatSac;
    }

    public string GetStudentCode()
    {
        string code = YearStart.ToString() + StudentID.ToString();
        if (code.Length > ConstValue.MaxLengthStudentCode)
            return code.Substring(code.Length - ConstValue.MaxLengthStudentCode,
                ConstValue.MaxLengthStudentCode);
        return code.PadLeft(10, '0');

    }



    public override string ToString()
    {
        return $@"Id: {Id}
Name: {Name}
BirthDate: {BirthDate}
Address: {Address}
Height: {Height}cm
Weight: {Weight}kg
StudentCode: {StudentCode}
StudentSchool: {StudentSchool}
YearStart: {YearStart}
GPA: {GPA}
Level: {Level}";
    }


 }
