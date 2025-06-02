namespace ConsoleApp1;

public class Response(bool success, string message)
{ 
    public bool Success { get; set; } = success;
    public string Message { get; set; } = message;

    public override string ToString() => Message;
}