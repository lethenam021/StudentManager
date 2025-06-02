namespace ConsoleApp1;

internal class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Address { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }

    public Person(string name, DateTime birthDate, string address, double height, double weight)
    {
        Name = name;
        BirthDate = birthDate;
        Address = address;
        Height = height;
        Weight = weight;
    }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}," +
               $" {nameof(Name)}: {Name}, " +
               $"{nameof(BirthDate)}: {BirthDate}, " +
               $"{nameof(Address)}: {Address}," +
               $" {nameof(Height)}: {Height}, " +
               $"{nameof(Weight)}: {Weight}";
    }
}

