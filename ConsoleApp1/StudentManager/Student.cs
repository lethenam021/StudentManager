using System.Text.Json.Serialization;
   namespace ConsoleApp1
{
 

    internal class Student : Person
    {
        public static int StudentId ;
        public string StudentCode { get; set; }
        public string StudentSchool { get; set; }
        public int YearStart { get; set; }
        public double Gpa { get; set; }

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

        [JsonConstructor]
        public Student(string name, DateTime birthDate, string address, double height, double weight,
            string studentSchool, int yearStart, double gpa)
            : base(name, birthDate, address, height, weight)
        {
            Id = ++StudentId;
            StudentSchool = studentSchool;
            YearStart = yearStart;
            Gpa = gpa;
            StudentCode = GetStudentCode();
        }

        private StudentLevel GetLevel()
        {
            if (Gpa < 3)
                return StudentLevel.Kem;
            if (Gpa < 5)
                return StudentLevel.Yeu;
            if (Gpa < 6.5)
                return StudentLevel.TrungBinh;
            if (Gpa < 7.5)
                return StudentLevel.Kha;
            if (Gpa < 9)
                return StudentLevel.Gioi;
            
            return StudentLevel.XuatSac;
        }

        private string GetStudentCode()
        {
            string code = YearStart.ToString() + Id.ToString();
            
            if (code.Length > ConstValue.MaxLengthStudentCode)
                return code.Substring(code.Length - ConstValue.MaxLengthStudentCode);
            
            return code.PadLeft(ConstValue.MaxLengthStudentCode, '0');
        }

        public override string ToString()
        {
            return $"{base.ToString()}, " +
                   $"{nameof(StudentCode)}: {StudentCode}, " +
                   $"{nameof(StudentSchool)}: {StudentSchool}, " +
                   $"{nameof(YearStart)}: {YearStart}, " +
                   $"{nameof(Gpa)}: {Gpa}, " +
                   $"{nameof(Level)}: {Level}";
        }
    }
}