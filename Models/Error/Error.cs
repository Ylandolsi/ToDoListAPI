namespace Models.Error;

public class Error
{
    public int ? Code { get; set; }
    public string ? Message { get; set; }

    public Error(int  code, string message)
    {
        Code = code;
        Message = message;
    }

    public static Error NotFound => new Error(404, "Not Found");
    public static Error BadRequest => new Error(400, "Bad Request");
    public static Error InternalServerError => new Error(500, "Internal Server Error");
    public static Error Unauthorized => new Error(401, "Unauthorized");
    public static Error None => new Error(0 , null);
}
