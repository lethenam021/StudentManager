namespace ConsoleApp1;

public class Response
{ 
    public bool Success { get; set; }
    public string Message { get; set; }

    public Response(bool _success, string _message)
    {
        Success = _success;
        Message = _message;
    }
    public override string ToString() => Message;
}