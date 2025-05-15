namespace ConsoleApp1;

internal class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Address { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }

    public Person(string _name, DateTime _birthDate, string _address, double _height, double _weight)
    {
        Name = _name;
        BirthDate = _birthDate;
        Address = _address;
        Height = _height;
        Weight = _weight;
    }

    public override string ToString()
    {
        return $@"
           Id: {Id},
           Name: {Name},
           BirthDate: {BirthDate},
           Address: {Address},
           Height: {Height},
           Weight: {Weight}
        }}";
    }
}