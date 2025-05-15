using System.Configuration;

namespace ConsoleApp1
{
    public class ConstValue
    {
        public const int MaxLengthName = 100;
        public const int MaxLengthStudentCode = 10;
        public const double MaxHeight = 300.0;
        public const double MinHeight = 50.0;
        public const double MaxWeight = 1000.0;
        public const double MinWeight = 5.0;
        public const int StudentSchool = 200;
        public const int StartStudy = 1900;
        public const int LengthStartStudy = 4;
        public const int MaxLengthSchool = 200;
        public const int MaxLengthAddress = 300;
        public const double MaxGPA = 10.0;
        public const double MinGPA = 0.0;
        public const int MinYearBirth = 1900;
        public const string FormatBirth = "yyyy-MM-dd";
        public const int YearNow = 2025;
        //public const string DateRegexPattern = @"^\d{4}-\d{2}-\d{2}$";
        // public static string FilePath => ConfigurationManager.AppSettings["FilePath"];
    }
}