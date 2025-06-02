using System.Text.RegularExpressions;
using System.Globalization;
namespace ConsoleApp1;

public static class Validation
{
    //checkGenerateId
    public static Response CheckGenerateId(string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(id.Trim()) || string.IsNullOrWhiteSpace(id))
        {
            return new Response(false,"Not null or Not Whitespace or Empty");
        }

        if (!int.TryParse(id,  out var convertId))
        {
            return new Response(false,"Not an integer");
        }

        if (convertId != int.Parse(id))
        {
            return new Response(false,"Not a valid id");
        }
        return convertId < 0  
            ? new Response(false,"Id must integer") 
            : new Response(true,"");
    }
    //checkName
    public static Response CheckNameInput(string name)
    {
        if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(name.Trim()) || string.IsNullOrWhiteSpace(name))
        {
            return new Response(false,"Not null or Not Whitespace or Empty");
        }

        if (!Regex.IsMatch(name, @"^[a-zA-Z\s\-_]+$"))
        {
            return new Response(false,"Invalid character in name");
        }
        return name.Length > ConstValue.MaxLengthName 
            ? new Response(false,$"Max length name: {ConstValue.MaxLengthName}") 
            : new Response(true,"");
    }
    
    //check birthdate
    public static Response CheckBirthDateInput(string birthDateString)
    {
        if (string.IsNullOrWhiteSpace(birthDateString))
        {
            return new Response(false, "Birth date must not be empty");
        }

        // Validate format first
        if (!Regex.IsMatch(birthDateString, @"^\d{4}-\d{2}-\d{2}$"))
        {
            return new Response(false, "Date must be in YYYY-MM-DD format");
        }

        if (!DateTime.TryParse(birthDateString, out DateTime birthDate))
        {
            return new Response(false, "Invalid date");
        }

        if (birthDate > DateTime.Now)
        {
            return new Response(false, "Birth date must not be in the future");
        }

        if (birthDate.Year < ConstValue.MinYearBirth)
        {
            return new Response(false, $"Person must be born after {ConstValue.MinYearBirth}");
        }

        return new Response(true, "");
    }
    //checkAddress
    public static Response CheckAddressInput(string address)
    {
        if (String.IsNullOrEmpty(address) || String.IsNullOrEmpty(address.Trim()) ||
            string.IsNullOrWhiteSpace(address.Trim()))
        {
            return new Response(false,"Not null or Whitespace or Empty");
        }
        return address.Length > ConstValue.MaxLengthAddress
            ? new Response(false,$"Max length address = {ConstValue.MaxLengthAddress}")
            : new Response(true,"");
    }
    //checkHeight
    public static Response CheckHeightInput(string heightString)
    {
        if (string.IsNullOrEmpty(heightString) || string.IsNullOrWhiteSpace(heightString))
        {
            return new Response(false,"Height must be a string");
        }

        if (!double.TryParse(heightString, out var heightConverted))
        {
            return new Response(false,"Height must be a number");
        }

        if (heightConverted < ConstValue.MinHeight || heightConverted > ConstValue.MaxHeight)
        {
            return new Response(false,"Height must be between " + ConstValue.MinHeight+ " and " + ConstValue.MaxHeight);
        }
        return new Response(true,"");
    }
    //checkWeightInput
    public static Response CheckWeightInput(string weightString)
    {
        if (string.IsNullOrEmpty(weightString) || string.IsNullOrWhiteSpace(weightString))
        {
            return new Response(false,"Not null or Whitespace or Empty");
        }

        if (!double.TryParse(weightString, out var weightConverted))
        {
            return new Response(false,"Weight must be a number");
        }

        if (weightConverted < ConstValue.MinWeight || weightConverted > ConstValue.MaxWeight)
        {
            return new Response(false,"Weight must be between " + ConstValue.MinWeight+ "and " + ConstValue.MaxWeight);
        }
        return new Response(true,"");
    }
   
    //checkSchool
    public static Response CheckSchoolInput(string school)
    {
        if (string.IsNullOrEmpty(school) || string.IsNullOrWhiteSpace(school))
        {
            return new Response(false,"Not null or Whitespace or Empty");
        }
        return school.Length>ConstValue.MaxLengthSchool
            ?new Response(false,"school max length <" + ConstValue.MaxLengthSchool)
            :new Response(true,school);
    }
    //checksStartYear
    public static Response CheckStartYearInput(string yearStart)
    {
        if (string.IsNullOrEmpty(yearStart) || string.IsNullOrWhiteSpace(yearStart))
        {
            return new Response(false,"Not null or Whitespace or Empty");
        }

        if (yearStart.Length != ConstValue.LengthStartStudy || !int.TryParse(yearStart, out var yearConverted)) 
        {
            return new Response(false,"Start year length= " + ConstValue.LengthStartStudy+ " and an integer " );
        }
      

       
        return yearConverted < ConstValue.StartStudy || yearConverted > ConstValue.YearNow
            ? new Response(false,"Year start must be from: "+ConstValue.StartStudy+" to "+ConstValue.YearNow)
            : new Response(true,"");
    }
    //checkGPA
    public static Response CheckGpaInput(string gpaString)
    {
        if (string.IsNullOrEmpty(gpaString) || string.IsNullOrWhiteSpace(gpaString))
        {
            return new Response(false, "GPA cannot be empty or whitespace");
        }

        // Try parsing with invariant culture to handle decimal points correctly
        if (!double.TryParse(
                gpaString.Trim(),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var gpaConverted))
        {
            return new Response(false, "GPA must be a number (use . as decimal separator)");
        }

        if (gpaConverted < ConstValue.MinGpa || gpaConverted > ConstValue.MaxGpa)
        {
            return new Response(false, $"GPA must be between {ConstValue.MinGpa} and {ConstValue.MaxGpa}");
        }
    
        return new Response(true, "");
    }
    //checkLevel
    public static Response CheckLevelInput(string level)
    {
        if (string.IsNullOrEmpty(level) || string.IsNullOrWhiteSpace(level))
        {
            return new Response(false,"Not null or Whitespace or Empty");
        }
        if (!Regex.IsMatch(level, @"^[a-zA-Z\s\-_]+$"))
        {
            return new Response(false,"Invalid character");
        }
        return new Response(true,"");
    }


}